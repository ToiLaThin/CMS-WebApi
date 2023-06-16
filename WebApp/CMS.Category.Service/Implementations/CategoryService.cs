using CMS.Base;
using CMS.Category.Repo;
namespace CMS.Category.Service
{
    using AutoMapper;
    using CMS.DataModel;
    using System.Collections.Generic;

    public class CategoryService : BaseService<Category, Category_DTO>, ICategoryService
    {
        private ICategoryRepository _categoryRepository;
        public CategoryService(IUnitOfWork<Category> iUnitOfWork, IMapper iMapper) : base(iUnitOfWork,iMapper) { 
            if (_unitOfWork.EntityRepository<CategoryRepository>() is IBaseRepository<Category> categoryRepository)
            {
                this._categoryRepository = (ICategoryRepository)categoryRepository;
            }
        }

        public Category_DTO AddCustom(Category_DTO categoryApi)
        {
            throw new NotImplementedException();
        }

        public Category_DTO? DeleteCustom(int id)
        {
            throw new NotImplementedException();
        }

        public Category_DTO? EditCustom(Category_DTO categoryApi)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category_DTO> GetAllCustom()
        {
            var allCategories = base.GetAll();
            return allCategories;
        }

        public Category_DTO? GetCustom(int id)
        {
            throw new NotImplementedException();
        }
    }
}
