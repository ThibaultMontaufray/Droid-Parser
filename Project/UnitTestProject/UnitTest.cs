using Droid_FlatFile;
using NUnit.Framework;
using System;
using System.Windows.Forms;

namespace UnitTestProject
{
    [TestFixture]
    public class UnitTest
    {
        [Test]
        public void TestUTRuns()
        {
            Assert.IsTrue(true);
        }
        [Test]
        public void Test_progress_bar()
        {
            try
            {
                GUI g = new GUI();
                Assert.IsTrue(true);
            }
            catch (Exception exp)
            {
                Assert.Fail(exp.Message);
            }
        }
        [Test]
        public void Test_view()
        {
            try
            {
                GUI2 g = new GUI2();
                Assert.IsTrue(true);
            }
            catch (Exception exp)
            {
                Assert.Fail(exp.Message);
            }
        }
        [Test]
        public void Test_Textcoloration()
        {
            try
            {
                TextColoration tc = new TextColoration("csharp");
                tc.Text = "public static void testmathod(stringa, int n, float c) {}";
                Assert.IsTrue(true);
            }
            catch (Exception exp)
            {
                Assert.Fail(exp.Message);
            }
        }
        [Test]
        public void Test_ColorLanguage()
        {
            try
            {
                ColorLangage cl = new ColorLangage("csharp");
                cl.toColour(new TextColoration("csharp"));
                Assert.IsTrue(true);
            }
            catch (Exception exp)
            {
                Assert.Fail(exp.Message);
            }
        }
        [Test]
        public void Test_interfaceParser()
        {
            try
            {
                Interface_parser ip = new Interface_parser(new System.Collections.Generic.List<string>(), "");
                string ret;
                ret = ip.CurrentData;
                var v1 = ip.Listmessages;
                Assert.IsTrue(true);
            }
            catch (Exception exp)
            {
                Assert.Fail(exp.Message);
            }
        }
    }
}
