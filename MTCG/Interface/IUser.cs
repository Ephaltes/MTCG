using MTCG.Entity;
using MTCG.Model;

namespace MTCG.Interface
{
    public interface IUser
    {
        public UserEntity UserEntity { get; set; }

        public bool VerifyToken(string token);

        public UserEntity GetUserByUsername(string username);

        public bool UpdateUser();

        public string CreateTokenForUser(string username, string password);

        public bool VerifyLogin();
    }
}