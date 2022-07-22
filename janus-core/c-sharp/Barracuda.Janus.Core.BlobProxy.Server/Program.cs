using System.Runtime.InteropServices;
using Barracuda.Janus.Core.BlobProxy.Server;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
{
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.ListenLocalhost(5000, o => o.Protocols = HttpProtocols.Http2);
    });
}

var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapGrpcService<BlobProxyService>());
app.Run();
