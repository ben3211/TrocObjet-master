public class LoanDAO
{

    public Guid IdLoan { get; set; } = Guid.NewGuid();

    public Guid IdBorrower { get; set; }
    public AppUserDAO Borrower { get; set; }

    public Guid IdObject { get; set; }
    public ObjectDAO Object { get; set; }
    public int NoteBorrower { get; set; }

    public int NoteOwner { get; set; }

    public DateTime BookingDateStart { get; set; }// date de réservation début
    public DateTime BookingDateEnd { get; set; }// date de réservation fin

    public DateTime LoanDateStart { get; set; }// date effective de prêt
    public DateTime LoanDateEnd { get; set; }// date effective de fin de prêt

    public int validation { get; set; }

    public string CommentOwner { get; set; }
    public string CommentBorrower { get; set; }




}