using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProtexhrWebApp.Database;
using Repository;

namespace ProtexhrWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : Controller
    {

        [HttpGet("all")]
       public IActionResult GetAll()
        {
            var e = ProtexhrDB.GetAllEmployees();

            if(e != null) 
            { 
                return Ok(e);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult Get(int id) 
        { 
            var e = ProtexhrDB.GetEmployee(id);

            if (e != null)
            {
                return Ok(e);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult InsertEmployee(Employee e)
        {
            ProtexhrDB.InsertEmployee(e);

            return Ok();
        }

        // Meglio usare PUT cuz ho letto e' usato per rimpiazzare dati esistenti
        [HttpPut]
        public IActionResult UpdateEmployee(Employee e) 
        {
            var ris = ProtexhrDB.UpdateEmployee(e);

            if (ris)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }



        [HttpDelete]
        public IActionResult DeleteEmployee(int id)
        {
            var ris = ProtexhrDB.DeleteEmployee(id);

            if (ris)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        /*
        // GET: ProtexhrAPI
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProtexhrAPI/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProtexhrAPI/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProtexhrAPI/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProtexhrAPI/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProtexhrAPI/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProtexhrAPI/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProtexhrAPI/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        */
    }
}
