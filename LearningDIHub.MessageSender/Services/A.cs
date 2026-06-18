using System;
using System.Collections.Generic;
using System.Text;

namespace LearningDIHub.Domain.Services
{
    public class A(B b) : IDisposable
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public void Dispose()
        {
            Console.WriteLine($"A Is Disposed {Id}");
            Id = Guid.Empty;
        }

        public string Print()
        {
            return $"A: {Id}, B: {b.Id}";
        }
    }
}
