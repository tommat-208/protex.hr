using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProtexhrWebApp.Database;
using Repository;

namespace potexhr.webApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeavesController : Controller
    {
        [HttpPost]
        public IActionResult Insert(LeaveRequest l)
        {
            ProtexhrDB.InsertLeaveRequest(l);

            return Ok();
        }

        [HttpGet("pending")]
        public IActionResult GetPending()
        {
            var l = ProtexhrDB.GetPendingLeaveRequests();

            if (l != null)
            {
                return Ok(l);
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpPut("approve")]
        public IActionResult Approve(LeaveRequest l)
        {
            l.Status = LeaveRequestStatus.Approved;
            var ris = ProtexhrDB.UpdateLeaveRequest(l);

            if (ris)
                return Ok();
            else
                return BadRequest();
        }


        [HttpPut("reject")]
        public IActionResult Reject(LeaveRequest l)
        {
            l.Status = LeaveRequestStatus.Rejected;
            var ris = ProtexhrDB.UpdateLeaveRequest(l);

            if (ris)
                return Ok();
            else
                return BadRequest();
        }

    }
}
