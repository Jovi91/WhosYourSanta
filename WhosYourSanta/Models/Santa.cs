using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WhosYourSanta.Models
{
    public class Santa : IUser
    {
        [Key]
        public int Id { get; protected set; }
        [MaxLength(20,ErrorMessage ="Nick nie może być dłuższy niż 20 znaków")]
        [Display(Name ="Imię/Ksywa")]
        public string Name { get; set; }
        [EmailAddress(ErrorMessage ="Podany format jest niepoprawny dla Adresu Email")]
        [MaxLength(40, ErrorMessage ="Email nie może być dłuższy niż 40 znaków")]
        public string Email { get; set; }
        public string Role { get; set; }
        public Santa DrawnSanta { get; set; }

        //navigation property
        public virtual Lottery Lottery { get; set; } //na razie nie potrzebna jeżeli będzie potrzebna (np do Lazy Loading) to trzeba zrobić migrację
    }
}
