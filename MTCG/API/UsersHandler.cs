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
using MTCG.Helpers;

namespace MTCG.API
{
    public class UsersHandler : DefaultRessourceHandler
    {
        public UsersHandler(IRequestContext req, IDatabase database) : base(req, database)
        {
        }

        protected override ResponseContext HandleGet()
        {
            ResponseContext responseContext = new ResponseContext();
            UserModell model = new UserModell(Database);

            if (RequestContext.HttpRequest.Count < 2)
            {
                responseContext.ResponseMessage.Add(new ResponseMessage()
                {
                    Status = StatusCodes.BadRequest,
                    ErrorMessage = "Missing Parameters"
                });
                responseContext.StatusCode = StatusCodes.BadRequest;
                return responseContext;
            }

            string user = RequestContext.HttpRequest[1];

            RequestContext.HttpHeader.TryGetValue("Authorization", out string token);
            var authorization = ConvertToAuthorizationEntity(token);

            if (authorization == null || !model.VerifyToken(authorization.Value) || string.IsNullOrEmpty(user) ||
                user != model.UserEntity.Username)
            {
                return NotAuthorized();
            }

            return SuccessObject(new
            {
                model.UserEntity.DisplayName,
                model.UserEntity.Username,
                model.UserEntity.Description,
                model.UserEntity.Image,
            }, StatusCodes.OK);
        }

        protected override ResponseContext HandlePost()
        {
            ResponseContext responseContext = new ResponseContext();
            if (String.IsNullOrWhiteSpace(RequestContext.HttpBody))
            {
                return EmptyBody();
            }

            var userEntity = JsonConvert.DeserializeObject<UserEntity>(RequestContext.HttpBody);

            UserModell model = new UserModell(Database);
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
            ResponseContext responseContext = new ResponseContext();
            UserModell model = new UserModell(Database);
            if (RequestContext.HttpRequest.Count < 2)
            {
                responseContext.ResponseMessage.Add(new ResponseMessage()
                {
                    Status = StatusCodes.BadRequest,
                    ErrorMessage = "Missing Parameters"
                });
                responseContext.StatusCode = StatusCodes.BadRequest;
                return responseContext;
            }

            string user = RequestContext.HttpRequest[1];

            RequestContext.HttpHeader.TryGetValue("Authorization", out string token);
            var authorization = ConvertToAuthorizationEntity(token);

            if (authorization == null || !model.VerifyToken(authorization.Value) || string.IsNullOrEmpty(user) ||
                user != model.UserEntity.Username)
            {
                return NotAuthorized();
            }

            if (String.IsNullOrWhiteSpace(RequestContext.HttpBody))
            {
                return EmptyBody();
            }

            var userToModify = JsonConvert.DeserializeObject<UserEntity>(RequestContext.HttpBody);
            int changes = 0;

            if (!string.IsNullOrEmpty(userToModify.Description))
            {
                model.UserEntity.Description = userToModify.Description;
                changes++;
            }

            if (!string.IsNullOrEmpty(userToModify.Image))
            {
                model.UserEntity.Image = userToModify.Image;
                changes++;
            }

            if (!string.IsNullOrEmpty(userToModify.DisplayName))
            {
                model.UserEntity.DisplayName = userToModify.DisplayName;
                changes++;
            }

            if (!string.IsNullOrEmpty(userToModify.Password))
            {
                var newPassword = Cryptography.GenerateSaltedHash(userToModify.Password);

                model.UserEntity.Password = newPassword.Hash;
                model.UserEntity.Salt = newPassword.Salt;
                changes++;
            }

            bool success = false;
            if (changes > 0)
                success = model.UpdateUser();

            if (success)
            {
                return SuccessObject("Updated Account Information", StatusCodes.OK);
            }
            return SuccessObject("Could not Update Account Information", StatusCodes.OK);
        }
    }
}