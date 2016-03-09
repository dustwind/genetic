using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace genetic
{
    class Knapsack
    {
        double W;//вместимость
        public List<Population> population = new List<Population>();
        public List<Population> parent = new List<Population>();
        List<Item> items = new List<Item>();
        int countPopul;

        public Knapsack(int startPopul, double w, Random rnd, List<Item> items, int pop, int iter)
        {
            this.W = w;
            this.population.Add(new Population(startPopul, items, rnd, w));
            this.countPopul = pop;
            this.items = items;

            while (population.Count<=iter)
            {
                createChildren(rnd, countPopul,startPopul);
                if (population[population.Count - 1].getSpecimen(0).price < population[population.Count - 2].getSpecimen(0).price) population.RemoveAt(population.Count - 1);
            }
        }

        /* выбрать 10 лучших родителей из популяции
         * рандомно выбираем двух родителей
         *      рандомно выбираем ген и выполняем кроссинговер
         *      каждый десятый мутирует
         * повторяем пока не будет достигнуто нужное количество детей
         * выполняем чистку популяции и сортировку
         */
        public void createChildren(Random rnd, int count, int countParent)
        {
            if (parent.Count == 0)
            {
                parent.Add(new Population(population[0]));
                population.Add(new Population(count, parent[0], rnd, items));
            }
            else
            {
                parent.Add(new Population(population[population.Count - 1], parent[parent.Count - 1], countParent));
                population.Add(new Population(count,parent[parent.Count-1],rnd,items));
            }
            if (clearPopul(population.Count - 1)) population[population.Count - 1].orderSpicemen();
        }

        //удаляем в выбранной популяции особи с превосходящим весом
        //сортируем по цене
        public bool clearPopul(int i)
        {
            int c = countPopul;
            for (int k = 0; k < c; k++)
            {
                if (population[i].getCount() < 2)
                {
                    population.RemoveAt(i);
                    return false;
                }
                if (W < population[i].getSpecimen(k).weight)
                {
                    population[i].deleteSpicemen(k);
                    c--;
                    k--;
                }
            }
            return true;
        }

        public string getBestSpecimen()
        {
            string str="";
            int s = population[population.Count - 1].getSpecimen(0).getDnk().Count();
            for (int i = 0; i < s; i++)
            {
                str =str+ population[population.Count - 1].getSpecimen(0).getDnk()[i].ToString();
            }

            str = str + "       " + population[population.Count - 1].getSpecimen(0).weight + "    " + population[population.Count - 1].getSpecimen(0).price;
            
            return str;
        }
    }
}
