using AutoMapper;
using CMS.Base;
using CMS.Category.Repo;
using CMS.Helper;
using Microsoft.EntityFrameworkCore;
namespace CMS.Category.Service
{
    using CMS.DataModel;

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
            var category = this._mapper.Map<Category>(categoryApi);
            int? categoryId = categoryApi.Id;
            Category categoryFinded = this._categoryRepository.Get<int?>(categoryId); //TO FIX
            if(categoryFinded == null) {
                return null;
            } 
            else
            {
                CategoryBuilder categoryBuilder = new CategoryBuilder(); //not have id since this is add
                Category categoryToAdd = categoryBuilder.SetCategoryWithOutId(category).Build();
                try {
                    this._categoryRepository.Add(categoryToAdd);
                    this.UnitOfWork.SaveChanges();
                }
                catch (Exception ex) {
                    throw new Exception(ex.Message);
                }
                return categoryApi;
            }
        }

        public Category_DTO? DeleteCustom(int id)
        {
            Category? categoryToFind = this._categoryRepository.Get<int>(id);
            if (categoryToFind != null)
            {
                this._categoryRepository.Remove(categoryToFind);
                try {
                    this.UnitOfWork.SaveChanges();
                    var result = this._mapper.Map<Category_DTO>(categoryToFind);
                    return result;
                }
                catch (Exception ex) {
                    throw new Exception(ex.Message);
                }
            }
            return null;
        }

        public Category_DTO? EditCustom(Category_DTO categoryApi)
        {
            Category? category = this._mapper.Map<Category>(categoryApi);
            int? categoryId = categoryApi.Id;
            Category? categoryFinded = this._categoryRepository.Get<int?>(categoryId);
            if (categoryFinded == null) {
                return null;
            }
            else {
                CategoryBuilder categoryBuilder = new CategoryBuilder(categoryFinded); //have id
                Category categoryToUpdate = categoryBuilder.SetCategoryWithOutId(category).Build();
                try
                {
                    this._categoryRepository.Update(categoryToUpdate);
                    this.UnitOfWork.SaveChanges();
                    return categoryApi;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public IEnumerable<Category_DTO> GetAllCustom()
        {
            var allCategories = base.GetAll();
            return allCategories;
        }

        public Category_DTO? GetCustom(int id)
        {
            Category? categoryToFind = this._categoryRepository.Get<int>(id);
            Category_DTO? categoryApiToRet = categoryToFind == null ? this._mapper.Map<Category_DTO>(categoryToFind) : null;
            return categoryApiToRet ?? null;
        }
    }
}
