using CMS.Base;

namespace CMS.Category.Service
{
    using CMS.DataModel;
    public interface ICategoryService: IBaseService<Category, Category_DTO>
    {
        IEnumerable<Category_DTO> GetAllCustom();

        Category_DTO AddCustom(Category_DTO categoryApi);

        Category_DTO? GetCustom(int id);

        Category_DTO? DeleteCustom(int id);

        Category_DTO? EditCustom(Category_DTO categoryApi);
    }
}
