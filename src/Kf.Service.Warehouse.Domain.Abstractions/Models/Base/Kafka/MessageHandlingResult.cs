namespace Kf.Service.Warehouse.Domain.Models.Base.Kafka;

public enum MessageHandlingResult
{
    Succeeded,
    FailedRequeue,
    FailedSentToDeadLetterQueue,
    Dropped
}
