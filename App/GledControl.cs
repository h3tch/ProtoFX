namespace App
{
    class GledControl : GLObject
    {
        public static string nullname = "__gled_control__";
        public OpenTK.GLControl control = null;

        public GledControl(OpenTK.GLControl control)
            : base(nullname, nullname)
        {
            this.control = control;
        }

        public override void Delete()
        {

        }
    }
}
