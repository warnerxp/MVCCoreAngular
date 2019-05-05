using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace MvcBandas.ViewModels
{
    public class ListadoViewModel<T>
    {
        public string TerminoBusqueda { get; set; }
        public IPagedList<T> Registros { get; set; }
        public int? Pagina { get; set; }
    }
}
