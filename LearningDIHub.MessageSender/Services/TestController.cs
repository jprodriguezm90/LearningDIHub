using System;
using System.Collections.Generic;
using System.Text;

namespace LearningDIHub.MessageSender
{
    public class TestController(IMessageService a,IMessageService b) : IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine($"TestController Is Disposed");
        }

        public string Print()
        {
            return $"Message Service 1: {a.Id}, Message Service 2: {b.Id}";
        }
    }
}
