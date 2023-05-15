using ProyectoMentopoker.Models;
using ProyectoMentopoker.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata;
using System.ComponentModel.Design;
using System.Linq;
using Microsoft.AspNetCore.Server.IIS.Core;

#region

//CREATE OR ALTER PROCEDURE SP_GET_PARTIDAS
//AS
//SELECT Partida_id, Cash_inicial, Cash_final, Fecha, Comentarios, Usuario_id FROM Partidas
//GO

//CREATE OR ALTER PROCEDURE SP_GET_RONDAS
//(@PARTIDA_ID NVARCHAR(50))
//AS
//SELECT Ronda_id, Cantidad_jugada, Ganancias, Partida_id FROM Rondas WHERE PARTIDA_ID = @PARTIDA_ID
//GO


//CREATE OR ALTER PROCEDURE SP_GET_JUGADAS
//(@RONDA_ID NVARCHAR(50))
//AS
//SELECT Jugada_id, Cantidad_jugada_Preflop, Seguimiento_Tabla, Identificador, Ronda_id  FROM Jugadas WHERE Ronda_id = @RONDA_ID
//GO



//AHORA ES ESTE
//CREATE OR ALTER   PROCEDURE [dbo].[GET_IDSTABLAS_JUGADAS]
//(@Jugada_id INT)
//AS

//SELECT C.Cell_id, C.Table_id, J.Cantidad_jugada_Preflop, T.Condicion, R.Cantidad_jugada, J.Seguimiento_Tabla, J.Ronda_id, J.Jugada_Id, R.Cantidad_jugada,R.Ganancias
//FROM Celdas C
//INNER JOIN Tablas T ON C.Table_id = T.Table_id
//INNER JOIN Jugadas J ON J.Identificador = C.Identificador
//INNER JOIN Rondas R ON R.Ronda_id = J.Ronda_id
//WHERE J.Jugada_id = @Jugada_id
//GO





//CREATE OR ALTER PROCEDURE GET_IDSTABLAS_JUGADAS
//(@Identificador INT)
//AS

//SELECT C.Cell_id, C.Table_id, T.Condicion, J.Cantidad_jugada, J.Seguimiento_Tabla, J.Ronda_id, J.Jugada_Id
//FROM Celdas C
//INNER JOIN Tablas T ON C.Table_id = T.Table_id
//INNER JOIN Jugadas J ON J.Identificador = C.Identificador
//WHERE C.Identificador = @Identificador



//SET ANSI_NULLS ON
//GO
//SET QUOTED_IDENTIFIER ON
//GO
//ALTER PROCEDURE [dbo].[SP_BORRAR_TODAS_PARTIDAS]
//(@USUARIO_ID NVARCHAR(50))
//AS
//  DECLARE @partidas_id TABLE (id NVARCHAR(50))
//  DECLARE @rondas_id TABLE (id NVARCHAR(50))
//  DECLARE @jugadas_id TABLE (id NVARCHAR(50))

//  --Guardar todas las partidas_id asociadas con el usuario
//  INSERT INTO @partidas_id (id)
//  SELECT partida_id FROM Partidas WHERE Usuario_id = @USUARIO_ID

//  -- Iterar dentro de cada partida y guardar las rondas y jugadas asociadas
//  DECLARE @partida_id NVARCHAR(50)
//  DECLARE cur CURSOR FOR SELECT id FROM @partidas_id
//  OPEN cur
//  FETCH NEXT FROM cur INTO @partida_id
//  WHILE @@FETCH_STATUS = 0
//  BEGIN
//    -- Guardar todas las rondas_id asociadas con la partida
//    INSERT INTO @rondas_id (id)
//    SELECT ronda_id FROM Rondas WHERE Partida_id = @partida_id

