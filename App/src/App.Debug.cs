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
        /// <summary>
        /// Raised when another image is selected for inspection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboImg_SelectedIndexChanged(object sender, EventArgs e)
            => pictureImg_Click(sender, e);

        /// <summary>
        /// Raised when another layer of the image is selected for inspection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Update image when clicking on it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureImg_Click(object sender, EventArgs e)
            => numImgLayer_ValueChanged(sender, e);
        #endregion

        #region Debug Buffer
        /// <summary>
        /// Raised when another buffer is selected for inspection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Update the buffer if the type is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBufType_SelectedIndexChanged(object sender, EventArgs e)
            => comboBuf_SelectedIndexChanged(sender, e);

        /// <summary>
        /// Update the buffer if the dimension is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numBufDim_ValueChanged(object sender, EventArgs e)
            => comboBuf_SelectedIndexChanged(sender, null);
        #endregion

        #region Debug Properties
        /// <summary>
        /// Raised when another ProtoFX "instance-object" is selected for inspection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboProp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboProp.SelectedItem == null || !(comboProp.SelectedItem is GLInstance))
                return;

            // gather needed info
            propertyGrid.SelectedObject = ((GLInstance)comboProp.SelectedItem).instance;
        }

        /// <summary>
        /// Update property grid when clicking on it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void propertyGrid_Click(object sender, EventArgs e)
            => propertyGrid.SelectedObject = propertyGrid.SelectedObject;

        /// <summary>
        /// Rerender the scene when a value in the property grid changed.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
            => DebugRender();
        #endregion

        #region Compiler Output
        /// <summary>
        /// On double clicking on a compiler error, jump to the line where the error happened.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Add an error to the compiler output tab.
        /// </summary>
        /// <param name="refDir"></param>
        /// <param name="filePath"></param>
        /// <param name="line"></param>
        /// <param name="msg"></param>
        private void AddOutputItem(string refDir, string filePath, int line, string msg)
        {
            var refUri = new Uri(refDir);
            var fileUri = new Uri(filePath);
            var relPath = refUri.MakeRelativeUri(fileUri).ToString();
            output.Rows.Add(new[] { relPath, line > 0 ? line.ToString(culture) : "", msg });
        }
        #endregion

        #region Debug Shader
        /// <summary>
        /// When the selection (caret) changed update the debug tab.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editor_UpdateUI(object sender, UpdateUIEventArgs e)
        {
            // get class references
            var editor = (CodeEditor)sender;

            // handle selection changed event, but only
            // update debug information for the compiled editor
            if (e.Change == UpdateChange.Selection && compiledEditor == editor)
                UpdateDebugListView(editor);
        }

        /// <summary>
        /// On mouse move check whether the mouse hovers over a debug variable.
        /// If so, show a popup with the variables value (if possible).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Show variables of the currently selected line in the editor.
        /// </summary>
        /// <param name="editor"></param>
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

        /// <summary>
        /// Add variable to debug list view item of the debug tab.
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="val"></param>
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
