using CMS.Base;
using CMS.Category.DataConnect;
using CMS.Category.Repo;
using CMS.Category.Service;
using Microsoft.EntityFrameworkCore;

namespace CMS.Category.Api
{
    using CMS.DataModel;

    public static class CategoryDependencyInjectionExtension
    {
        public static IServiceCollection CategoryDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<DbContext, CategoryContext>();//to change to CategoryContext
            services.AddTransient<IBaseService<Category, Category_DTO>, CategoryService>();
            services.AddTransient<IUnitOfWork<Category>, UnitOfWork<Category>>();
            services.AddTransient<IBaseRepository<Category>, CategoryRepository>();

            return services;
        }
    }
}