//    -- Iterar dentro de cada ronda y guardar las jugadas asociadas
//    DECLARE @ronda_id NVARCHAR(50)
//    DECLARE cur2 CURSOR FOR SELECT id FROM @rondas_id
//    OPEN cur2
//    FETCH NEXT FROM cur2 INTO @ronda_id
//    WHILE @@FETCH_STATUS = 0
//    BEGIN
//      INSERT INTO @jugadas_id (id)
//      SELECT jugada_id FROM Jugadas WHERE Ronda_id = @ronda_id
//      FETCH NEXT FROM cur2 INTO @ronda_id
//    END
//    CLOSE cur2
//    DEALLOCATE cur2
//  FETCH NEXT FROM cur INTO @partida_id
//  END
//  CLOSE cur
//  DEALLOCATE cur

//  -- Borrar todas las jugadas asociadas con cada ronda
//  DELETE FROM Jugadas WHERE Ronda_id IN (SELECT id FROM @rondas_id)

//  --Borrar todas las rondas asociadas con cada partida
//  DELETE FROM Rondas WHERE Partida_id IN (SELECT id FROM @partidas_id)

//  --Borrar las partidas asociadas con el usuario
//  DELETE FROM Partidas WHERE Usuario_id = @USUARIO_ID



//VFINAL(de momento
//ALTER PROCEDURE[dbo].[GET_IDSTABLAS_JUGADAS]
//(
//    @Ronda_id INT,
//    @Cell_id NVARCHAR(4) = NULL,
//    @Table_id INT = NULL,
//    @Cantidad_jugada float = null
//)
//AS
//BEGIN
//    SELECT C.Cell_id, C.Table_id, T.Condicion, J.Cantidad_jugada_Preflop, J.Seguimiento_Tabla, J.Ronda_id, J.Jugada_Id, R.Cantidad_jugada, R.Ganancias
//    FROM Celdas C
//    INNER JOIN Tablas T ON C.Table_id = T.Table_id
//    INNER JOIN Jugadas J ON J.Identificador = C.Identificador
//    INNER JOIN Rondas R ON R.Ronda_id = J.Ronda_id
//    WHERE R.Ronda_id = @Ronda_id 
//    AND (@Cell_id IS NULL OR C.Cell_id = @Cell_id)
//    AND(@Table_id IS NULL OR C.Table_id = @Table_id)

//    AND(@Cantidad_jugada IS NULL OR R.Cantidad_jugada > @Cantidad_jugada)
//END






//CREATE OR ALTER PROCEDURE SP_BORRAR_PARTIDAS
//(@PARTIDA_ID NVARCHAR(50))
//AS
//  DECLARE @rondas_id TABLE (id NVARCHAR(50))
//  DECLARE @jugadas_id TABLE (id NVARCHAR(50))

//  --Guardar todas las rondas_id asociadas con la partida
//  INSERT INTO @rondas_id (id)
//  SELECT ronda_id FROM Rondas WHERE Partida_id = @PARTIDA_ID

//  -- Iterar dentro de cada ronda y guardar las jugadas asociadas
//  DECLARE @ronda_id NVARCHAR(50)
//  DECLARE cur CURSOR FOR SELECT id FROM @rondas_id
//  OPEN cur
//  FETCH NEXT FROM cur INTO @ronda_id
//  WHILE @@FETCH_STATUS = 0
//  BEGIN
//    INSERT INTO @jugadas_id (id)
//    SELECT jugada_id FROM Jugadas WHERE Ronda_id = @ronda_id
//    FETCH NEXT FROM cur INTO @ronda_id
//  END
//  CLOSE cur
//  DEALLOCATE cur

//  --borrar todas las jugadas asociadas con cada ronda
//  DELETE FROM Jugadas WHERE Ronda_id IN (SELECT id FROM @rondas_id)

//  --borrar todas las rondas asociadas con cada partida
//  DELETE FROM Rondas WHERE Partida_id = @PARTIDA_ID

//  --Borrar la partida asociada con el id de partida
//  DELETE FROM Partidas WHERE Partida_id = @PARTIDA_ID
//GO





//create or alter procedure /*SP_GETCELLID*/
//(@identificador int)
//as
//select cell_id from Celdas where celdas.Identificador = @identificador
//go


