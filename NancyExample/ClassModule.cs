using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;

namespace NancyExample
{
    public class ClassModule : NancyModule
    {
        public ClassModule()
        {
            Get["/"] = _ => "Hello World!";
            Get["/status"] = _ => "Hello World! I'm at subdirectory - status!";

            Get["/class/{id}"] = parameters =>
            {
                int ID = parameters.id;

                //return Negotiate.WithStatusCode(HttpStatusCode.OK).WithModel(ID);
                return "Yeeeeee" + ID.ToString();
            };

        }
    }
}