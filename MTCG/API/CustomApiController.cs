using System;
using System.Collections.Generic;
using MTCG.Interface;
using MTCG.Model;
using Serilog;
using WebServer;
using WebServer.API;
using WebServer.Interface;
using WebServer.RessourceHandler;

namespace MTCG.API
{
    public class CustomApiController : BaseApiController
    {
        public IDatabase Database { get; set; }
        public CustomApiController(ITcpClient client)
        {
            _client = client;
            _endpointList = new List<string>(){"users","sessions","packages"};
            Database = new DatabaseModell();
        }
        
        public override string ForwardToEndPointHandler()
        {
            _responseContext = new ResponseContext();
            try
            {
                _requestContext = new RequestContext();
                string data = ReceiveFromClient();

                _requestContext.ParseRequestFromHeader(data);

                DefaultRessourceHandler handler=null;
                
                switch (GetRequestedEndPoint())
                {
                    case "users":
                    {
                        handler = new UsersHandler(_requestContext,Database);
                        break;
                    }
                    case "sessions":
                    {
                        handler = new SessionsHandler(_requestContext,Database);
                        break;
                    }
                    case "packages":
                    {
                        handler = new PackagesHandler(_requestContext,Database);
                        break;
                    }
                    default:
                        _responseContext.ResponseMessage.Add(new ResponseMessage()
                        {
                            ErrorMessage = "Unknown Endpoint",
                            Status = StatusCodes.InternalServerError
                        });

                        _responseContext.StatusCode = StatusCodes.InternalServerError;
                        return _responseContext.BuildResponse();
                }
                _responseContext = handler.Handle();
            }
            catch (Exception e)
            {
                _responseContext.ResponseMessage.Add(new ResponseMessage()
                {
                    ErrorMessage = e.Message,
                    Status = StatusCodes.InternalServerError
                });

                _responseContext.StatusCode = StatusCodes.InternalServerError;
                Log.Error(e.Message);
            }

            return _responseContext.BuildResponse();
        
        }
    }
}