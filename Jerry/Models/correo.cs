using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jerry.Models
{
    public class Correo
    {
        [Key]
        public int correoID { get; set; }

        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}