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
    public class PlayListCrudFactory : CrudFactory
    {
        PlaylistMapper mapper;
        public PlayListCrudFactory() : base()
        {
            mapper = new PlaylistMapper();
            dao = SqlDAO.GetInstance();
        }
        public override void Create(BaseDTO dto)
        {
            var play = (Playlist)dto;

            var sqlOperation = mapper.GetCreateStatements(play);

            dao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(BaseDTO dto)
        {
            Playlist play = (Playlist)dto;
            dao.ExecuteProcedure(mapper.DeleteStatements(play));
        }

        public void DeletePlaylist(int playlistId)
        {
            var sqlOperation = mapper.DeletePlaylist(playlistId);

            dao.ExecuteProcedure(sqlOperation);
        }

        public override T Retrieve<T>()
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            var playlist = new List<T>();
            var sqlOperation = mapper.GetRetriveAllStatement();
            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objsPojo = mapper.BuildObjects(lstResult);
                foreach (var c in objsPojo)
                {
                    playlist.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return playlist;
        }

        public override T RetrieveById<T>(int Id)
        {
            throw new NotImplementedException();
        }

        public override void Update(BaseDTO dto)
        {
            var play = (Playlist)dto;

            var sqlOperation = mapper.GetUpdateStatements(play);

            dao.ExecuteProcedure(sqlOperation);
        }
    }
}
