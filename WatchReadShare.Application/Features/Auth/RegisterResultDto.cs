namespace WatchReadShare.Application.Features.Auth
{
    public class RegisterResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string? Token { get; set; }
    }
}
