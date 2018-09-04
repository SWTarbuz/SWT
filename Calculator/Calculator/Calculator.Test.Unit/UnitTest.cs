using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Calculator.Test.Unit
{
    public class UnitTest
    {
        private Calculator uut;

        [SetUp]
        public void Setup()
        {
            uut = new Calculator();
        }

        [Test]
        public void Add_Integers_Test()
        {
            Assert.AreEqual(40, uut.Add(23, 17));

        }

        [Test]
        public void Add_Deci_Test()
        {
            Assert.AreEqual(10, uut.Add(5.5, 4.5));

        }

        [Test]
        public void Multiply_Integers_Test()
        {
            Assert.AreEqual(8, uut.Multiply(4, 2));

        }

        [Test]
        public void Multiply_deci_Test()
        {
            Assert.AreEqual(10.5, uut.Multiply(3.5, 3));

        }

        [Test]
        public void Subtract_Integers_Test()
        {
            Assert.AreEqual(23, uut.Subtract(40, 17));

        }

        [Test]
        public void Power_Integers_Test()
        {
            Assert.AreEqual(256, uut.Power(2, 8));

        }

        [Test]
        public void Power_Integers2_Test()
        {
            Assert.AreEqual(16, uut.Power(2, 4));

        }
    }
}
