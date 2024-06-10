using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
	/*
	 * Clase: Encargada de definir los procedures de la Base de Datos.
	 * Los parametros que requiere para su función.
	 */
	public class SqlOperation
	{
		public string ProcedureName { get; set; }

		public List<SqlParameter> Parameters { get; set; }

		public SqlOperation()
		{

			Parameters = new List<SqlParameter>();
		}

		//Metodos para agregar parametros a la lista.

		//AGREGA UN PARAMETRO VARCHAR:
		public void AddVarcharParam(string paramName, string paramValue)
		{
			Parameters.Add(new SqlParameter("@P_" + paramName, paramValue));
		}

		//AGREGA UN PARAMETRO INT:
		public void AddIntParam(string paramName, int paramValue)
		{
			var param = new SqlParameter("@P_" + paramName, SqlDbType.Int)
			{
				Value = paramValue
			};
			Parameters.Add(param);
		}


		//AGREGA UN PARAMETRO SMALL INT:
		public void AddSmallIntParam(string paramName, int paramValue)
		{
			var param = new SqlParameter("@P_" + paramName, SqlDbType.SmallInt)
			{
				Value = paramValue
			};
			Parameters.Add(param);
		}

		//AGREGA UN PARAMETRO DOUBLE:
		public void AddDoubleParam(string paramName, double paramValue)
		{
			var param = new SqlParameter("@P_" + paramName, SqlDbType.Decimal)
			{
				Value = paramValue
			};
			Parameters.Add(param);
		}

		//AGREGA UN PARAMETRO DATE TIME:
		public void AddDateTimeParam(string paramName, DateTime paramValue)
		{
			var param = new SqlParameter("@P_" + paramName, SqlDbType.DateTime)
			{
				Value = paramValue
			};
			Parameters.Add(param);
		}

		//AGREGA UN PARAMETRO DATE:
		public void AddDateParam(string paramName, DateTime paramValue)
		{
			var param = new SqlParameter("@P_" + paramName, SqlDbType.Date)
			{
				Value = paramValue
			};
			Parameters.Add(param);
		}

        //AGREGA UN PARAMETRO TIME SPAN:
        public void AddTimeParam(string paramName, TimeSpan paramValue)
        {
            SqlParameter param = new SqlParameter("@P_" + paramName, SqlDbType.Time);
            param.Value = paramValue;
            Parameters.Add(param);
        }

    }
}
