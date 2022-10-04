using BlogApp.Api.Entities;

namespace BlogApp.Api.Inerfaces.Managers
{
    public interface IAuthManager
    {
        public string GenerateToken(User user);
    }
}
