using System.Collections.Generic;
using System.Linq;
using CustomerGrpc.Models;

namespace CustomerGrpc.Providers
{
	public class ChatRoomProvider : IChatRoomProvider
	{
		private static readonly List<ChatRoom> ChatRooms = new List<ChatRoom>
		{
			new ChatRoom(1),
			new ChatRoom(2),
			new ChatRoom(3)
		};
		
		public ChatRoom GetFreeChatRoom()
		{
			return ChatRooms.FirstOrDefault(c => c.CustomersInRoom.Count < 10);
		}
		
		public ChatRoom GetChatRoomById(int roomId)
		{
			return ChatRooms.FirstOrDefault(c => c.Id == roomId);
		}

		public ChatRoom AddChatRoom()
		{
			var newRoom = new ChatRoom(ChatRooms.Count + 1);
			ChatRooms.Add(newRoom);
			return newRoom;
		}
	}
}