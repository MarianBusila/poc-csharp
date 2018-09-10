using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Codility;

namespace UnitTest
{
    [TestClass]
    public class UnitTestCodility
    {
        [TestMethod]
        public void TestBinaryGap()
        {

            Assert.AreEqual(BinaryGap.Solution(9), 2);

            Assert.AreEqual(BinaryGap.Solution(2), 0);

            Assert.AreEqual(BinaryGap.Solution(5), 1);

            Assert.AreEqual(BinaryGap.Solution(1041), 5);

            Assert.AreEqual(BinaryGap.Solution(0), 0);

            Assert.AreEqual(BinaryGap.Solution(511), 0);

            Assert.AreEqual(BinaryGap.Solution(126), 0);

            Assert.AreEqual(BinaryGap.Solution(12320), 6);
        }

        [TestMethod]
        public void TestCyclicRotation()
        {
            Assert.AreEqual(string.Join(",", CyclicRotation.Solution(new int[] { 1, 2, 3, 4, 5 }, 2)), "4,5,1,2,3");
            Assert.AreEqual(string.Join(",", CyclicRotation.Solution(new int[] { 1, 2, 3, 4, 5 }, 0)), "1,2,3,4,5");
            Assert.AreEqual(string.Join(",", CyclicRotation.Solution(new int[] { 1 }, 4)), "1");
            Assert.AreEqual(string.Join(",", CyclicRotation.Solution(new int[] { }, 2)), "");
        }

        [TestMethod]
        public void TestPermMissingElem()
        {
            Assert.AreEqual(PermMissingElem.Solution(new int[] { 2, 3, 5, 4 }), 1);
            Assert.AreEqual(PermMissingElem.Solution(new int[] { 2 }), 1);
        }

        [TestMethod]
        public void TestTask1()
        {
            Assert.AreEqual(Task1.Solution(0), 0);
            Assert.AreEqual(Task1.Solution(10000), 10000);
            Assert.AreEqual(Task1.Solution(24), 42);
            Assert.AreEqual(Task1.Solution(54), 54);
            Assert.AreEqual(Task1.Solution(149), 941);
            Assert.AreEqual(Task1.Solution(9494), 9944);
            Assert.AreEqual(Task1.Solution(3275), 7532);
            Assert.AreEqual(Task1.Solution(4205), 5420);
        }

        [TestMethod]
        public void TestTask2()
        {
            Assert.AreEqual(Task2.Solution(""), "");
            Assert.AreEqual(Task2.Solution("A"), "A");
            Assert.AreEqual(Task2.Solution("ACCAABBC"), "AC");
            Assert.AreEqual(Task2.Solution("ABCBBCBA"), "");
            Assert.AreEqual(Task2.Solution("BABABA"), "BABABA");
            Assert.AreEqual(Task2.Solution("AACCBB"), "");
            Assert.AreEqual(Task2.Solution("ABCBB"), "ABC");
            Assert.AreEqual(Task2.Solution("AAABC"), "ABC");

            var largeString = "A";
            for (int i = 0; i < 50000 - 2; i++)
            {
                largeString += "B";
            }
            largeString += "C";
            Assert.AreEqual(Task2.Solution(largeString), "AC");
        }

        [TestMethod]
        public void TestTask3()
        {
            Assert.AreEqual(Task3.Solution(955), 4);
            Assert.AreEqual(Task3.Solution(877), 3);
            Assert.AreEqual(Task3.Solution(235980), 9); // here
            Assert.AreEqual(Task3.Solution(56173), 3);
            Assert.AreEqual(Task3.Solution(30307228), 5);
        }
    }
}
