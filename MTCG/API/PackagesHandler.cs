using System;
using MTCG.Entity;
using MTCG.Helpers;
using MTCG.Interface;
using MTCG.Model;
using Newtonsoft.Json;
using WebServer;
using WebServer.API;
using WebServer.Interface;
using WebServer.RessourceHandler;

namespace MTCG.API
{
    public class PackagesHandler : DefaultRessourceHandler
    {

        public PackagesHandler(IRequestContext req, IDatabase database) : base(req, database)
        {
        }
        protected override ResponseContext HandlePost()
        {
            ResponseContext responseContext = new ResponseContext();
            
            var token = RequestContext.HttpHeader["Authorization"].HeaderToAuthorizationEntity();

           /*  if (token == null)
            {
                responseContext.ResponseMessage.Add(new ResponseMessage()
                {
                    Status = StatusCodes.Unauthorized,
                    ErrorMessage = "No UserToken provided"
                });
                responseContext.StatusCode = StatusCodes.Unauthorized;
                return responseContext;
            } */

            
            if (String.IsNullOrWhiteSpace(RequestContext.HttpBody))
            {
                responseContext.ResponseMessage.Add(new ResponseMessage()
                {
                    Status = StatusCodes.BadRequest,
                    ErrorMessage = "Body is empty"
                });
                responseContext.StatusCode = StatusCodes.BadRequest;
                return responseContext;
            }

            var packageEntity = JsonConvert.DeserializeObject<PackageEntity>(RequestContext.HttpBody);


            return null;
        }
    }
}