using HNGStage2.Models;
using System.Linq.Expressions;

namespace HNGStage2.Repository
{
    public interface IPersonRepository
    {
        Task<int> AddPerson(PersonDto person);
        Task<int> UpdatePerson(Person person);
        Task<Person?> GetPerson(string user_id);
        Task<List<Person>?> GetPeople(Expression<Func<Person, bool>>? predicate = null);
        Task<int> DeletePerson(string user_id);
    }
}
