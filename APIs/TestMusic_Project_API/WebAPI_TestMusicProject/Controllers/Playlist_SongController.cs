using DTOs;
using LogicManagers.Managers;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI_TestMusicProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Playlist_SongController : Controller
    {
        [HttpPost]
        [Route("Create/{playlistId}/{songId}")]
        public async Task<IActionResult> InsertSong(int playlistId, int songId)
        {
            try
            {
                var logic = new Playlist_SongManager();
                logic.InsertSongToPlaylist(playlistId,songId);
                return Ok("Insert Ok");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete]
        [Route("Delete/{playlistId}/{songId}")]
        public async Task<IActionResult> RemoveSong(int playlistId, int songId)
        {
            try
            {
                var logic = new Playlist_SongManager();
                logic.RemoveSongPlaylist(playlistId,songId);
                return Ok("Revome Ok");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("RetrieveById/{id}")]
        public async Task<IActionResult> RetrieveById(int id)
        {
            try
            {
                var logic = new Playlist_SongManager();
                var playlist = logic.RetrieveById<Playlist>(id);
                return Ok(playlist);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
