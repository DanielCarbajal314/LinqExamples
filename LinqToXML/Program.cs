using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LinqToXML
{
    class Program
    {
        static void Main(string[] args)
        {
            XDocument document = XDocument.Load("../../Catalog.xml");
            Program.ListAllArtistFromUK(document);
            Program.ImprimirSeparacion();
            Program.PrecioPromedio(document);
            Console.ReadKey();
        }

        static private void ListAllArtistFromUK(XDocument document)
        {
            var CDs = from element in document.Descendants("CATALOG").Elements("CD")
                      let country = (string)element.Element("COUNTRY")
                      where country.Equals("UK")
                      select new
                      {
                          Titulo = (string)element.Element("TITLE").Value,
                          Artista = (string)element.Element("ARTIST").Value
                      };
            var ListOfCDs = CDs.ToList();
            ListOfCDs.ForEach(x => Console.WriteLine(x.Titulo + " - " + x.Artista));
        }

        static private void PrecioPromedio(XDocument document)
        {
            var PaisPrecio =  from element in document.Descendants("CATALOG").Elements("CD")
                              select new
                              {
                                  Pais = (string)element.Element("COUNTRY").Value,
                                  precio = double.Parse(element.Element("PRICE").Value)                   
                              };

            var PaisPrecioPromedio =   from paisPrecio in PaisPrecio
                                       group paisPrecio by new { paisPrecio.Pais } into agrupamiento
                                       select new
                                       {
                                           Pais = agrupamiento.Key.Pais,
                                           PrecioPromedioPorCD = agrupamiento.Average(x => x.precio)
                                       };

            var ListaDePaisPrecioPromedios = PaisPrecioPromedio.ToList();
            ListaDePaisPrecioPromedios.ForEach(x => Console.WriteLine(x.Pais + " - " + x.PrecioPromedioPorCD));
        }

        static private void ImprimirSeparacion()
        {
            Console.WriteLine();
            Console.WriteLine("=========================");
            Console.WriteLine();
        }

    }
}
