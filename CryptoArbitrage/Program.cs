using CryptoArbitrageLibrary.Model;
using CryptoArbitrage;
using CryptoArbitrage.Middleware;
using CryptoArbitrageLibrary.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddHttpClient();
builder.Services.AddMemoryCache();
builder.Services.Configure<CryptoArbitrageLibrary.Configuration.Markets>(builder.Configuration.GetSection(("Markets")));


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IMarkets, CryptoArbitrageLibrary.Model.Markets>();

builder.Services.AddScoped<IMultipleSymbols, MultipleSymbols>();
builder.Services.AddScoped<IMultipleTrades, MultipleTrades>();

builder.Services.AddHostedService<TradeWorker>();
builder.Services.AddHostedService<SymbolWorker>();

builder.Services.AddHostedService<CryptoArbitrageLibrary.Service.Background.Bigone>();
builder.Services.AddHostedService<CryptoArbitrageLibrary.Service.Background.Binance>();
builder.Services.AddHostedService<CryptoArbitrageLibrary.Service.Background.Bybit>();
builder.Services.AddHostedService<CryptoArbitrageLibrary.Service.Background.Kraken>();

builder.Services.AddScoped<CryptoArbitrageLibrary.Repository.Bigone.IBigone, CryptoArbitrageLibrary.Repository.Bigone.Bigone>();
builder.Services.AddScoped<CryptoArbitrageLibrary.Repository.Binance.IBinance, CryptoArbitrageLibrary.Repository.Binance.Binance>();
builder.Services.AddScoped<CryptoArbitrageLibrary.Repository.ByBit.IBybit, CryptoArbitrageLibrary.Repository.ByBit.Bybit>();
builder.Services.AddScoped<CryptoArbitrageLibrary.Repository.Kraken.IKraken, CryptoArbitrageLibrary.Repository.Kraken.Kraken>();

builder.Services.AddScoped<CryptoArbitrageLibrary.Service.IBigone, CryptoArbitrageLibrary.Service.Bigone>();
builder.Services.AddScoped<CryptoArbitrageLibrary.Service.IBinance, CryptoArbitrageLibrary.Service.Binance>();
builder.Services.AddScoped<CryptoArbitrageLibrary.Service.IBybit, CryptoArbitrageLibrary.Service.Bybit>();
builder.Services.AddScoped<CryptoArbitrageLibrary.Service.IKraken, CryptoArbitrageLibrary.Service.Kraken>();

builder.Services.Decorate<CryptoArbitrageLibrary.Service.IBigone, CryptoArbitrageLibrary.Service.Caching.Bigone>();
builder.Services.Decorate<CryptoArbitrageLibrary.Service.IBinance, CryptoArbitrageLibrary.Service.Caching.Binance>();
builder.Services.Decorate<CryptoArbitrageLibrary.Service.IBybit, CryptoArbitrageLibrary.Service.Caching.Bybit>();
builder.Services.Decorate<CryptoArbitrageLibrary.Service.IKraken, CryptoArbitrageLibrary.Service.Caching.Kraken>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapGet("/markets", (IMarkets myMarkets) =>
    {
        return myMarkets.GetMarkets();
    })
    .WithName("GetMarkets")
    .WithOpenApi();

app.MapGet("/symbols", (IMultipleSymbols multipleSymbols) =>
    {
        return multipleSymbols.GetSymbols();
    })
    .WithName("GetSymbols")
    .WithOpenApi();


app.MapGet("/trade/{symbol}", (string symbol, IMultipleTrades multipleTrades) =>
    {
        return multipleTrades.GetTrades(symbol);
    })
    .WithName("GetTrades")
    .WithOpenApi();


app.MapGet("/trade/BTCUSDT", ( IMultipleTrades multipleTrades) =>
    {
        return multipleTrades.GetBtcUsdTrades();
    })
    .WithName("GetBTCUSDTTrades")
    .WithOpenApi();


app.MapGet("/trade/top5/BTCUSDT", (IMultipleTrades multipleTrades) =>
    {
        return multipleTrades.GetLast5BtcUsdTrades();
    })
    .WithName("GetTop5BTCUSDTTrades")
    .WithOpenApi();


app.MapGet("/trade/top/BTCUSDT", (IMultipleTrades multipleTrades) =>
    {
        return multipleTrades.GetLastBtcUsdTrades();
    })
    .WithName("GetTopBTCUSDTTrades")
    .WithOpenApi();


    
app.MapGet("/trade/top/chart/BTCUSDT", (IMultipleTrades multipleTrades) =>
        {
            return multipleTrades.GetLastChartsBtcUsdPrice();
        }
    )
    .WithName("GetTopChartBTCUSDT")
    .WithOpenApi();


app.UseMiddleware<ErrorLogging>();

app.Run();

Console.ReadLine();