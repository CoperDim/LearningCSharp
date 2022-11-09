using GuessMyAge.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GuessMyAge.Services
{
    public class PersonService : IPersonService
    {
        List<Person> persons = new List<Person>
            {
                new Person(74, "Steven Spielberg", "Réalisateur", Genre.Man, "Réalisateur, producteur et scénariste Américain. Connu entre autres pour ses films Jurassic Park, ET, Indiana Jones..."),
                new Person(67, "François Hollande", "Politique", Genre.Man, "Président de la République française du 15 mai 2012 au 14 mai 2017."),
                new Person(56, "Joanne Rowling", "Auteure", Genre.Woman, "Romancière britanique de livres pour enfants, notamment la série de livres Harry Potter" ),
                new Person(57, "Brad Pitt", "Réalisateur", Genre.Man, "Acteur américain, a joué dans Fight Club, Benjamin Button"),
                new Person(78, "Catherine Deneuve", "Réalisateur", Genre.Woman, "Actrice français dont les films les plus célèbres sont Les Parapluies de Cherbourg, Belle de jour, Le Dernier Métro ainsi qu'Indochine.")
            };
        public async Task<IEnumerable<Person>> GetAll()
        {
            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri("https://localhost:5001");
                var result = await http.GetAsync("/api/Person");
                var response = await result.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                options.Converters.Add(new DateOnlyConverter());
                IEnumerable<Person> values = JsonSerializer.Deserialize<IEnumerable<Person>>(response, options);
                return values;
            }

        }

        public void AddPerson(Person person)
        {
            persons.Add(person);
        }
    }
}

public class DateOnlyConverter : JsonConverter<DateOnly>
{
    private readonly string serializationFormat;

    public DateOnlyConverter() : this(null)
    {
    }

    public DateOnlyConverter(string? serializationFormat)
    {
        this.serializationFormat = serializationFormat ?? "yyyy-MM-dd";
    }

    public override DateOnly Read(ref Utf8JsonReader reader,
                            Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return DateOnly.Parse(value!);
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value,
                                        JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToString(serializationFormat));
}