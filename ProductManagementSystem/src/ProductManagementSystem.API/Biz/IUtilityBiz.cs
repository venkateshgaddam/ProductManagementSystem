using ProductManagement.Common.Models;

namespace ProductManagementSystem.API.Biz
{
    public interface IUtilityBiz
    {
        Task<List<CategoryInfo>> GetCategories();
    }
}
