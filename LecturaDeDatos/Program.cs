using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace LecturaDeDatos
{
    class Program
    {
        static void Main(string[] args)
        {
            Program.ListarPersonasConSusGrupo();
            Program.ImprimirSeparacion();
            Program.ListarGruposConElNumeroDeMiembrosInscritos();
            Program.ImprimirSeparacion();
            Program.ListarGruposConEdadPromedio();
            Console.ReadKey();
        }

        private static void ListarPersonasConSusGrupo() {
            using (EntityFrameworkForLinqEntities db = new EntityFrameworkForLinqEntities())
            {
                var query = from persona in db.Persona
                            join grupo in db.GrupoDePersonas on persona.IdGrupoDePersonas equals grupo.Id
                            select new
                            {
                                NombreDeLaPersona = persona.Nombre,
                                NombreDelGrupoAlQuePertenece = grupo.Nombre
                            };

                query.ToList().ForEach(x => Console.WriteLine(x.NombreDeLaPersona + " - " + x.NombreDelGrupoAlQuePertenece));
            }
        }


        private static void ListarGruposConElNumeroDeMiembrosInscritos()
        {
            using (EntityFrameworkForLinqEntities db = new EntityFrameworkForLinqEntities())
            {
                var query = from persona in db.Persona
                            join grupo in db.GrupoDePersonas on persona.IdGrupoDePersonas equals grupo.Id
                            group grupo by new { grupo.Nombre } into agrupamiento
                            select new
                            {
                                NombreDelGrupo = agrupamiento.Key.Nombre,
                                NumeroDePersonasInscritas = agrupamiento.Count(),
                            };

                query.ToList().ForEach(x => Console.WriteLine(x.NombreDelGrupo + " - " + x.NumeroDePersonasInscritas));
            }
        }

        private static void ListarGruposConEdadPromedio()
        {
            using (EntityFrameworkForLinqEntities db = new EntityFrameworkForLinqEntities())
            {
                var queryDeferido = from persona in db.Persona
                                    join grupo in db.GrupoDePersonas on persona.IdGrupoDePersonas equals grupo.Id
                                    select new
                                    {
                                        NombreDelGrupo = grupo.Nombre,
                                        EdadDeUnMiembro = persona.Edad
                                    };

                var queryDiferidoAnidado = from grupoEdad in queryDeferido
                                           group grupoEdad by new { grupoEdad.NombreDelGrupo } into agrupamiento
                                           select new {
                                               NombreDelGrupo = agrupamiento.Key.NombreDelGrupo,
                                               EdadPromedioEnElGrupo = agrupamiento.Average(x=>x.EdadDeUnMiembro)
                                           };

                var queryEjecutadoYCargadoEnLista = queryDiferidoAnidado.ToList();
                queryEjecutadoYCargadoEnLista.ForEach(x => Console.WriteLine(x.NombreDelGrupo + " - " + x.EdadPromedioEnElGrupo));

            }
        }


        static private void ImprimirSeparacion()
        {
            Console.WriteLine();
            Console.WriteLine("=========================");
            Console.WriteLine();
        }

    }
}
