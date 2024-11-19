using System.Collections.ObjectModel;

namespace SOTISProj.Repo
{
    public class Student
    {
        public int Id {  get; private set; }
        public string Name { get;  set; }
        public string Surname { get; set; }
        public ICollection<Subject> Subjects { get; set; }
        public Student() { }
        public bool Validate()
        {
            //TODO: implement model validation
            return true;
        }

    }
}
