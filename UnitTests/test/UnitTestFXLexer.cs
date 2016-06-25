using App;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

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
            code = File.ReadAllText("../../../App/demos/simple.tech");
        }

        [TestMethod]
        public void TestMethodFindBufferKeyword()
        {
            var word = "buffer";
            var pos = code.IndexOf("buffer buf_pos");
            var keywords = lexer.GetPotentialKeywords(code, word, pos);
            foreach (var keyword in keywords)
                Assert.IsTrue(keyword.StartsWith(word));
            Assert.AreEqual(keywords.Count(), 1);
        }

        [TestMethod]
        public void TestMethodFindBufferCommandsKeyword()
        {
            var word = "";
            var pos = code.IndexOf("usage staticDraw");
            var keywords = lexer.GetPotentialKeywords(code, word, pos);
            Assert.AreEqual(keywords.Count(), 4);
            word = "usa";
            pos += word.Length;
            keywords = lexer.GetPotentialKeywords(code, word, pos);
            Assert.AreEqual(keywords.Count(), 1);
            Assert.AreEqual(keywords.First(), "usage");
        }

        [TestMethod]
        public void TestMethodFindKeywords()
        {
            var word = "";
            var pos = code.IndexOf(word);
            var keywords = lexer.GetPotentialKeywords(code, word, 0);
            Assert.AreEqual(keywords.Count(), 5);
        }

        [TestMethod]
        public void TestMethodFindBufferStyle()
        {
            var word = "buffer";
            var pos = code.IndexOf(word);
            var style = lexer.GetKeywordStyle(code, word, pos);
            Assert.AreEqual(style, 1);
        }

        [TestMethod]
        public void TestMethodFindBufferHint()
        {
            var word = "buffer";
            var pos = code.IndexOf(word);
            var hint = lexer.GetKeywordHint(code, word, pos);
            Assert.IsNotNull(hint);
        }
    }
}
