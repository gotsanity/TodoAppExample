namespace TodoApp.Models
{
    public class Todo
    {
        public int TodoID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; }
        public DateTime CreatedOn { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
    }
}
