using System;
using FibonacciExchangeCommon.Abstract;
using FibonacciExchangeCommon.Async;
using FibonacciExchangeCommon.Calculator;
using FibonacciExchangeCommon.Concrete;
using FibonacciExchangeCommon.Model;
using Moq;
using NUnit.Framework;

namespace FibonacciExchange.Tests
{
    [TestFixture]
    internal class TaskManagerTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidElementInput()
        {
            var calc = new Mock<IFiboCaculator>();
            calc.Setup(x => x.CalcNext(It.IsAny<byte[]>())).Returns((byte[] b) => b);
            var log = new Mock<ILog>();
            var abw = new Mock<IAsyncBackGroundWorker>();
            var sender = new Mock<IResultSender>();
            var tm = new TaskManager(calc.Object, sender.Object, abw.Object, log.Object);
            tm.Process(new ProcessingElement());
        }


        [Test]
        public void ValidElementInput()
        {
            var isSended= false;

            var calc = new Mock<IFiboCaculator>();
            calc.Setup(x => x.CalcNext(It.IsAny<byte[]>())).Returns((byte[] b) => b);
            var log = new Mock<ILog>();
            var abw = new Mock<IAsyncBackGroundWorker>();
            abw.Setup(x => x.AddItemToQueue(It.IsAny<Action>(), It.IsAny<Action<Exception>>()))
                .Callback((Action a, Action<Exception> err) => a()).Verifiable();
            var sender = new Mock<IResultSender>();
            sender.Setup(x => x.Send(It.IsAny<ProcessingElement>()))
                .Callback((ProcessingElement element) => { isSended = true; }).Verifiable();
            var tm = new TaskManager(calc.Object, sender.Object, abw.Object, log.Object);
            tm.Process(new ProcessingElement() { TaskId = Guid.NewGuid(), Data = new[] { (byte)1 } });

            Assert.True(isSended);
        }
    }
}
