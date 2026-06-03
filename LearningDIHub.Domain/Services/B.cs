using System;
using System.Collections.Generic;
using System.Text;

namespace LearningDIHub.Domain.Services
{
    public class B()
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Print()
        {
            return $"B: {Id}";
        }
    }
}
