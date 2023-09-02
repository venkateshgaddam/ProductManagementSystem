using ProductManagement.Common.Models;

namespace ProductManagementSystem.API.Biz
{
    public class UtilityBiz : IUtilityBiz
    {
        public async Task<List<CategoryInfo>> GetCategories()
        {
            List<CategoryInfo> resultSet = new()
            {
                //1. Electornics
                new CategoryInfo()
                {
                    Category = Category.Electronics.ToString(),
                    SubCategories = new List<string>(){ "TV","WashingMachine","Fridge","Speaker","Mobile" }
                },

                //2.Apparel
                new CategoryInfo()
                {
                    Category = Category.Apparel.ToString(),
                    SubCategories = new List<string>() { "Men's Clothing", "Women's Clothing", "Kids", "Accessories" }

                },

                //3.BabyCare
                new CategoryInfo()
                {
                    Category = Category.BabyCare.ToString(),
                    SubCategories = new List<string>() { "BabySoaps", "Toys", "Diapers", "ToyGifts", "Nursing" }
                },

                //4.Footwear
                new CategoryInfo()
                {
                    Category = Category.Footwear.ToString(),
                    SubCategories = new List<string>() { "Men's Footwear", "Women's Footwear", "Kids Footwear" }
                },
                


                //5.HomeFurniture
                new CategoryInfo()
                {
                    Category = Category.HomeFurniture.ToString(),
                    SubCategories = new List<string>(){"Decor","Chairs","Tables","SofaSet","Kitchen Furniture" },
                },

                //6.Books
                new CategoryInfo()
                {
                    Category = Category.Books.ToString(),
                    SubCategories = new List<string>(){ "ScienceFiction","TextBooks","KidsBooks","eBooks","Indian Language Books" }
                }
            };
            return await Task.FromResult(resultSet);
        }
    }
}
