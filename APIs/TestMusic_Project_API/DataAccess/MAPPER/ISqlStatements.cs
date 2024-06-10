using DataAccess.DAO;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.MAPPER
{
	public interface ISqlStatements
	{
		SqlOperation GetCreateStatements(BaseDTO Dto);

		SqlOperation GetUpdateStatements(BaseDTO Dto);

		SqlOperation DeleteStatements(BaseDTO Dto);

		SqlOperation GetRetrieveByIdStatements(int Id);

		SqlOperation GetRetriveAllStatement();
	}
}
