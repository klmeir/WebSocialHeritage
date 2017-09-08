using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSocialHeritage.Clases
{
    public class Comentario
    {

        private String comment;
        private String usuario;
        private float rating;

        public String Comment
        {
            get { return comment; }
            set { comment = value; }
        }

        public String Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        public float Rating
        {
            get { return rating; }
            set { rating = value; }
        } 


    }
}