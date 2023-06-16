
using CMS.Base;
using CMS.Post.DataConnect;
using CMS.Post.Service;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace CMS.Post.Api
{
    using CMS.DataModel;
    using CMS.Post.Repo;

    public static class DependencyInjectionExtension
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
