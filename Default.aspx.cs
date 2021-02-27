using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace domoarigato
{
    public partial class Default : System.Web.UI.Page
    {

        mssqli BDcon = new mssqli("Data Source=ec2-54-208-60-90.compute-1.amazonaws.com;Initial Catalog=serialDB;User ID=sa;Password=Utl.2021");
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            /* el dato escrito es el tercer argumento de la función BDcon.actualizarColumna(),
             * de ese dato:
               - el primer digito indica si se quiere lecturaDigital/escrituraDigital/lecturaAnalogica
               - el segundo y tercer digitos indica el número de pin para la lectura/escritura
               - el cuarto digito (obligatorio solo si se va a hacer una escritura de pin digital) establece el valor de escritura*/

            BDcon.actualizarColumna("scada", "leer", "013");
            BDcon.actualizarColumna("scada", "cola", "1");
            
            //retardo para evitar leer datos viejos, el servidor scada se actualiza cada 1 seg
            var t = Task.Run(async delegate
            {
                await Task.Delay(2000);
                return 42;
            });
            t.Wait();
            Label1.Text= "Estado: "+BDcon.LeerUnRegsitro("scada","escribir",0)+"<br>Leido el: "+DateTime.Now+ "<br><br>Interpretación: " +
                "<br>* El primer dígito indica la última acción ejecutada," +
                "<br>0=lectura y 1=escritura, de un pin digital de arduino." +
                "<br>* El segundo y tercer dígitos son el número de pin." +
                "<br>* El cuarto dígito indica el valor de la lectura/escritura en el pin.";
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            BDcon.actualizarColumna("scada","leer","1131");
            BDcon.actualizarColumna("scada", "cola", "1");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            BDcon.actualizarColumna("scada", "leer", "1130");
            BDcon.actualizarColumna("scada", "cola", "1");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            /* el dato escrito es el tercer argumento de la función BDcon.actualizarColumna(),
             * de ese dato:
               - el primer digito indica si se quiere lecturaDigital/escrituraDigital/lecturaAnalogica
               - el segundo y tercer digitos indica el número de pin para la lectura/escritura
               - el cuarto digito (obligatorio solo si se va a hacer una escritura de pin digital) establece el valor de escritura*/

            BDcon.actualizarColumna("scada", "leer", "200");
            BDcon.actualizarColumna("scada", "cola", "1");

            //retardo para evitar leer datos viejos, el servidor scada se actualiza cada 1 seg
            var t = Task.Run(async delegate
            {
                await Task.Delay(2000);
                return 42;
            });
            t.Wait();
            Label1.Text = "Estado: " + BDcon.LeerUnRegsitro("scada", "escribir", 0) + "<br>Leido el: " + DateTime.Now + "<br><br>Interpretación: " +
                "<br>* El primer digito indica la última acción ejecutada," +
                "<br>2=lectura de un pin analógico de arduino." +
                "<br>* El segundo y tercer dígitos son el número de pin." +
                "<br>* Los dígitos restantes son el valor del pin.";
        }
    }

    /*********** A PARTIR DE AQUÍ NO MODIFICAR*********/
    // No modificar el código de esta clase, proporciona funcionalidad similar a  mysqli
    class mssqli
    {
        //Variables de instancia
        public string cadenaCnx { get; private set; }
        private SqlCommand cmd = new SqlCommand();
        private SqlDataReader lector;


        public mssqli(string cadCnx) //el constructor recibe la cadena de conexion
        {
            this.cadenaCnx = cadCnx;
        }

        public string LeerUnRegsitro(string tabla, string columna, int numReg)
        {
            //variable para el dato leido desde la base de datos
            string datoBD;

            //se crea objeto para la conexion
            SqlConnection sqlConexion = new SqlConnection(cadenaCnx);

            //se arma el query
            cmd.CommandText = "SELECT " + columna + " FROM " + tabla; //lectura
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConexion;

            //se abre la conexion
            sqlConexion.Open();

            //se ejecuta query de lectura
            lector = cmd.ExecuteReader();

            // Los datos de lecturaestan accesibles desde el objeto reader a partir de este punto
            lector.Read();
            datoBD = lector.GetString(numReg); //tomo solo el elemento indicado en numReg
            lector.Close();

            // se cierra la conexion
            sqlConexion.Close();

            //el metodo regresa el dato leido desde la base de datos
            return datoBD;
        }

        public void actualizarColumna(string tabla, string columna, string dato)
        {
            //se crea objeto para la conexion
            SqlConnection sqlConexion = new SqlConnection(cadenaCnx);

            //se arma un nuevo query
            cmd.CommandText = "UPDATE " + tabla + " SET " + columna + "=\'" + dato + "\'"; //escritura
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConexion;

            //se abre la conexion
            sqlConexion.Open();

            // ahora se ejecuta query de escritura
            lector = cmd.ExecuteReader();

            // se cierra la conexion
            sqlConexion.Close();
        }
    }
}