using App;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class UnitTestFXLexer
    {
        private static Fx2Lexer lexer;
        private static string code;

        public TestContext TestContext { get; set; }

        [ClassInitialize]
        public static void ClassInitialize(TestContext a)
        {
            lexer = new Fx2Lexer();
            code = "";
        }

        [TestMethod]
        public void TestMethodFindKeywords()
        {
            var word = "buffer";
            var keywords = lexer.GetPotentialKeywords(code, word, code.IndexOf(word));
        }

        [TestMethod]
        public void TestMethodFindStyle()
        {
            var word = "buffer";
            var style = lexer.GetKeywordStyle(code, word, code.IndexOf(word));
        }

        [TestMethod]
        public void TestMethodFindHint()
        {
            var word = "buffer";
            var hint = lexer.GetKeywordHint(code, word, code.IndexOf(word));
        }
    }
}
