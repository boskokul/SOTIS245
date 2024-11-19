using System.Collections.ObjectModel;

namespace SOTISProj.Repo
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Student> Students { get; set;}
        public ICollection<Professor> Professors { get; set; }
        public Subject() { }
        public bool Validate()
        {
            //TODO: implement model validation
            return true;
        }
    }

}
