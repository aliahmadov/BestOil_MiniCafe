using Guna.UI2.WinForms;
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
        double sumOil = 0;
        public Form1()
        {
            InitializeComponent();
            usdTxtb.Enabled = false;
            litresTxtb.Enabled = false;
            maskedTextBox1.Enabled = false;
            maskedTextBox2.Enabled = false;
            maskedTextBox3.Enabled = false;
            maskedTextBox5.Enabled = false;
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
            foodList.Add(cocaCola);
            foodList.Add(pizza);
            foodList.Add(hotDog);
            oils.Add(oil3);
            oils.Add(oil2);
            oils.Add(oil);

            int index = 0;
            foreach (var item in groupBox1.Controls)
            {
                if (item is GroupBox gB)
                {
                    foreach (var item2 in gB.Controls)
                    {
                        if (item2 is CheckBox cB)
                        {
                            cB.Text = foodList[index].Name;
                            foreach (var item3 in cB.Parent.Controls)
                            {
                                if (item3 is Guna2HtmlLabel lB)
                                {
                                    lB.Text = foodList[index].Price.ToString();
                                    index++;
                                }
                            }
                        }
                    }
                }
            }
            comboBox1.DataSource = oils;


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
            foreach (var item in groupBox1.Controls)
            {
                if (item is GroupBox gB)
                {
                    foreach (var item2 in gB.Controls)
                    {
                        if (item2 is CheckBox cB)
                        {
                            if (cB.Checked)
                            {
                                foreach (var item3 in cB.Parent.Controls)
                                {
                                    if (item3 is MaskedTextBox mB)
                                    {
                                        mB.Enabled = true;
                                        HelperCalculator();
                                    }
                                }
                            }
                            else
                            {
                                foreach (var item3 in cB.Parent.Controls)
                                {
                                    if (item3 is MaskedTextBox mB)
                                    {
                                        mB.Enabled = false;
                                        mB.Clear();
                                        HelperCalculator();
                                    }
                                    else if (item3 is Label lB)
                                    {
                                        lB.ForeColor = Color.Red;
                                        lB.Text = "0$";
                                    }
                                    
                                }
                            }
                        }
                    }
                }
            }
        }

        private void HelperCalculator()
        {
            sumFood = 0;
            foreach (var item in groupBox1.Controls)
            {
                if (item is GroupBox gB)
                {
                    foreach (var item2 in gB.Controls)
                    {
                        if (item2 is Label lB)
                        {
                            if (lB.Text != "")
                            {

                                sumFood += double.Parse(lB.Text.Split('$')[0]);
                                sumFoodLbl.Text = sumFood.ToString() + "$";
                            }
                        }
                    }
                }

            }
        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            var maskedBox = sender as MaskedTextBox;
            int amount = 0;
            if (maskedBox.Text != "")
            {
                amount = int.Parse(maskedBox.Text);
            }

            double price = 0;
            foreach (var item in maskedBox.Parent.Controls)
            {
                if (item is Guna2HtmlLabel gB)
                {
                    if (gB.Text != "")
                    {
                        price = double.Parse(gB.Text);
                    }
                }
            }
            var sumPrice = amount * price;

            foreach (var item in maskedBox.Parent.Controls)
            {
                if (item is Label lB)
                {
                    lB.ForeColor = Color.Red;
                    lB.Text = sumPrice.ToString() + "$";
                }
            }

            HelperCalculator();
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            sumOil = double.Parse(oilPriceLbl.Text.Split('$')[0]);
            var total_price = sumOil + sumFood;

            foreach (var item in guna2GroupBox2.Controls)
            {
                if(item is Label lB)
                {
                    lB.ForeColor= Color.Green;
                    lB.Font = new Font("Times New Roman", 48);
                    lB.Location=new Point(lB.Location.X-300, lB.Location.Y-10);    
                    lB.Text = total_price.ToString() + "$";
                }
            }
        }
    }
}
