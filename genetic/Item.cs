using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace genetic
{
    class Item
    {
        double weight;//вес
        double cost;//стоимость

        public Item(double w, double c)
        {
            this.weight = w;
            this.cost = c;
        }

        public double getWeight() { return this.weight; }

        public double getCost() { return this.cost; }
    }
}
