using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClassicGarage.Models
{
    [Table("Owner")]
    public class OwnerModel
    {
        public int ID { get; set; }
        [DisplayName("Imię")]
        public string FirstName { get; set; }
        [DisplayName("Nazwisko")]
        public string LastName { get; set; }
        [DisplayName("Telefon")]
        public string Phone { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }

        public virtual ICollection<Cars> Cars { get; set; }
    }
}