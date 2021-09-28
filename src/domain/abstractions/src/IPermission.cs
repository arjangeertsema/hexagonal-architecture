using System.Threading.Tasks;

namespace example.domain.abstractions
{
    public interface IPermission
    {
        Task<bool> HasPermission(params string[] permissions);
    }
}