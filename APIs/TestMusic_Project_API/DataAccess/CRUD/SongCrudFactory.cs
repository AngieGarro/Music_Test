using DataAccess.DAO;
using DataAccess.MAPPER;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CRUD
{
    public class SongCrudFactory : CrudFactory
    {
        SongsMapper mapper;
        public SongCrudFactory() : base()
        {
            mapper = new SongsMapper();
            dao = SqlDAO.GetInstance();
        }

        public override void Create(BaseDTO dto)
        {
            var song = (Song)dto;

            var sqlOperation = mapper.GetCreateStatements(song);

            dao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(BaseDTO dto)
        {
            throw new NotImplementedException();
        }

        public override T Retrieve<T>()
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            var list = new List<T>();
            var sqlOperation = mapper.GetRetriveAllStatement();
            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objsPojo = mapper.BuildObjects(lstResult);
                foreach (var c in objsPojo)
                {
                    list.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return list;
        }

        public override T RetrieveById<T>(int Id)
        {
            throw new NotImplementedException();
        }

        public override void Update(BaseDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
