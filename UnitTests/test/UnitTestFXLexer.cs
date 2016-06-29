using App.Lexer;
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
            var keywords = lexer.GetPotentialKeywords(code, pos, word);
            foreach (var keyword in keywords)
                Assert.IsTrue(keyword.StartsWith(word));
            Assert.AreEqual(1, keywords.Count());
        }

        [TestMethod]
        public void TestMethodFindBufferCommandsKeyword()
        {
            var word = "";
            var pos = code.IndexOf("usage staticDraw");
            var keywords = lexer.GetPotentialKeywords(code, pos, word);
            Assert.AreEqual(4, keywords.Count());
            word = "usa";
            pos += word.Length;
            keywords = lexer.GetPotentialKeywords(code, pos, word);
            Assert.AreEqual(1, keywords.Count());
            Assert.AreEqual("usage", keywords.First());
        }

        [TestMethod]
        public void TestMethodFindKeywords()
        {
            var word = "";
            var keywords = lexer.GetPotentialKeywords(code, 0, word);
            Assert.AreEqual(13, keywords.Count());
            word = "s";
            keywords = lexer.GetPotentialKeywords(code, 0, word);
            Assert.AreEqual(2, keywords.Count());
            word = "t";
            keywords = lexer.GetPotentialKeywords(code, 0, word);
            Assert.AreEqual(3, keywords.Count());
            word = "tex";
            keywords = lexer.GetPotentialKeywords(code, 0, word);
            Assert.AreEqual(2, keywords.Count());
            word = "textu";
            keywords = lexer.GetPotentialKeywords(code, 0, word);
            Assert.AreEqual(1, keywords.Count());
        }

        [TestMethod]
        public void TestMethodFindBufferStyle()
        {
            var word = "buffer";
            var pos = code.IndexOf(word);
            var style = lexer.GetKeywordStyle(code, pos, word);
            Assert.AreEqual(1, style);
        }

        [TestMethod]
        public void TestMethodFindBufferHint()
        {
            var word = "buffer";
            var pos = code.IndexOf(word);
            var hint = lexer.GetKeywordHint(code, pos, word);
            Assert.IsNotNull(hint);
        }
    }
}
