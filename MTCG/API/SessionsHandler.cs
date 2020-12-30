using MTCG.Entity;
using MTCG.Interface;
using MTCG.Model;
using Newtonsoft.Json;
using WebServer;
using WebServer.API;
using WebServer.Interface;

namespace MTCG.API
{
    public class SessionsHandler : DefaultRessourceHandler
    {
        public SessionsHandler(IRequestContext req, IDatabase database) : base(req, database)
        {
        }

        protected override ResponseContext HandlePost()
        {

            if (string.IsNullOrWhiteSpace(RequestContext.HttpBody)) return EmptyBody();

            var userEntity = JsonConvert.DeserializeObject<UserEntity>(RequestContext.HttpBody);

            if (userEntity == null || string.IsNullOrEmpty(userEntity.Username) ||
                string.IsNullOrEmpty(userEntity.Password))
            {
                return CustomError("Missing Parameter", StatusCodes.BadRequest);
            }

            var model = new UserModell(Database);
            model.UserEntity = userEntity;
            if (model.VerifyLogin()) return SuccessObject(model.UserEntity.Token, StatusCodes.OK);
            return NotAuthorized();
        }
    }
}