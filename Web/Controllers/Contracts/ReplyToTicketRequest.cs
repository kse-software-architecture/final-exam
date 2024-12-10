namespace FinalExam.Controllers.Contracts;

public class ReplyToTicketRequest
{
    public string Message { get; init; }
    
    public int UserId { get; init; }
}