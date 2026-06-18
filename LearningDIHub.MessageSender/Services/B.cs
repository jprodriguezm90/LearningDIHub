using System;
using System.Collections.Generic;
using System.Text;

namespace LearningDIHub.Domain.Services
{
    public class B() : IDisposable
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public void Dispose()
        {
            Console.WriteLine($"B Is Disposed {Id}");
            Id = Guid.Empty;
        }

        public string Print()
        {
            return $"B: {Id}";
        }
    }
}
