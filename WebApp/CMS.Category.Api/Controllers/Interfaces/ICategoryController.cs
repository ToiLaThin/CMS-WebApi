using CMS.DataModel;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Category.Api.Controllers
{
    public interface ICategoryController
    {
        ActionResult<IEnumerable<Category_DTO>> GetAll();

    }
}
