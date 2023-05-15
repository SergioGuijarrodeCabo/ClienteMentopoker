using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using ProyectoMentopoker.Models;
using static System.Net.Mime.MediaTypeNames;

#region

//INSERT INTO Partidas (Partida_id, Cash_inicial, Cash_final, Fecha) VALUES(1, 20, 25, GETDATE())
//INSERT INTO Rondas (Ronda_id, Cantidad_jugada, Ganancias, Partida_id) VALUES(1, 20, 250, 1)
//INSERT INTO Jugadas (Jugada_id, Cantidad_jugada, Seguimiento_Tabla, Identificador, Ronda_id) VALUES(1, 20, 0, 234, 1)


//CREATE OR ALTER PROCEDURE SP_GET_TABLA
//(@ID INT)
//AS
//SELECT * FROM celdas WHERE table_id=@ID
//GO


//CREATE OR ALTER PROCEDURE SP_INSERT_PARTIDA
//(
//@CASH_INICIAL FLOAT,
//@CASH_FINAL FLOAT,
//@COMENTARIOS NVARCHAR(200),
//@USUARIO_ID NVARCHAR(50)
//)
//AS
//INSERT INTO Partidas  (Partida_id, Cash_inicial, Cash_final, Fecha, Comentarios, Usuario_id) VALUES((SELECT max(CAST(Partida_id AS INT)) from Partidas) + 1, @CASH_INICIAL, @CASH_FINAL, GETDATE(), @COMENTARIOS, @USUARIO_ID)
//GO


//CREATE OR ALTER PROCEDURE SP_INSERT_RONDA
//(

//@CANTIDAD_RONDA FLOAT,
//@GANANCIAS FLOAT,
//@PARTIDA_ID NVARCHAR(50)
//)
//AS
//INSERT INTO Rondas (Ronda_id, Cantidad_jugada, Ganancias, Partida_id) VALUES((SELECT max(CAST(Ronda_id AS INT)) from Rondas) + 1, @CANTIDAD_RONDA, @GANANCIAS, @PARTIDA_ID)
//GO

//CREATE OR ALTER PROCEDURE SP_INSERT_JUGADA
//(

//@CANTIDAD_JUGADA_PREFLOP FLOAT,
//@SEGUIMIENTO_TABLA BIT,
//@IDENTIFICADOR INT,
//@RONDA_ID NVARCHAR(50)
//)
//AS
//INSERT INTO Jugadas (Jugada_id, Cantidad_jugada_Preflop, Seguimiento_Tabla, Identificador, Ronda_id) VALUES((SELECT max(CAST(Jugada_id AS INT)) from Jugadas) + 1, @CANTIDAD_JUGADA_PREFLOP, @SEGUIMIENTO_TABLA, @IDENTIFICADOR, @RONDA_ID)
//GO


//CREATE OR ALTER PROCEDURE SP_FIND_IDENTIFICADOR
//(
//@CELL_ID NVARCHAR(4),
//@TABLE_ID INT
//)
//AS
//SELECT Identificador FROM Celdas WHERE  Table_id =@TABLE_ID AND Cell_id =@CELL_ID
//GO



#endregion


namespace ProyectoMentopoker.Repositories
{


    public class RepositoryTablas
    {

        SqlConnection cn;
        SqlCommand com;
        SqlDataReader reader;

        public RepositoryTablas()
        {

            //Conexión de casa
            //string connectionString = @"Data Source=DESKTOP-E38C8U3;Initial Catalog=PROYECTOMENTOPOKER;User ID=sa;Password=MCSD2022";

            //Conexión de clase
            //string connectionString = @"Data Source=LOCALHOST\DESARROLLO;Initial Catalog=PROYECTOMENTOPOKER;User ID=sa;Password=MCSD2022";

            //Conexión azure 
            string connectionString = @"Data Source=sgserversql.database.windows.net;Initial Catalog=PRIMERADB;User ID=Adminsg;Password=Sgpass1234";


            // this.cn = new SqlConnection(HelperConfiguartion.GetConnectionString());
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
        }


