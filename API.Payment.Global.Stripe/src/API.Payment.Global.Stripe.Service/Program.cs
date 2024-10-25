using API.Payment.Global.Stripe.Domain.Implementation.Interfaces;
using API.Payment.Global.Stripe.Domain.Implementation.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IConsumerService, ConsumerService>();

builder.Services.AddTransient<IStripeService, StripeService>();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
