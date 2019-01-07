using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClassicGarage.Models
{
    [Table("Adverts")]
    public class Adverts
    {
        [Key]

        public int ID { get; set; }
        [DisplayName("Auto")]
        public int CarID { get; set; }
        [DisplayName("Opis")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [DisplayName("Czy aktywne")]
        public bool Active { get; set; }
        [DisplayName("Cena")]
        public int Price { get; set; }
        [DisplayName("Obraz")]
        public string Photo { get; set; }


        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Telefon")]
        public int phone { get; set; }


        public virtual Cars Car { get; set; }

        //cos tu nie gra XD
    }
}