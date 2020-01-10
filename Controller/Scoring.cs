using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotallyFairRace.Controller
{
    public static class Scoring
    {
        public static decimal MakeScore(decimal numOfCars, decimal place, decimal bid)
        {
            if(numOfCars!=2)
                return Math.Round((numOfCars / 2 - place) / (numOfCars / 2) * bid * (numOfCars / 2) / place, 0);
            return Math.Round((place==2?-2:2) * bid, 2);
        }
    }
} 
