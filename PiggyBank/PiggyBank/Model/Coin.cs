using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiggyBank.Model
{
    

    class Coin : Money
    {
        double PI = 3.14;
        public double Radius { get; set; }
        public  double Thickness { get; set; }
        public override double Volume()
        {
            return PI * Radius * Radius * Thickness;
        }
    }
}
