using Nancy;
using Nancy.ModelBinding;
using System.Linq;

namespace NancyExample
{
    public class ClassModule : NancyModule
    {
        private class Score
        {
            public int id;
            public int score;
        }

        public ClassModule() : base("/")
        {
            Get["/"] = _ => "Hello World!";

            Post["/"] = _ =>
            {
                Score tmp = this.Bind<Score>();

                string x = "";

                if (this.Request.Headers.Keys.Contains("TestingHeader"))
                    x = Request.Headers["TestingHeader"].First();

                

                return string.Format(" id = {0} , score = {1} ,testingHeader = {2} ", tmp.id, tmp.score , x);
            };
            
            Get["/status"] = _ => "Hello World! I'm at subdirectory - status!";

            Get["/class/{id}"] = parameters =>
            {
                int ID = parameters.id;
                
                return "Yeeeeee" + ID.ToString();
            };
        }
    }
}