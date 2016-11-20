using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jerry.Models
{
    public class Reservacion
    { 
        [Key]
        public int reservacionID { get; set; }

        [Required]
        [Display(Name = "Fecha de Reservación")]
        [DataType(DataType.Date)]
        public DateTime fechaReservacion { get; set; }

        [Required]
        [Display(Name = "Fecha del Evento")]
        [DataType(DataType.Date)]
        public DateTime fechaEvento { get; set; }

        [Required]
        [Display(Name ="Costo")]
        public decimal costo { get; set; }

        [Required]
        [Display(Name ="Detalles")]
        public string Detalles { get; set; }

        [Required]
        public int salonID { get; set; }
        //Una reservacion es unicamente a un salon
        virtual public Salon salon { get; set; }

        [Required]
        [Display(Name ="Cliente")]
        public int clienteID { get; set; }
        //Una reservacion pertenece unicamente a un cliente
        virtual public Cliente cliente { get; set; }

        //Una reservacion puede tener muchos pagos asociados a ella.
        virtual public ICollection<Pago> pagos { get; set; }

    }
}