        public List<Celda> GetTabla(int id)
        {
            List<Celda> tabla = new List<Celda>();
            string sql = "select * from Celdas where table_id=@ID";

            SqlParameter pamid = new SqlParameter("@ID", id);

            this.com.Parameters.Add(pamid);

            this.com.CommandType = System.Data.CommandType.Text;

            this.com.CommandText = sql;

            this.cn.Open();

            this.reader = this.com.ExecuteReader();


            while (this.reader.Read())
            {

                int identificador = int.Parse(this.reader["Identificador"].ToString());
                int table_id = int.Parse(this.reader["table_id"].ToString());
                string id_celda = this.reader["cell_id"].ToString();
                //string id_celda = this.reader["Id_celda"].ToString();
                string background_color = this.reader["background_color"].ToString();
                string text_color = this.reader["text_color"].ToString();

                Celda celda = new Celda(identificador, table_id, id_celda, background_color, text_color);
                tabla.Add(celda);
            }

            this.reader.Close();

            this.cn.Close();

            this.com.Parameters.Clear();

            return tabla;

        }



        //public Boolean insertPartida(int[] ids_Jugadas, int[] ids_Rondas, double[] ganancias_Rondas,
        //    string[] cell_ids_Jugadas, int[] table_ids_Jugadas, double[] cantidades_Jugadas,
        //    Boolean[] seguimiento_jugadas, double dinero_inicial, double dinero_actual)
        //{

        //}


        public async Task<Boolean> insertPartida(int[] ids_Jugadas, int[] ids_Rondas, double[] ganancias_Rondas, double[] cantidades_Rondas,
            string[] cell_ids_Jugadas, int[] table_ids_Jugadas, double[] cantidades_Jugadas,
            Boolean[] seguimiento_jugadas, double dinero_inicial, double dinero_actual, string comentario, string usuario_id)
        {
            int exito = 0;
            Boolean exitob = false;


            SqlParameter pamcashinicial = new SqlParameter("@CASH_INICIAL", dinero_inicial);
            this.com.Parameters.Add(pamcashinicial);
            SqlParameter pamcashfinal = new SqlParameter("@CASH_FINAL", dinero_actual);
            this.com.Parameters.Add(pamcashfinal);
            SqlParameter pamcomentario = new SqlParameter("@COMENTARIOS", comentario);
            this.com.Parameters.Add(pamcomentario);
            SqlParameter pamusuarioid = new SqlParameter("@USUARIO_ID", usuario_id);
            this.com.Parameters.Add(pamusuarioid);
            this.com.CommandType = System.Data.CommandType.StoredProcedure;
            this.com.CommandText = "SP_INSERT_PARTIDA";

            this.cn.Open();
            exito = await this.com.ExecuteNonQueryAsync();
            this.com.Parameters.Clear();
            this.cn.Close();

            if (exito == 1)
            {
                exitob = true;
            }


            this.com.CommandText = "SELECT MAX(CAST(Partida_id AS INT)) from Partidas";
            this.com.CommandType = System.Data.CommandType.Text;
            this.cn.Open();
            string partidaId = this.com.ExecuteScalar().ToString();
            this.cn.Close();


            List<int> posicionJugadas = new List<int>();
            for (int x = 0; x < ids_Jugadas.Length; x++)
            {
                posicionJugadas.Add(x);
            }



            //var rondaIds = new List<string>();
            int numRonda = 1;

            //List<int> jugadasInsertadas = new List<int>();
            //Boolean insercion = true;

            for (int i = 0; i <= ids_Rondas.Length; i++)
            {
                await this.insertRonda(cantidades_Rondas[i], ganancias_Rondas[i], partidaId, cell_ids_Jugadas[i], table_ids_Jugadas[i], cantidades_Jugadas[i], seguimiento_jugadas[i]);




                numRonda++;
            }
            numRonda = 1;




            return exitob;
        }


