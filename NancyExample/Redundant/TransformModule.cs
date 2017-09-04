using MySql.Data.MySqlClient;
using Nancy;
using System.Collections.Generic;

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

                List<MyTransform> myTransformList = new List<MyTransform>();

                lock (MySQLConnectionManager.Instance.databaseConnection)
                    using (MySqlCommand command = new MySqlCommand(sqlString, MySQLConnectionManager.Instance.databaseConnection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                float positionX = reader.GetFloat(1);
                                float positionY = reader.GetFloat(2);
                                float positionZ = reader.GetFloat(3);
                                // in this case the return format is json
                                myTransformList.Add(new MyTransform(id, positionX, positionY, positionZ));
                            }
                            return myTransformList;
                        }
                    }
            };

        }

    }
}