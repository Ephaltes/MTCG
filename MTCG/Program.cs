using System;
using System.Net;
using MTCG.Entity;
using MTCG.Helpers;
using MTCG.Model;
using MTCG.Model.BaseClass;
using Newtonsoft.Json;

namespace MTCG
{
    class Program
    {
        static void Main(string[] args)
        {
            
            
             var webserver = new WebServer.Model.BaseServerModell(IPAddress.Any, 10001);
             webserver.Start();
             webserver.Listen(5); 
        }
    }
}
