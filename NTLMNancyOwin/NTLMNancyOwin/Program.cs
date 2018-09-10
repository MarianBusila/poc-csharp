
namespace NTLMNancyOwin
{
    using System;
    using System.Net;
    using System.Security.Principal;
    using Microsoft.Owin.Hosting;
    using Nancy;
    using Nancy.Owin;

    using Owin;

    internal class Program
    {
        private static void Main(string[] args)
        {
            using (WebApp.Start<Startup>("http://*:9000"))
            {
                Console.WriteLine("Press Enter to quit.");
                Console.ReadKey();
            }
        }
    }

    internal class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var listener = (HttpListener)app.Properties["System.Net.HttpListener"];
            listener.AuthenticationSchemes = AuthenticationSchemes.Ntlm;
           listener.UnsafeConnectionNtlmAuthentication = true;

            app.UseNancy();
        }
    }

    public class MyModule : NancyModule
    {
        public MyModule()
        {
            Get[""] = _ =>
            {                
                var env = Context.GetOwinEnvironment();
                var user = (IPrincipal)env["server.User"];

                return "Hello " + user.Identity.Name;
            };
        }
    }
}
