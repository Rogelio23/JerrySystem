using Jerry.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jerry.ViewModels
{
    public class ReservacionesViewModel
    {
        [Required]
        [Display(Name = "Nombre")]
        public string nombre { get; set; }

        [Required]
        [Display(Name = "Salon")]
        public string nombreSalon { get; set; }

        [Required]
        [Display(Name = "Inicio del evento")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fechaEventoInicial { get; set; }

        [Required]
        [Display(Name = "Fin del evento")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fechaEventoFinal { get; set; }

        [Required]
        [Display(Name = "Detalles")]
        public string Detalles { get; set; }

        [Required]
        [Display(Name = "Costo")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal costo { get; set; }

        [Required]
        public int salonID { get; set; }


        public ReservacionesViewModel() { }
        public ReservacionesViewModel(Reservacion reservacion)
        {
            this.nombre = reservacion.cliente.nombre;
            this.nombreSalon = reservacion.salon.nombre;
            this.fechaEventoInicial = reservacion.fechaEventoInicial;
            this.fechaEventoFinal = reservacion.fechaEventoFinal;
            this.Detalles = reservacion.Detalles;
            this.costo = reservacion.costo;
        }

    }
}