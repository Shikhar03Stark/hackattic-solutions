using System.Text.Json.Serialization;

namespace HelpMeUnpack.DTO;

public class RequestPayload
{
    [JsonPropertyName("bytes")]
    public string? Bytes { get; set;}
}
