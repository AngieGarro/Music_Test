using DataAccess.CRUD;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicManagers.Managers
{
    public class SongManager
    {
        public SongManager() { }

        public void Create(Song song)
        {
            var crud = new SongCrudFactory();
            crud.Create(song);
        }


        public List<Song> RetrieveAll()
        {
            var crud = new SongCrudFactory();
            var List = crud.RetrieveAll<Song>();

            return List;
        }

    }
}
