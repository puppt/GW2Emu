using System;
using GameRevision.GW2Emu.Common.Events;
using NUnit.Framework;

namespace GameRevision.GW2Emu.Tests.Common.Events
{
    [TestFixture()]
    public class EventAggregatorTest
    {

        [Test()]
        public void TestCase ()
        {
            // create the event aggregator and the objects
            // that contains the handler methods
            var aggr = new EventAggregator();

            var handlerObj1 = new TestHandlers();
            var handlerObj2 = new TestHandlers();

            aggr.Register(handlerObj1);
            aggr.Register(handlerObj2);

            // create some mock events and set their sample values
            var evt1 = new Event1(); evt1.Foo = 1337;
            var evt2 = new Event2(); evt2.Bar = "Hello";

            // trigger the events: 1st time
            aggr.Trigger(evt1);
            aggr.Trigger(evt2);

            Assert.IsTrue(handlerObj1.GotEvt1);
            Assert.AreEqual(handlerObj1.Evt1Data, 1337);

            Assert.IsTrue(handlerObj2.GotEvt1);
            Assert.AreEqual(handlerObj2.Evt1Data, 1337);

            Assert.IsTrue(handlerObj1.GotEvt2);
            Assert.AreEqual(handlerObj1.Evt2Data, "Hello");

            Assert.IsTrue(handlerObj2.GotEvt2);
            Assert.AreEqual(handlerObj2.Evt2Data, "Hello");

            // prepare the handlers for the next test
            handlerObj1.Reset();
            handlerObj2.Reset();

            evt1.Foo = 42;
            evt2.Bar = "Blubb";

            // trigger the events: 2st time
            aggr.Trigger(evt2);
            aggr.Trigger(evt1);


            Assert.IsTrue(handlerObj1.GotEvt1);
            Assert.AreEqual(handlerObj1.Evt1Data, 42);

            Assert.IsTrue(handlerObj2.GotEvt1);
            Assert.AreEqual(handlerObj2.Evt1Data, 42);

            Assert.IsTrue(handlerObj1.GotEvt2);
            Assert.AreEqual(handlerObj1.Evt2Data, "Blubb");

            Assert.IsTrue(handlerObj2.GotEvt2);
            Assert.AreEqual(handlerObj2.Evt2Data, "Blubb");
        }
    }


    public class TestHandlers : IRegisterable
    {
        public bool GotEvt1 = false;
        public int Evt1Data = -1;

        public bool GotEvt2 = false;
        public String Evt2Data = "";


        public void Reset()
        {
            GotEvt1 = false; Evt1Data = -1;
            GotEvt2 = false; Evt2Data = "";
        }


        public void RegisterMeWith(IEventAggregator aggregator)
        {
            aggregator.Register<Event1>(OnEvent1);
            aggregator.Register<Event2>(OnEvent2);
        }


        public void OnEvent1(Event1 evt)
        {
            GotEvt1 = true;
            Evt1Data = evt.Foo;
        }


        public void OnEvent2(Event2 evt)
        {
            GotEvt2 = true;
            Evt2Data = evt.Bar;
        }
    }


    public class Event1 : IEvent
    {
        public int Foo;
    }


    public class Event2 : IEvent
    {
        public String Bar;
    }
}

