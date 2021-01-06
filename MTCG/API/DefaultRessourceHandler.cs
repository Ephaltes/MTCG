using MTCG.Entity;
using MTCG.Interface;
using WebServer;
using WebServer.API;
using WebServer.Interface;
using WebServer.RessourceHandler;

namespace MTCG.API
{
    public class DefaultRessourceHandler : BaseRessourceHandler
    {
        protected IDatabase Database;
        protected IRequestContext RequestContext;

        public DefaultRessourceHandler(IRequestContext req, IDatabase database)
        {
            RequestContext = req;
            Database = database;
        }

        protected override ResponseContext HandleGet()
        {
            return NotImplemented();
        }

        protected override ResponseContext HandlePost()
        {
            return NotImplemented();
        }

        protected override ResponseContext HandlePut()
        {
            return NotImplemented();
        }

        protected override ResponseContext HandleDelete()
        {
            return NotImplemented();
        }

        public override ResponseContext Handle()
        {
            ResponseContext responseContext;
            switch (RequestContext.HttpMethod)
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
                    responseContext = NotImplemented();
                    break;
            }

            return responseContext;
        }

        protected ResponseContext NotImplemented()
        {
            return CustomError("Not Implemented", StatusCodes.BadRequest);
        }

        protected ResponseContext SomeThingWrong()
        {
            return CustomError("Something went wrong", StatusCodes.InternalServerError);
        }

        protected ResponseContext EmptyBody()
        {
            return CustomError("Body is empty", StatusCodes.BadRequest);
        }

        protected ResponseContext NotAuthorized()
        {
            return CustomError("Not Authorized", StatusCodes.Unauthorized);
        }

        protected ResponseContext CustomError(string msg, StatusCodes code)
        {
            var responseContext = new ResponseContext();
            responseContext.ResponseMessage.Add(new ResponseMessage
            {
                Status = code,
                ErrorMessage = msg
            });
            responseContext.StatusCode = code;
            return responseContext;
        }

        protected ResponseContext SuccessObject(object msg, StatusCodes code)
        {
            var responseContext = new ResponseContext();
            responseContext.ResponseMessage.Add(new ResponseMessage
            {
                Status = code,
                Object = msg
            });
            responseContext.StatusCode = code;
            return responseContext;
        }

        protected AuthorizationEntity ConvertToAuthorizationEntity(string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            var splitted = token.Trim().Split(" ");
            if (splitted.Length < 2)
                return null;

            return new AuthorizationEntity {Type = splitted[0], Value = splitted[1]};
        }
    }
}