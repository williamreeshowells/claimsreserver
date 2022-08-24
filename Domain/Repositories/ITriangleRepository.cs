using Domain.AggregateRoots;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface ITriangleRepository
    {
        List<Triangle> GetAll(string filePath);
        void SaveCalculation(string filePath, TriangleCalculation calculation);
    }
}
