namespace ITI.Entities.Views;
public class StudentWithAddress
{
    public int StudentId { get; set; }
    public string? FirstName { get; set; }
    public string? Address { get; set; }

    public override string ToString()
        => $"[{StudentId}] {FirstName} {Address}";
}
