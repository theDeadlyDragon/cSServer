// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;      //required
using System.Net.Sockets;    //required

using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServerTest
{
   class Program
   {

      // TcpListener server = new TcpListener(IPAddress.Any, 9999);  
      // // we set our IP address as server's address, and we also set the port: 9999
      //
      // server.Start();  // this will start the server
      //
      // while (true)   //we wait for a connection
      // {
      //     TcpClient client = server.AcceptTcpClient();  //if a connection exists, the server will accept it
      //
      //     NetworkStream ns = client.GetStream(); //networkstream is used to send/receive messages
      //
      //     byte[] hello = new byte[100];   //any message must be serialized (converted to byte array)
      //     hello = Encoding.Default.GetBytes("hello world");  //conversion string => byte array
      //
      //     ns.Write(hello, 0, hello.Length);     //sending the message
      //
      //     while (client.Connected)  //while the client is connected, we look for incoming messages
      //     {
      //         byte[] msg = new byte[1024];     //the messages arrive as byte array
      //         ns.Read(msg, 0, msg.Length);   //the same networkstream reads the message sent by the client
      //         Console.WriteLine(Encoding.Default.GetString(msg).Trim(' ')); //now , we write the message as string
      //     }
      // }



      static void Main(string[] args)
      {
         SQLiteConnection sqlite_conn;
         sqlite_conn = CreateConnection();
         CreateTable(sqlite_conn);
         InsertData(sqlite_conn);
         ReadData(sqlite_conn);
      }

      static SQLiteConnection CreateConnection()
      {

         SQLiteConnection sqlite_conn;
         // Create a new database connection:
         sqlite_conn = new SQLiteConnection("Data Source=database.db;Version = 3;New = True;Compress = True;");
         
         // Open the connection:
         try
         {
            sqlite_conn.Open();
         }
         catch (Exception ex)
         {

         }

         return sqlite_conn;
      }

      static void CreateTable(SQLiteConnection conn)
      {

         SQLiteCommand sqlite_cmd;
         string Createsql = "CREATE TABLE SampleTable(Col1 VARCHAR(20), Col2 INT)";
         string Createsql1 = "CREATE TABLE SampleTable1(Col1 VARCHAR(20), Col2 INT)";
         sqlite_cmd = conn.CreateCommand();
         sqlite_cmd.CommandText = Createsql;
         sqlite_cmd.ExecuteNonQuery();
         sqlite_cmd.CommandText = Createsql1;
         sqlite_cmd.ExecuteNonQuery();

      }

      static void InsertData(SQLiteConnection conn)
      {
         SQLiteCommand sqlite_cmd;
         
         Console.WriteLine("insert data");
         sqlite_cmd = conn.CreateCommand();
         sqlite_cmd.CommandText = "INSERT INTO SampleTable(Col1, Col2) VALUES('Test Text ', 1);";
         sqlite_cmd.ExecuteNonQuery();
         sqlite_cmd.CommandText = "INSERT INTO SampleTable(Col1, Col2) VALUES('Test1 Text1 ', 2);";
         sqlite_cmd.ExecuteNonQuery();
         sqlite_cmd.CommandText = "INSERT INTO SampleTable(Col1, Col2) VALUES('Test2 Text2 ', 3);";
         sqlite_cmd.ExecuteNonQuery();


         sqlite_cmd.CommandText = "INSERT INTO SampleTable1(Col1, Col2) VALUES('Test3 Text3 ', 3);";
         sqlite_cmd.ExecuteNonQuery();

      }

      static void ReadData(SQLiteConnection conn)
      {
         SQLiteDataReader sqlite_datareader;
         SQLiteCommand sqlite_cmd;
         sqlite_cmd = conn.CreateCommand();
         sqlite_cmd.CommandText = "SELECT * FROM SampleTable";

         sqlite_datareader = sqlite_cmd.ExecuteReader();
         while (sqlite_datareader.Read())
         {
            string myreader = sqlite_datareader.GetString(0);
            Console.WriteLine(myreader);
         }

         conn.Close();
      }
   }
}
    
