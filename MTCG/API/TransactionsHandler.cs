using System;
using System.Linq;
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
    public class TransactionsHandler : DefaultRessourceHandler
    {

        public TransactionsHandler(IRequestContext req, IDatabase database) : base(req, database)
        {
        }
        protected override ResponseContext HandlePost()
        {
            ResponseContext responseContext = new ResponseContext();
            UserModell model = new UserModell(Database);

            if (RequestContext.HttpRequest[1].ToLower() != "packages")
                return CustomError("Wrong Parameter",StatusCodes.BadRequest);
                
            
            RequestContext.HttpHeader.TryGetValue("Authorization", out string token);
            var authorization = ConvertToAuthorizationEntity(token);

            if (token == null || !model.VerifyToken(authorization.Value))
            {
                return NotAuthorized();
            }

             if (model.UserEntity.Coins < 5)
                 return CustomError("Not enough coins", StatusCodes.BadRequest);

             var packages = Database.GetPackages().OrderBy(x=>x.PackageAmount).ToList();
             
             var cards = packages[0].Open(model.UserEntity);

             if (cards == null)
                 return CustomError("No Packages available", StatusCodes.NotFound);
             
             return SuccessObject(cards,StatusCodes.OK);
        }

    }
}