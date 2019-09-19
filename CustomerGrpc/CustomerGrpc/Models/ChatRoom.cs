using System.Collections.Generic;

namespace CustomerGrpc.Models
{
	public class ChatRoom
	{
		public int Id { get; }
		public List<Customer> CustomersInRoom { get; }
		public ChatRoom(int id)
		{
			Id = id;
			CustomersInRoom = new List<Customer>();
		}
	}
}