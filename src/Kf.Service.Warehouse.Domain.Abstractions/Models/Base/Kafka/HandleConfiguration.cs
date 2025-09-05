namespace Kf.Service.Warehouse.Domain.Models.Base.Kafka;

public class HandleConfiguration
{
    public string Topic { get; set; } = string.Empty;

    public int MessageTimeoutMs { get; set; }

    public string GroupId { get; set; } = string.Empty;

    public bool Disabled { get; set; }
}
