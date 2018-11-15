using System;
using RestSharp;
using RestSharp.Deserializers;

namespace RestDemo
{
	class Program
	{
		static void Main(string[] args)
		{
			// URL: https://forverkliga.se/JavaScript/api/simple.php?key=value
			Console.WriteLine("RESTsharp demo");
			RestClient client = new RestClient("https://forverkliga.se/JavaScript/api/");

			IRestRequest request = new RestRequest
			{
				Method = Method.GET,
				RequestFormat = DataFormat.Json,
				Resource = "simple.php"
			};
			request.Parameters.Add(new Parameter("key", "value", ParameterType.QueryString));
			var response = client.Execute(request);

			Console.WriteLine("Request sent, received response with status code " + response.StatusCode);

			var deserializer = new JsonDeserializer();
			try
			{
				var data = deserializer.Deserialize<Conversation>(response);
				Console.WriteLine($"Received data with message ={data.Message} and time ={data.Time}");
			}
			catch(Exception e)
			{
				Console.WriteLine("An error occurred: " + e.Message);
			}

			Console.WriteLine("Press any key to exit");
			Console.ReadLine();
		}
		public class Conversation
		{
			public string Message { get; set; }
			public string Time { get; set; }
		}
	}
}
