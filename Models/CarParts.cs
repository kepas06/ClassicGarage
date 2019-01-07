using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClassicGarage.Models
{
    [Table("Parts")]
    public class CarParts
    {
        public int ID { get; set; }
        [DisplayName("Samochód")]
        public int? CarID { get; set; }
        [DisplayName("Część Naprawy")]
        public int? RepairID { get; set; }
        [DisplayName("Nazwa")]
        public string Name { get; set; }

        [DisplayName("Cena Zakupu")]
        public int Cost_Buy { get; set; }
 
        [DisplayName("Cena sprzedazy")]
        public int? Cost_Sell { get; set; }

        [DisplayName("Data Zakupu")]
        [DisplayFormat(DataFormatString = "{0:d}")]

        public DateTime Buy_Date { get; set; }
        [DisplayName("Data sprzedaży")]

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? Sell_Date { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        public virtual Cars Car { get; set; }
        public virtual RepairModel Repair { get; set; }
    }
}