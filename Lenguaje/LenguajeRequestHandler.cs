using System.Text.RegularExpressions;
using MongoDB.Bson;
using MongoDB.Driver;

public static class LenguajeRequestHandler {

    public static IResult ListarRegistros(string idCategoria){

        var filterBuilder = new FilterDefinitionBuilder<DatosLenguaje>();
        var filter = filterBuilder.Eq(x => x.IdCategoria, idCategoria);
        
        BaseDatos bd = new BaseDatos();
        var coleccion = bd.ObtenerCollection<DatosLenguaje>("Lenguaje");
        var lista = coleccion.Find(filter).ToList();

        return Results.Ok(lista.Select(x => new {
            Id = x.Id.ToString(),
            IdCategoria = x.IdCategoria,
            Titulo = x.Titulo,
            Descripcion = x.Descripcion,
            EsVideo = x.EsVideo,
            Url = x.Url
        }).ToList());
    }

    public static IResult CrearRegistro (LenguajeDato lenguajeDTO) {
        if (string.IsNullOrWhiteSpace(lenguajeDTO.IdCategoria)) {
            return Results.BadRequest("El apartado no puede estar vacio");
        }
        if (string.IsNullOrWhiteSpace(lenguajeDTO.Descripcion)) {
            return Results.BadRequest("La descripción no puede estar vacia");
        }
        if (string.IsNullOrWhiteSpace(lenguajeDTO.Titulo)) {
            return Results.BadRequest("El titulo no puede estar vacio");
        }
        if (string.IsNullOrWhiteSpace(lenguajeDTO.Url)) {
            return Results.BadRequest("El URL no puede estar vacio");
        }
        if (!ObjectId.TryParse(lenguajeDTO.IdCategoria, out ObjectId idCategoria)) {
            return Results.BadRequest($"El Id de la categoria ({lenguajeDTO.IdCategoria}) no es valido");
        }
        BaseDatos bd = new BaseDatos();

        var filterBuilderCategorias = new FilterDefinitionBuilder<DatosCategorias>();
        var filterCategoria = filterBuilderCategorias.Eq(x => x.Id, idCategoria);
        var coleccionCategoria = bd.ObtenerCollection<DatosCategorias>("Categorias");
        var categoria = coleccionCategoria.Find(filterCategoria).FirstOrDefault();

        if (categoria == null) {
            return Results.NotFound($"No existe una categoria con ID = `{lenguajeDTO.IdCategoria}`");
        }
        
        DatosLenguaje registro = new DatosLenguaje();
        registro.Titulo = lenguajeDTO.Titulo;
        registro.EsVideo = lenguajeDTO.EsVideo;
        registro.Descripcion = lenguajeDTO.Descripcion;
        registro.Url = lenguajeDTO.Url;
        registro.IdCategoria = lenguajeDTO.IdCategoria;

        var coleccionLenguaje = bd.ObtenerCollection<DatosLenguaje>("Lenguaje");
        coleccionLenguaje!.InsertOne(registro);

        return Results.Ok(registro.Id.ToString());
    }

    public static IResult Eliminar (string id) {
        if (!ObjectId.TryParse(id, out ObjectId idLenguaje)) {
            return Results.BadRequest($"El ID proporcionado ({id}) no es válido");
        }

        BaseDatos bd =new BaseDatos();
        var filterBuilder = new FilterDefinitionBuilder<DatosLenguaje>();
        var filter = filterBuilder.Eq(x => x.Id, idLenguaje);
        var coleccion = bd.ObtenerCollection<DatosLenguaje>("Lenguaje");
        coleccion!.DeleteOne(filter);

        return Results.NoContent();
    }

    public static IResult Buscar(string texto) {
        var queryExpr = new BsonRegularExpression(new Regex(texto, RegexOptions.IgnoreCase));
        var filterBuilder = new FilterDefinitionBuilder<DatosLenguaje>();
        var filter = filterBuilder.Regex("Titulo", queryExpr) |
            filterBuilder.Regex("Descripcion", queryExpr);

        BaseDatos bd = new BaseDatos();
        var coleccion = bd.ObtenerCollection<DatosLenguaje>("Lenguaje");
        var lista = coleccion.Find(filter).ToList();
        
        return Results.Ok(lista.Select(x => new {
            Id = x.Id.ToString(),
            IdCategoria = x.IdCategoria,
            Titulo = x.Titulo,
            Descripcion = x.Descripcion,
            EsVideo = x.EsVideo,
            Url = x.Url
        }).ToList());
    }
} 