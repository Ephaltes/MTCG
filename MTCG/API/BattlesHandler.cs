using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
using Constant = MTCG.Model.Constant;

namespace MTCG.API
{
    public class BattlesHandler : DefaultRessourceHandler
    {
        public BattlesHandler(IRequestContext req, IDatabase database) : base(req, database)
        {
        }

        protected override ResponseContext HandlePost()
        {
            UserModell model = new UserModell(Database);

            RequestContext.HttpHeader.TryGetValue("Authorization", out string token);
            var authorization = ConvertToAuthorizationEntity(token);

            if (authorization == null || !model.VerifyToken(authorization.Value))
            {
                return NotAuthorized();
            }

            if (model.Deck.Count < Constant.MAXCARDSINDECK)
            {
                return CustomError("Not Enough Cards in Deck", StatusCodes.BadRequest);
            }

            GameModell gameModell = new GameModell(model);

            return SuccessObject(gameModell.GetLog(), StatusCodes.OK);

        }
    }
}