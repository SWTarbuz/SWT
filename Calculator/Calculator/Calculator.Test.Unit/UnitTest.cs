﻿using System;
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
        public void Add_Integers_Test2()
        {
            Assert.AreEqual(110, uut.Add(105, 5));

        }

        [Test]
        public void Add_Decimal_Test()
        {
            Assert.AreEqual(10, uut.Add(5.5, 4.5));

        }

        [Test]
        public void Add_Decimal_Test2()
        {
            Assert.AreEqual(20, uut.Add(10.5, 9.5));

        }

        [Test]
        public void Add_Negative_Test()
        {
            Assert.AreEqual(-10, uut.Add(-5, -5));

        }

        [Test]
        public void Multiply_Integers_Test()
        {
            Assert.AreEqual(8, uut.Multiply(4, 2));

        }

        [Test]
        public void Multiply_Decimal_Test()
        {
            Assert.AreEqual(10.5, uut.Multiply(3.5, 3));

        }

        [Test]
        public void Multiply_Negative_Test()
        {
            Assert.AreEqual(4, uut.Multiply(-2, -2));

        }
        [Test]
        public void Multiply_Negative_Test2()
        {
            Assert.AreEqual(16, uut.Multiply(-4, -4));

        }

        [Test]
        public void Subtract_Integers_Test()
        {
            Assert.AreEqual(23, uut.Subtract(40, 17));

        }

        [Test]
        public void Subtract_Decimal_Test()
        {
            Assert.AreEqual(23, uut.Subtract(40.5, 17.5));

        }

        [Test]
        public void Subtract_Negative_Test()
        {
            Assert.AreEqual(0, uut.Subtract(-20, -20));

        }

        [Test]
        public void Power_Integers_Test()
        {
            Assert.AreEqual(256, uut.Power(2, 8));

        }

        [Test]
        public void Power_Decimal_Test()
        {
            Assert.AreEqual(39.06, Math.Round(uut.Power(2.5, 4),2));

        }

        [Test]
        public void Power_Negative_Test()
        {
            Assert.AreEqual(0.06, Math.Round(uut.Power(-4, -2), 2));

        }

        [Test]
        public void log_Integers_Test()
        {
            Assert.AreEqual(1, uut.LogBase10(10));

        }

        [Test]
        public void log_Decimal_Test()
        {
            Assert.AreEqual(1.02, Math.Round(uut.LogBase10(10.5),2));

        }

        [Test]
        public void log_Negative_Test()
        {
            Assert.AreEqual(Double.NaN, uut.LogBase10(-2));

        }

        [Test]
        public void Divide_Integer_Test()
        {
            Assert.AreEqual(10,uut.Divide(100,10));

        }

        [Test]
        public void Divide_Decimal_Test()
        {
            Assert.AreEqual(10.3, uut.Divide(20.6,2));

        }

        [Test]
        public void Divide_Negative_Test()
        {
            Assert.AreEqual(-2, uut.Divide(-10, 5));
        }
    }
}
