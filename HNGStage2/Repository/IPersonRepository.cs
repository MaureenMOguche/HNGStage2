using HNGStage2.Models;
using System.Linq.Expressions;

namespace HNGStage2.Repository
{
    public interface IPersonRepository
    {
        Task<Person?> AddPerson(PersonDto person);
        Task<Person?> UpdatePerson(Person person);
        Task<Person?> GetPerson(string user_id);
        Task<List<Person>?> GetPeople(Expression<Func<Person, bool>>? predicate = null);
        Task<int> DeletePerson(string user_id);
    }
}
