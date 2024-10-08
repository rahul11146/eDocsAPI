using eDocsAPI.Models;

namespace eDocsAPI.Interface
{
    public interface IProject
    {
        Task<IList<Project>> Get(string IsActive);
        Task<Project> Find(string id);
        Task Add(Project model);
        Task Update(Project model);
        Task<Project> Remove(Project model);
    }
}
