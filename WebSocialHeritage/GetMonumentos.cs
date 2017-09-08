using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Web.Script.Serialization;
using System.Web.Script.Services;

namespace WebSocialHeritage
{
    public class GetMonumentos : IHttpHandler
    {

        public bool IsReusable {

            get {
                return true;
            }
        }


        public void ProcessRequest(HttpContext context) { 
        
        
            try{
                switch(context.Request.HttpMethod){
                
                    case "POST" :

                        break;
                        
                    case "GET":

                         string ruta = "server=sql5.freesqldatabase.com; database=sql5126778; Uid=sql5126778; pwd=RIv77yWahZ;";

                                List<Monumentos> listMonumentos = new List<Monumentos>();

                                var json = "";
                                MySqlConnection conexion = new MySqlConnection(ruta);
                                conexion.Open();

                                MySqlCommand command = conexion.CreateCommand();
                                command.CommandText = ("SELECT p.idPun_Patr, p.lat, p.long  FROM punto_patrimonial p");
                                command.Connection = conexion;

                                MySqlDataReader reader = command.ExecuteReader();


                                while (reader.Read())
                                {
                                    Monumentos mou = new Monumentos();
                
                                    //mou.Idmonumento = reader.GetString(0).ToString();
                                    mou.Latitud = reader.GetDecimal(1);
                                    mou.Longitud = reader.GetDecimal(2);

                                    listMonumentos.Add(mou);
                                }

                                command.Connection.Close();

                                JavaScriptSerializer jss = new JavaScriptSerializer();
                                //json = jss.Serialize(listMonumentos);

                                HttpContext.Current.Response.Write(jss.Serialize(listMonumentos));
                        break;

                    default:

                        break;

                
                }
            }catch(Exception e){

                HttpContext.Current.Response.Write("Error en el Web Service");
            }
        }

    }
}