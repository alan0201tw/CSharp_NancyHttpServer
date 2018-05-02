using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.ModelBinding;

namespace NancyExample
{
    public class MeiChuApi : NancyModule
    {
        public MeiChuApi() : base("/meichu")
        {
            Get["test"] = _ =>
            {
                return "TEST";
            };
        }
    }
}