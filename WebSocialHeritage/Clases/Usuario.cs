using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSocialHeritage.Clases
{
    public class Usuario
    {
        private int id;
        private String nombre;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public String Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

       
    }
}