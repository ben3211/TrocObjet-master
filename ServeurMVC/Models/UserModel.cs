public class UserModel {
    public Guid IdUser { get; set; } = Guid.NewGuid();
    public string LastName{get;set;}
    public string FirstName{get;set;}
    public string PhoneNumber{get;set;}
    public string City{get;set;}
    public ICollection<ObjectDAO> Objects{get;set;}
}