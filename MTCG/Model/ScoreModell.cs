using System.Collections.Generic;
using MTCG.Entity;
using MTCG.Interface;

namespace MTCG.Model
{
    public class ScoreModell
    {
        public List<ScoreEntity> ScoreBoard { get => LoadScoreBoard(); }

        private IDatabase _database { get; set; }
        
        public ScoreModell(IDatabase database)
        {
            _database = database;
        }

        protected List<ScoreEntity> LoadScoreBoard()
        {
            var userEntity = _database.LoadScoreBoard();

            List<ScoreEntity> retList = new List<ScoreEntity>();

            foreach (var user in userEntity)
            {
                retList.Add(new ScoreEntity()
                {
                    DisplayName = user.DisplayName,
                    Draw = user.Draw,
                    Elo = user.Elo,
                    Lose = user.Lose,
                    Win = user.Win
                });
            }

            if (retList.Count < 1)
                return null;

            return retList;
        }
    }
}