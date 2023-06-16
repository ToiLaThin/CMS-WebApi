using Microsoft.AspNetCore.Mvc;
namespace CMS.Base
{
    public class BaseController<TEntity, TApiModel> where TEntity : class, new() where TApiModel : class
    {
        protected readonly IBaseService<TEntity, TApiModel> _service;

        public BaseController(IBaseService<TEntity, TApiModel> service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public IEnumerable<TApiModel> GetAll()
        {
            return _service.GetAll();
        }
    }
}
