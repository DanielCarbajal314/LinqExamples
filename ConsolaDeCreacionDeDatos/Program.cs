using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;


namespace ConsolaDeCreacionDeDatos
{
    class Program
    {
        static void Main(string[] args)
        {
            EntityFrameworkForLinqEntities db = new EntityFrameworkForLinqEntities();
            GrupoDePersonas Niños = generarGrupo(db,"Niños");
            GrupoDePersonas Adolecentes = generarGrupo(db, "Adolecentes");
            GrupoDePersonas Adultos = generarGrupo(db, "Adultos");
            generarPersona(db, "Daniel Carbajal",  28, Adultos.Id);
            generarPersona(db, "Javier Espinoza", 38, Adultos.Id);
            generarPersona(db, "Luis Rodriguez", 22, Adultos.Id);
            generarPersona(db, "Manuel Escobedo", 34, Adultos.Id);
            generarPersona(db, "Paola Ravelo", 44, Adultos.Id);
            generarPersona(db, "Paola Ravelo", 10, Niños.Id);
            generarPersona(db, "Claudia Arbe", 7, Niños.Id);
            generarPersona(db, "Alexandra Del Pino", 8, Niños.Id);
            generarPersona(db, "Carlos Neyra", 12, Niños.Id);
            generarPersona(db, "Gustavo Torpoco", 13, Niños.Id);
            generarPersona(db, "Manuel Esquives", 5, Niños.Id);
            generarPersona(db, "Paolo Maldonado", 4, Niños.Id);
            generarPersona(db, "Luisa De La Roza", 14, Adolecentes.Id);
            generarPersona(db, "Carmen Chu", 17, Adolecentes.Id);
            generarPersona(db, "Julia Juventus", 18, Adolecentes.Id);
            generarPersona(db, "Rodrigo Rivadeneira", 14, Adolecentes.Id);
            generarPersona(db, "Paul Del Prado", 15, Adolecentes.Id);
            generarPersona(db, "Juan Ugalde", 16, Adolecentes.Id);
        }

        static private GrupoDePersonas generarGrupo(EntityFrameworkForLinqEntities db, string NombreDelGrupo) {
            GrupoDePersonas grupo = db.GrupoDePersonas.FirstOrDefault(x => x.Nombre.Equals(NombreDelGrupo));
            if (grupo == null)
            {
                grupo = new GrupoDePersonas();
                grupo.Nombre = NombreDelGrupo;
                db.GrupoDePersonas.Add(grupo);
                db.SaveChanges();
            }
            return grupo;
       }

        static private Persona generarPersona(EntityFrameworkForLinqEntities db, string nombrePersona,int edad, int idDelGrupo)
        {
            Persona persona = db.Persona.FirstOrDefault(x => x.Nombre.Equals(nombrePersona));
            if (persona == null)
            {
                persona = new Persona();
                persona.Nombre = nombrePersona;
                persona.Edad = edad;
                persona.IdGrupoDePersonas = idDelGrupo;
                db.Persona.Add(persona);
                db.SaveChanges();
            }
            return persona;
        }

    }
}
