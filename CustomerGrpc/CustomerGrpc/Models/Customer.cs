using System;
using System.Dynamic;
using Grpc.Core;

namespace CustomerGrpc.Models
{
	public class Customer
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string ColorInConsole { get; set; }
		public IAsyncStreamWriter<ChatMessage> Stream { get; set; }
	}
}