using MySql.Data.MySqlClient;
using Nancy;

namespace NancyExample
{
    public class TransformModule : NancyModule
    {
        public class MyTransform
        {
            public int id;
            public float positionX, positionY, positionZ;

            public MyTransform(int id, float positionX, float positionY, float positionZ)
            {
                this.id = id;
                this.positionX = positionX;
                this.positionY = positionY;
                this.positionZ = positionZ;
            }
        };

        public TransformModule() : base("/transform")
        {
            Get["/position"] = _ =>
            {
                string sqlString = @"Select * from transformtable;";

                using (MySqlCommand command = new MySqlCommand(sqlString, MySQLConnectionManager.Instance.databaseConnection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            float positionX = reader.GetFloat(1);
                            float positionY = reader.GetFloat(2);
                            float positionZ = reader.GetFloat(3);

                            return new MyTransform(id, positionX, positionY, positionZ);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            };

        }

    }
}