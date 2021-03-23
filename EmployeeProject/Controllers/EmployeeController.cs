using DataModel.Models;
using DataModel.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmployeeProject.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeController(IEmployeeRepository repository) => _repository = repository;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _repository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _repository.GetSingleByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
                return BadRequest(employee);
            var result = await _repository.Insert(employee);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
                return BadRequest(employee);
            var result = await _repository.Update(employee);

            return result ? Ok("Updated Successfully") : Problem("Error while updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repository.Remove(id);

            return result ? Ok("Removed have been successfully") : Problem("Error while Deleting");
        }
    }
}
