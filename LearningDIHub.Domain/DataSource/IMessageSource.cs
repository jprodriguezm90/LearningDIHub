using LearningDIHub.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningDIHub.Domain.DataSource
{
    public interface IMessageSource
    {
        Message GetMessage();
    }
}
