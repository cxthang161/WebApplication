using Dapper;
using Microsoft.Data.SqlClient;
using WebApplication.Models.Entities;
using WebApplication.Providers;

namespace WebApplication.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        private readonly string? _dbContext;
        private readonly TokenProvider _tokenProvider;

        public UserRepository(IConfiguration configuration, TokenProvider tokenProvider)
        {
            _dbContext = configuration.GetConnectionString("DefaultCollection");
            _tokenProvider = tokenProvider;
        }
        public async Task<string?> AuthLogin(Users user)
        {
            using var connection = new SqlConnection(_dbContext);
            string sql = "SELECT * FROM Users WHERE UserName = @UserName AND Password = @Password";

            var foundUser = await connection.QueryFirstOrDefaultAsync<Users>(sql, new
            {
                UserName = user.UserName,
                Password = user.Password
            });

            if (foundUser == null)
            {
                return null;
            }

            return _tokenProvider.CreateToken(foundUser);
        }
    }
}
