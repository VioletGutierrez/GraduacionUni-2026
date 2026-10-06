using GraduacionUni.Api.Domain.Entities;

namespace GraduacionUni.Api.Application.Services;

public interface ITokenService
{
    string CreateToken(User user);
}