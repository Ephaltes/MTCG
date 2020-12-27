using MTCG.Interface;
using MTCG.Model;
using WebServer;
using WebServer.API;
using WebServer.Interface;

namespace MTCG.API
{
    public class ScoreHandler : DefaultRessourceHandler
    {
        public ScoreHandler(IRequestContext req, IDatabase database) : base(req, database)
        {
        }

        protected override ResponseContext HandleGet()
        {
            var model = new UserModell(Database);

            RequestContext.HttpHeader.TryGetValue("Authorization", out var token);
            var authorization = ConvertToAuthorizationEntity(token);

            if (authorization == null || !model.VerifyToken(authorization.Value)) return NotAuthorized();

            var scoreModel = new ScoreModell(Database);

            var scoreBoard = scoreModel.ScoreBoard;

            if (scoreBoard == null)
                return CustomError("No User for ScoreBoard", StatusCodes.NotFound);

            return SuccessObject(scoreModel.ScoreBoard, StatusCodes.OK);
        }
    }
}