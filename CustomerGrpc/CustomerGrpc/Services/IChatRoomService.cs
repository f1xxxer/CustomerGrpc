using System;
using System.Threading.Tasks;
using Grpc.Core;

namespace CustomerGrpc.Services
{
	public interface IChatRoomService
	{
		Task BroadcastMessageAsync(ChatMessage message);
		Task<int> AddCustomerToChatRoomAsync(Customer customer);
		void ConnectCustomerToChatRoom(int roomId, Guid customerId, IAsyncStreamWriter<ChatMessage> asyncStreamWriter);
		void DisconnectCustomer(int roomId, Guid customerId);
	}
}