using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhosYourSanta.Models
{
    public class MockLotteryRepository: ILotteryRepository
    {
        public AppDbContext Context { get; }
        
        List<IdentityUser> users;
        List<Lottery> lotteries;

        IdentityUser jowi;
        IdentityUser paw;
        List<Santa> santas1;
        List<Santa> santas2;
        List<Santa> santas3;

        public MockLotteryRepository(AppDbContext context)
        {
            Context = context;
            jowi = context.Users.Find("2b067e53-f15c-44fe-81f5-1d8924ad7736");
            paw = context.Users.Find("fe6706c3-b877-4586-9c58-d04d7c81dfe3");

            users = new List<IdentityUser>()
            {
                jowi,
                paw
            };

            santas1 = new List<Santa>()
            {
                new Santa(){Name="A",Email="a@wp.pl"},
                new Santa(){Name="B", Email="b@wp.pl"},
                new Santa(){Name="C", Email="c@wp.pl"}
            };

            santas2 = new List<Santa>()
            {
                new Santa(){Name="AA",Email="aa@wp.pl"},
                new Santa(){Name="BB", Email="bb@wp.pl"},
                new Santa(){Name="CC", Email="cc@wp.pl"}
            };

            santas3 = new List<Santa>()
            {
                new Santa(){Name="AAA",Email="aaa@wp.pl"},
                new Santa(){Name="BBB", Email="bbb@wp.pl"},
                new Santa(){Name="CCC", Email="ccc@wp.pl"}
            };

            lotteries = new List<Lottery>()
            {
                new Lottery(){Name="CrazyJowi", Admin=jowi, Santas=santas1 },
                new Lottery(){Name="CrazyPaw", Admin=paw, Santas=santas2},
                new Lottery(){Name="CrazyJovi2", Admin=jowi, Santas=santas3},
                new Lottery(){Name="Good Frankies", Admin=jowi, Santas=santas2},
                new Lottery(){Name="Makarony", Admin=paw, Santas=santas1}
            };
        }

 


        public List<Lottery> GetLotteries(string idUser)
        {
            var user = Context.Users.Find(idUser);


            return lotteries.Where(i => i.Admin == user).ToList();

        }

    }
}
