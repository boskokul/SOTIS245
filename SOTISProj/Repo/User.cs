namespace SOTISProj.Repo
{
    public class User
    {
        public int Id { get; set; } 
        public string Username { get; private set; }
        public string Password { get; set; }
        public UserRole Role { get; private set; }

        public User(string username, string password, UserRole role)
        {
            Username = username;
            Password = password;
            Role = role;
            
        }
        public string GetPrimaryRoleName()
        {
            return Role.ToString().ToLower();
        }
    }

    public enum UserRole
    {
        Admin,
        Student
    }
}
