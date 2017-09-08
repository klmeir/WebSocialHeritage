using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSocialHeritage.Clases
{
    public class ContenidoPunto
    {
        private Int32 puntoMonumento;
        private String url_contenido;
        private String nombre;
        private String descripcion;




        public String Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public Int32 PuntoMonumento
        {
            get { return puntoMonumento; }
            set { puntoMonumento = value; }
        }

        public String Url_contenido
        {
            get { return url_contenido; }
            set { url_contenido = value; }
        }

        public String Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        
    }
}