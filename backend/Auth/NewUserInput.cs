using HotChocolate;

namespace backend.Auth
{
    public record UserRegisterInput (
        [GraphQLNonNullType] string Username,
        [GraphQLNonNullType] string Email,
        [GraphQLNonNullType] string Password
    );
    public record UserLoginInput (
        [GraphQLNonNullType] string Email,
        [GraphQLNonNullType] string Password
    );
}