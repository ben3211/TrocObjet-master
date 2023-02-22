using System.Text.Json.Serialization;
public class UserModel {
     [JsonPropertyName("id")]
    public Guid IdUser { get; set; }
    [JsonPropertyName("lNa")]
    public string LastName{get;set;}

    [JsonPropertyName("fNa")]
    public string FirstName{get;set;}

     [JsonPropertyName("pn")]
    public string PhoneNumber{get;set;}

     [JsonPropertyName("c")]
    public string City{get;set;}
    public ICollection<ObjectDAO>? Objects{get;set;}
}