using System;
using System.Collections.Generic;
using System.Text;

namespace LearningDIHub.Domain.DataSource
{
    public sealed class HttpSourceOptions
    {
        public const string SectionName = "MessageSource";
        public string URI { get; set; }
    }
}
