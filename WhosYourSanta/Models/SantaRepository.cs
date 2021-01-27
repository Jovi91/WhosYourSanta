using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhosYourSanta.Models
{
    public class SantaRepository : ISantaRepository
    {
        public AppDbContext Context { get; }        

        public SantaRepository(AppDbContext context)
        {
            Context = context;
        }


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
            // return Context.Santas.Find(Id);
            return Context.Santas.Include("DrawnSanta").Where(s => s.Id == Id).FirstOrDefault();
        }

        public IEnumerable<Santa> GetSantasFromLottery(int idLottery)
        {
            
            throw new NotImplementedException();
        }

        public Santa Update(Santa santaChanges)
        {
            var santa = Context.Santas.Attach(santaChanges);
            santa.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            Context.SaveChanges();

            return santaChanges;
        }

        public Santa GetDrawnSanta(int santaWhoDrawsId)
        {
            var Santa = Context.Santas.Include("DrawnSanta").Where(i => i.Id == santaWhoDrawsId).FirstOrDefault();
            //var Santa = Context.Santas.Where(i => i.Id == santaWhoDrawsId).FirstOrDefault();
            var DrawnSanta = Santa.DrawnSanta;
            return DrawnSanta;
        }

        public List<Santa> GetSantasWithEmail(string email)
        {
            return GetAllSantas().Where(s => s.Email == email).ToList();
        }
    }
}
