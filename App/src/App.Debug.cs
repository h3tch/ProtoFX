using protofx.gl;
using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace protofx
{
    public partial class App
    {
        #region Debug Image

        /// <summary>
        /// Raised when another image is selected for inspection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboImg_SelectedIndexChanged(object sender, EventArgs e)
        {
            PictureImg_Click(sender, e);
        }

        /// <summary>
        /// Raised when another layer of the image is selected for inspection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumImgLayer_ValueChanged(object sender, EventArgs e)
        {
            var item = comboImg.SelectedItem;
            if (item == null)
                return;

            Bitmap bmp;

            if (item is gl.Image image)
            {
                // update the maximum number of mipmap layers
                numImgLayer.Maximum = Math.Max(Math.Max(image.Length, image.Depth) - 1, 0);
                // read image data from the GPU
                bmp = image.Read((int)numImgLayer.Value, 0);
            }
            else if (item is Instance instance)
            {
                // mipmap layers are not supported for GLInstance visualizations
                numImgLayer.Maximum = 0;
                // get visualization
                bmp = (Bitmap)instance.Visualize();
            }
            else return;

            bmp?.RotateFlip(RotateFlipType.RotateNoneFlipY);
            panelImg.Image.Image = bmp;
        }

        /// <summary>
        /// Update image when clicking on it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureImg_Click(object sender, EventArgs e)
        {
            NumImgLayer_ValueChanged(sender, e);
        }

        #endregion

        #region Debug Buffer

        /// <summary>
        /// Raised when another buffer is selected for inspection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBuf_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = comboBuf.SelectedItem;
            if (item == null || !(item is gl.Buffer || item is Instance))
                return;

            // gather format info
            var type = (string)comboBufType.SelectedItem;
            var dim = (int)numBufDim.Value;
            var name = (item as gl.Object).Name;

            // gather data
            var data = (item is gl.Buffer)
                ? (item as gl.Buffer).Read()
                : (byte[])(item as Instance).Visualize();

            // convert data to specified type
            var da = data.To(type, out Type colType);

            // CREATE TABLE
            var dt = new DataTable(name);
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
            var ds = new DataSet(name);
            ds.Tables.Add(dt);
            tableBuf.DataSource = ds;
            tableBuf.DataMember = name;
        }

        /// <summary>
        /// Update the buffer if the type is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBufType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBuf_SelectedIndexChanged(sender, e);
        }

        /// <summary>
        /// Update the buffer if the dimension is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumBufDim_ValueChanged(object sender, EventArgs e)
        {
            ComboBuf_SelectedIndexChanged(sender, null);
        }

        /// <summary>
        /// Update the buffer on click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TableBuf_Click(object sender, EventArgs e)
        {
            ComboBuf_SelectedIndexChanged(sender, e);
        }

        #endregion

        #region Debug Properties

        /// <summary>
        /// Raised when another ProtoFX "instance-object" is selected for inspection.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void ComboProp_SelectedIndexChanged(object s, EventArgs e)
        {
            // gather needed info
            if (comboProp.SelectedItem is Instance)
                propertyGrid.SelectedObject = (comboProp.SelectedItem as Instance).Owner;
        }

        /// <summary>
        /// Update property grid when clicking on it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PropertyGrid_Click(object s, EventArgs e)
        {
            propertyGrid.SelectedObject = propertyGrid.SelectedObject;
        }

        /// <summary>
        /// Rerender the scene when a value in the property grid changed.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void PropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            DebugRender(true);
        }

        #endregion

        #region Compiler Output

        /// <summary>
        /// On double clicking on a compiler error, jump to the line where the error happened.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void Output_DoubleClick(object s, EventArgs e)
        {
            // if no item is selected and no code compiled, return
            var view = s as DataGridView;
            if (view.SelectedRows.Count == 0 || CompiledEditor == null)
                return;

            // get line from selected item
            var text = view.SelectedRows[0].Cells[1].Value as string;
            if (!int.TryParse(text, NumberStyles.Integer, CultureInfo.CurrentCulture, out int line))
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

        private Glsl.TraceInfo[] DebugInfo;
        private int CurrentDebugInfo;

        /// <summary>
        /// Step to next breakpoint.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void DebugStepBreakpoint_Click(object s = null, EventArgs e = null)
        {
            if (DebugInfo.Length == 0)
                return;

            // goto next debug info
            CurrentDebugInfo = ++CurrentDebugInfo % DebugInfo.Length;

            // select all debug information after the current line
            var lines = DebugInfo.Skip(CurrentDebugInfo).Select(x => x.Location.Line);

            // get all breakpoints
            var points = CompiledEditor.GetBreakpoints();

            // find all debug information that contains a breakpoint
            var union = from line in lines
                        join point in points on line equals point
                        select line;

            // next debug line found
            if (union.Count() > 0)
            {
                var line = union.First();
                CurrentDebugInfo = DebugInfo.IndexOf(x => x.Location.Line == line, CurrentDebugInfo);
            }
            // no breakpoint found
            else
                CurrentDebugInfo = -1;

            // update debug interface
            DebugInterfaceUpdate();
        }

        /// <summary>
        /// Step over the function.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void DebugStepOver_Click(object s = null, EventArgs e = null)
        {
            if (DebugInfo.Length == 0)
                return;

            if (CurrentDebugInfo >= 0)
            {
                // get the current debug level (to prevent stepping into a lover level)
                var level = DebugInfo[CurrentDebugInfo].Location.Level;
                // goto next debug info
                CurrentDebugInfo = ++CurrentDebugInfo % DebugInfo.Length;
                // goto next debug info in the same level
                CurrentDebugInfo = DebugInfo.IndexOf(x => x.Location.Level == level, CurrentDebugInfo);
            }
            else
                // goto first debug info (e.g. first press or begin anew)
                CurrentDebugInfo = 0;

            // update debug interface
            DebugInterfaceUpdate();
        }

        /// <summary>
        /// Step into the function.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void DebugStepInto_Click(object s = null, EventArgs e = null)
        {
            if (DebugInfo.Length == 0)
                return;

            // goto next debug info
            CurrentDebugInfo = ++CurrentDebugInfo % DebugInfo.Length;

            // update debug interface
            DebugInterfaceUpdate();
        }

        /// <summary>
        /// Undo one step.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void DebugStepBack_Click(object s = null, EventArgs e = null)
        {
            if (DebugInfo.Length == 0)
                return;

            // goto previous debug info
            CurrentDebugInfo = Math.Max(0, --CurrentDebugInfo);

            // update debug interface
            DebugInterfaceUpdate();
        }

        /// <summary>
        /// Reset debugging interface.
        /// </summary>
        private void DebugResetInterface()
        {
            DebugInfo = Glsl.Debugger.DebugTrace.ToArray();
            CurrentDebugInfo = -1;
            DebugStepBreakpoint_Click();
        }

        /// <summary>
        /// Update debug interface.
        /// </summary>
        private void DebugInterfaceUpdate()
        {
            if (CurrentDebugInfo < 0)
                return;
            var trace = DebugInfo[CurrentDebugInfo];
            var range = new int[2];

            // highlight current debug variable
            range[0] = CompiledEditor.Lines[trace.Location.Line].Position + trace.Location.Column;
            range[1] = range[0] + trace.Location.Length;
            CompiledEditor.ClearIndicators(CodeEditor.DebugHighlight);
            CompiledEditor.AddIndicators(CodeEditor.DebugHighlight, new[] { range });

            // place execution marker
            CompiledEditor.RemoveExecutionMarker();
            CompiledEditor.AddExecutionMarker(trace.Location.Line);

            // scroll debug variable into view
            CompiledEditor.ScrollRange(range[0], range[1]);

            // show debug information for the line
            UpdateDebugListView();
        }

        /// <summary>
        /// When the call tip is shown, check whether there
        /// are some performance statistics we can show.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void Editor_ShowCallTip(object s, ShowTipEventArgs e)
        {
            var editor = s as CodeEditor;

            // is the selected word a performance keyword
            switch (editor.GetWordFromPosition(e.TextPosition))
            {
                case "pass": ShowPerfTip<Pass>(editor, e.TextPosition); break;
                case "tech": ShowPerfTip<Tech>(editor, e.TextPosition); break;
            }
        }

        /// <summary>
        /// Cancel the performance call tip
        /// if the normal call tip is canceled.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void Editor_CancleCallTip(object s, CancleTipEventArgs e)
        {
            (s as CodeEditor).PerfTipCancel();
        }

        /// <summary>
        /// Show debug information on mouse hover.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void Editor_MouseHover(object s, EventArgs e)
        {
            // get class references
            var editor = s as CodeEditor;

            // only update debug information for the compiled editor
            if (CompiledEditor != editor)
                return;

            // convert cursor position to text position
            var location = editor.PointToScreen(Point.Empty);
            var mouse = editor.PointToClient(Cursor.Position);
            var pos = editor.CharPositionFromPoint(mouse.X, mouse.Y);
            var line = editor.LineFromPosition(pos);
            var column = pos - editor.Lines[line].Position;
            
            // get debug info for the position
            var info = editor.GetWordFromPosition(pos)?.Length > 0
                ? Glsl.Debugger.GetTraceInfo(line, column, CurrentDebugInfo) : null;
            
            // disable code hints if debug information is shown
            if (!(editor.EnableCodeHints = info == null))
                editor.CallTipShow(editor.WordStartPosition(pos, true), info?.ToString());
        }

        /// <summary>
        /// Show variables of the currently selected line in the editor.
        /// </summary>
        /// <param name="editor"></param>
        private void UpdateDebugListView()
        {
            // RESET DEBUG LIST VIEW
            debugListView.Clear();
            debugListView.AddColumn("X", 80);
            debugListView.AddColumn("Y", 80);
            debugListView.AddColumn("Z", 80);
            debugListView.AddColumn("W", 80);

            if (0 <= CurrentDebugInfo && CurrentDebugInfo < (DebugInfo?.Length ?? 0))
            {
                // get debug variables of the line where the caret is placed
                var line = DebugInfo[CurrentDebugInfo].Location.Line;

                var startIdx = CurrentDebugInfo;
                while (startIdx > 0 && DebugInfo[startIdx - 1].Location.Line == line)
                    startIdx--;

                var endIdx = CurrentDebugInfo;
                while (endIdx < DebugInfo.Length && DebugInfo[endIdx].Location.Line == line)
                    endIdx++;

                // get debug info for the line
                while (startIdx < endIdx)
                {
                    NewVariableItem(DebugInfo[startIdx].Name, DebugInfo[startIdx].OutputArray);
                    startIdx++;
                }
            }

            debugListView.Refresh();
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

            // add list group for this debug variable
            var dbgVarGroup = new ListViewGroup(groupName);
            debugListView.AddGroup(dbgVarGroup);

            // is matrix
            if (val.Rank > 1)
            {
                // get matrix size
                int rows = val.GetLength(0);
                int cols = val.GetLength(1);

                for (int r = 0; r < rows; r++)
                    NewVariableRow(Enumerable.Range(0, cols)
                        .Select(c => val.GetValue(r, c)), dbgVarGroup);
            }
            // is vector or scalar
            else
                NewVariableRow(Enumerable.Range(0, val.Length)
                    .Select(c => val.GetValue(c)), dbgVarGroup);

        }

        private void NewVariableRow(IEnumerable<object> val, ListViewGroup dbgVarGroup)
        {
            // convert row of debug variable to string array
            var row = from v in val
                      select string.Format(CultureInfo.CurrentCulture, "{0:0.000}", v);
            // add row to list view
            var item = new ListViewItem(row.ToArray())
            {
                Group = dbgVarGroup
            };
            debugListView.AddItem(item);
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
            if (!(glControl.Scene.TryGetValue(name, out var obj) && obj is T))
                return;

            // if there are performance timings, show them
            if (((T)obj).TimingsCount > 0)
            {
                (var frames, var times) = PostProcessPerfData(((T)obj).Frames, ((T)obj).Timings, 10);
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
        private (IEnumerable<int>, IEnumerable<float>) PostProcessPerfData(
            IEnumerable<int> frames, IEnumerable<float> times,
            int multipleOf = 10, bool removeOutliers = false)
        {
            IEnumerable<int> X;
            IEnumerable<float> Y;

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

            return (X, Y);
        }

        #endregion

        /// <summary>
        /// Render and get debug variables.
        /// </summary>
        private void DebugRender(bool traceDebugInfo)
        {
            // enable or disable traceing debug information
            if (traceDebugInfo)
                glControl.Scene
                    .Where(x => x.Value is Pass)
                    .ForEach(x => ((Pass)x.Value).TraceDebugInfo = true);

            // render the scene
            glControl.Render();

            // UPDATE DEBUG INFORMATION IF NECESSARY
            if (traceDebugInfo)
                DebugResetInterface();

            // only use debugging if the selected editor
            // was used to generate the debug information
            if (CompiledEditor == SelectedEditor && CompiledEditor != null)
                UpdateDebugListView();
        }
    }
}
