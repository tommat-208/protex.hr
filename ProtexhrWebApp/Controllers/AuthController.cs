using Microsoft.AspNetCore.Mvc;
using potexhr.webApp.Database;
using protexhr.repository;

namespace potexhr.webApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        [HttpGet]
        public IActionResult GetUserPerson(string username)
        {
            var up = ProtexhrAuthDB.GetUserPerson(username);

            if (up != null)
            {
                return Ok(up);
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpPost]
        public IActionResult InsertUserPerson(UserPerson up)
        {
            try
            {
                ProtexhrAuthDB.InsertUserPerson(up);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult UpdateUserPerson(UserPerson up)
        {
            try
            {
                var ris = ProtexhrAuthDB.UpdateUserPerson(up);

                if (ris)
                    return Ok();
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult DeleteUserPerson(string username)
        {
            var ris = ProtexhrAuthDB.DeleteUserPerson(username);

            if (ris)
                return Ok();
            else
                return BadRequest();
        }



        [HttpGet("usertypes")]
        public IActionResult GetAllUserType()
        {
            var l = ProtexhrAuthDB.GetAllUserType();

            return Ok(l);
        }

        
        




    }
}
