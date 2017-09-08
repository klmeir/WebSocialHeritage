using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using WebSocialHeritage.Clases;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;


namespace WebSocialHeritage
{
    /// <summary>
    /// Descripción breve de WebService1
    /// </summary>
    [WebService(Namespace = "http://socialheritage/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        //const string ruta = "server=mysql14337-env-2222075.j.facilcloud.com; database=socialheritage; Uid=root; pwd=DYCcva86319;";
       // const string ruta = "server=sql3.freesqldatabase.com; database=sql3138637; Uid=sql3138637; pwd=BVlbuvJWTu;";

        const string ruta = "server=MYSQL5006.myASP.NET; database=db_a115f3_social; Uid=a115f3_social; pwd=social123;";


        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void GetMonenPuntos(int idUsuaio)
        {

           
            List<Monumentos> listMonumentos = new List<Monumentos>();

            var json = "";
            MySqlConnection conexion = new MySqlConnection(ruta);
            conexion.Open();

            MySqlCommand command = conexion.CreateCommand();
            if (idUsuaio == 1)
            command.CommandText = ("SELECT p.idPun_Patr, p.lat, p.long, p.url_img_punto, p.nombre, p.descripcion  FROM punto_patrimonial p where p.publicado = 1");
            else
                command.CommandText = ("SELECT p.idPun_Patr, p.lat, p.long, p.url_img_punto, p.nombre, p.descripcion  FROM punto_patrimonial p where p.publicado = 0");


            command.Connection = conexion;

            MySqlDataReader reader = command.ExecuteReader();


            while (reader.Read())
            {
                Monumentos mou = new Monumentos();
                
                mou.Idmonumento = reader.GetInt32(0);
                mou.Latitud = reader.GetDecimal(1);
                mou.Longitud = reader.GetDecimal(2);
                mou.Url_img = reader.GetString(3);
                mou.Nombre = reader.GetString(4);
                mou.Descripcion = reader.GetString(5);


               
                

                listMonumentos.Add(mou);
            }

            command.Connection.Close();

            //json = jss.Serialize(listMonumentos);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";

            JavaScriptSerializer jss = new JavaScriptSerializer();
            Context.Response.Write(jss.Serialize(listMonumentos));
           // return json;
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void GetContVideosAudiosPunto(int idpunto, int idtipocont)
        {

            List<ContenidoPunto> listaContenidos = new List<ContenidoPunto>();
            MySqlConnection conexion = new MySqlConnection(ruta);
            conexion.Open();

            MySqlCommand command = conexion.CreateCommand();
            command.CommandText = ("SELECT c.idContenido, c.idPun_Patr, c.url, c.nombre, c.descripcion  FROM contenido c WHERE c.idTipoCont = " + idtipocont + " and c.idPun_Patr = " + idpunto);
            command.Connection = conexion;

            MySqlDataReader reader = command.ExecuteReader();


            while (reader.Read())
            {
                ContenidoPunto contenido = new ContenidoPunto();


                contenido.PuntoMonumento = reader.GetUInt16(1);
                contenido.Url_contenido = reader.GetString(2);
                contenido.Nombre = reader.GetString(3);
                contenido.Descripcion = reader.GetString(4);


                listaContenidos.Add(contenido);
            }

            command.Connection.Close();

            //json = jss.Serialize(listMonumentos);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";

            JavaScriptSerializer jss = new JavaScriptSerializer();
            Context.Response.Write(jss.Serialize(listaContenidos));

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void GetContenidosImagenesDePunto(int idpunto)
        {

            List<ContenidoPunto> listaContenidos = new List<ContenidoPunto>();
            MySqlConnection conexion = new MySqlConnection(ruta);
            conexion.Open();

            MySqlCommand command = conexion.CreateCommand();
            command.CommandText = ("SELECT c.idContenido, c.idPun_Patr, c.url, c.nombre  FROM contenido c WHERE c.idTipoCont = 1 and c.idPun_Patr = " + idpunto);
            command.Connection = conexion;

            MySqlDataReader reader = command.ExecuteReader();


            while (reader.Read())
            {
                ContenidoPunto contenido = new ContenidoPunto();


                contenido.PuntoMonumento = reader.GetUInt16(1);
                contenido.Url_contenido = reader.GetString(2);
                contenido.Nombre = reader.GetString(3);

                listaContenidos.Add(contenido);
            }

            command.Connection.Close();

            //json = jss.Serialize(listMonumentos);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";

            JavaScriptSerializer jss = new JavaScriptSerializer();
            Context.Response.Write(jss.Serialize(listaContenidos));

        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void InsertPunto(String descrip, String imgbase64, String lati, String longitud, String url, String nom)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";

            //String PathServerAppSocial = "http://sheritage.somee.com/";
            String PathServerAppSocial = "http://jhonatanms56-001-site1.etempurl.com/img/";
            String resul = "";
            // Convert base 64 string to byte[]
            byte[] imageBytes = Convert.FromBase64String(imgbase64);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);

            Random r = new Random(DateTime.Now.Millisecond);
            int nameRandomForNow = r.Next(10000, 99999);

            var result = "n";

           
            MySqlConnection conexion = new MySqlConnection(ruta);
            conexion.Open();

            MySqlCommand command = conexion.CreateCommand();
            

            try
            {

                //String filepath = "D:/DZHosts/LocalUser/andfer25/www.sheritage.somee.com/" + nameRandomForNow + ".Jpeg";
                String filepath = "h:/root/home/jhonatanms56-001/www/site1/img/" + nameRandomForNow + ".Jpeg";
                
                image.Save(filepath, ImageFormat.Jpeg);

                String sentenci = "INSERT INTO punto_patrimonial ( `descripcion`, `lat`, `long`, `url_img_punto`, `nombre`) " +
                   "VALUES ('" + descrip + "', " + lati + ", " + longitud + ", '" + PathServerAppSocial + nameRandomForNow + ".Jpeg" + "', '" + nom + "');";


                command.CommandText = (sentenci);
                command.Connection = conexion;
                command.ExecuteNonQuery();


               Context.Response.Write(result = "Insert Exito");
                command.Connection.Close();

            }
            catch (Exception e)
            {

                Context.Response.Write(result = "Error al Crear Monumento");
            }

            //Context.Response.Write(result = "Monumento Creado");
            //command.Connection.Close();

        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void aprobarMonumento(int idMonumento)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";

            MySqlConnection conexion = new MySqlConnection(ruta);
            conexion.Open();

            MySqlCommand command = conexion.CreateCommand();

            String result = "";
            try
            {

                String sentenci = "UPDATE punto_patrimonial SET publicado = 1 WHERE   idPun_Patr = "+ idMonumento+"";


                command.CommandText = (sentenci);
                command.Connection = conexion;
                command.ExecuteNonQuery();


                Context.Response.Write(result = "Actualizacion Exitosa");
                command.Connection.Close();

            }
            catch (Exception e)
            {

                Context.Response.Write(result = "Error al Crear Monumento");
            }

            //Context.Response.Write(result = "Monumento Creado");
            //command.Connection.Close();

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void CargarImag(String imgbase64, int idtipocont, int idpuntopatr, String descripcion)
        {
            //String PathServerAppSocial = "http://socialheritage.somee.com/";
            String PathServerAppSocial = "http://jhonatanms56-001-site1.etempurl.com/img/";
            String resul = "";
            // Convert base 64 string to byte[]
            byte[] imageBytes = Convert.FromBase64String(imgbase64);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);

           MySqlConnection conexion = new MySqlConnection(ruta);
            conexion.Open();

            MySqlCommand command = conexion.CreateCommand();
            

            Random r = new Random(DateTime.Now.Millisecond);
            int nameRandomForNow = r.Next(10000, 99999);
            try
            {
                //String filepath = "D:/DZHosts/LocalUser/andfer25/www.SHeritage.somee.com/" + nameRandomForNow + ".Jpeg";
                String filepath = "h:/root/home/jhonatanms56-001/www/site1/img/" + nameRandomForNow + ".Jpeg";
                image.Save(filepath, ImageFormat.Jpeg);

                command.CommandText = ("INSERT INTO contenido (`idTipoCont`, `idPun_Patr`, `url`, `nombre`, `descripcion`) " +
                                                    "VALUES (" + idtipocont + ", " + idpuntopatr + ", '" + PathServerAppSocial + nameRandomForNow + ".Jpeg" + "', '" + nameRandomForNow + "', '" + descripcion + "');");
                command.Connection = conexion;

                command.ExecuteNonQuery();

                resul = "Imagen Guardada";
            }
            catch (Exception ex)
            {

                resul = "Error al Guardar" + ex.GetBaseException();
            }

           // command.Connection.Close();


            Context.Response.Clear();
            Context.Response.ContentType = "application/json";

            Context.Response.Write(resul);

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void ValidarUsuario(string user, string pass)
        {

            int validUser = 0;
            Usuario usu = new Usuario();
            MySqlConnection conexion = new MySqlConnection(ruta);
            conexion.Open();

            MySqlCommand command = conexion.CreateCommand();
            command.CommandText = ("select idusuarios, user from usuarios usu where usu.user = '"+user+"' AND usu.password = '"+pass+"'");
            command.Connection = conexion;

            MySqlDataReader reader = command.ExecuteReader();


            while (reader.Read())
            {
                
                usu.Id = reader.GetInt32(0);
                usu.Nombre = reader.GetString(1);

            }


            command.Connection.Close();

            //json = jss.Serialize(listMonumentos);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";

            JavaScriptSerializer jss = new JavaScriptSerializer();
            Context.Response.Write(jss.Serialize(usu));

        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void AgregarUsuario(String user, String pass)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";

            var result = "n";
            MySqlConnection conexion = new MySqlConnection(ruta);
            conexion.Open();

            MySqlCommand command = conexion.CreateCommand();
            command.CommandText = ("INSERT INTO usuarios (`user`, `password`) " +
                                    "VALUES ('" + user + "', '" + pass + "');");
            command.Connection = conexion;

            try
            {
                command.ExecuteNonQuery();


                //Context.Response.Write(result = "Insert Exitoso");
                //command.Connection.Close();

            }
            catch (Exception e)
            {

                Context.Response.Write(result = "Error al Crear Usuario");
            }

            Context.Response.Write(result = "Usuario Creado");
            command.Connection.Close();

        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void AgregarComentario(String cometario, int idpunto, int idusuario, String ratingcoment)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";

            var result = "n";
            MySqlConnection conexion = new MySqlConnection(ruta);
            conexion.Open();

            MySqlCommand command = conexion.CreateCommand();
            command.CommandText = ("INSERT INTO comentarios (`comentario`, `idPun_Patr`,`iduser`,`comenrating`) "+
                                    "VALUES ('" + cometario + "', " + idpunto + ", " + idusuario + ", " + ratingcoment + ");");
            command.Connection = conexion;

            try
            {
                command.ExecuteNonQuery();


            }
            catch (Exception e)
            {

                Context.Response.Write(result = "Error al Crear Comentario");
            }

            Context.Response.Write(result = "Comentario Creado");
            command.Connection.Close();

        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void GetComentariosDelPunto(int idpunto)
        {

            List<Comentario> listaComentarios = new List<Comentario>();
            MySqlConnection conexion = new MySqlConnection(ruta);
            conexion.Open();

            MySqlCommand command = conexion.CreateCommand();
            command.CommandText = ("SELECT c.comentario, c.comenrating	FROM comentarios c WHERE c.idPun_Patr = " + idpunto);
            command.Connection = conexion;

            MySqlDataReader reader = command.ExecuteReader();


            while (reader.Read())
            {
                Comentario coment = new Comentario();


                coment.Comment = reader.GetString(0);
                coment.Rating = reader.GetFloat(1);

                listaComentarios.Add(coment);
            }

            command.Connection.Close();

            //json = jss.Serialize(listMonumentos);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";

            JavaScriptSerializer jss = new JavaScriptSerializer();
            Context.Response.Write(jss.Serialize(listaComentarios));

        }




    }
}
