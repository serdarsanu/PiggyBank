using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiggyBank.Model
{
    class Banknote : Money
    {
        public double Width { get; set; }
        public double Length { get; set; }
        public bool isFold { get; set; } = false;
        public override double Volume()
        {
            return Width * Length * Height;
        }
    }
}