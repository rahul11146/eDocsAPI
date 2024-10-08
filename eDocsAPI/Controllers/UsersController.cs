using eDocsAPI.Interface;
using eDocsAPI.Models;
using eDocsAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace eDocsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUser _userRepository;

        public UsersController(IUser userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet, Route("GetProjects")]
        public IActionResult Index(string IsActive)
        {
            var projects = _userRepository.Get(IsActive).Result;
            return Ok(projects);
        }

        [HttpGet, Route("GetProjectDetails")]
        public async Task<IActionResult> Details(string id)
        {
            var project = await _userRepository.Find(id);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        [HttpGet, Route("Authenticate")]
        public async Task<IActionResult> Authenticate(string username, string password)
        {
            var user = await _userRepository.Authenticate(username, password);

            if (user?.UserName == null)
            {
                return NotFound();
            }

            return Ok(user);
        }


        [HttpPost, Route("AddProject")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Users model)
        {
            if (ModelState.IsValid)
            {
                await _userRepository.Add(model);
            }
            return Ok(new { IsSuccess = "Y", result = model.UserName + " added successfully !!" });
        }

        [HttpGet, Route("EditProject")]
        public async Task<IActionResult> Edit(string id)
        {
            var product = await _userRepository.Find(id);

            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost, Route("UpdateProject")]
        public async Task<IActionResult> Edit(string id, Users model)
        {
            var product = await _userRepository.Find(id);

            if (product == null)
            {
                return BadRequest();
            }


            await _userRepository.Update(model);
            return Ok(new { IsSuccess = "Y", result = model.UserName + " updated successfully !!" });
        }

        [HttpPost, Route("DeleteProject")]
        public async Task<IActionResult> Delete(string id)
        {
            var product = await _userRepository.Find(id);

            if (product == null)
            {
                return BadRequest();
            }
            return Ok(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            var product = await _userRepository.Find(id);
            await _userRepository.Remove(product);
            return RedirectToAction(nameof(Index));
        }

    }
}
