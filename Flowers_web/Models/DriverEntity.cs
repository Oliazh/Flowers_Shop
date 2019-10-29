using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flowers_web.Models
{
   public class Route
    {
        public int OrderId { get; set; }

        /// <summary>
        /// Конечный путь
        /// </summary>
        public string EndAddress { get; set; }

        [DisplayName("Distance in km.")] public double Distance { get; set; }
        public TimeSpan Time { get; set; }
        public string Status { get; set; }
    }

  public  class DriverEntity
    {
        public List<Route> Routes { get; set; }
        public string DriverName { get; set; }

        public DriverEntity(string name)
        {
            DriverName = name;
            Routes = new List<Route>();

        }
    }
}