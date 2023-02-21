using System.Text.Json.Serialization;
public class SearchResult
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    [JsonPropertyName("d")]
    public string Description { get; set; }
    [JsonPropertyName("l")]
    public string Label { get; set; }
}