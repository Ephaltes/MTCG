using System;
using MTCG.Entity;
using MTCG.Interface;
using MTCG.Model;
using Newtonsoft.Json;
using WebServer;
using WebServer.API;
using WebServer.Interface;
using WebServer.RessourceHandler;

namespace MTCG.API
{
    public class SessionsHandler : DefaultRessourceHandler
    {

        public SessionsHandler(IRequestContext req, IDatabase database) : base(req, database)
        {
            
        }
        protected override ResponseContext HandlePost()
        {
            ResponseContext responseContext = new ResponseContext();
            
            if (String.IsNullOrWhiteSpace(RequestContext.HttpBody))
            {
                return EmptyBody();
            }

            UserEntity userEntity = JsonConvert.DeserializeObject<UserEntity>(RequestContext.HttpBody);

            if (userEntity == null || string.IsNullOrEmpty(userEntity.Username) ||
                string.IsNullOrEmpty(userEntity.Password))
            {
                responseContext.ResponseMessage.Add(new ResponseMessage()
                {
                    Status = StatusCodes.BadRequest,
                    ErrorMessage = "Missing Password or Username"
                });
                responseContext.StatusCode = StatusCodes.BadRequest;
                return responseContext;
            }
            
            UserModell model = new UserModell(Database);
            model.UserEntity = userEntity;
            if (model.VerifyLogin())
            {
                responseContext.ResponseMessage.Add(new ResponseMessage()
                {
                  Object = model.UserEntity.Token,
                  Status = StatusCodes.OK
                });
                responseContext.StatusCode = StatusCodes.OK;
                return responseContext;
            }
            
            return SomeThingWrong();
        }
    }
}