using System.Collections.Generic;
using MTCG.Entity;
using MTCG.Interface;

namespace MTCG.Model
{
    public class ScoreModell
    {
        public ScoreModell(IDatabase database)
        {
            _database = database;
        }

        public List<ScoreEntity> ScoreBoard => LoadScoreBoard();

        private IDatabase _database { get; }

        protected List<ScoreEntity> LoadScoreBoard()
        {
            var userEntity = _database.LoadScoreBoard();

            if (userEntity == null)
                return null;
            
            var retList = new List<ScoreEntity>();

            foreach (var user in userEntity)
                retList.Add(new ScoreEntity
                {
                    DisplayName = user.DisplayName,
                    Draw = user.Draw,
                    Elo = user.Elo,
                    Lose = user.Lose,
                    Win = user.Win
                });

            if (retList.Count < 1)
                return null;

            return retList;
        }
    }
}