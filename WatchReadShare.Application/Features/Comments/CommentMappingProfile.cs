using AutoMapper;
using WatchReadShare.Application.Features.Comments.Create;
using WatchReadShare.Application.Features.Comments.Dto;
using WatchReadShare.Application.Features.Comments.Update;
using WatchReadShare.Domain.Entities;

namespace WatchReadShare.Application.Features.Comments
{
    public class CommentMappingProfile : Profile
    {
        public CommentMappingProfile()
        {
            CreateMap<Comment, CommentDto>().ReverseMap();
            CreateMap<CreateCommentRequest, Comment>()
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content.ToLowerInvariant()));
            CreateMap<UpdateCommentRequest, Comment>()
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content.ToLowerInvariant()));

        }
    }
}
