using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProtexhrWebApp.Database;
using Repository;

namespace potexhr.webApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AttendancesController : Controller
    {
        [HttpPost]
        public IActionResult Insert(AttendanceRecord a)
        {
            ProtexhrDB.InsertAttendanceRecord(a);

            return Ok();
        }

        [HttpGet]
        public IActionResult GetByEmployeeId(int employee_id)
        {
            var l = ProtexhrDB.GetAttendanceRecordsByEmployeeId(employee_id);

            if(l != null)
            {
                return Ok(l);
            }
            else
            {
                return BadRequest();
            }
        }



    }
}
