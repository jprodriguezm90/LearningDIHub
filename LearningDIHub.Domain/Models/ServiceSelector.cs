using System;
using System.Collections.Generic;
using System.Text;

namespace LearningDIHub.Domain.Models
{
    public sealed class ServiceSelector
    {
        public const string SectionName = "MessageService";
        public int SelectedService { get; set; }
    }
}
