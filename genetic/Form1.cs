using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace genetic
{
    public partial class Form1 : Form
    {
        Random rnd;
        List<Item> items;
        Knapsack bag;

        public Form1()
        {
            InitializeComponent();
            this.pustBudetTuta();
            pullToLV();
        }

        private void button_go_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[2].Points.Clear();
            chart2.Series[0].Points.Clear();

            listViewItems.Clear();
            rnd = new Random();
            this.pustBudetTuta();
            pullToLV();
            bag = new Knapsack(Decimal.ToInt32(nudParent.Value),Decimal.ToDouble(nudWeight.Value),rnd,items,Decimal.ToInt32(nudChild.Value), Decimal.ToInt32(nudP.Value));

            plot();

            label_best.Text = bag.getBestSpecimen();
        }

        public void plot()
        {
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart2.ChartAreas[0].AxisX.Minimum = 0;
            for (int i = 0; i < Decimal.ToInt32(nudP.Value); i++)
            {
                for (int j = 0; j < bag.population[i].getCount(); j++)
                {
                    chart1.Series[2].Points.AddXY(i, bag.population[i].getSpecimen(j).price);
                }

                for (int j = 0; j < bag.parent[i].getCount(); j++)
                {
                    chart1.Series[1].Points.AddXY(i, bag.parent[i].getSpecimen(j).price);
                }

                if (i > 0)
                {
                    chart1.Series[0].Points.AddXY(i, bag.population[i].getSpecimen(0).price);
                    chart2.Series[0].Points.AddXY(i, bag.population[i].getSpecimen(0).weight);
                }

            }
        }

        public void pustBudetTuta()
        {
            items = new List<Item>();
            items.Add(new Item(1,836));
            items.Add(new Item(4,437));
            items.Add(new Item(12,64));
            items.Add(new Item(3,73));
            items.Add(new Item(11,648));
            items.Add(new Item(7,326));
            items.Add(new Item(10,73));
            items.Add(new Item(4,342));
            items.Add(new Item(6,234));
            items.Add(new Item(2,638));
        }

        public void pullToLV()
        {
            listViewItems.Columns.Add("Вес");
            listViewItems.Columns.Add("Цена");

            for (int i = 0; i < items.Count - 1; i++)
            {
                ListViewItem item1 = new ListViewItem(new[] {items[i].getWeight().ToString(),items[i].getCost().ToString()});
                listViewItems.Items.Add(item1);
            }

        }
        
    }
}
