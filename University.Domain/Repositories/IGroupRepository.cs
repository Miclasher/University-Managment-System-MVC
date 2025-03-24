
using University.Domain.Models;
namespace University.Domain.Repositories
{
    public interface IGroupRepository : IRepository<Group>
    {
        Task DeleteStudentsFromGroup(Guid id, CancellationToken cancellationToken = default);
    }
}
