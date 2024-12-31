namespace WatchReadShare.Application.Features.Comments.Dto
{
    public record CommentDto(int Id, string Content, string Username, string AssociatedEntity)
    {
    }
}
