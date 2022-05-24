using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TempReport
{
    public class Report1Row
    {
        public string NazivArtikla { get; set; }
        public double Cijena { get; set; }
        public DateTime DatumObjave { get; set; }
        public string Godiste { get; set; }

        public static List<Report1Row> Get()
        {
            return new List<Report1Row> { };
        }
    }
}