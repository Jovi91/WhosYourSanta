using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhosYourSanta.Models
{
    public class SantaRepository : ISantaRepository
    {
        public SantaRepository(AppDbContext context)
        {
            Context = context;
        }

        public AppDbContext Context { get; }

        public Santa Add(Santa santa)
        {
            Context.Santas.Add(santa);
            Context.SaveChanges();
            return santa;
        }

        public Santa Delete(int Id)
        {
            Santa santa = Context.Santas.Find(Id);

            if(santa != null)
            {
                Context.Santas.Remove(santa);
                Context.SaveChanges();
            }

            return santa;

        }

        public IEnumerable<Santa> GetAllSantas()
        {
            return Context.Santas;
        }

        public Santa GetSanta(int Id)
        {
           return Context.Santas.Find(Id);

        }

        public Santa Update(Santa santaChanges)
        {
            var santa = Context.Santas.Attach(santaChanges);
            santa.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            Context.SaveChanges();

            return santaChanges;
        }
    }
}
