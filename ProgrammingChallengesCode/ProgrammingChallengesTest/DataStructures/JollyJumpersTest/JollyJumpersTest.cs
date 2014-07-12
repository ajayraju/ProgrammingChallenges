using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgrammingChallengesCode.DataStructures.JollyJumpers;

namespace ProgrammingChallengesTest.DataStructures.JollyJumpersTest
{
    [TestClass]
    public class JollyJumpersTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestJollyJumperWithNullInput()
        {
            int[] input = null;
            JollyJumpers.IsJollyJumper(input);

        }

        [TestMethod]
        public void TestJollyJumpersWithEmptyArray()
        {
            int[] input =  new int[2];
            var result = JollyJumpers.IsJollyJumper(input);
            Assert.AreEqual(Status.NotJolly,result);
        }

        [TestMethod]
        public void TestJollyWithSingleElement()
        {
            int[] input = {18};
            Assert.AreEqual(Status.Jolly,JollyJumpers.IsJollyJumper(input));

        }

        [TestMethod]
        public void TestJollyInput1()
        {
            int[] input = {1, 3};
            Assert.AreEqual(Status.NotJolly,JollyJumpers.IsJollyJumper(input));
        }

        [TestMethod]
        public void TestJollyInput2()
        {
            int[] input = {1, 4, 2, 3};
            Assert.AreEqual(Status.Jolly, JollyJumpers.IsJollyJumper(input));
        }
        [TestMethod]
        public void TestJollyInput3()
        {
            int[] input = { 4,1,4,2,3 };
            Assert.AreEqual(Status.NotJolly, JollyJumpers.IsJollyJumper(input));
        }
    }
}
