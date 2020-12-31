using System;
using System.Collections.Generic;
using MTCG.Interface;
using MTCG.Model;
using Serilog;
using WebServer;
using WebServer.API;
using WebServer.Interface;

namespace MTCG.API
{
    public class CustomApiController : BaseApiController
    {
        public CustomApiController(ITcpClient client)
        {
            _client = client;
            _endpointList = new List<string>
            {
                "users", "sessions", "packages", "transactions", "cards", "deck", "stats", "battles", "score",
                "tradings"
            };
            Database = new DatabaseModell();
        }

        public IDatabase Database { get; set; }

        public override string ForwardToEndPointHandler()
        {
            _responseContext = new ResponseContext();
            try
            {
                _requestContext = new RequestContext();
                var data = ReceiveFromClient();

                _requestContext.ParseRequestFromHeader(data);

                DefaultRessourceHandler handler = null;

                switch (GetRequestedEndPoint())
                {
                    case "users":
                        handler = new UsersHandler(_requestContext, Database);
                        break;
                    case "sessions":
                        handler = new SessionsHandler(_requestContext, Database);
                        break;
                    case "packages":
                        handler = new PackagesHandler(_requestContext, Database);
                        break;
                    case "transactions":
                        handler = new TransactionsHandler(_requestContext, Database);
                        break;
                    case "cards":
                        handler = new CardsHandler(_requestContext, Database);
                        break;
                    case "deck":
                        handler = new DeckHandler(_requestContext, Database);
                        break;
                    case "stats":
                        handler = new StatsHandler(_requestContext, Database);
                        break;
                    case "score":
                        handler = new ScoreHandler(_requestContext, Database);
                        break;
                    case "battles":
                        handler = new BattlesHandler(_requestContext, Database);
                        break;
                    case "tradings":
                        handler = new TradingsHandler(_requestContext, Database);
                        break;
                    default:
                        _responseContext.ResponseMessage.Add(new ResponseMessage
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
                _responseContext.ResponseMessage.Add(new ResponseMessage
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