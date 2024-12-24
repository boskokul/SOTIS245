using SOTISProj.RepositoryInterfaces;

namespace SOTISProj.Repo
{
    public class UserRepository : IUserRepository
    {
        private MyDbContext _context;
        public UserRepository(MyDbContext myDbContext)
        {
            _context = myDbContext;
        }
        public User? Get(long id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public User? GetActiveByName(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }
    }
}
