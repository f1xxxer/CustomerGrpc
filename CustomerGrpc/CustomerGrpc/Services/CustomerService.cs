using System;
using System.IO;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;


namespace CustomerGrpc.Services
{
	public class CustomerService : CustomerGrpc.CustomerService.CustomerServiceBase
	{
		private readonly ILogger<CustomerService> _logger;
		private readonly IChatRoomService _chatRoomService;


		public CustomerService(ILogger<CustomerService> logger, IChatRoomService chatRoomService)
		{
			_logger = logger;
			_chatRoomService = chatRoomService;
		}

		public override async Task<JoinCustomerReply> JoinCustomerChat(JoinCustomerRequest request, ServerCallContext context)
		{
			return new JoinCustomerReply { RoomId = await _chatRoomService.AddCustomerToChatRoomAsync(request.Customer) };
		}

		public override async Task SendMessageToChatRoom(IAsyncStreamReader<ChatMessage> requestStream, IServerStreamWriter<ChatMessage> responseStream,
			ServerCallContext context)
		{
			var httpContext = context.GetHttpContext();
			_logger.LogInformation($"Connection id: {httpContext.Connection.Id}");

			if (!await requestStream.MoveNext())
			{
				return;
			}

			_chatRoomService.ConnectCustomerToChatRoom(requestStream.Current.RoomId, Guid.Parse(requestStream.Current.CustomerId), responseStream);
			var user = requestStream.Current.CustomerName;
			_logger.LogInformation($"{user} connected");

			try
			{
				while (await requestStream.MoveNext())
				{
					if (!string.IsNullOrEmpty(requestStream.Current.Message))
					{
						if (string.Equals(requestStream.Current.Message, "qw!", StringComparison.OrdinalIgnoreCase))
						{
							break;
						}
						await _chatRoomService.BroadcastMessageAsync(requestStream.Current);
					}
				}
			}
			catch (IOException)
			{
				_chatRoomService.DisconnectCustomer(requestStream.Current.RoomId, Guid.Parse(requestStream.Current.CustomerId));
				_logger.LogInformation($"Connection for {user} was aborted.");
			}
		}
	}
}