        public async Task insertRonda(double cantidad_Ronda, double ganancias_Ronda, string partida_id, string cell_id, int table_id, double cantidad_jugada, Boolean seguimiento_tabla)
        {

            SqlParameter pamcantidad = new SqlParameter("@CANTIDAD_RONDA", cantidad_Ronda);
            this.com.Parameters.Add(pamcantidad);
            SqlParameter pamganancias = new SqlParameter("@GANANCIAS", ganancias_Ronda);
            this.com.Parameters.Add(pamganancias);
            SqlParameter pampartidaid = new SqlParameter("@PARTIDA_ID", partida_id);
            this.com.Parameters.Add(pampartidaid);
            this.com.CommandType = System.Data.CommandType.StoredProcedure;
            this.com.CommandText = "SP_INSERT_RONDA";

            this.cn.Open();
            await this.com.ExecuteNonQueryAsync();
            this.com.Parameters.Clear();
            this.cn.Close();

            SqlParameter pamcellid = new SqlParameter("@CELL_ID", cell_id);
            this.com.Parameters.Add(pamcellid);
            SqlParameter pamtableid = new SqlParameter("@TABLE_ID", table_id);
            this.com.Parameters.Add(pamtableid);
            this.com.CommandType = System.Data.CommandType.StoredProcedure;
            this.com.CommandText = "SP_FIND_IDENTIFICADOR";
            this.cn.Open();
            int identificador = int.Parse(this.com.ExecuteScalar().ToString());
            this.com.Parameters.Clear();
            this.cn.Close();

            string rondaId;
            this.com.CommandText = "SELECT MAX(CAST(Ronda_id AS INT)) from Rondas";
            this.com.CommandType = System.Data.CommandType.Text;
            this.cn.Open();
            rondaId = this.com.ExecuteScalar().ToString();
            this.cn.Close();



            await this.insertJugada(cantidad_jugada, seguimiento_tabla, identificador, rondaId);
        }


        public async Task insertJugada(double cantidad_Jugada, Boolean seguimiento_jugada,
            int identificador, string ronda_id)
        {
            SqlParameter pamcantidad = new SqlParameter("@CANTIDAD_JUGADA_PREFLOP", cantidad_Jugada);
            this.com.Parameters.Add(pamcantidad);
            int seguimiento;
            if (seguimiento_jugada == true)
            {
                seguimiento = 1;
            }
            else
            {
                seguimiento = 0;
            }
            SqlParameter pamseguimiento = new SqlParameter("@SEGUIMIENTO_TABLA", seguimiento);
            this.com.Parameters.Add(pamseguimiento);
            pamseguimiento.SqlDbType = SqlDbType.Bit;
            SqlParameter pamidentificador = new SqlParameter("@IDENTIFICADOR", identificador);
            this.com.Parameters.Add(pamidentificador);
            SqlParameter pamrondaid = new SqlParameter("@RONDA_ID", ronda_id);
            this.com.Parameters.Add(pamrondaid);
            this.com.CommandType = System.Data.CommandType.StoredProcedure;
            this.com.CommandText = "SP_INSERT_JUGADA";

            this.cn.Open();
            await this.com.ExecuteNonQueryAsync();
            this.com.Parameters.Clear();
            this.cn.Close();


        }

















        //public async Task<Boolean> insertPartida(int[] ids_Jugadas, int[] ids_Rondas, double[] ganancias_Rondas, double[] cantidades_Rondas,
        //string[] cell_ids_Jugadas, int[] table_ids_Jugadas, double[] cantidades_Jugadas,
        //Boolean[] seguimiento_jugadas, double dinero_inicial, double dinero_actual, string comentario, string usuario_id)
        //{
        //    int exito = 0;
        //    Boolean exitob = false;

        //    using (this.cn)
        //    {
        //        await this.cn.OpenAsync();
        //        using (var transaction = this.cn.BeginTransaction())
        //        {
        //            try
        //            {

