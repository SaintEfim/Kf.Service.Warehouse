namespace Kf.Service.Warehouse.Domain.Models.Base.Kafka;

public class SendConfiguration
{
    public string Topic { get; set; } = string.Empty;

    public int MessageTimeoutMs { get; set; }

    public bool Disabled { get; set; }
}
