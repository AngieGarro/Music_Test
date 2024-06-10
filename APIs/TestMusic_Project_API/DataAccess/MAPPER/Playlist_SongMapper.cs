using DataAccess.DAO;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.MAPPER
{
    public class Playlist_SongMapper : IObjectMapper
    {
        public BaseDTO BuildObject(Dictionary<string, object> row)
        {
            var playlist = new Playlist
            {
                Id = (int)row["id_playlist"],
                Name = row["name"].ToString(), 
                Songs = new List<Song>()
            };

            var song = new Song
            {
                Id = (int)row["id_song"],
                Title = row["title"].ToString(),
                ArtistName = row["artistName"].ToString(),
                Album = row["album"].ToString(),
                Duration = (TimeSpan)row["duration"]
            };

            playlist.Songs.Add(song);

            return playlist;
        }

        public List<BaseDTO> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var playlists = new List<Playlist>();
            Playlist currentPlaylist = null;

            foreach (var row in lstRows)
            {
                if (currentPlaylist == null || (int)row["id_playlist"] != currentPlaylist.Id)
                {
                    currentPlaylist = new Playlist
                    {
                        Id = (int)row["id_playlist"],
                        Name = row["name"].ToString(),
                        Songs = new List<Song>()
                    };
                    playlists.Add(currentPlaylist);
                }

                var song = new Song
                {
                    Id = (int)row["id_song"],
                    Title = row["title"].ToString(),
                    ArtistName = row["artistName"].ToString(),
                    Album = row["album"].ToString(),
                    Duration = (TimeSpan)row["duration"]
                };

                currentPlaylist.Songs.Add(song);
            }

            return playlists.Cast<BaseDTO>().ToList(); ;
        }

        public SqlOperation GetInsertSongToPlaylistStatements(int playlistId, int songId)
        {
            var operation = new SqlOperation()
            {
                ProcedureName = "ADD_SongToPlayList"
            };

            operation.AddIntParam("id_playlist", playlistId);
            operation.AddIntParam("id_song", songId);

            return operation;
        }

        public SqlOperation GetRetrieveByIdStatements(int Id)
        {
            var operation = new SqlOperation
            {
                ProcedureName = "LIST_SongsInPlaylist"
            };
            operation.AddIntParam("id_playlist", Id);
            return operation;
        }

        public SqlOperation GetRemoveSongFromPlaylistStatements(int playlistId, int songId)
        {
            var operation = new SqlOperation()
            {
                ProcedureName = "REMOVE_SongFromPlayList"
            };

            operation.AddIntParam("id_playlist", playlistId);
            operation.AddIntParam("id_song", songId);

            return operation;
        }

    }
}