        //                using (var command = CreateCommand("SP_INSERT_PARTIDA", transaction, new[]
        //                {
        //                new SqlParameter("@CASH_INICIAL", dinero_inicial),
        //                new SqlParameter("@CASH_FINAL", dinero_actual),
        //                new SqlParameter("@COMENTARIOS", comentario),
        //                new SqlParameter("@USUARIO_ID", usuario_id)
        //                }))
        //                {
        //                    await command.ExecuteNonQueryAsync();
        //                }

        //                //SqlParameter pamcashinicial = new SqlParameter("@CASH_INICIAL", dinero_inicial);
        //                //    this.com.Parameters.Add(pamcashinicial);
        //                //    SqlParameter pamcashfinal = new SqlParameter("@CASH_FINAL", dinero_actual);
        //                //    this.com.Parameters.Add(pamcashfinal);
        //                //    SqlParameter pamcomentario = new SqlParameter("@COMENTARIOS", comentario);
        //                //    this.com.Parameters.Add(pamcomentario);
        //                //    SqlParameter pamusuarioid = new SqlParameter("@USUARIO_ID", usuario_id);
        //                //    this.com.Parameters.Add(pamusuarioid);
        //                //    this.com.CommandType = System.Data.CommandType.StoredProcedure;
        //                //    this.com.CommandText = "SP_INSERT_PARTIDA";
        //                //    this.com.Transaction = transaction;
        //                //    await this.com.ExecuteNonQueryAsync();
        //                //     this.com.Parameters.Clear();

        //                this.com.CommandText = "SELECT MAX(CAST(Partida_id AS INT)) from Partidas";
        //                this.com.CommandType = System.Data.CommandType.Text;
        //                this.com.Transaction = transaction;
        //                    string partidaId = this.com.ExecuteScalar().ToString();

        //                    List<int> posicionJugadas = new List<int>();
        //                    for (int x = 0; x < ids_Jugadas.Length; x++)
        //                    {
        //                        posicionJugadas.Add(x);
        //                    }

        //                    int numRonda = 1;

        //                    for (int i = 0; i < ids_Rondas.Length; i++)
        //                    {
        //                        await insertRonda(cantidades_Rondas[i], ganancias_Rondas[i], partidaId, cell_ids_Jugadas[i], table_ids_Jugadas[i], cantidades_Jugadas[i], seguimiento_jugadas[i], transaction);
        //                        numRonda++;
        //                    }
        //                    numRonda = 1;

        //                    transaction.Commit();

        //            }
        //            catch (Exception ex)
        //            {
        //                // Roll back the transaction if something failed
        //                transaction.Rollback();
        //                // Handle the exception
        //            }

        //            this.cn.Close();
        //        }
        //    }

        //    if (exito == 0)
        //    {
        //        exitob = true;
        //    }

        //    return exitob;
        //}



        //public async Task insertRonda(double cantidad_Ronda, double ganancias_Ronda, string partida_id, string cell_id, int table_id, double cantidad_jugada, Boolean seguimiento_tabla, SqlTransaction transaction)
        //{


        //    using (var command = CreateCommand("SP_INSERT_RONDA", transaction, new[]
        //              {
        //                new SqlParameter("@CANTIDAD_RONDA", cantidad_Ronda),
        //                new SqlParameter("@GANANCIAS", ganancias_Ronda),
        //                new SqlParameter("@PARTIDA_ID", partida_id),
        //                }))
        //    {
        //        await command.ExecuteNonQueryAsync();
        //    }

        //    //SqlParameter pamcantidad = new SqlParameter("@CANTIDAD_RONDA", cantidad_Ronda);
        //    //this.com.Parameters.Add(pamcantidad);
        //    //SqlParameter pamganancias = new SqlParameter("@GANANCIAS", ganancias_Ronda);
        //    //this.com.Parameters.Add(pamganancias);
        //    //SqlParameter pampartidaid = new SqlParameter("@PARTIDA_ID", partida_id);
        //    //this.com.Parameters.Add(pampartidaid);
        //    //this.com.CommandType = System.Data.CommandType.StoredProcedure;
        //    //this.com.CommandText = "SP_INSERT_RONDA";

