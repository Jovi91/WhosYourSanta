using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WhosYourSanta.Models;

namespace WhosYourSanta.ViewModel
{
    public class AddLotteryViewModel
    {

        [Key]
        public int Id { get; protected set; }
        [Required]
        public string Name { get; set; }
        public IdentityUser Admin { get; set; }
        public List<Santa> Santas { get; set; } = new List<Santa>();

        public Santa Santa { get; set; }

        [Display(Name="Chcę wziąć udział w loterii")]
        public bool AdminTakesPartInLottery { get; set; }


        
    }
}
