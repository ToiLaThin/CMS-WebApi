using CMS.DataModel;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Category.Api
{
    public interface ICategoryController
    {
        ActionResult<IEnumerable<Category_DTO>> GetAll();

        ActionResult<Category_DTO> GetOne(int id);

        ActionResult<Category_DTO> Add(Category_DTO category);

        ActionResult<Category_DTO> Delete(int id);

        ActionResult<Category_DTO> Update(Category_DTO category);


    }
}
