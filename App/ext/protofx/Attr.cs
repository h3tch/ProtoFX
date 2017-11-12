using System;

namespace protofx
{
    class FxAttribute : Attribute
    {
        public string LexerXml { get; private set; }

        public FxAttribute(params string[] xml)
        {
            LexerXml = String.Concat(xml);
        }
    }
}
