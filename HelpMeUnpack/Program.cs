using System.Buffers.Text;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using HelpMeUnpack.DTO;

namespace HelpMeUnpack;

public class Program {

    private static string Hostname = "https://www.hackattic.com";
    private static string GetBytesPath = "/challenges/help_me_unpack/problem?access_token=";
    private static string SubmitResultPath = "/challenges/help_me_unpack/solve?access_token=";

    public static async Task Main(string[] args){

        if(args.Length < 1){
            Console.WriteLine("Usage: dotnet run <access_token>");
            return;
        }
        var access_token = args[0];
        var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(Hostname + GetBytesPath + access_token);
        var input = await response.Content.ReadFromJsonAsync<RequestPayload>();
        Console.WriteLine($"Received input: {input?.Bytes}");

        string byteString;
        if (input?.Bytes == null)
        {
            Console.WriteLine("No input received");
            return;
        }
        else
        {
            byteString = input.Bytes;
        }

        byte[] bytes = Convert.FromBase64String(byteString);

        foreach (var b in bytes){
            Console.Write(b + " ");
        }
        Console.WriteLine($"\nTotal length of bytes {bytes.Length}");


        var intValue = BitConverter.ToInt32(bytes.AsSpan()[0..4]);
        var uintValue = BitConverter.ToUInt32(bytes.AsSpan()[4..8]);
        var int16Value = BitConverter.ToInt16(bytes.AsSpan()[8..12]);
        var floatValue = BitConverter.ToSingle(bytes.AsSpan()[12..16]);
        var doubleValue = BitConverter.ToDouble(bytes.AsSpan()[16..24]);

        var copy = bytes[24..32].ToList();
        copy.Reverse();
        var bigEndianDoubleValue = BitConverter.ToDouble(copy.ToArray());

        var responsePayload = new ResponsePayload
        {
            IntValue = intValue,
            UintValue = uintValue,
            Int16Value = int16Value,
            FloatValue = floatValue,
            DoubleValue = doubleValue,
            BigEndianDoubleValue = bigEndianDoubleValue
        };

        var responsePayloadJson = JsonSerializer.Serialize(responsePayload);
        Console.WriteLine($"Response payload: {responsePayloadJson}");

        var responsePayloadContent = new StringContent(responsePayloadJson, Encoding.UTF8, "application/json");
        var verdictResponse = await httpClient.PostAsync(Hostname + SubmitResultPath + access_token, responsePayloadContent);
        var verdictJson = await verdictResponse.Content.ReadAsStringAsync();
        Console.WriteLine($"Verdict: {verdictJson}");
    }
}