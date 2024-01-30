using System.Xml.Linq;
using VisaAPI.Models;

namespace VisaAPI.Services
{
    public class VisaService
    {
        static List<Visa> Visas { get; }
        static int nextId = 3;
        static VisaService()
        {
            Visas = new List<Visa>
        {
            new Visa { Id = 1, FirstName = "Vince Clark", LastName = "Tañola", Country = "Philippines" },
            new Visa { Id = 2, FirstName = "John", LastName = "Doe", Country = "America"}
        };
        }

        public static List<Visa> GetAll() => Visas;

        public static Visa? Get(int id) => Visas.FirstOrDefault(p => p.Id == id);

        public static void Add(Visa visa)
        {
            visa.Id = nextId++;
            Visas.Add(visa);
        }

        public static void Delete(int id)
        {
            var visa = Get(id);
            if (visa is null)
                return;

            Visas.Remove(visa);
        }

        public static void Update(Visa visa)
        {
            var index = Visas.FindIndex(p => p.Id == visa.Id);
            if (index == -1)
                return;

            Visas[index] = visa;
        }
    }
}
