using System;
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
    public class PackagesHandler : DefaultRessourceHandler
    {

        public PackagesHandler(IRequestContext req, IDatabase database) : base(req, database)
        {
        }
        protected override ResponseContext HandlePost()
        {
            ResponseContext responseContext = new ResponseContext();
            UserModell model = new UserModell(Database);
            
            RequestContext.HttpHeader.TryGetValue("Authorization", out string token);
            var authorization = ConvertToAuthorizationEntity(token);

            if (token == null || !model.VerifyToken(authorization.Value))
            {
                return NotAuthorized();
            }
             
            if (String.IsNullOrWhiteSpace(RequestContext.HttpBody))
            {
                return EmptyBody();
            }

            var packageEntity = JsonConvert.DeserializeObject<PackageEntity>(RequestContext.HttpBody);

            if (packageEntity.CardsInPackage.Count < 1)
                return CardNotValid();
            
            foreach (var card in packageEntity.CardsInPackage)
            {
                if(string.IsNullOrEmpty(card.Id))
                    card.GenerateIdForCard();


                if ( (card.CardType == CardType.MonsterCard && card.Race == Race.Unknow)
                    || card.Damage <= 0)
                    return CardNotValid();
              
            }
            
            if (Database.AddPackage(packageEntity))
            {
                responseContext.ResponseMessage.Add(new ResponseMessage()
                {
                    Status = StatusCodes.Created,
                    Object = "Package Created successful"
                });
                responseContext.StatusCode = StatusCodes.Created;
            }
            else
            {
                responseContext = SomeThingWrong();
            }

            return responseContext;
        }

        private ResponseContext CardNotValid()
        {
            ResponseContext responseContext = new ResponseContext();
            responseContext.ResponseMessage.Add(new ResponseMessage()
            {
                Status = StatusCodes.BadRequest,
                ErrorMessage = "CardEntity is not valid"
            });
            responseContext.StatusCode = StatusCodes.BadRequest;
            return responseContext;
        }
        
    }
}