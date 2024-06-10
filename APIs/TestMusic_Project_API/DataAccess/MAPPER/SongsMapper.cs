using DataAccess.DAO;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.MAPPER
{
    public class SongsMapper : IObjectMapper, ISqlStatements
    {
        public BaseDTO BuildObject(Dictionary<string, object> row)
        {
            var song = new Song()
            {
                Id = (int)row["id"],
                Title = (string)row["title"],
                ArtistName = (string)row["artistName"],
                Album = (string)row["album"],
                Duration = (TimeSpan)row["duration"],

            };

            return song;
        }

        public List<BaseDTO> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseDTO>();

            foreach (var row in lstRows)
            {
                var song = BuildObject(row);
                lstResults.Add(song);
            }

            return lstResults;
        }

        public SqlOperation DeleteStatements(BaseDTO Dto)
        {
            throw new NotImplementedException();
        }

        //Para crear canciones
        public SqlOperation GetCreateStatements(BaseDTO Dto)
        {
            var operation = new SqlOperation()
            {
                ProcedureName = "CREATE_SONGS_PR"
            };

            var s = (Song)Dto;
            operation.AddVarcharParam("TITLE", s.Title);
            operation.AddVarcharParam("ARTIST_NAME", s.ArtistName);
            operation.AddVarcharParam("ALBUM", s.Album);
            operation.AddTimeParam("DURATION", s.Duration);

            return operation;
        }

        public SqlOperation GetRetrieveByIdStatements(int Id)
        {
            throw new NotImplementedException();
        }

        //Listar Canciones --> Catálogo de Canciones
        public SqlOperation GetRetriveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_SONGS_PR" };
            return operation;
        }

        public SqlOperation GetUpdateStatements(BaseDTO Dto)
        {
            throw new NotImplementedException();
        }
    }
}
