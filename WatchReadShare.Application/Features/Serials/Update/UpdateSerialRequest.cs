namespace WatchReadShare.Application.Features.Serials.Update;

public record UpdateSerialRequest(int Id, string Name, string Description, int CategoryId, int GenreId);


