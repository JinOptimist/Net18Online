namespace Net18Online.Models.Abstractions;
public interface INotifier
{
    public void Inform(string message);
    
    public void Assist(string message);
    
    public void Compliment(string message);
    
    public void Critical(string message);

    public void Major(string message);
}
