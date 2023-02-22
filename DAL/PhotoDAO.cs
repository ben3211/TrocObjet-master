public class PhotoDAO{

     public Guid IdPhoto { get; set; }=Guid.NewGuid();

      public Guid IdObject { get; set; }
      public ObjectDAO Object { get; set; }

      public byte[]? Bytes {get; set;}
      public string Commentaire {get;set;}
}