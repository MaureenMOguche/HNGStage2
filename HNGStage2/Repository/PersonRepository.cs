using HNGStage2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace HNGStage2.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly AppDbContext _db;

        public PersonRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Person?> AddPerson(PersonDto person)
        {
            Person newPerson = new Person()
            {
                Name = person.Name,
            };
            var result = await _db.Persons.AddAsync(newPerson);
            var res = await _db.SaveChangesAsync();
            
            return result.Entity;
        }

        public async Task<int> DeletePerson(string userId)
        {
            var res = int.TryParse(userId, out int id);
            if (res == true)
            {
                var person = await _db.Persons.FirstOrDefaultAsync(x => x.Id.Equals(id));
                _db.Persons.Remove(person);
                return await _db.SaveChangesAsync();
            }
            var personI = await _db.Persons.FirstOrDefaultAsync(x => x.Name.ToLower().Contains(userId));
            _db.Persons.Remove(personI);
            return await _db.SaveChangesAsync();
        }

        public async Task<List<Person>?> GetPeople(Expression<Func<Person, bool>>? predicate = null)
        {
            IQueryable<Person> people = _db.Persons;
            if (predicate != null)
            {
                people = people.Where(predicate);
                return await people.ToListAsync();
            }
            
            return await people.ToListAsync();
        }

        public async Task<Person?> GetPerson(string user_id)
        {
            Person person = new();
            var res = int.TryParse(user_id, out int id);
            if (res == true)
            {
                person = await _db.Persons.FirstOrDefaultAsync(x => x.Id.Equals(id));
                return person;
            }
             person = await _db.Persons.FirstOrDefaultAsync(x => x.Name.ToLower().Contains(user_id.ToLower()));
            
            return person;
        }

        public async Task<Person?> UpdatePerson(Person person)
        {
            
            var result = _db.Update(person);
            var res = await _db.SaveChangesAsync();
            return result.Entity;
        }
    }
}
