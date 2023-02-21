public class ObjectDAO
{
    public Guid IdObject { get; set; } = Guid.NewGuid();
    public Guid IdOwner { get; set; }
    public AppUserDAO Owner { get; set; }
    public string Label { get; set; }
    public string Description { get; set; }
    public decimal EstimatedPrice { get; set; }
    public Guid? IdPhoto { get; set; }
    public PhotoDAO? Photo { get; set; }
    public ICollection<LoanDAO> Loans { get; set; }
    public ICollection<PhotoDAO>? Photos { get; set; }
}