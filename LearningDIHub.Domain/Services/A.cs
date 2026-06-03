using System;
using System.Collections.Generic;
using System.Text;

namespace LearningDIHub.Domain.Services
{
    public class A(B b)
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Print()
        {
            return $"A: {Id}, B: {b.Id}";
        }
    }
}
