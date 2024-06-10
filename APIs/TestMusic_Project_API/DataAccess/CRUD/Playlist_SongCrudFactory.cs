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
    public class Playlist_SongCrudFactory : CrudFactory
    {
        Playlist_SongMapper mapper;
        public Playlist_SongCrudFactory() : base()
        {
            mapper = new Playlist_SongMapper();
            dao = SqlDAO.GetInstance();
        }

        public override void Create(BaseDTO dto)
        {
            throw new NotImplementedException();
        }

        public override void Delete(BaseDTO dto)
        {
            throw new NotImplementedException();
        }

        // Asignar canciones a un PlayList
        public void InsertSongToPlaylist(int playlistId, int songId)
        {
            var sqlOperation = mapper.GetInsertSongToPlaylistStatements(playlistId,songId);

            dao.ExecuteProcedure(sqlOperation);
        }
        

        // Remover canciones a un PlayList
        public void RemoveSongFromPlaylist(int playlistId, int songId)
        {
            var sqlOperation = mapper.GetRemoveSongFromPlaylistStatements(playlistId, songId);

            dao.ExecuteProcedure(sqlOperation);
        }

        public override T Retrieve<T>()
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            throw new NotImplementedException();
        }

        //  Desplegar canciones presentes en un PlayList
        public override T RetrieveById<T>(int Id)
        {
            var operation = mapper.GetRetrieveByIdStatements(Id);
            var lstResult = dao.ExecuteQueryProcedure(operation);
            var objects = mapper.BuildObjects(lstResult);

            if (objects.Count > 0)
                return (T)Convert.ChangeType(objects[0], typeof(T));
            else
                return default(T);
        }

        public override void Update(BaseDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
