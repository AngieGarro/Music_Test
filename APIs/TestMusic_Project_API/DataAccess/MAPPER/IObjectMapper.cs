using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.MAPPER
{
	interface IObjectMapper
	{
		List<BaseDTO> BuildObjects(List<Dictionary<string, object>> lstRows);
		BaseDTO BuildObject(Dictionary<string, object> row);
	}
}
