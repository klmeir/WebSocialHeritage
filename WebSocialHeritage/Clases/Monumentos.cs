using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSocialHeritage
{
    public class Monumentos
    {
        private Int32 idmonumento;
        private Decimal longitud;
        private Decimal latitud;
        private String url_imag;
        private String nombre;
        private String descripcion; 


        public Monumentos() { 
        
        }

        public String Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }


        public String Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }


        public String Url_img
        {
            get { return url_imag; }
            set { url_imag = value; }
        }

        public Decimal Latitud
        {
            get { return latitud; }
            set { latitud = value; }
        }

        public Int32 Idmonumento
        {
            get { return idmonumento; }
            set { idmonumento = value; }
        }

        public Decimal Longitud
        {
            get { return longitud; }
            set { longitud = value; }
        }    
        
       }
}