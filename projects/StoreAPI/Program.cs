using Messages;
using MessageClient;
using MessageClient.Factory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var easyNetQFactory = new EasyNetQFactory();


builder.Services.AddSingleton<MessageClient<OrderResponseMessage>>(easyNetQFactory.CreateTopicMessageClient<OrderResponseMessage>("StoreAPI", "").Connect());
builder.Services.AddSingleton<MessageClient<OrderResponseMessage>>(easyNetQFactory.CreatePubSubMessageClient<OrderResponseMessage>("StoreAPI").Connect());
builder.Services.AddSingleton<MessageClient<OrderResponseMessage>>(easyNetQFactory.CreateTopicMessageClient<OrderResponseMessage>("OrderService", "newOrder").Connect());
builder.Services.AddSingleton<MessageClient<OrderResponseMessage>>(easyNetQFactory.CreateTopicMessageClient<OrderResponseMessage>("OrderService", "orderCompletion").Connect());
builder.Services.AddSingleton<MessageClient<OrderRequestMessage>>(easyNetQFactory.CreateTopicMessageClient<OrderRequestMessage>("ShippingService", "newShippingOrder").Connect());
builder.Services.AddSingleton<MessageClient<OrderRequestMessage>>(easyNetQFactory.CreateTopicMessageClient<OrderRequestMessage>("StockService", "NewOrderStock").Connect());

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
