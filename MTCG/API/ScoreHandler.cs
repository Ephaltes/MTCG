using System;
using System.Collections.Generic;
using MTCG.Entity;
using MTCG.Helpers;
using MTCG.Interface;
using MTCG.Model;
using MTCG.Model.BaseClass;
using Newtonsoft.Json;
using WebServer;
using WebServer.API;
using WebServer.Interface;
using WebServer.RessourceHandler;

namespace MTCG.API
{
    public class ScoreHandler : DefaultRessourceHandler
    {

        public ScoreHandler(IRequestContext req, IDatabase database) : base(req, database)
        {
        }

        protected override ResponseContext HandleGet()
        {
            UserModell model = new UserModell(Database);

            RequestContext.HttpHeader.TryGetValue("Authorization", out string token);
            var authorization = ConvertToAuthorizationEntity(token);

            if (authorization == null || !model.VerifyToken(authorization.Value))
            {
                return NotAuthorized();
            }

            ScoreModell scoreModel = new ScoreModell(Database);

            var scoreBoard = scoreModel.ScoreBoard;

            if (scoreBoard == null)
                return CustomError("No User for ScoreBoard", StatusCodes.NotFound);
            
            return SuccessObject(scoreModel.ScoreBoard, StatusCodes.OK);
        }
    }
}