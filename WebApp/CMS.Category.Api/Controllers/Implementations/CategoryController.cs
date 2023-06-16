using CMS.Base;
using CMS.Category.Service;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Category.Api
{
    using CMS.DataModel;

    public class CategoryController : BaseController<Category, Category_DTO>, ICategoryController
    {
        public CategoryController(IBaseService<Category, Category_DTO> iCategoryService) : base(iCategoryService) { }

        [HttpPost]
        [Route("Add")]
        public ActionResult<Category_DTO> Add([FromBody] Category_DTO category)
        {
            ICategoryService categoryService = this._service as ICategoryService;
            var result = categoryService.AddCustom(category);
            if (result == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(result);
        }

        [HttpDelete]
        [Route("Delete")]
        public ActionResult<Category_DTO> Delete([FromQuery] int id)
        {
            ICategoryService categoryService = this._service as ICategoryService;
            var result = categoryService.DeleteCustom(id);
            if (result == null)
            {
                return new ConflictObjectResult(result);
            }
            return new OkObjectResult(result);
        }

        [HttpGet]
        [Route("Get")]
        public ActionResult<Category_DTO> GetOne(int id)
        {
            ICategoryService categoryService = this._service as ICategoryService;
            var result = categoryService.FindById(id);
            if (result == null) {
                return new NotFoundResult();
            }
            return new OkObjectResult(result);
        }

        [HttpPut]
        [Route("Update")]
        public ActionResult<Category_DTO> Update([FromBody] Category_DTO category)
        {
            ICategoryService categoryService = this._service as ICategoryService;
            var result = categoryService.EditCustom(category);
            if(result == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(result);
        }

        [HttpGet]
        [Route("GetAll")]
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
