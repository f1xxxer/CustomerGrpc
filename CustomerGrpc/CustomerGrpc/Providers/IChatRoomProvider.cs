using CustomerGrpc.Models;

namespace CustomerGrpc.Providers
{
	public interface IChatRoomProvider
	{
		ChatRoom GetFreeChatRoom();
		ChatRoom GetChatRoomById(int roomId);
		ChatRoom AddChatRoom();
	}
}