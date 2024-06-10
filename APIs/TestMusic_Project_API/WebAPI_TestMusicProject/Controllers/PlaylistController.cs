using DTOs;
using LogicManagers.Managers;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI_TestMusicProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistController : Controller
    {
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] Playlist play)
        {
            try
            {
                var logic = new PlaylistManager();
                logic.Create(play);
                return Ok(play);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(Playlist play)
        {
            try
            {
                var logic = new PlaylistManager();
                logic.Update(play);
                return Ok(play);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete]
        [Route("Delete/{playlistId}")]
        public async Task<IActionResult> DeletePlaylist(int playlistId)
        {
            try
            {
                var logic = new PlaylistManager();
                logic.DeletePlaylist(playlistId);
                return Ok(playlistId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Llamado de Datos.
        [HttpGet]
        [Route("RetrieveAll")]
        public async Task<IActionResult> RetrieveAll()
        {
            try
            {
                var logic = new PlaylistManager();
                var List = logic.RetrieveAll();
                return Ok(List);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
