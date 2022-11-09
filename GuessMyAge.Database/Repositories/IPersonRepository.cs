using GuessMyAge.Database.Entities;

namespace GuessMyAge.Database.Repositories
{
    public interface IPersonRepository
    {
        void Create(PersonDatabaseEntity person);

        void Update(PersonDatabaseEntity person);

        IEnumerable<PersonDatabaseEntity> GetAll();

        PersonDatabaseEntity GetById(int id);
        void Delete(int id);
    }
}
