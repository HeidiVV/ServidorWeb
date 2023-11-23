using MongoDB.Bson;

public class DatosInicioSesion {

    public ObjectId Id { get; set; }
    public string CorreoElectronico { get; set; } = "";
    public string Contraseña { get; set; } = "";
}