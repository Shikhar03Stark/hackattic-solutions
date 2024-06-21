using System.Text.Json.Serialization;

namespace HelpMeUnpack;

public class ResponsePayload
{
    [JsonPropertyName("int")]
    public int IntValue { get; set; }

    [JsonPropertyName("uint")]
    public uint UintValue { get; set; }

    [JsonPropertyName("short")]
    public int Int16Value { get; set; }

    [JsonPropertyName("float")]
    public float FloatValue { get; set; }

    [JsonPropertyName("double")]
    public double DoubleValue { get; set; }
    
    [JsonPropertyName("big_endian_double")]
    public double BigEndianDoubleValue { get; set; }


}
