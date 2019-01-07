using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClassicGarage.Models
{
    [Table("Repairs")]
    public class RepairModel
    {

        public int ID { get; set; }

        public int? CarID { get; set; }
        [DisplayName("Nazwa")]
        public string Name { get; set; }
        [DisplayName("Opis")]
        public string Description { get; set; }
        [DisplayName("Cena")]
        public int? Price { get; set; }

        
        public string Email { get; set; }
        public virtual Cars Car { get; set; }

        public virtual ICollection<CarParts> Part { get; set; }
    }
}