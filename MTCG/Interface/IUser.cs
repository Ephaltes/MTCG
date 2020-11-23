using MTCG.Entity;
using MTCG.Model;

namespace MTCG.Interface
{
    public interface IUser
    {
        public UserEntity UserEntity { get; set; }
        public StackModell Stack { get; }
        public DeckModell Deck { get; }
        
        public bool VerifyToken(string token);

        public string CreateTokenForUser(string username, string password);
    }
}