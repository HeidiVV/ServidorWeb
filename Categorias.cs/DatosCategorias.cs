using MongoDB.Bson;

public class DatosCategorias {
    
    public ObjectId Id { get; set; }
    public string NombreCategorias { get; set; } = string.Empty;
    public string UrlIcono { get; set; } = string.Empty;

}