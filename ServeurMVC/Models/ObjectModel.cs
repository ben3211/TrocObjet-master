using System.Text.Json.Serialization;
public class ObjectModel
{
    [JsonPropertyName("id")]
    public Guid IdObject { get; set; }

    [JsonPropertyName("ow")]
    public AppUserDAO? Owner { get; set; }

    [JsonPropertyName("l")]
    public string Label { get; set; }
    [JsonPropertyName("d")]
    public string Description { get; set; }

    [JsonPropertyName("ep")]
    public decimal EstimatedPrice { get; set; }

    
    [JsonPropertyName("idp")]
    public IEnumerable<Guid>? IdPhotos { get; set; }
    


}