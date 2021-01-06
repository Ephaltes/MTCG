using MTCG.Model;

namespace MTCG.Entity
{
    public class ReportEntity
    {
        public GameEnd GameEnd { get; set; }
        public string Log { get; set; }
        public string Player1 { get; set; }
        public string Player2 { get; set; }

        public string Winner { get; set; }
    }
}