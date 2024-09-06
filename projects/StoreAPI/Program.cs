using Messages;
using MessageClient;
using MessageClient.Factory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var easyNetQFactory = new EasyNetQFactory();


builder.Services.AddSingleton<MessageClient<OrderResponseMessage>>(easyNetQFactory.CreateTopicMessageClient<OrderResponseMessage>("StoreAPI", "").Connect());
builder.Services.AddSingleton<MessageClient<OrderRequestMessage>>(easyNetQFactory.CreatePubSubMessageClient<OrderRequestMessage>("StoreAPI").Connect());
builder.Services.AddSingleton<MessageClient<OrderResponseMessage>>(easyNetQFactory.CreateTopicMessageClient<OrderResponseMessage>("OrderService", "").Connect());
builder.Services.AddSingleton<MessageClient<OrderRequestMessage>>(easyNetQFactory.CreatePubSubMessageClient<OrderRequestMessage>("OrderService").Connect());
builder.Services.AddSingleton<MessageClient<OrderResponseMessage>>(easyNetQFactory.CreateTopicMessageClient<OrderResponseMessage>("ShippingService", "").Connect());
builder.Services.AddSingleton<MessageClient<OrderRequestMessage>>(easyNetQFactory.CreatePubSubMessageClient<OrderRequestMessage>("StockService").Connect());

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
