public class AppUserDAO{

    public Guid IdUser { get; set; }

    public AccountDAO Account{get;set;}


    public string LastName{get;set;}

    public string FirstName{get;set;}

    public string PhoneNumber{get;set;}

    
    public string City{get;set;}

     public ICollection<LoanDAO>? Loans{get;set;}

      public ICollection<ObjectDAO> Objects{get;set;}
}