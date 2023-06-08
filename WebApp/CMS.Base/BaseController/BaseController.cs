using Microsoft.AspNetCore.Mvc;
namespace CMS.Base
{
    public class BaseController<TEntity> where TEntity : class, new()
    {
        protected readonly IBaseService<TEntity> _service;

        public BaseController(IBaseService<TEntity> service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public IEnumerable<TEntity> GetAll()
        {
            return _service.GetAll();
        }
    }
}
