using App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace OpenTK
{
    class GraphicControl : GLControl
    {
        #region FIELDS

        public static string nullname = "__control__";
        // indicates if rendering should be enabled
        private bool render = false;
        // number of exceptions thrown in the previous rendering pass
        private int renderExceptions = 0;
        // reference to compiler output
        private DataGridView output;
        // a collection of all objects making up the scene
        private Dictionary<string, object> scene = new Dictionary<string, object>();
        // performance measurement class
        private FXPerf perf;
        private bool renderingEnabled = false;
        private GLCsharp csExtensions = new GLCsharp(new[] { "../ext" });

        #endregion

        #region PROPERTIES
        
        public Dictionary<string, object> Scene { get { return scene; } }
        public int Frame { get; private set; } = 0;
        public int TimingsCount => perf?.TimingsCount ?? 0;
        public IEnumerable<float> Timings => perf?.Timings ?? Enumerable.Empty<float>();
        public IEnumerable<int> Frames => perf?.Frames ?? Enumerable.Empty<int>();
        public bool Rendering {
            get => renderingEnabled;
            set {
                if (renderingEnabled != value)
                {
                    if (renderingEnabled = value)
                        Application.Idle += HandleIdle;
                    else
                        Application.Idle -= HandleIdle;
                }
            }
        }

        #endregion

        /// <summary>
        /// Instantiate and initialize graphics control based on OpenTK.
        /// </summary>
        public GraphicControl() : base() { }

        /// <summary>
        /// Setup all internal events of the class.
        /// </summary>
        public void AddEvents(DataGridView output)
        {
            Paint += HandlePaint;
            MouseDown += HandleMouseDown;
            MouseMove += HandleMouseMove;
            MouseUp += HandleMouseUp;
            Resize += HandleResize;
            KeyUp += HandleKeyUp;
            this.output = output;
        }

        /// <summary>
        /// Remove all events from the class.
        /// </summary>
        public void RemoveEvents()
        {
            Events.Dispose();
        }

        /// <summary>
        /// Render scene.
        /// </summary>
        public void Render()
        {
            // clear existing render exceptions
            if (renderExceptions > 0)
            {
                for (int i = 0; i < output.Rows.Count; i++)
                    if ((string)output.Rows[i].Cells[0].Value == "render")
                        output.Rows.RemoveAt(i--);
                renderExceptions = 0;
            }

            try
            {
                MakeCurrent();

                // clear debug trace
                App.Glsl.Debugger.ClearDebugTrace();

                // begin timer query
                if (perf == null)
                    perf = new FXPerf(nullname, null, 309, false);
                perf.MeasureTime();
                perf.StartTimer(Frame);

                // render the scene
                foreach (var x in from o in scene where o.Value is GLTech select o.Value as GLTech)
                    x.Exec(ClientSize.Width, ClientSize.Height, Frame);

                // end timer query
                perf.EndTimer();

                SwapBuffers();
            }
            catch (Exception ex)
            {
                // add exception to output
                output.Rows.Add(new[] { "render", string.Empty, (ex.InnerException ?? ex).Message });
                renderExceptions++;
            }

            // increase render frame
            Frame++;
        }

        /// <summary>
        /// Add a new object to the scene.
        /// </summary>
        /// <param name="block"></param>
        /// <param name="debugging"></param>
        public void AddObject(Compiler.Block block, bool debugging)
        {
            // GET CLASS TYPE, ANNOTATION AND NAME
            var typeName = block.Type[0].ToString().ToUpper() + block.Type.Substring(1);
            var type = Type.GetType($"App.GL{typeName}") ?? csExtensions.GetType(typeName);

            // check for errors
            if (type == null)
                throw new CompileException($"{block.Type} '{block.Name}'")
                    .Error($"Class type '{block.Type}' not known.", block);
            if (scene.ContainsKey(block.Name))
                throw new CompileException($"{block.Type} '{block.Name}'")
                    .Error($"Class name '{block.Name}' already exists.", block);

            var @params = new Params()
            {
                Block = block,
                Scene = scene,
                Debug = debugging
            };

            // instantiate class
            var instance = Activator.CreateInstance(type, @params);
            scene.Add(block.Name, instance);
        }
        
        /// <summary>
        /// Clear all scene objects.
        /// </summary>
        public void ClearScene()
        {
            Events.Dispose();
            // call delete method of OpenGL resources
            scene.ForEach(x => ((GLObject)x.Value).Delete());
            // clear list of classes
            scene.Clear();
            // add default OpenTK glControl
            scene.Add(nullname, new GLReference(nullname, "internal", this));
        }

        #region EVENTS

        /// <summary>
        /// On resize, redraw the scene.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void HandleResize(object s, EventArgs e)
        {
            Render();
        }

        /// <summary>
        /// On paint event, redraw the scene.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void HandlePaint(object s, PaintEventArgs e)
        {
            Render();
        }

        /// <summary>
        /// On mouse down, activate rendering.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void HandleMouseDown(object s, MouseEventArgs e)
        {
            render = true;
        }

        /// <summary>
        /// On mouse up, stop rendering.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void HandleMouseUp(object s, MouseEventArgs e)
        {
            if (render)
                Render();
            render = false;
        }

        /// <summary>
        /// When the mouse is moving over the control, render the scene.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void HandleMouseMove(object s, MouseEventArgs e)
        {
            if (render)
                Render();
        }

        /// <summary>
        /// On key up, render.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void HandleKeyUp(object s, KeyEventArgs e)
        {
            Render();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleIdle(object sender, EventArgs e)
        {
            // Pause so the render loop does not
            // take up too much processing time
            Thread.Sleep(1);
            // Redraw the OpenGL control
            Invalidate();
        }

        #endregion

        class Params
        {
            public Compiler.Block Block;
            public Dictionary<string, object> Scene;
            public bool Debug;
            public ILookup<string, string[]> Commands => Block.ToLookup();
            public string Name => Block.Name;
        }
    }
}
