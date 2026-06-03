using System;
using System.Collections.Generic;
using System.Text;

namespace LearningDIHub.Domain.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Body { get; set; }
    }
    
}
