using CMS.Base;
using CMS.Post.DataConnect;
using CMS.Post.Service;
using Microsoft.EntityFrameworkCore;
using CMS.Post.Repo;

namespace CMS.Post.Api
{
    using CMS.DataModel;

    public static class PostDependencyInjectionExtension
    {
        public static IServiceCollection PostDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<DbContext, PostContext>();
            services.AddTransient<IBaseService<Post, Post_DTO>, PostService>();
            services.AddTransient(typeof(IUnitOfWork<Post>), typeof(UnitOfWork<Post>)) ; //?
            services.AddTransient<IBaseRepository<Post>, PostRepository>();

            return services;
        }
    }
}
