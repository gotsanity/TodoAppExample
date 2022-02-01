namespace TodoApp.Models
{
    public class Location
    {
        public int LocationID { get; set; }
        public string Name { get; set; }
        public List<Todo> Todos { get; set; }
    }
}
