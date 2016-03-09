using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace genetic
{
    class Population
    {
        List<Specimen> popul=new List<Specimen>();

        //начальная популяция
        public Population(int count, List<Item> items, Random rnd, double w)
        {
            Specimen sp;
            while (this.popul.Count<count)
            {
                sp=new Specimen(items,rnd);
                if (sp.weight<w)this.popul.Add(sp);
            }
        }

        //дети
        public Population(int count, Population parent, Random rnd, List<Item> items)
        {
            for (double i = 0; i < count; i++)
            {
                int k = rnd.Next(0, parent.popul.Count);
                Specimen firstParent = parent.popul[k];
                int kk = rnd.Next(0, parent.popul.Count);
                while (k == kk) { kk = rnd.Next(0, parent.popul.Count); }
                Specimen secondParent = parent.popul[kk];

                if (i / 10.0 == (int)i / 10)
                {
                    int p = rnd.Next(0, items.Count);
                    k = rnd.Next(0, items.Count);
                    this.popul.Add(new Specimen(firstParent, secondParent, k, items, p));
                }
                else
                {
                    k = rnd.Next(0, items.Count);
                    this.popul.Add(new Specimen(firstParent, secondParent, k, items));
                }
            }
        }

        //родительская
        public Population(Population pop)
        {
            for (int i = 0; i < pop.popul.Count; i++)
            {
                this.popul.Add(pop.popul[i]);
            }
        }

        public Population(Population pop, Population parent, int countParent)
        {
            for (int i = 0; i < countParent-2; i++)
            {
                if (i < pop.popul.Count) this.popul.Add(pop.popul[i]);
            }

            int k = 0;
            while (this.popul.Count < countParent)
            {
                this.popul.Add(parent.popul[k]);
                k++;
            }
        }

        public Specimen getSpecimen(int k)
        {
            return this.popul[k];
        }

        public void deleteSpicemen(int k)
        {
            popul.RemoveAt(k);
        }

        public void orderSpicemen()
        {
            this.popul=popul.OrderBy(s => s.price).ToList();
            this.popul.Reverse();
        }

        public int getCount()
        {
            return this.popul.Count();
        }
    }
}
