using System.Collections.ObjectModel;

namespace SOTISProj.Repo
{
    public class Field // OBLAST al
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RootTerm { get; set; }
        public Field() { }
        public bool Validate()
        {
            //TODO: implement model validation
            return true;
        }
    }

}
