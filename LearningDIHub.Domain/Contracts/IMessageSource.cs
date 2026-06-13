using LearningDIHub.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningDIHub.Domain.Contracts
{
    public interface IMessageSource
    {
        Message GetMessage();
    }
}
