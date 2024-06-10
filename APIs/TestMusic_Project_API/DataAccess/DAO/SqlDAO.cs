using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
	/*
	 * Clase: Permite comunicarse a la Base de Datos.
	 * Ejecuta las secuencias del store procedure.
	 * Unica clase con esta capacidad.
	 * Implementa el Patron singleton - Solo exista una unica instancia a la clase.
	 */
		public class SqlDAO
		{
			//ALMACENA LA RUTA DE ACCESO A LA BD
			private string connectionString = "";

			//SINGLETON: CLASE PV A LA MISMA INSTANCIA.
			private static SqlDAO instance;

			//CONTRUCTOR PRIVADO: CONTIENE LA RUTA.
			private SqlDAO()
			{
            connectionString = "Data Source=localhost;Initial Catalog=Bd_TestMusic;Integrated Security=True;";
			}

        //IMPLEMENTA EL PATRÓN SINGLETON - METODO QUE EXPONE LA INSTANCIA.
        public static SqlDAO GetInstance()
			{
				if (instance == null)
				{
					instance = new SqlDAO();
				}

				return instance;
			}

			//ENVIA DATA A LA BD POR MEDIO DEL STORE PROCEDURE.
			public void ExecuteProcedure(SqlOperation operation)
			{
				using (var conn = new SqlConnection(connectionString))
				using (var command = new SqlCommand(operation.ProcedureName, conn)
				{
					CommandType = CommandType.StoredProcedure
				})
				{
					foreach (var param in operation.Parameters)
					{
						command.Parameters.Add(param);
					}

					conn.Open();
					command.ExecuteNonQuery();
				}

			}

			//Encargado de ejecutar Store Procedures pero con una Respuesta.
			public List<Dictionary<string, object>> ExecuteQueryProcedure(SqlOperation operation)
			{
				var lstResult = new List<Dictionary<string, object>>();

				using (var conn = new SqlConnection(connectionString))
				using (var command = new SqlCommand(operation.ProcedureName, conn)
				{
					CommandType = CommandType.StoredProcedure
				})
				{
					foreach (var param in operation.Parameters)
					{
						command.Parameters.Add(param);
					}

					conn.Open();

					//Inicia el proceso de extraccion de la Data.
					var reader = command.ExecuteReader();
					//Validar Resultados.
					if (reader.HasRows)
					{
						//Si hay contenido, inicia el proceso de lectura del Row. (While)
						while (reader.Read())
						{
							var dict = new Dictionary<string, object>();
							for (var lp = 0; lp < reader.FieldCount; lp++)
							{
								//Se define la llave y el valor.
								var Key = reader.GetName(lp);
								var Value = reader.GetValue(lp);

								dict[Key] = Value;
								//dict.Add(reader.GetName(lp), reader.GetValue(lp));
							}
							lstResult.Add(dict);
						}
					}
				}

				return lstResult;
			}
		}
	}

