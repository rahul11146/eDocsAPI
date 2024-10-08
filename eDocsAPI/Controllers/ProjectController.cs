using eDocsAPI.Interface;
using eDocsAPI.Models;
using eDocsAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace eDocsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProject _projectRepository;

        public ProjectController(IProject projectRepository)
        {
            _projectRepository = projectRepository;
        }

        [HttpGet, Route("GetProjects")]
        public IActionResult Index()
        {
            var projects = _projectRepository.Get();
            return Ok(projects);
        }

        [HttpGet, Route("GetProjectDetails")]
        public async Task<IActionResult> Details(string id)
        {
            var project = await _projectRepository.Find(id);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        //public IActionResult Create()
        //{
        //    return Ok();
        //}

        [HttpPost, Route("AddProject")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Project model)
        {
            if (ModelState.IsValid)
            {
                await _projectRepository.Add(model);
            }
            return Ok(new { IsSuccess = "Y", result = model.ProjectName + " added successfully !!" });
        }

        [HttpGet, Route("EditProject")]
        public async Task<IActionResult> Edit(string id)
        {
            var product = await _projectRepository.Find(id);

            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost, Route("UpdateProject")]
        public async Task<IActionResult> Edit(string id, Project model)
        {
            var product = await _projectRepository.Find(id);

            if (product == null)
            {
                return BadRequest();
            }


            await _projectRepository.Update(model);
            return Ok(new { IsSuccess = "Y", result = model.ProjectName + " updated successfully !!" });
        }

        [HttpPost, Route("DeleteProject")]
        public async Task<IActionResult> Delete(string id)
        {
            var product = await _projectRepository.Find(id);

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
            var product = await _projectRepository.Find(id);
            await _projectRepository.Remove(product);
            return RedirectToAction(nameof(Index));
        }

    }
}
