namespace ProductManagement.Common.Models
{
    public enum Category
    {
        Electronics = 1,
        Mobiles,
        Apparel,
        Footwear,
        HomeFurniture,
        BabyCare,
        Books
    }

    public class SubCategories
    {
        public Category Category { get; set; }
        public int SubCategoryId { get; set; }
        public string Name { get; set; }
    }
}
