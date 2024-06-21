using System.Text.Json.Serialization;

namespace HelpMeUnpack.DTO;

public class VerdictResponse
{
    [JsonPropertyName("result")]
    public string Result {get; set;}
}
