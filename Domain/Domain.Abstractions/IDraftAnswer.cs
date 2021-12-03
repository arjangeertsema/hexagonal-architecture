namespace Domain.Abstractions;

public interface IDraftAnswer
{    
    void Accept(string acceptedBy);
    void Reject(string rejection, string rejectedBy);    
    void Modify(string answer, string modifiedBy);    
    Message Send();
}
