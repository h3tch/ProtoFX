namespace App
{
    class GraphicControl : GLObject
    {
        public static string nullname = "__gled_control__";
        public OpenTK.GLControl control;

        public GraphicControl(OpenTK.GLControl control)
            : base(nullname, nullname)
        {
            this.control = control;
        }

        public override void Delete() { }
    }
}
