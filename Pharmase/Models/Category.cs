namespace Pharmase.Models
{
    public class Category
    {
        // hello
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Medicine> Medicines { get; set; }
    }

}
