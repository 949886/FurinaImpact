using FurinaImpact.Proxy;

Console.Title = "FurinaImpact | Proxy";

ProxyService service = new();
AppDomain.CurrentDomain.ProcessExit += (_, _) =>
{
    Console.WriteLine("Shutting down...");
    service.Shutdown();
};

Thread.Sleep(-1);