using DataAccess.CRUD;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicManagers.Managers
{
    public class PlaylistManager
    {
        public PlaylistManager() { }

        public void Create(Playlist play)
        {
            var crud = new PlayListCrudFactory();
            crud.Create(play);
        }

        public void Delete(Playlist play)
        {
            var crud = new PlayListCrudFactory();
            crud.Delete(play);
        }

        public void DeletePlaylist(int playlistId)
        {
            var crud = new PlayListCrudFactory();
            crud.DeletePlaylist(playlistId);
        }

        public void Update(Playlist play)
        {
            var crud = new PlayListCrudFactory();
            crud.Update(play);
        }

        public List<Playlist> RetrieveAll()
        {
            var crud = new PlayListCrudFactory();
            var List = crud.RetrieveAll<Playlist>();

            return List;
        }

    }
}
