using MTCG.Entity;

namespace MTCG.Interface
{
    public interface IDatabase
    {
        public bool CreateUser(UserEntity userEntity);

        public bool UserExists(string username);

        public UserEntity GetUserByToken(string token);
    }
}