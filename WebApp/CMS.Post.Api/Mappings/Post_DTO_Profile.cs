using AutoMapper;
namespace CMS.Post.Api
{
    using CMS.DataModel;
    public class Post_DTO_Profile : Profile
    {
        public Post_DTO_Profile()
        {
            CreateMap<Post, Post_DTO>()
                .ReverseMap();
        }
    }
}
