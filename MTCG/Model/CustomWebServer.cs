﻿using System;
using System.Net;
using MTCG.API;
using Serilog;
using WebServer.API;

namespace MTCG.Model
{
    public class CustomWebServer : WebServer.Model.BaseServerModell
    {
        public CustomWebServer(IPAddress ipAddress, int port) : base(ipAddress, port)
        {
        }

        public override void HandleClient()
        {
            Log.Debug("Waiting for a connection... ");
            try
            {
                WebServer.Model.TcpClient client = new WebServer.Model.TcpClient(_listener.AcceptTcpClient());
                Log.Debug($"Client {client.RemoteEndPoint} connected");

                CustomApiController controller = new CustomApiController(client);
                controller.Respond(controller.ForwardToEndPointHandler());

                Log.Debug($"Client {client.RemoteEndPoint} disconnected\r\n");
                client.Close();
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }

            _semaphore.Release();
        }
    }
}