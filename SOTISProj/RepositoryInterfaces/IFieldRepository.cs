using SOTISProj.Repo;
namespace SOTISProj.RepositoryInterfaces
{
    public interface IFieldRepository
    {
        public Field getByRootTerm(string term);
    }
}
