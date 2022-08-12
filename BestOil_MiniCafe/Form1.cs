using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BestOil_MiniCafe
{
    public partial class Form1 : Form
    {
        List<Oil> oils = new List<Oil>();
        Oil selectedOil = new Oil();
        List<Food> foodList = new List<Food>();
        double sumFood = 0;
        public Form1()
        {
            InitializeComponent();
            usdTxtb.Enabled = false;
            litresTxtb.Enabled = false;

            Oil oil = new Oil()
            {
                Name = "AI-92",
                Price = 1.0
            };
            Oil oil2 = new Oil()
            {
                Price = 1.60,
                Name = "Premium 95"
            };
            Oil oil3 = new Oil()
            {
                Name = "Diesel",
                Price = 0.8
            };

            Food hotDog = new Food()
            {
                Name = "Hot-Dog",
                Price = 4.2
            };

            Food pizza = new Food()
            {
                Name = "Margaritto",
                Price = 7.2
            };

            Food cocaCola = new Food()
            {
                Name = "Coca-Cola",
                Price = 1.2
            };
            Food potato = new Food()
            {
                Name = "Potato Chips",
                Price = 3.5
            };
            foodList.Add(potato);
            guna2HtmlLabel4.Text = potato.Price.ToString();
            foodList.Add(cocaCola);
            guna2HtmlLabel3.Text = cocaCola.Price.ToString();
            foodList.Add(pizza);
            guna2HtmlLabel2.Text = pizza.Price.ToString();
            foodList.Add(hotDog);
            guna2HtmlLabel1.Text = hotDog.Price.ToString();
            oils.Add(oil3);
            oils.Add(oil2);
            oils.Add(oil);
            int index = 0;
            foreach (var item in groupBox1.Controls)
            {
                if (item is CheckBox cB)
                {
                    cB.Text = foodList[index].Name;
                    index++;
                }
                else if (item is MaskedTextBox mB)
                {
                    mB.Enabled = false;
                }
            }

            comboBox1.DataSource = oils;


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedOil = comboBox1.SelectedItem as Oil;

            if (selectedOil != null)
            {
                oilPrice.Text = selectedOil.Price.ToString();
            }
        }

        private void guna2GroupBox1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            usdTxtb.Enabled = false;
            litresTxtb.Enabled = true;



        }

        private void litresTxtb_TextChanged(object sender, EventArgs e)
        {
            bool hasParsed = double.TryParse(litresTxtb.Text, out double parsed);

            if (hasParsed)
            {
                double price = parsed * selectedOil.Price;
                oilPriceLbl.Text = price.ToString() + " $";
            }
            else
            {
                oilPriceLbl.Text = "";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            usdTxtb.Enabled = true;
            litresTxtb.Enabled = false;

        }

        private void usdTxtb_TextChanged(object sender, EventArgs e)
        {
            bool hasParsed = double.TryParse(usdTxtb.Text, out double parsed);

            if (hasParsed)
            {
                oilPriceLbl.Text = parsed.ToString() + " $";
                litresTxtb.Text = (parsed / selectedOil.Price).ToString();
            }
            else
            {
                oilPriceLbl.Text = "";
                litresTxtb.Text = "";
            }
        }

        private void hotDogCb_CheckedChanged(object sender, EventArgs e)
        {
            if (hotDogCb.Checked)
                maskedTextBox1.Enabled = true;
            else maskedTextBox1.Enabled = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                maskedTextBox2.Enabled = true;
            else maskedTextBox2.Enabled = false;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
                maskedTextBox3.Enabled = true;
            else maskedTextBox3.Enabled = false;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
                maskedTextBox4.Enabled = true;
            else maskedTextBox4.Enabled = false;
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            var maskedBox = sender as MaskedTextBox;

            bool hasParsed = double.TryParse(maskedBox.Text, out double parsed);

            if (hasParsed)
            {
                sumFood = parsed * double.Parse(guna2HtmlLabel1.Text);
                sumFoodLbl.Text = sumFood.ToString() + "$";
            }
            else
            {
                sumFoodLbl.Text = "";
                sumFood = 0;
            }
        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            var maskedBox = sender as MaskedTextBox;

            bool hasParsed = double.TryParse(maskedBox.Text, out double parsed);

            if (hasParsed)
            {
                sumFood += parsed * double.Parse(guna2HtmlLabel2.Text);
                sumFoodLbl.Text = sumFood.ToString() + "$";
            }
            else
            {
                sumFoodLbl.Text = "";
                sumFood = 0;
            }
        }
    }
}
