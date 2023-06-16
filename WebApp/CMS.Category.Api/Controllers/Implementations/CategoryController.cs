using CMS.Base;
using CMS.Category.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CMS.Category.Api.Controllers
{
    using CMS.DataModel;

    public class CategoryController : BaseController<Category, Category_DTO>, ICategoryController
    {
        public CategoryController(IBaseService<Category, Category_DTO> iCategoryService) : base(iCategoryService) { }

        ActionResult<IEnumerable<Category_DTO>> ICategoryController.GetAll()
        {
            ICategoryService categoryService = this._service as ICategoryService;
            var result = categoryService.GetAllCustom();
            if (result == null) {
                return new NotFoundResult();
            }
            else
            {
                return new OkObjectResult(result);
            }
        }
    }
}
