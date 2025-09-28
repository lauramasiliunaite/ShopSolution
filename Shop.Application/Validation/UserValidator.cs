using Shop.Application.DTOs;

namespace Shop.Application.Validation;

public static class UserValidator
{
    public static void Validate(UserDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Email))
            throw new ArgumentException("Email is required.");

        if (string.IsNullOrWhiteSpace(dto.Password))
            throw new ArgumentException("Password is required.");

        if (dto.Password.Length < 6)
            throw new ArgumentException("Password must be at least 6 characters long.");
    }
}