//create or alter procedure SP_GET_CELLID
//(@identificador int)
//as
//select Cell_id from Celdas where Identificador = @identificador
//go



//create or alter procedure SP_GET_RONDA_IDJUGADA
//  (@Ronda_id NVARCHAR(50))
//  AS
//  SELECT * FROM Rondas where Ronda_id = @Ronda_Id

//  GO

#endregion


namespace ProyectoMentopoker.Repositories
{
    public class RepositoryEstadisticas 
    {
        private MentopokerContext context;
        private SqlConnection cn;
        private SqlCommand com;

        public RepositoryEstadisticas(MentopokerContext context)
        {
            this.context = context;
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

        public List<PartidaModel> GetAllPartidas()
        {
                var consulta = from datos in this.context.Partidas
                               select datos;
                return consulta.ToList();

        }

        public PartidaModel FindPartida(string Partida_id)
        {
            var consulta = from datos in this.context.Partidas
                           where datos.Partida_id == Partida_id
                           select datos;
            return consulta.FirstOrDefault();
        }

        public async Task BorrarPartidas(string Usuario_id)
        {
            SqlParameter pamID = new SqlParameter("@USUARIO_ID", Usuario_id);
            this.com.Parameters.Add(pamID);

            this.com.CommandTimeout = 60;
   
            this.com.CommandType = System.Data.CommandType.StoredProcedure;
            this.com.CommandText = "SP_BORRAR_TODAS_PARTIDAS";

            this.cn.Open();
            await this.com.ExecuteNonQueryAsync();
            this.com.Parameters.Clear();
            this.cn.Close();

        }

        public async Task UpdatePartida(string Partida_id, double Cash_inicial, double Cash_Final, string Comentarios)
        {
            PartidaModel partida = this.FindPartida(Partida_id);
            partida.Cash_Inicial = Cash_inicial;
            partida.Cash_Final = Cash_Final;
            partida.Comentarios = Comentarios;
            await this.context.SaveChangesAsync();
        }

        //public async Task DeletePartida(string Partida_id)
        //{
        //    List<RondaModel> rondasPartida = this.GetRondas(Partida_id);
        //    List<JugadaModel> jugadasPartida = new List<JugadaModel>();
        //    for (int i=0;i<rondasPartida.Count; i++)
        //    {
        //        jugadasPartida.Add(this.GetJugadas(rondasPartida[i].Ronda_id));
        //    }
        //    for (int i = 0; i < jugadasPartida.Count; i++)
        //    {
        //        this.context.Jugadas.Remove(jugadasPartida[i]);

        //    }
        //    for (int i = 0; i < rondasPartida.Count; i++)
        //    {
        //        this.context.Rondas.Remove(rondasPartida[i]);
        //    }
        //    PartidaModel partida = this.FindPartida(Partida_id);
        //    this.context.Partidas.Remove(partida);
        //    await this.context.SaveChangesAsync();
        //}


        public async Task DeletePartida(string Partida_id)
        {


            SqlParameter pamID = new SqlParameter("@PARTIDA_ID", Partida_id);
            this.com.Parameters.Add(pamID);

            this.com.CommandType = System.Data.CommandType.StoredProcedure;
            this.com.CommandText = "SP_BORRAR_PARTIDAS";

            this.cn.Open();
             await this.com.ExecuteNonQueryAsync();
            this.com.Parameters.Clear();
            this.cn.Close();

        }



        public List<RondaModel> GetRondas(string partida_id)
        {
            var consulta = from datos in this.context.Rondas
                           where datos.Partida_id == partida_id
                           select datos;


            List<RondaModel> rondas = consulta.ToList();
            return rondas;
        }

        public List<JugadaModel> GetJugadas(string ronda_id)
        {
            var consulta = from datos in this.context.Jugadas
                           where datos.Ronda_id == ronda_id
                           select datos;

            return consulta.ToList();
        }




        public ConjuntoPartidasUsuario GetPartidas(int Usuario_id, string peticion, DateTime? fechaInicio = null, DateTime? fechaFinal = null, string? cell_id = null, int? condicion =null, double? cantidadJugada = null)
            {
            //string sql = "SP_GET_PARTIDAS";
            //var consulta = this.context.Partidas.FromSqlRaw(sql);
            //List<Partida> partidas = consulta.ToList();

            //for(int i=0;i<partidas.Count;i++)
            //{
            //    partidas[i].Rondas = this.GetRondas(int.Parse(partidas[i].Partida_id));

            //}

            //var consulta = from datos in this.context.Partidas
            //                where datos.Usuario_id == Usuario_id.ToString()
            //                select datos;





            List<PartidaModel> partidas = new List<PartidaModel>();




            if (fechaInicio !=null || fechaFinal !=null)
            {
                //  var consulta = from datos in this.context.Partidas
                //                where datos.Usuario_id == Usuario_id.ToString() &&
                //                      (!fecha.HasValue || datos.Fecha > fecha.Value)
                //                select datos;
                //partidas = consulta.ToList();


                var consulta = from datos in this.context.Partidas
                               where datos.Usuario_id == Usuario_id.ToString() &&
                                     ((!fechaInicio.HasValue || datos.Fecha >= fechaInicio.Value) &&
                                      (!fechaFinal.HasValue || datos.Fecha <= fechaFinal.Value))
                               select datos;
                partidas = consulta.ToList();

            }
            else { 
                var consulta = from datos in this.context.Partidas
                               select datos;
                partidas = consulta.ToList();
            }
          
            
            


            ConjuntoPartidasUsuario conjunto = new ConjuntoPartidasUsuario();
           

            List<RondaModel> rondas = new List<RondaModel>();
            List<JugadasCalculadasModel> jugadas = new List<JugadasCalculadasModel>();
            
            for (int i=0;i<partidas.Count;i++)
            {
                rondas.AddRange(this.GetRondas(partidas[i].Partida_id));
               
            }
            for (int i = 0; i < rondas.Count; i++)
            {
                JugadasCalculadasModel jugada = this.GetJugadasCalculadas(rondas[i].Ronda_id, cell_id, condicion, cantidadJugada);
                if (jugada != null)
                {
                    jugadas.Add(jugada);
                }
                

            }

            conjunto.Partidas = partidas;
            conjunto.Rondas = rondas;
            conjunto.Jugadas = jugadas;

            //if (peticion == "partidas")
            //{
            //    conjunto.EstadisticasPartidas = this.GetEstadisticasPartidas(conjunto);
            //}
            //if (peticion == "jugadasFecha") 
            //{
            //    conjunto.EstadisticasJugadas = this.GetEstadisticasJugadas(conjunto);

            //}if(peticion == "jugadasCellid")
            //{
            //    conjunto.EstadisticasJugadas = this.GetEstadisticasJugadas(conjunto, cell_id);
            //}
          




            return conjunto;


        }

       
        public JugadasCalculadasModel GetJugadasCalculadas(string ronda_id, string? cell_id = null, int? condicion = null, double? cantidadJugada = null)
        {
            JugadasCalculadasModel jugada = new JugadasCalculadasModel();
         

                var parameterR = new SqlParameter("@Ronda_id", ronda_id);
                var parameter1 = new SqlParameter("@Cell_id", cell_id ?? (object)DBNull.Value);
                var parameter2 = new SqlParameter("@Table_id", condicion ?? (object)DBNull.Value);
                var parameter3 = new SqlParameter("@Cantidad_jugada", cantidadJugada ?? (object)DBNull.Value);
                string sql = "EXECUTE GET_IDSTABLAS_JUGADAS @Ronda_id, @Cell_id, @Table_id, @Cantidad_jugada";
                jugada = this.context.JugadasCalculadas.FromSqlRaw(sql, parameterR, parameter1, parameter2, parameter3).AsEnumerable().FirstOrDefault();

            return jugada;
        }

        public EstadisticasPartidas GetEstadisticasPartidas(int Usuario_id, string peticion, DateTime? fechaInicio = null, DateTime? fechaFinal = null)
        {
            EstadisticasPartidas stats = new EstadisticasPartidas();

   

            stats.partidas = this.GetPartidas(Usuario_id, peticion, fechaInicio, fechaFinal);

            double rentabilidades = 0;

            double medias = 0 ;
            for (int i = 0; i< stats.partidas.Partidas.Count; i++)
            {
                stats.GananciasPartidasAcumuladas += (stats.partidas.Partidas[i].Cash_Final - stats.partidas.Partidas[i].Cash_Inicial);
                stats.CashInicialPartidas += stats.partidas.Partidas[i].Cash_Inicial;
                stats.CashFinalPartidas += stats.partidas.Partidas[i].Cash_Final;
                medias+=(stats.partidas.Partidas[i].Cash_Final - stats.partidas.Partidas[i].Cash_Inicial);
                rentabilidades+=((stats.partidas.Partidas[i].Cash_Final - stats.partidas.Partidas[i].Cash_Inicial) / stats.partidas.Partidas[i].Cash_Inicial *100);
                
            }

            stats.MediaGananciasPartidas = medias / stats.partidas.Partidas.Count;
            
            

            stats.RentabilidadPartidas = rentabilidades / stats.partidas.Partidas.Count;



            return stats;
        }



        public EstadisticasJugadas GetEstadisticasJugadas(int Usuario_id, string peticion, DateTime? fechaInicio = null, DateTime? fechaFinal = null, string? cell_id = null, int? condicion = null, double? cantidadJugada = null)
            {
            
            
            
            EstadisticasJugadas stats = new EstadisticasJugadas();

       

            stats.partidas = this.GetPartidas(Usuario_id, peticion, fechaInicio, fechaFinal, cell_id, condicion, cantidadJugada);
      


            //List <JugadasCalculadasModel> jugadas = new List<JugadasCalculadasModel>();


            var numRondas = 0;


            //if (cell_id == null)
            //{
            //List<RondaModel> rondasACalcular = stats.partidas.Rondas;


            List<RondaModel> rondasACalcular = new List<RondaModel>();

            List<JugadasCalculadasModel> JugadasRondas = new List<JugadasCalculadasModel>();

            





            for (int i = 0; i < stats.partidas.Rondas.Count; i++)
            {


                string seguimiento = "si";
               var jugadaInsertada = false;
                   for (int x = 0; x <stats.partidas.Jugadas.Count; x++) {
                    if (jugadaInsertada == false)
                    {

                        if (stats.partidas.Rondas[i].Ronda_id == stats.partidas.Jugadas[x].Ronda_id)
                        {
                            
                            

                                if (stats.partidas.Jugadas[x].Seguimiento_Tabla == false)
                                {
                                    seguimiento = "no";

                                    jugadaInsertada = true;
                                    JugadasRondas.Add(stats.partidas.Jugadas[x]);
                                   rondasACalcular.Add(stats.partidas.Rondas[i]);
                                }
                                if (stats.partidas.Jugadas[x].Seguimiento_Tabla == true)
                                {
                                    seguimiento = "si";
                                    jugadaInsertada = true;
                                    JugadasRondas.Add(stats.partidas.Jugadas[x]);
                                   rondasACalcular.Add(stats.partidas.Rondas[i]);

                            }
                                stats.SeguimientoTipoRondas.Add(seguimiento.ToString());
                                stats.Rondas_ids.Add(stats.partidas.Rondas[i].Ronda_id);
                                numRondas++;
                                //if ((partidas.Jugadas[x].Seguimiento_Tabla == true || partidas.Jugadas[x].Seguimiento_Tabla == false) && (contadorFalse ==1 && contadorTrue == 1))
                                //{
                                //    seguimiento = "mixto";
                                //}

                            
                        }
                    }
                    else { break; }
                    
                }
               
               
                
            }

            var rondasSi = 0;
            var rondasNo = 0;

 

            List<double> rentabilidadRondasSiNegativa = new List<double>();
            rentabilidadRondasSiNegativa.Add(0);

            List<double> rentabilidadRondasNoNegativa = new List<double>();
            rentabilidadRondasNoNegativa.Add(0);
            //this.RentabilidadTipoRondas.Add(0);


            for (int i = 0; i < numRondas; i++)
            {
                if (stats.SeguimientoTipoRondas[i].Equals("si"))
                {

                    stats.CantidadesRondas[0] += (rondasACalcular[i].Cantidad_jugada);
                    stats.GananciasTipoRondas[0] += (rondasACalcular[i].Ganancias);
                    stats.CantidadesJugadas[0] += (JugadasRondas[i].Cantidad_jugada_Preflop);
                    stats.MediaCantidadesJugadas[0] += (JugadasRondas[i].Cantidad_jugada_Preflop);

                    if (rondasACalcular[i].Ganancias < 0)
                    {
                        rentabilidadRondasSiNegativa[0] += (-100);
                    }
                    if (rondasACalcular[i].Ganancias > 0)
                    {

                        stats.RentabilidadTipoRondas[0] += ((rondasACalcular[i].Ganancias /*+ rondasACalcular[i].Cantidad_jugada*/) / rondasACalcular[i].Cantidad_jugada) * 100;
                    }
                    stats.MediaGananciasTipoRondas[0] += (rondasACalcular[i].Ganancias);
                    rondasSi++;
                }
                if (stats.SeguimientoTipoRondas[i].Equals("no"))
                {
                    stats.CantidadesRondas[1] += (rondasACalcular[i].Cantidad_jugada);
                    stats.GananciasTipoRondas[1] += (rondasACalcular[i].Ganancias);
                    stats.CantidadesJugadas[1] += (JugadasRondas[i].Cantidad_jugada_Preflop);
                    stats.MediaCantidadesJugadas[1] += (JugadasRondas[i].Cantidad_jugada_Preflop);

                    if (rondasACalcular[i].Ganancias < 0)
                    {
                        rentabilidadRondasNoNegativa[0] += (-100);

                    }
                    if(rondasACalcular[i].Ganancias > 0)
                    {

                        stats.RentabilidadTipoRondas[1] += (rondasACalcular[i].Ganancias / rondasACalcular[i].Cantidad_jugada) * 100;
                    }
                    stats.MediaGananciasTipoRondas[1] += (rondasACalcular[i].Ganancias);
                    rondasNo++;
                }

                // stats.CantidadesJugadasTipoRondas.Add(partidas.Rondas[i].Cantidad_jugada);
                //stats.GananciasTipoRondas.Add(partidas.Rondas[i].Ganancias);
                //stats.RentabilidadTipoRondas.Add((partidas.Rondas[i].Ganancias + partidas.Rondas[i].Cantidad_jugada) / partidas.Rondas[i].Cantidad_jugada);
            }

            stats.MediaCantidadesJugadas[0] = stats.MediaCantidadesJugadas[0] / rondasSi;
            stats.MediaCantidadesJugadas[1] = stats.MediaCantidadesJugadas[1] / rondasNo;


            double rent = stats.RentabilidadTipoRondas[1];

            stats.RentabilidadTipoRondas[0] = (stats.RentabilidadTipoRondas[0] + rentabilidadRondasSiNegativa[0])/ rondasSi;
            stats.RentabilidadTipoRondas[1] = (stats.RentabilidadTipoRondas[1] + rentabilidadRondasNoNegativa[0]) / rondasNo;

            double rentFinal = stats.RentabilidadTipoRondas[1];

            stats.MediaGananciasTipoRondas[0] = stats.MediaGananciasTipoRondas[0] / rondasSi;
            stats.MediaGananciasTipoRondas[1] = stats.MediaGananciasTipoRondas[1] / rondasNo;



            return stats;
        }
    }

     
        
}

