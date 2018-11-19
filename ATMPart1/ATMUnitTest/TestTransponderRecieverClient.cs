﻿using System;
using System.Collections.Generic;
using System.Globalization;
using ATMPart1;
using Castle.DynamicProxy.Generators;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace ATMUnitTest
{
    class TestTransponderRecieverClient
    {
        private ITransponderReceiver _fakeTransponderReceiver;
        private ITrackFormatter _formatter;
        private ITrackManager _tm;
        private TransponderRecieverClient _uut;

        private int _eventsRecieved;

        [SetUp]
        public void Setup()
        {
            _eventsRecieved = 0;

            _fakeTransponderReceiver = Substitute.For<ITransponderReceiver>();
            _formatter = Substitute.For<ITrackFormatter>();
            _tm = Substitute.For<ITrackManager>();

            _uut = new TransponderRecieverClient(_fakeTransponderReceiver, _formatter, _tm);
            _fakeTransponderReceiver.TransponderDataReady += (sender, args) => _eventsRecieved++;
        }

        //really not sure how to test this, as this just tests that the event is sent.
        [Test]
        public void TestReception_LegalValues_RecievesData()
        {
            List<string> testData = new List<string>();
            testData.Add("ATR423;39045;12932;14000;20151006213456789");
            testData.Add("BCD123;10005;85890;12000;20151006213456789");
            testData.Add("XYZ987;25059;75654;4000;20151006213456789");

            _fakeTransponderReceiver.TransponderDataReady +=
                Raise.EventWith(this, new RawTransponderDataEventArgs(testData));

            Assert.That(_eventsRecieved, Is.EqualTo(1));
        }

        [Test]
        public void TestReception_LegalValues_FormatterCalled()
        {
            List<string> testData = new List<string>();
            testData.Add("ATR423;39045;12932;14000;20151006213456789");
            testData.Add("BCD123;10005;85890;12000;20151006213456789");
            testData.Add("XYZ987;25059;75654;4000;20151006213456789");

            _fakeTransponderReceiver.TransponderDataReady +=
                Raise.EventWith(this, new RawTransponderDataEventArgs(testData));


            //Assert.That(_formatter.Received().RecieveTrack(Arg.Any<string>());
        }
    }
}