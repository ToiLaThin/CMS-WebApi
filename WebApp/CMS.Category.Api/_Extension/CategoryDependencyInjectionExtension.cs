using CMS.Base;
using CMS.Category.Repo;
using CMS.Category.Service;
using CMS.Post.DataConnect; //to change
using Microsoft.EntityFrameworkCore;

namespace CMS.Category.Api._Extension
{
    using CMS.DataModel;

    public static class CategoryDependencyInjectionExtension
    {
        public static IServiceCollection CategoryDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<DbContext, PostContext>();//to change to CategoryContext
            services.AddTransient<IBaseService<Category, Category_DTO>, CategoryService>();
            services.AddTransient<IUnitOfWork<Category>, UnitOfWork<Category>>();
            services.AddTransient<IBaseRepository<Category>, CategoryRepository>();

            return services;
        }
    }
}
