using SOTISProj.Repo;
using System.Net;

namespace SOTISProj.RepositoryInterfaces
{
    public interface IUserRepository
    {
        public User? Get(long id);
        public User? GetActiveByName(string username);
    }
}
