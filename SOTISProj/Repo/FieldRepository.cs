using SOTISProj.RepositoryInterfaces;

namespace SOTISProj.Repo
{
    public class FieldRepository : IFieldRepository
    {
        private MyDbContext _dbContext;
        public FieldRepository(MyDbContext context) {
            _dbContext = context;
        }
        public Field getByRootTerm(string term)
        {
            return _dbContext.Fields.FirstOrDefault(f => f.RootTerm == term);
        }
    }
}
