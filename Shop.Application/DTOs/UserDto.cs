namespace Shop.Application.DTOs
{
    public record UserDto
    {
        public string Email { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
    }
}
