using RandomNameGeneratorLibrary;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
Console.CancelKeyPress += (s,e) => cancellationTokenSource.Cancel();
CancellationToken cancellationToken = cancellationTokenSource.Token;

Console.Write("Input base URL or ENTER to default: ");
string? baseUrl = Console.ReadLine();
if (string.IsNullOrWhiteSpace(baseUrl))
{
    baseUrl = "http://localhost:8480";
    Console.WriteLine($" - Setup default URL: {baseUrl}");
}
else
{
    Console.WriteLine($" - Setup URL: {baseUrl}");
}

Console.WriteLine("Start generating...");
using (HttpClient client = new HttpClient())
{
    Random rnd = Random.Shared;
    for (int i = 0; i < 100; i++)
    {
        Console.Write($"[{i.ToString().PadLeft(3, '0')}%] Create...");

        JsonObject name = new JsonObject()
        {
            ["family"] = rnd.GenerateRandomFirstName()
        };

        if (rnd.NextDouble() > 0.5)
            name["use"] = "official";

        if (rnd.NextDouble() > 0.5)
            name["given"] = new JsonArray(Enumerable.Range(0, rnd.Next(0, 3))
                .Select(v => rnd.GenerateRandomLastName())
                .Select(v => JsonValue.Create(v))
                .ToArray());

        JsonObject raw = new JsonObject()
        {
            ["name"] = name,
            ["birthDate"] = DateTime.UnixEpoch.AddSeconds(0.8 * Math.Abs(Random.Shared.Next()))
        };

        if (rnd.NextDouble() > 0.5)
            raw["gender"] = rnd.Next(0, 4) switch
            {
                0 => "male",
                1 => "female",
                2 => "other",
                _ => "unknown",
            };

        if (rnd.NextDouble() > 0.5)
            raw["active"] = rnd.NextDouble() > 0.5;


        var message = await client.PostAsJsonAsync($"{baseUrl}/patient", raw, cancellationToken);
        JsonObject? rawMessage = await message.Content.ReadFromJsonAsync<JsonObject>(cancellationToken: cancellationToken);
        Guid guid = rawMessage!["response"]!.GetValue<Guid>();
        Console.WriteLine($"OK: {guid}!");
    }
}