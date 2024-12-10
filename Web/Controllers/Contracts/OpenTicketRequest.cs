namespace FinalExam.Controllers.Contracts;

public class OpenTicketRequest
{
    public string Question { get; init; }
    
    public int UserId { get; init; }
}