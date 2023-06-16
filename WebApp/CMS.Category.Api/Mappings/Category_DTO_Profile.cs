using AutoMapper;
namespace CMS.Category.Api
{
    using CMS.DataModel;
    public class Category_DTO_Profile : Profile
    {
        public Category_DTO_Profile()
        {
            CreateMap<Category_DTO, Category>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name))
                .ForMember(
                    dest => dest.Description,
                    opt => opt.MapFrom(src => src.Description))
                .ForMember(
                    dest => dest.CreateDate,
                    opt => opt.AllowNull())
                .ForMember(
                    dest => dest.CreateBy,
                    opt => opt.AllowNull())
                .ForMember(
                    dest => dest.ModifiedDate,
                    opt => opt.AllowNull())
                .ForMember(
                    dest => dest.ModifiedBy,
                    opt => opt.AllowNull())
                .ReverseMap();
        }
    }
}
