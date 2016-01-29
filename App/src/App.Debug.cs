using ScintillaNET;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace App
{
    partial class App
    {
        #region Debug Image
        private void comboImg_SelectedIndexChanged(object sender, EventArgs e)
            => pictureImg_Click(sender, e);

        private void numImgLayer_ValueChanged(object sender, EventArgs e)
        {
            if (comboImg.SelectedItem == null || !(comboImg.SelectedItem is GLImage))
                return;

            // get selected image
            var img = (GLImage)comboImg.SelectedItem;
            numImgLayer.Maximum = Math.Max(Math.Max(img.length, img.depth) - 1, 0);

            // read image data from GPU
            glControl.MakeCurrent();
            var bmp = img.Read((int)numImgLayer.Value, 0);
            bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            pictureImg.Image = bmp;
        }

        private void pictureImg_Click(object sender, EventArgs e)
            => numImgLayer_ValueChanged(sender, e);
        #endregion

        #region Debug Buffer
        private void comboBuf_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBuf.SelectedItem == null || !(comboBuf.SelectedItem is GLBuffer))
                return;

            // gather needed info
            var buf = (GLBuffer)comboBuf.SelectedItem;
            var type = (string)comboBufType.SelectedItem;
            var dim = (int)numBufDim.Value;

            // read data from GPU
            glControl.MakeCurrent();
            var data = buf.Read();

            // convert data to specified type
            Type colType;
            Array da = data.To(type, out colType);

            // CREATE TABLE
            var dt = new DataTable(buf.name);
            // create columns
            for (int i = 0; i < dim; i++)
                dt.Columns.Add(i.ToString(), colType);
            // create rows
            for (int i = 0; i < da.Length;)
            {
                var row = dt.NewRow();
                for (int c = 0; c < dim && i < da.Length; c++)
                    row.SetField(c, da.GetValue(i++));
                dt.Rows.Add(row);
            }

            // update GUI
            var ds = new DataSet(buf.name);
            ds.Tables.Add(dt);
            tableBuf.DataSource = ds;
            tableBuf.DataMember = buf.name;
        }

        private void comboBufType_SelectedIndexChanged(object sender, EventArgs e)
            => comboBuf_SelectedIndexChanged(sender, e);

        private void numBufDim_ValueChanged(object sender, EventArgs e)
            => comboBuf_SelectedIndexChanged(sender, null);
        #endregion

        #region Debug Properties
        private void comboProp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboProp.SelectedItem == null || !(comboProp.SelectedItem is GLInstance))
                return;

            // gather needed info
            propertyGrid.SelectedObject = ((GLInstance)comboProp.SelectedItem).instance;
        }

        private void propertyGrid_Click(object sender, EventArgs e)
            => propertyGrid.SelectedObject = propertyGrid.SelectedObject;

        private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
            => DebugRender();
        #endregion

        #region Debug Shader
        private void editor_UpdateUI(object sender, UpdateUIEventArgs e)
        {
            // get class references
            var editor = (CodeEditor)sender;

            // handle selection changed event, but only
            // update debug information for the compiled editor
            if (e.Change == UpdateChange.Selection && compiledEditor == editor)
                UpdateDebugListView(editor);
        }

        private void editor_MouseMove(object sender, MouseEventArgs e)
        {
            // get class references
            var editor = (CodeEditor)sender;

            // only update debug information for the compiled editor
            if (compiledEditor != editor)
                return;

            // convert cursor position to text position
            int pos = editor.CharPositionFromPoint(e.X, e.Y);

            // get debug variable information from position
            var dbgVar = GLDebugger.GetDebugVariableFromPosition(editor, pos);
            // no debug variable found
            if (dbgVar.IsDefault())
            {
                editor.CallTipCancel();
                return;
            }

            // get debug variable value
            var dbgVal = GLDebugger.GetDebugVariableValue(dbgVar.ID, glControl.Frame - 1);
            editor.CallTipShow(pos, dbgVal == null
                ? "No debug information."
                : GLDebugger.DebugVariableToString(dbgVal));
        }

        private void output_DoubleClick(object sender, EventArgs e)
        {
            // if no item is selected and no code compiled, return
            var view = (DataGridView)sender;
            if (view.SelectedRows.Count == 0 || compiledEditor == null)
                return;

            // get line from selected item
            int line;
            var text = view.SelectedRows[0].Cells[1].Value as string;
            if (!int.TryParse(text, NumberStyles.Integer, culture, out line))
                return;

            // scroll to line
            line = Math.Max(1, line - compiledEditor.LinesOnScreen / 2);
            compiledEditor.LineScroll(line - compiledEditor.FirstVisibleLine - 1, 0);
        }
        
        private void UpdateDebugListView(CodeEditor editor)
        {
            // RESET DEBUG LIST VIEW
            debugListView.Clear();
            debugListView.View = View.Details;
            debugListView.FullRowSelect = true;
            debugListView.Columns.Add("X", 80);
            debugListView.Columns.Add("Y", 80);
            debugListView.Columns.Add("Z", 80);
            debugListView.Columns.Add("W", 80);

            // if the code has been edited no debug information can
            // be shown, because debug variables might have been
            // added or removed, which leads to invalid debug output
            if (((TabPage)editor.Parent).Text.EndsWith("*"))
                return;

            // get debug variables of the line where the caret is placed
            var dbgLine = editor.LineFromPosition(editor.CurrentPosition);
            var dbgVars = GLDebugger.GetDebugVariablesFromLine(editor, dbgLine).Select(x => x);
            dbgVars.Select(Var => GLDebugger.GetDebugVariableValue(Var.ID, glControl.Frame - 1))
                   .Zip(dbgVars, (Val, Var) => { if (Val != null) NewVariableItem(Var.Name, Val); });
        }

        private void NewVariableItem(string groupName, Array val)
        {
            // CREATE LIST VIEW ROWS

            int rows = val.GetLength(0);
            int cols = val.GetLength(1);

            // add list group for this debug variable
            // -- dbgVar.Value ... debug variable name
            var dbgVarGroup = new ListViewGroup(groupName);
            debugListView.Groups.Add(dbgVarGroup);

            for (int r = 0; r < rows; r++)
            {
                // convert row of debug variable to string array
                var row = from c in Enumerable.Range(0, cols)
                          select string.Format(culture, "{0:0.000}", val.GetValue(r, c));
                // add row to list view
                var item = new ListViewItem(row.ToArray());
                item.Group = dbgVarGroup;
                debugListView.Items.Add(item);
            }
        }

        private void AddOutputItem(string file, int line, string msg)
            => output.Rows.Add(new[] { file, line > 0 ? line.ToString(culture) : "", msg });
        #endregion

        private void DebugRender()
        {
            // render the scene
            glControl.Render();

            // only use debugging if the selected editor
            // was used to generate the debug information
            if (compiledEditor == GetSelectedEditor() && compiledEditor != null)
                UpdateDebugListView(compiledEditor);
        }
    }
}
