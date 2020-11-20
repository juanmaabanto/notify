using System;
using System.Collections.Generic;
using System.Linq;
using Expertec.Sigeco.CrossCutting.SeedWork.Domain;
using Expertec.Lcc.Services.Notify.API.Infrastructure.Exceptions;

namespace Expertec.Lcc.Services.Notify.API.Models
{
    public class TipoAlerta : Enumeration
    {
        #region Variables

        public static TipoAlerta Advertencia = new TipoAlerta(1, "warning", "fas fa-exclamation-triangle");
        public static TipoAlerta Error = new TipoAlerta(2, "danger", "fas fa-exclamation-circle");
        public static TipoAlerta Exito = new TipoAlerta(3, "success", "fas fa-check-circle");
        public static TipoAlerta Informacion = new TipoAlerta(4, "info", "fas fa-info-circle");
        public string IconoCls { get; set; }

        #endregion

        #region Constructor

        protected TipoAlerta() {}

        protected TipoAlerta(int TipoAlertaId, string nombre, string iconoCls) : base(TipoAlertaId, nombre) 
        {
            IconoCls = iconoCls;
        }

        #endregion

        #region Metodos

        public static IEnumerable<TipoAlerta> List() =>
            new [] { Advertencia, Error, Exito, Informacion };

        public static TipoAlerta FromName(string name)
        {
            var state = List().SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new NotifyDomainException($"Valores posibles para Tipo Notificación: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static TipoAlerta From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new NotifyDomainException($"Valores posibles para Tipo Notificación: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        #endregion
    }
}