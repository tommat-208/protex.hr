using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProtexhrWebApp.Database;
using Repository;

namespace potexhr.webApp.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class RolesController : Controller
    {

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var l = ProtexhrDB.GetAllRoles();

            if(l!= null)
            {
                return Ok(l); 
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Insert(Role r)
        {
            ProtexhrDB.InsertRole(r);

            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var ris = ProtexhrDB.DeleteRole(id);


            if (ris)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var ris = ProtexhrDB.GetRole(id);

            if (ris!=null)
            {
                return Ok(ris);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult Update(Role r)
        {
            var ris = ProtexhrDB.UpdateRole(r);

            if (ris)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }






    }
}
