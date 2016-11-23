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
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime fechaReservacion { get; set; }

        [Required]
        [Display(Name = "Inicio del evento")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime fechaEventoInicial { get; set; }

        [Required]
        [Display(Name = "Fin del evento")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime fechaEventoFinal { get; set; }

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

        
        public static bool validarFecha(Reservacion reservacion)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            DateTime fechaI = reservacion.fechaEventoInicial;
            DateTime fechaF = reservacion.fechaEventoFinal;
            var query = db.reservaciones.Where(res=>res.fechaEventoInicial<=fechaI && res.fechaEventoFinal >= fechaI || res.fechaEventoInicial <= fechaF && res.fechaEventoFinal >= fechaF).Count();
            //var query2= db.reservaciones.Where(res => res.fechaEventoFinal >= fechaI).Count();
            if (query > 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }


    }
}