        //    ////this.cn.Open();
        //    //this.com.Transaction = transaction;
        //    //await this.com.ExecuteNonQueryAsync();
        //    //this.com.Parameters.Clear();
        //    ////this.cn.Close();
        //    ///

        //    int identificador = 0;
        //    using (var command = CreateCommand("SP_FIND_IDENTIFICADOR", transaction, new[]
        //              {
        //                new SqlParameter("@CELL_ID", cell_id),
        //                new SqlParameter("@TABLE_ID", table_id),
        //                }))
        //    {
        //         identificador = int.Parse(this.com.ExecuteScalar().ToString());
        //    }


        //    //SqlParameter pamcellid = new SqlParameter("@CELL_ID", cell_id);
        //    //this.com.Parameters.Add(pamcellid);
        //    //SqlParameter pamtableid = new SqlParameter("@TABLE_ID", table_id);
        //    //this.com.Parameters.Add(pamtableid);
        //    //this.com.CommandType = System.Data.CommandType.StoredProcedure;
        //    //this.com.CommandText = "SP_FIND_IDENTIFICADOR";
        //    ////this.cn.Open();
        //    //this.com.Transaction = transaction;
        //    //int identificador = int.Parse(this.com.ExecuteScalar().ToString());
        //    //this.com.Parameters.Clear();
        //    //this.cn.Close();

        //    string rondaId;
        //    this.com.CommandText = "SELECT MAX(CAST(Ronda_id AS INT)) from Rondas";
        //    this.com.CommandType = System.Data.CommandType.Text;
        //    //this.cn.Open();
        //    this.com.Transaction = transaction;
        //    rondaId = this.com.ExecuteScalar().ToString();
        //    //this.cn.Close();



        //    await this.insertJugada(cantidad_jugada, seguimiento_tabla, identificador, rondaId, transaction);

        //}


        //public async Task insertJugada(double cantidad_Jugada, Boolean seguimiento_jugada, 
        //    int identificador, string ronda_id, SqlTransaction transaction)
        //{
        //    SqlParameter pamcantidad = new SqlParameter("@CANTIDAD_JUGADA_PREFLOP", cantidad_Jugada);
        //    this.com.Parameters.Add(pamcantidad);
        //    int seguimiento;
        //    if(seguimiento_jugada == true)
        //    {
        //        seguimiento = 1;
        //    }
        //    else
        //    {
        //        seguimiento = 0;
        //    }
        //    SqlParameter pamseguimiento = new SqlParameter("@SEGUIMIENTO_TABLA", seguimiento);
        //    pamseguimiento.SqlDbType = SqlDbType.Bit;
        //    this.com.Parameters.Add(pamseguimiento);

        //    SqlParameter pamidentificador = new SqlParameter("@IDENTIFICADOR", identificador);
        //    this.com.Parameters.Add(pamidentificador);
        //    SqlParameter pamrondaid = new SqlParameter("@RONDA_ID", ronda_id);
        //    this.com.Parameters.Add(pamrondaid);
        //    this.com.CommandType = System.Data.CommandType.StoredProcedure;
        //    this.com.Transaction = transaction;
        //    this.com.CommandText = "SP_INSERT_JUGADA";

        //    //this.cn.Open();
        //    await this.com.ExecuteNonQueryAsync();
        //    this.com.Parameters.Clear();
        //    //this.cn.Close();


        //}


        //private SqlCommand CreateCommand(string commandText, SqlTransaction transaction, SqlParameter[] parameters = null)
        //{
        //    var command = new SqlCommand(commandText, this.cn);
        //    command.CommandType = System.Data.CommandType.StoredProcedure;
        //    command.Transaction = transaction;

        //    if (parameters != null)
        //    {
        //        command.Parameters.AddRange(parameters);
        //    }

        //    return command;
        //}



    }


}