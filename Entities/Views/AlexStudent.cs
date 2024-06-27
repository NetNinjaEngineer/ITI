namespace ITI.Entities.Views
{
    public record AlexStudentView(int Id, string? Name, string? Address)
    {
        public override string? ToString()
            => $"[{Id}] {Name} {Address}";
    }
}
