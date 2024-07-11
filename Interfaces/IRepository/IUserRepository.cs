using HNGSTAGETWO.Models;

namespace HNGSTAGETWO.Interfaces.IRepository
{
    public interface IUserRepository:IBaseRepository<User>
    {
        Task<User> LoginAsync(string email, string password);
        Task<User> GetUserById(string id);
    }
}
