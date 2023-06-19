using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiggyBank.Model
{
    public abstract class Money
    {
        public string Name { get; set; }
        public double Value { get; set; }
        public double Height { get; set; }
        public abstract double Volume();
        public double PiggyBankVolume() {
            Random rnd = new Random();
            return (rnd.Next(125, 176) * Volume()) / 100;
        }
    }
}
