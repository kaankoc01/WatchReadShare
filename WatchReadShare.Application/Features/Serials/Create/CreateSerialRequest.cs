namespace WatchReadShare.Application.Features.Serials.Create;

public record CreateSerialRequest(string Name, string Description, int CategoryId, int GenreId);
