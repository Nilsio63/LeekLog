namespace LeekLog.Abstractions.Entites;

public class FeedbackEntity : BaseEntity
{
    public Guid UserId { get; set; }

    public DateOnly Date { get; set; }
    public double? BodyWeight { get; set; }
    public string Notes { get; set; } = string.Empty;
}
