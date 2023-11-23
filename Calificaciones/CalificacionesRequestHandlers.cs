public static class CalificacionesRequestHandlers {
    public static IResult MostrarCalificacionesAlumno(long numControl,
        int parcial, bool soloAsignaturas, bool soloAcreditadas) {
        if(numControl == 22328051050202){
            List<CalificacionMateria> list = new List<CalificacionMateria>();

            CalificacionMateria m1 = new CalificacionMateria();
            m1.Calificacion = 10;
            m1.Materia = "Programacion Orientada a Objetos";
            m1.Parcial = 1;
            m1.NumControl = 22328051050202;

            CalificacionMateria m2 = new CalificacionMateria();
            m2.Calificacion = 9;
            m2.Materia = "Programacion Orientada a Eventos";
            m1.Parcial = 1;
            m1.NumControl = 22328051050202;

            CalificacionMateria m3 = new CalificacionMateria();
            m3.Calificacion = 7.2;
            m3.Materia = "GeoTri";
            m3.Parcial = 1;
            m3.NumControl = 22328051050202;

            CalificacionMateria m4 = new CalificacionMateria();
            m4.Calificacion = 7.5;
            m4.Materia = "Etica";
            m4.Parcial = 1;
            m4.NumControl = 22328051050202;

            CalificacionMateria m5 = new CalificacionMateria();
            m5.Calificacion = 9;
            m5.Materia = "Ingles";
            m5.Parcial = 1;
            m5.NumControl = 22328051050202;

            CalificacionMateria m6 = new CalificacionMateria();
            m6.Calificacion = 7.7;
            m6.Materia = "Biologia";
            m6.Parcial = 1;
            m6.NumControl = 22328051050202;

            list.Add(m1);
            list.Add(m2);
            list.Add(m3);
            list.Add(m4);
            list.Add(m5);
            list.Add(m6);

            return Results.Ok(list);
        }
        else {
            return Results.NotFound($"No existe un alumno con número de control {numControl}");
        }
    }
}