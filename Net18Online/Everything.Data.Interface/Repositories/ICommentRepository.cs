using Everything.Data.Interface.Models;

namespace Everything.Data.Interface.Repositories;

public interface ICommentRepository<T> : IBaseRepository<T>
    where T : ICommentData
{
}
