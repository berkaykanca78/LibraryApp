using LibraryApp.WebToken.Jwt.Models;

namespace LibraryApp.WebToken.Jwt.Abstract
{
    public interface IJwtHelper
    {
        JwtDto GetJwtDto();
        string GetValueFromToken(string propertyName);
    }
}
