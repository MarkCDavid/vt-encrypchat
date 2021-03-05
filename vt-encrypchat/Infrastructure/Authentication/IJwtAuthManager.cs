using System;
using System.Collections.Immutable;
using System.Security.Claims;

namespace vt_encrypchat.Infrastructure.Authentication
{
    public interface IJwtAuthManager
    {
        IImmutableDictionary<string, RefreshToken> UsersRefreshTokensReadOnlyDictionary { get; }
        JwtAuthResult GenerateTokens(string username, Claim[] claims, DateTime now);
    }
}