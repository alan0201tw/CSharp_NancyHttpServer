using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using MySql.Data.MySqlClient;
using Nancy.ModelBinding;

namespace NancyExample
{
    public class ChatModule : NancyModule
    {
        public class MyConversation
        {
            public int conversationID;
            public string sendTime;
            public string message;

            public MyConversation()
            {
                conversationID = 0;
                sendTime = string.Empty;
                message = string.Empty;
            }

            public MyConversation(int conversationID, string sendTime, string message)
            {
                this.conversationID = conversationID;
                this.sendTime = sendTime;
                this.message = message;
            }
        }


        public ChatModule() : base("/chat")
        {
            Get["/allchat"] = _ =>
            {
                string sqlString = @"Select * from conversation;";

                List<MyConversation> myConversationList = new List<MyConversation>();

                lock (MySQLConnectionManager.Instance.databaseConnection)
                {
                    using (MySqlCommand command = new MySqlCommand(sqlString, MySQLConnectionManager.Instance.databaseConnection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string sendTime = reader.GetString(1);
                                string message = reader.GetString(2);
                                // in this case the return format is json
                                myConversationList.Add(new MyConversation(id, sendTime, message));
                            }
                            return myConversationList;
                        }
                    }
                }
            };

            Post["/updatechat"] = parameters =>
            {
                string sqlString = @"INSERT INTO `testing`.`conversation` (`idconversation`, `sendTime`, `message`) VALUES(@conversationID, @sendTime, @message);SELECT LAST_INSERT_ID();";

                lock (MySQLConnectionManager.Instance.databaseConnection)
                {
                    using (MySqlCommand command = new MySqlCommand(sqlString, MySQLConnectionManager.Instance.databaseConnection))
                    {
                        MyConversation conversation = this.Bind<MyConversation>();
                        // body need to contain sendTime and message
                        command.Parameters.AddWithValue("conversationID", conversation.conversationID);
                        command.Parameters.AddWithValue("sendTime", conversation.sendTime);
                        command.Parameters.AddWithValue("message", conversation.message);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int latestID = reader.GetInt32(0);
                                // current version no need for unique id
                                
                                return HttpStatusCode.Accepted;
                            }
                            return HttpStatusCode.NotAcceptable;
                        }
                    }
                }
            };
        }
    }
}