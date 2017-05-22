using Nancy;

namespace NancyExample
{
    public class DrinkModule : NancyModule
    {
        public DrinkModule() : base("/drink")
        {
            Get["/"] = _ =>
            {
                return "You are currently at drink page.";
            };


            Get["/{drinktype}"] = parameters =>
            {
                string drinkType = parameters.drinktype;

                return string.Format(" You are drinking {0}", drinkType);
            };
        }
    }
}