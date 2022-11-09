using GuessMyAge.Business.Converters;
using GuessMyAge.Database.Repositories;
using GuessMyAge.Models;

namespace GuessMyAge.Business.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public Person Create(Person model)
        {
            var entity = model.ToDatabaseEntity();

            _personRepository.Create(entity);
            return GetById(entity.Id);
        }

        public void Delete(int id)
        {
            _personRepository.Delete(id);
        }

        public IEnumerable<Person> GetAll()
        {
            var entities = _personRepository.GetAll();

            return entities.ToModels();
        }

        public Person GetById(int id)
        {
            var entity = _personRepository.GetById(id);

            return entity.ToPerson();
        }

        public void Update(Person model)
        {
            _personRepository.Update(model.ToDatabaseEntity());
        }
    }
}
