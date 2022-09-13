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

        [TestMethod]
        public void TestConsecSum()
        {
            Assert.AreEqual(ConsecSum.Solution(5), 2);
            Assert.AreEqual(ConsecSum.Solution(9), 3);
            Assert.AreEqual(ConsecSum.Solution(15), 4);
            Assert.AreEqual(ConsecSum.Solution(36), 3);

            Assert.AreEqual(ConsecSum.Solution2(5), 2);
            Assert.AreEqual(ConsecSum.Solution2(9), 3);
            Assert.AreEqual(ConsecSum.Solution2(15), 4);
            Assert.AreEqual(ConsecSum.Solution2(36), 3);
        }

        [TestMethod]
        public void TestDiagonalMatrixSum()
        {
            string resultStr = DiagonalMatrixSum.MatrixToString(DiagonalMatrixSum.Solution(new int[,] { { 1, 2, 3 }, { 1, 2, 1 }, { 2, 3, 4 } }));
            Assert.AreEqual(resultStr, "{1,3,6},{2,6,10},{4,11,19}");

            resultStr = DiagonalMatrixSum.MatrixToString(DiagonalMatrixSum.Solution(new int[,] { { 1, 1, 4, 2 }, { 5, 1, 3, 6 }, { 2, 1, 3, 2 } }));
            Assert.AreEqual(resultStr, "{1,2,6,8},{6,8,15,23},{8,11,21,31}");
        }

        [TestMethod]
        public void TestPassingCars()
        {
            Assert.AreEqual(PassingCars.Solution(new[] { 0, 1, 0, 1, 1 }), 5);
            Assert.AreEqual(PassingCars.Solution(new[] { 0, 0, 0, 0, 1 }), 4);
            Assert.AreEqual(PassingCars.Solution(new[] { 1, 0, 0, 0, 0 }), 0);
            Assert.AreEqual(PassingCars.Solution(new[] { 1, 0, 0, 1, 0, 1, 1 }), 8);
        }

        [TestMethod]
        public void TestDistinct()
        {
            Assert.AreEqual(Distinct.Solution(new[] { 2, 1, 1, 2, 3, 1 }), 3);
            Assert.AreEqual(Distinct.Solution(new[] { 1, 1, 3, 2, 3, 5 }), 4);
            Assert.AreEqual(Distinct.Solution(new[] { -100, -1000, 0, 2, 3, 5 }), 6);
            Assert.AreEqual(Distinct.Solution(new int[] { } ), 0);

        }
    }
}
