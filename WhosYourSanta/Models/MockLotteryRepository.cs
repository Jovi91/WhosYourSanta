using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhosYourSanta.Models
{
    public class MockLotteryRepository: ILotteryRepository
    {
        List<IdentityUser> users;
        IdentityUser jowi = new IdentityUser() { Email = "jowi@wp.pl", UserName = "jowi@wp.pl", PasswordHash= "AQAAAAEAACcQAAAAEKdiy29I+ijpQR4YirW9TBRnXrvV9NrC6ksXtRJ2FjPEd8MNTcFx5A34JEECSnqPqw==" };
        IdentityUser paw = new IdentityUser() { Email = "paw@wp.pl", UserName = "paw@wp.pl" };

        List<Santa> santas1;
        List<Santa> santas2;
        List<Santa> santas3;

        List<Lottery> lotteries;
        public MockLotteryRepository()
        {
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
                new Lottery(){Name="CrazyJovi2", Admin=jowi, Santas=santas3}
            };
        }

        public List<Lottery> GetLotteries(IdentityUser user)
        {
            return lotteries.Where(i => i.Admin == user).ToList();
        }

    }
}
