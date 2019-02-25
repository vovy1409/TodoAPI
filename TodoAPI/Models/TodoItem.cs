using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Models
{
    [Table("Todos")]
    public class TodoItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
