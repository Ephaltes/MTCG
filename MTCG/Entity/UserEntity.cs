using Serilog.Configuration;

namespace MTCG.Entity
{
    public class UserEntity
    {
        public int Coins;
        public string Description;
        public string DisplayName;
        public int Draw;
        public double Elo;
        public int Id;
        public string Image;
        public int Lose;
        public string Password;
        public string Salt;
        public string Token;
        public string Username;
        public int Win;
        public double WRatio
        {
            get
            {
                var sum = Win + (double) Lose + Draw;
                return sum == 0 ? 0 : Win / sum;
            }
        }
    }
}