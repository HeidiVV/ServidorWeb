using MongoDB.Driver;

public static class CategoriasRequestHandler {
    public static IResult Crear(CategoriaDato categoriaDato) {
        if (string.IsNullOrWhiteSpace(categoriaDato.NombreCategorias)) {
            return Results.BadRequest("El nombre de la categoria no puede estar vacio");
        }
        if (string.IsNullOrWhiteSpace(categoriaDato.UrlIcono)) {
            return Results.BadRequest("El icono no puede estar vacio");
        }
        BaseDatos cdatos = new  BaseDatos();
        var coleccion = cdatos.ObtenerCollection<DatosCategorias>("Categorias");
        if(coleccion == null){
            throw new Exception("No existe la coleccion Categorias");

        }
        FilterDefinitionBuilder<DatosCategorias> filterBuilder = new FilterDefinitionBuilder<DatosCategorias>();
        var filter = filterBuilder.Eq(x =>x.NombreCategorias, categoriaDato.NombreCategorias);
         
         DatosCategorias? registro = coleccion.Find(filter).FirstOrDefault();
         if(registro != null){
            return Results.BadRequest($"Ya existe una categoria con el nombre {categoriaDato.NombreCategorias}");
         }
         registro = new DatosCategorias();
         registro.NombreCategorias = categoriaDato.NombreCategorias;
         registro.UrlIcono = categoriaDato.UrlIcono;

         coleccion!.InsertOne(registro);
         string nuevoId = registro.Id.ToString();

         return Results.Ok(nuevoId);
    }

    public static IResult Listar() {
        var filterBuilder = new FilterDefinitionBuilder<DatosCategorias>();
        var filter = filterBuilder.Empty;

        BaseDatos cdatos = new BaseDatos();
        var coleccion = cdatos.ObtenerCollection<DatosCategorias>("Categorias");
        List<DatosCategorias> mongoDbList = coleccion.Find(filter).ToList();

        var lista = mongoDbList.Select(x => new {
            Id = x.Id.ToString(),
            Nombre = x.NombreCategorias,
            UrlIcono = x.UrlIcono
        }).ToList();

        return Results.Ok(lista);
    }
}