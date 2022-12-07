
using ClinicService.Proto;

using ClinicServiceData;

using Grpc.Core;

using static ClinicService.Proto.ClinicService;

namespace ClinicService.Services
{
	public class ClinicService : ClinicServiceBase
	{
		private readonly ClinicServiceDbContext dbContext;
		public ClinicService(ClinicServiceDbContext dbContext)
		{
			this.dbContext=dbContext;
		}
		public override Task<CreateClientResponse> CreateClient(CreateClientRequest request, ServerCallContext context)
		{
			try
			{
				var client = new Client()
				{
					Document = request.Document,
					Surname = request.Surname,
					FirstName = request.Firstname,
					Patronimic = request.Patronimic
				};

				dbContext.Clients.Add(client);
				dbContext.SaveChanges();

				var response = new CreateClientResponse()
				{
					ClientId = client.Id,
					ErrCode = 0,
					ErrMessage ="",
				};
				return Task.FromResult(response);
			}
			catch (Exception)
			{
				var response = new CreateClientResponse()
				{
					ErrCode = 1,
					ErrMessage ="Internal server error"
				};
				return Task.FromResult(response);
			}
		}

		public override Task<GetClientsResponse> GetClients(GetClientsRequest request, ServerCallContext context)
		{
			try
			{
				var response = new GetClientsResponse();

				IList<ClientResponse> clients = dbContext.Clients.Select(client => new ClientResponse()
				{

					ClientId = client.Id,
					Document = client.Document,
					Surname = client.Surname,
					Firstname = client.FirstName,
					Patronimic = client.Patronimic,
				}).ToList();

				response.Clients.AddRange(clients);
				return Task.FromResult(response);

			}
			catch (Exception)
			{

				var response = new GetClientsResponse()
				{
					ErrCode = 1,
					ErrMessage = "Internal server error"
				};
				return Task.FromResult(response);
			}

		}
	}
}
