
using Azure.Messaging.ServiceBus;

namespace VisaAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
                app.UseSwagger();
                app.UseSwaggerUI();
            //}

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            const string ServiceBusConnectionString = "Endpoint=sb://example.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=AbCdEfGhIjKlMnOpQrStUvWxYz==";
            const string QueueName = "visamessages";

            //static void Main(string[] args)
            //{
            //    Console.WriteLine("Sending a message to the Sales Messages queue...");
            //    SendSalesMessageAsync().GetAwaiter().GetResult();
            //    Console.WriteLine("Message was sent successfully.");
            //}

            static async Task SendSalesMessageAsync()
            {
                await using var client = new ServiceBusClient(ServiceBusConnectionString);

                await using ServiceBusSender sender = client.CreateSender(QueueName);
                try
                {
                    string messageBody = $"A message for Visa from privatemessagesender.cs";
                    var message = new ServiceBusMessage(messageBody);
                    Console.WriteLine($"Sending message: {messageBody}");
                    await sender.SendMessageAsync(message);
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
                }
                finally
                {
                    // Calling DisposeAsync on client types is required to ensure that network
                    // resources and other unmanaged objects are properly cleaned up.
                    await sender.DisposeAsync();
                    await client.DisposeAsync();
                }
            }

            app.Run();

        }


        //public class Privatemessagesender
        //{
        //    const string ServiceBusConnectionString = "Endpoint=sb://example.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=AbCdEfGhIjKlMnOpQrStUvWxYz==";
        //    const string QueueName = "visamessages";

        //    //static void Main(string[] args)
        //    //{
        //    //    Console.WriteLine("Sending a message to the Sales Messages queue...");
        //    //    SendSalesMessageAsync().GetAwaiter().GetResult();
        //    //    Console.WriteLine("Message was sent successfully.");
        //    //}

        //    static async Task SendSalesMessageAsync()
        //    {
        //        await using var client = new ServiceBusClient(ServiceBusConnectionString);

        //        await using ServiceBusSender sender = client.CreateSender(QueueName);
        //        try
        //        {
        //            string messageBody = $"A message for Visa from privatemessagesender.cs";
        //            var message = new ServiceBusMessage(messageBody);
        //            Console.WriteLine($"Sending message: {messageBody}");
        //            await sender.SendMessageAsync(message);
        //        }
        //        catch (Exception exception)
        //        {
        //            Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
        //        }
        //        finally
        //        {
        //            // Calling DisposeAsync on client types is required to ensure that network
        //            // resources and other unmanaged objects are properly cleaned up.
        //            await sender.DisposeAsync();
        //            await client.DisposeAsync();
        //        }
        //    }
        //}
    }
}


//changed github account from personal to Accenture
