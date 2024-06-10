using DataAccess.DAO;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.MAPPER
{
    public class PlaylistMapper : IObjectMapper, ISqlStatements
    {
        public BaseDTO BuildObject(Dictionary<string, object> row)
        {
            var playlist = new Playlist()
            {
                Id = (int)row["id"],
                Name = (string)row["name"],
                CreateDate = (DateTime)row["createDate"],

            };

            return playlist;
        }

        public List<BaseDTO> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseDTO>();

            foreach (var row in lstRows)
            {
                var playlist = BuildObject(row);
                lstResults.Add(playlist);
            }

            return lstResults;
        }

        //ELIMINAR PLAYLIST
        public SqlOperation DeletePlaylist(int id)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_PLAYLIST_PR" };
            operation.AddIntParam("ID", id);

            return operation;

        }
        public SqlOperation DeleteStatements(BaseDTO Dto)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_PLAYLIST_PR" };

            var play = (Playlist)Dto;
            operation.AddIntParam("ID", play.Id);

            return operation;
        }

        //CREAR PLAYLIST
        public SqlOperation GetCreateStatements(BaseDTO Dto)
        {
            var operation = new SqlOperation()
            {
                ProcedureName = "CREATE_PLAYLIST_PR"
            };

            var play = (Playlist)Dto;
            operation.AddVarcharParam("NAME", play.Name);
            operation.AddDateTimeParam("CREATE_DATE", play.CreateDate);

            return operation;
        }

        public SqlOperation GetRetrieveByIdStatements(int Id)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetriveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_PLAYLIST_PR" };
            return operation;
        }

        public SqlOperation GetUpdateStatements(BaseDTO Dto)
        {
            var operation = new SqlOperation()
            {
                ProcedureName = "UPD_PLAYLIST_PR"
            };

            var play = (Playlist)Dto;
            operation.AddIntParam("ID", play.Id);
            operation.AddVarcharParam("NAME", play.Name);

            return operation;
        }
    }
}
