﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhosYourSanta.Models
{
    interface ISantaRepository
    {
        Santa GetSanta(int Id);
        IEnumerable<Santa> GetAllSantas();
        Santa Add(Santa santa);
        Santa Update(Santa santaChanges);
        Santa Delete(int Id);
    }
}