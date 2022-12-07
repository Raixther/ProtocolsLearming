// See https://aka.ms/new-console-template for more information
using Grpc.Net.Client;

using static ClinicService.Proto.ClinicService;

try
{
	AppContext.SetSwitch("System.Net.Http.SocketHttpHandler.Http2UnencryptedSupport", true);


using var channel = GrpcChannel.ForAddress("http://localhost:5120");

ClinicServiceClient client = new(channel);

var response = client.CreateClient(new ClinicService.Proto.CreateClientRequest() { Document = "Ветстраховка", Firstname = "Жора", Surname = "Иванов", 
	Patronimic = "Петрович"} );


Console.WriteLine(response.ClientId + response.ErrCode+ response.ErrMessage);
	Console.ReadLine();

}
catch (Exception ex)
{
	Console.WriteLine(ex.Message);
	Console.WriteLine();
	Console.WriteLine(ex.Source);
}	

