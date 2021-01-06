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
    public class PackagesHandler : DefaultRessourceHandler
    {
        public PackagesHandler(IRequestContext req, IDatabase database) : base(req, database)
        {
        }

        protected override ResponseContext HandlePost()
        {
            var model = new UserModell(Database);

            RequestContext.HttpHeader.TryGetValue("Authorization", out var token);
            var authorization = ConvertToAuthorizationEntity(token);

            if (authorization == null || !model.VerifyToken(authorization.Value)) return NotAuthorized();

            if (string.IsNullOrWhiteSpace(RequestContext.HttpBody)) return EmptyBody();

            var packageEntity = JsonConvert.DeserializeObject<PackageEntity>(RequestContext.HttpBody);

            var packageModell = new PackageModell(Database);

            var ret = packageModell.AddPackage(packageEntity);
            
            if (ret == 1 || ret == 2)
                return CardNotValid();

            if (ret == 0)
            {
                return SuccessObject("Package created sucessful", StatusCodes.Created);
               
            }
            return SomeThingWrong();
        }

        private ResponseContext CardNotValid()
        {
            return CustomError("CardEntity is not valid", StatusCodes.BadRequest);
        }
    }
}