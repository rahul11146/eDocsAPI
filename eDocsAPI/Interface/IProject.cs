using eDocsAPI.Models;

namespace eDocsAPI.Interface
{
    public interface IProject
    {
        Task<IEnumerable<Project>> Get();
        Task<Project> Find(string id);
        Task Add(Project model);
        Task Update(Project model);
        Task<Project> Remove(Project model);
    }
}
