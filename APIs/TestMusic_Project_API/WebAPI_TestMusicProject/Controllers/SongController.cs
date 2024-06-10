using DTOs;
using LogicManagers.Managers;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI_TestMusicProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : Controller
    {
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] Song song)
        {
            try
            {
                var logic = new SongManager();
                logic.Create(song);
                return Ok(song);

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
                var logic = new SongManager();
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
