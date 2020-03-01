namespace APP.Domain.Entities
{
    public class Todo : DeletedEntity
    {
        public string Title { get; set; }
        public bool? IsDone { get; set; }
    }
}
