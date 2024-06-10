using DataAccess.CRUD;
using DataAccess.MAPPER;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicManagers.Managers
{
    public class Playlist_SongManager
    {
        public Playlist_SongManager() { }

        public void InsertSongToPlaylist(int playlistId, int songId)
        {
            var crud = new Playlist_SongCrudFactory();
            crud.InsertSongToPlaylist(playlistId,songId);
        }

        public void RemoveSongPlaylist(int playlistId, int songId)
        {
            var crud = new Playlist_SongCrudFactory();
            crud.RemoveSongFromPlaylist(playlistId, songId);
        }

        public T RetrieveById<T>(int Id)
        {
            var crud = new Playlist_SongCrudFactory();
            var playlist = crud.RetrieveById<T>(Id);
            return playlist;
        }

    }
}
