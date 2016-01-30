namespace App
{
    class GLText : GLObject
    {
        public string text { get; private set; }

        public GLText(Compiler.Block block, Dict scene, bool debugging)
            : base(block.Name, block.Anno)
        {
            text = block.Text;
        }
        
        public override void Delete() { }
    }
}
