using LearningDIHub.Domain.Models;

namespace LearningDIHub.DataSource
{
    public interface IMessageSource
    {
        Message GetMessage();
    }
}
