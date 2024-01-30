using Microsoft.AspNetCore.Mvc;
using VisaAPI.Models;
using VisaAPI.Services;

namespace VisaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VisaController : ControllerBase
    {
        public VisaController()
        {

        }

        // GET all action
        [HttpGet]
        public ActionResult<List<Visa>> GetAll() =>
        VisaService.GetAll();

        // GET by Id action
        [HttpGet("{id}")]
        public ActionResult<Visa> Get(int id)
        {
            var visa = VisaService.Get(id);

            if (visa == null)
                return NotFound();

            return visa;
        }

        // POST action
        [HttpPost]
        public IActionResult Create(Visa visa)
        {
            VisaService.Add(visa);
            return CreatedAtAction(nameof(Get), new { id = visa.Id }, visa);
        }

        // PUT action
        [HttpPut("{id}")]
        public IActionResult Update(int id, Visa visa)
        {
            if (id != visa.Id)
                return BadRequest();

            var existingVisa = VisaService.Get(id);
            if (existingVisa is null)
                return NotFound();

            VisaService.Update(visa);

            return NoContent();
        }

        // DELETE action
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var visa = VisaService.Get(id);

            if (visa is null)
                return NotFound();

            VisaService.Delete(id);

            return NoContent();
        }
    }
}
