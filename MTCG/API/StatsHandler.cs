using MTCG.Interface;
using MTCG.Model;
using WebServer;
using WebServer.API;
using WebServer.Interface;

namespace MTCG.API
{
    public class StatsHandler : DefaultRessourceHandler
    {
        public StatsHandler(IRequestContext req, IDatabase database) : base(req, database)
        {
        }

        protected override ResponseContext HandleGet()
        {
            var model = new UserModell(Database);

            RequestContext.HttpHeader.TryGetValue("Authorization", out var token);
            var authorization = ConvertToAuthorizationEntity(token);

            if (authorization == null || !model.VerifyToken(authorization.Value)) return NotAuthorized();

            return SuccessObject(new
            {
                model.UserEntity.Username,
                model.UserEntity.DisplayName,
                model.UserEntity.Elo,
                model.UserEntity.Win,
                model.UserEntity.Lose,
                model.UserEntity.Draw,
                model.UserEntity.WRatio
            }, StatusCodes.OK);
        }
    }
}