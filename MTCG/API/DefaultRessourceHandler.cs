using System;
using MTCG.Interface;
using WebServer;
using WebServer.API;
using WebServer.Interface;
using WebServer.RessourceHandler;

namespace MTCG.API
{
    public class DefaultRessourceHandler : BaseRessourceHandler
    {
        protected IRequestContext RequestContext;
        protected IDatabase Database;
        
        public DefaultRessourceHandler(IRequestContext req,IDatabase database)
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
            var responseContext = new ResponseContext();
            responseContext.ResponseMessage.Add(new ResponseMessage()
            {
                Status = StatusCodes.BadRequest,
                ErrorMessage = "Not Implemented"
            });
            responseContext.StatusCode = StatusCodes.BadRequest;
            return responseContext;
        }
        
        protected ResponseContext SomeThingWrong()
        {
            var responseContext = new ResponseContext();
            responseContext.ResponseMessage.Add(new ResponseMessage()
            {
                Status = StatusCodes.InternalServerError,
                ErrorMessage = "Something went Wrong"
            });
            responseContext.StatusCode = StatusCodes.InternalServerError;
            return responseContext;
        }

        protected ResponseContext EmptyBody()
        {
            var responseContext = new ResponseContext();
            responseContext.ResponseMessage.Add(new ResponseMessage()
            {
                Status = StatusCodes.BadRequest,
                ErrorMessage = "Body is empty"
            });
            responseContext.StatusCode = StatusCodes.BadRequest;
            return responseContext;
        }

        protected ResponseContext NotAuthorized()
        {
            var responseContext = new ResponseContext();
            responseContext.ResponseMessage.Add(new ResponseMessage()
            {
                Status = StatusCodes.Unauthorized,
                ErrorMessage = "Not Authorized"
            });
            responseContext.StatusCode = StatusCodes.Unauthorized;
            return responseContext;
        }

        protected ResponseContext CustomError(string msg, StatusCodes code)
        {
            var responseContext = new ResponseContext();
            responseContext.ResponseMessage.Add(new ResponseMessage()
            {
                Status = code,
                ErrorMessage = msg
            });
            responseContext.StatusCode = code;
            return responseContext;
        }
    }
}