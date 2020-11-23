using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
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
    public class UsersHandler : BaseRessourceHandler
    {
        private IRequestContext _requestContext;
        private IDatabase _database;
        
        public UsersHandler(IRequestContext req,IDatabase database)
        {
            _requestContext = req;
            _database = database;
        }
        
        protected override ResponseContext HandleGet()
        {
            ResponseContext responseContext = new ResponseContext();
            if (_requestContext.HttpRequest.Count < 2)
            {
                responseContext.ResponseMessage.Add(new ResponseMessage()
                {
                    Status = StatusCodes.BadRequest,
                    ErrorMessage = "Missing Parameters"
                });
                return responseContext;
            }

           
            UserModell model = new UserModell(_database);
            var entity = model.GetUserByUsername(_requestContext.HttpRequest[1]);
            ResponseMessage msg = new ResponseMessage();

            if (entity == null)
            {
                msg.Status = StatusCodes.BadRequest;
                msg.ErrorMessage = "User not found";
                responseContext.StatusCode = StatusCodes.BadRequest;
            }
            else
            {
                msg.Status = StatusCodes.OK;
                msg.Object = entity;
                responseContext.StatusCode = StatusCodes.OK;
            }
           
            responseContext.ResponseMessage.Add(msg);
            return responseContext;
        }

        protected override ResponseContext HandlePost()
        {
           ResponseContext responseContext = new ResponseContext();
           if (String.IsNullOrWhiteSpace(_requestContext.HttpBody))
           {
               responseContext.ResponseMessage.Add(new ResponseMessage()
               {
                   Status = StatusCodes.BadRequest,
                   ErrorMessage = "Body is empty"
               });

               return responseContext;
           }

           var userEntity = JsonConvert.DeserializeObject<UserEntity>(_requestContext.HttpBody);
           
           UserModell model = new UserModell(_database);
           var token = model.CreateTokenForUser(userEntity.Username, userEntity.Password);
           ResponseMessage msg = new ResponseMessage();

           if (string.IsNullOrWhiteSpace(token))
           {
               msg.Status = StatusCodes.BadRequest;
               msg.ErrorMessage = "User already exists";
               responseContext.StatusCode = StatusCodes.BadRequest;
           }
           else
           {
               msg.Status = StatusCodes.Created;
               msg.Object = $"Token:{token}";
               responseContext.StatusCode = StatusCodes.Created;
           }
           
           responseContext.ResponseMessage.Add(msg);
           return responseContext;
        }

        protected override ResponseContext HandlePut()
        {
            throw new System.NotImplementedException();
        }

        protected override ResponseContext HandleDelete()
        {
            throw new System.NotImplementedException();
        }

        public override ResponseContext Handle()
        {
            ResponseContext responseContext;
            switch (_requestContext.HttpMethod)
            {
                case HttpMethods.GET:
                    responseContext = HandleGet();
                    break;
                case HttpMethods.POST:
                    responseContext = HandlePost();
                    break;
                case HttpMethods.PUT:
                    responseContext = HandlePut();
                    break;
                case HttpMethods.DELETE:
                    responseContext = HandleDelete();
                    break;
                default:
                    throw new NotImplementedException("HttpMethod not Implemented");
            }
            return responseContext;
        }
    }
}