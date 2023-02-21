public class PhotoDAO{

     public Guid IdPhoto { get; set; }=Guid.NewGuid();

      public Guid IdObject { get; set; }
      public ObjectDAO Object { get; set; }

      public byte[]? ObjectPhoto {get; set;}
      public string Path {get;set;}
}