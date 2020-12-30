using MTCG.Entity;
using MTCG.Helpers;
using MTCG.Interface;
using MTCG.Model;
using Newtonsoft.Json;
using WebServer;
using WebServer.API;
using WebServer.Interface;

namespace MTCG.API
{
    public class UsersHandler : DefaultRessourceHandler
    {
        public UsersHandler(IRequestContext req, IDatabase database) : base(req, database)
        {
        }

        protected override ResponseContext HandleGet()
        {
            var model = new UserModell(Database);

            if (RequestContext.HttpRequest.Count < 2)
                return CustomError("Missing Parameters", StatusCodes.BadRequest);
           

            var user = RequestContext.HttpRequest[1];

            RequestContext.HttpHeader.TryGetValue("Authorization", out var token);
            var authorization = ConvertToAuthorizationEntity(token);

            if (authorization == null || !model.VerifyToken(authorization.Value) || string.IsNullOrEmpty(user) ||
                user != model.UserEntity.Username)
                return NotAuthorized();

            return SuccessObject(new
            {
                model.UserEntity.DisplayName,
                model.UserEntity.Username,
                model.UserEntity.Description,
                model.UserEntity.Image
            }, StatusCodes.OK);
        }

        protected override ResponseContext HandlePost()
        {
            var responseContext = new ResponseContext();
            if (string.IsNullOrWhiteSpace(RequestContext.HttpBody)) return EmptyBody();

            var userEntity = JsonConvert.DeserializeObject<UserEntity>(RequestContext.HttpBody);

            var model = new UserModell(Database);
            var token = model.CreateTokenForUser(userEntity.Username, userEntity.Password);
            var msg = new ResponseMessage();

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
            var model = new UserModell(Database);
            if (RequestContext.HttpRequest.Count < 2)
                return CustomError("Missing Parameter", StatusCodes.BadRequest);
           

            var user = RequestContext.HttpRequest[1];

            RequestContext.HttpHeader.TryGetValue("Authorization", out var token);
            var authorization = ConvertToAuthorizationEntity(token);

            if (authorization == null || !model.VerifyToken(authorization.Value) || string.IsNullOrEmpty(user) ||
                user != model.UserEntity.Username)
                return NotAuthorized();

            if (string.IsNullOrWhiteSpace(RequestContext.HttpBody)) return EmptyBody();

            var userToModify = JsonConvert.DeserializeObject<UserEntity>(RequestContext.HttpBody);
            var changes = 0;

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

            var success = false;
            if (changes > 0)
                success = model.UpdateUser();

            if (success) return SuccessObject("Updated Account Information", StatusCodes.OK);
            return SuccessObject("Could not Update Account Information", StatusCodes.InternalServerError);
        }
    }
}