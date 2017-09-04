using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;

namespace NancyExample.HttpSystem
{
    public class RegisterModule : NancyModule
    {
        public List<int> onlineClientIDList;
        public Dictionary<int, Queue<Inform>> informToBeTransmitted;

    }
}