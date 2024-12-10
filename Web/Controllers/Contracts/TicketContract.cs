namespace FinalExam.Controllers.Contracts;

public class TicketContract
{
    public string TicketId { get; init; }
    
    public List<TicketMessage> Dialog { get; init; }
}

public class TicketMessage
{
    public bool FromUser { get; init; }
    
    public string Content { get; init; }
}