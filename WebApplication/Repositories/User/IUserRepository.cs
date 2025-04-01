using WebApplication.Models.Entities;

namespace WebApplication.Repositories
{
    public interface IUserRepository
    {
        Task<string?> AuthLogin(Users user);
    }
}
