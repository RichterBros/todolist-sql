namespace ToDoList.Models
{
  public class CategoryItem
    {       
        public int CategoryItemId { get; set; } // primary key
        public int ItemId { get; set; } // foreign key
        public int CategoryId { get; set; } // foreign key
        public Item Item { get; set; }
        public Category Category { get; set; }
    }
}