using MongoDB.Bson;

public class DatosRegistro {
    public ObjectId Id { get; set; }
    public string CorreoElectronico { get; set; } = "";
    public string Contraseña { get; set;} =  "";
    public string Usuario { get; set; } = "";
}