namespace WatchReadShare.Application.Features.Comments.Create;


public record CreateCommentRequest(string Content, int UserId, int? MovieId, int? SerialId);
  
