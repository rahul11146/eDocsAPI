using eDocsAPI.Models;

namespace eDocsAPI.Interface
{
    public interface IUser
    {
        Task<IList<Users?>> Get(string IsActive);
        Task<Users?> Find(string id);
        Task<Users?> Authenticate(string username,string password);
        Task Add(Users model);
        Task Update(Users model);
        Task<Users> Remove(Users model);
    }
}
