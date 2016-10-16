using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace App
{
    public partial class App
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
            numImgLayer.Maximum = Math.Max(Math.Max(img.Length, img.Depth) - 1, 0);

            // read image data from GPU
            glControl.MakeCurrent();
            var bmp = img.Read((int)numImgLayer.Value, 0);
            bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            panelImg.Image.Image = bmp;
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
            var col = dt.Columns.Add("#", typeof(string));
            // create columns
            for (int i = 0; i < dim; i++)
                dt.Columns.Add(i.ToString(), colType);
            // create rows
            for (int i = 0; i < da.Length;)
            {
                var row = dt.NewRow();
                row.SetField(0, $"{{{i}}}");
                for (int c = 0; c < dim && i < da.Length; c++)
                    row.SetField(c + 1, da.GetValue(i++));
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
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void comboProp_SelectedIndexChanged(object s, EventArgs e)
        {
            // gather needed info
            if (comboProp.SelectedItem is GLInstance)
                propertyGrid.SelectedObject = (comboProp.SelectedItem as GLInstance).Instance;
        }

        /// <summary>
        /// Update property grid when clicking on it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void propertyGrid_Click(object s, EventArgs e)
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
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void output_DoubleClick(object s, EventArgs e)
        {
            // if no item is selected and no code compiled, return
            var view = s as DataGridView;
            if (view.SelectedRows.Count == 0 || CompiledEditor == null)
                return;

            // get line from selected item
            int line;
            var text = view.SelectedRows[0].Cells[1].Value as string;
            if (!int.TryParse(text, NumberStyles.Integer, CultureInfo.CurrentCulture, out line))
                return;

            // scroll to line
            line = Math.Max(1, line - CompiledEditor.LinesOnScreen / 2);
            CompiledEditor.LineScroll(line - CompiledEditor.FirstVisibleLine - 1, 0);
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
            var lineNumber = line > 0 ? line.ToString(CultureInfo.CurrentCulture) : string.Empty;
            output.Rows.Add(new[] { relPath, lineNumber, msg });
        }

        #endregion

        #region Debug Shader

        /// <summary>
        /// When the selection (caret) changed update the debug tab.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void editor_UpdateUI(object s, UpdateUIEventArgs e)
        {
            // get class references
            var editor = s as CodeEditor;

            // handle selection changed event, but only
            // update debug information for the compiled editor
            if (e.Change == UpdateChange.Selection && CompiledEditor == editor)
                UpdateDebugListView(editor);
        }

        /// <summary>
        /// On mouse move check whether the mouse hovers over a debug variable.
        /// If so, show a popup with the variables value (if possible).
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void editor_MouseMove(object s, MouseEventArgs e)
        {
            // get class references
            var editor = s as CodeEditor;

            // only update debug information for the compiled editor
            if (CompiledEditor != editor)
                return;

            // convert cursor position to text position
            int pos = editor.CharPositionFromPoint(e.X, e.Y);

            // get debug variable information from position
            var dbgVar = FxDebugger.GetDebugVariableFromPosition(editor, pos);
            // no debug variable found
            if (dbgVar != null)
            {
                // get debug variable value
                var dbgVal = FxDebugger.GetDebugVariableValue(dbgVar.ID, glControl.Frame - 1);
                if (dbgVal != null)
                {
                    pos = editor.WordStartPosition(pos, true);
                    editor.CallTipShow(pos, FxDebugger.DebugVariableToString(dbgVal));
                    editor.EnableCodeHints = false;
                    return;
                }
            }

            editor.EnableCodeHints = true;
        }

        /// <summary>
        /// When the call tip is shown, check whether there
        /// are some performance statistics we can show.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void editor_ShowCallTip(object s, ShowTipEventHandlerArgs e)
        {
            var editor = s as CodeEditor;

            // is the selected word a performance keyword
            switch (editor.GetWordFromPosition(e.TextPosition))
            {
                case "pass": ShowPerfTip<GLPass>(editor, e.TextPosition); break;
                case "tech": ShowPerfTip<GLTech>(editor, e.TextPosition); break;
            }
        }

        /// <summary>
        /// Cancel the performance call tip
        /// if the normal call tip is canceled.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void editor_CancleCallTip(object s, CancleTipEventHandlerArgs e)
            => (s as CodeEditor).PerfTipCancel();

        /// <summary>
        /// Show variables of the currently selected line in the editor.
        /// </summary>
        /// <param name="editor"></param>
        private void UpdateDebugListView(CodeEditor editor)
        {
            // RESET DEBUG LIST VIEW
            debugListView.Clear();
            debugListView.AddColumn("X", 80);
            debugListView.AddColumn("Y", 80);
            debugListView.AddColumn("Z", 80);
            debugListView.AddColumn("W", 80);

            // if the code has been edited no debug information can
            // be shown, because debug variables might have been
            // added or removed, which leads to invalid debug output
            if ((editor.Parent as TabPage).Text.EndsWith("*"))
            {
                debugListView.Visible = false;
                return;
            }

            // get debug variables of the line where the caret is placed
            var first = editor.LineFromPosition(editor.SelectionStart);
            var last = editor.LineFromPosition(editor.SelectionEnd);
            var dbgVars = FxDebugger.GetDebugVariablesFromLine(editor, first, last - first + 1);
            debugListView.Visible = dbgVars.Count() > 0;
            dbgVars.Select(Var => FxDebugger.GetDebugVariableValue(Var.ID, glControl.Frame - 1))
                   .ForEach(dbgVars, (Val, Var) => NewVariableItem(Var.Name, Val));
            debugListView.Update();
        }

        /// <summary>
        /// Add variable to debug list view item of the debug tab.
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="val"></param>
        private void NewVariableItem(string groupName, Array val)
        {
            if (val == null)
                return;

            // get matrix size
            int rows = val.GetLength(0);
            int cols = val.GetLength(1);

            // add list group for this debug variable
            var dbgVarGroup = new ListViewGroup(groupName);
            debugListView.AddGroup(dbgVarGroup);

            for (int r = 0; r < rows; r++)
            {
                // convert row of debug variable to string array
                var row = from c in Enumerable.Range(0, cols)
                          select string.Format(CultureInfo.CurrentCulture, "{0:0.000}", val.GetValue(r, c));
                // add row to list view
                var item = new ListViewItem(row.ToArray());
                item.Group = dbgVarGroup;
                debugListView.AddItem(item);
            }
        }

        #endregion

        #region Performance Tip

        /// <summary>
        /// Show performance tip for tech object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="editor"></param>
        /// <param name="position"></param>
        private void ShowPerfTip<T>(CodeEditor editor, int position) where T : FXPerf
        {
            // get name
            var pos = editor.WordEndPosition(position, false);
            pos = editor.WordEndPosition(pos + 1, true);
            var name = editor.GetWordFromPosition(pos);

            // get object
            var obj = glControl.Scene.GetValueOrDefault<T>(name);
            if (obj == null)
                return;

            // if there are performance timings, show them
            if (obj.TimingsCount > 0)
            {
                IEnumerable<int> frames;
                IEnumerable<float> times;
                PostProcessPerfData(obj.Frames, obj.Timings, out frames, out times, 10);
                editor.PerfTipShow(position, frames.ToArray(), times.ToArray());
            }
        }

        /// <summary>
        /// Post process performance data for the GUI.
        /// </summary>
        /// <param name="frames"></param>
        /// <param name="times"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="multipleOf"></param>
        /// <param name="removeOutliers"></param>
        private void PostProcessPerfData(IEnumerable<int> frames, IEnumerable<float> times,
            out IEnumerable<int> X, out IEnumerable<float> Y,
            int multipleOf = 10, bool removeOutliers = false)
        {
            // remove statistical outliers
            if (removeOutliers)
            {
                var mean = times.Average();
                var std = times.Select(a => Math.Abs(a - mean)).Average();
                var lower = mean - std * 1.5f;
                var upper = mean + std * 1.5f;
                var sel = times.Select(t => lower <= t && t <= upper);
                X = frames.Zip(sel, (a, b) => b ? a : int.MinValue).Where(a => a != int.MinValue);
                Y = times.Zip(sel, (a, b) => b ? a : float.NaN).Where(a => !float.IsNaN(a));
            }
            else
            {
                X = frames;
                Y = times;
            }

            // make frame number relative to current frame
            int fistFrame = X.ElementAt(0);
            int lastIdx = X.LastIndexOf(a => ((a - fistFrame) % multipleOf) == 0) + 1;
            X = X.Take(lastIdx).Select(a => a - fistFrame);
            Y = Y.Take(lastIdx);
        }

        #endregion

        /// <summary>
        /// Render and get debug variables.
        /// </summary>
        private void DebugRender()
        {
            // render the scene
            glControl.Render();

            // only use debugging if the selected editor
            // was used to generate the debug information
            if (CompiledEditor == SelectedEditor && CompiledEditor != null)
                UpdateDebugListView(CompiledEditor);
        }
    }
}
