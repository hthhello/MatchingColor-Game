using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tro_Choi_Lat_Hinh
{
    public partial class Form1 : Form
    {
        TableLayoutPanel table = new TableLayoutPanel();
        int so = 4;
        int[] ID;
        bool[] State;
        int ID1 = -1;
        int ID2 = -1;
        int time = 20;
        int IDk1;
        int IDk2;
        double score = 0;
        int ctime = 0;
        public Form1()
        {
            InitializeComponent();
        }
        void Init()
        {
            ID = new int[so * so];
            State = new bool[so * so];
            for (int i = 0; i < so*2; i++) // dem so hinh
            {
                ID[i] = 0;
                State[i] = false;
            }
            int dem = 0;
            for (int i = 0; i < so*2; i++)
            {
                do
                {
                    Random random = new Random();
                    int IDx = random.Next() % (so*so);
                    if (ID[IDx] == 0)
                    {
                        dem++;
                        ID[IDx] = i;
                    }
                } while (dem < 2);
                dem = 0;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            table.ColumnCount = so;
            table.RowCount = so;
            table.Dock = DockStyle.Fill;
            for (int i = 0; i < so*so; i++)
            {
                PictureBox pict = new PictureBox();
                pict.Name = i.ToString();
                pict.Image = Image.FromFile("HTH.jpg");
                pict.Margin = new Padding(1, 1, 1, 1);
                pict.Width = 170;
                pict.Height = 170;
                pict.Click += PictureBox_Click;
                table.Controls.Add(pict);
            }
            this.Controls.Add(table);
            Init();
            
            
        }
        private void reDraw()
        {
            DoubleBuffered = true;
            table.Controls.Clear();
            table.ColumnCount = so;
            table.RowCount = so;
            table.Dock = DockStyle.Fill;
            for (int i = 0; i < so*so; i++)
            {
                PictureBox pict = new PictureBox();
                pict.Name = i.ToString();
                if (State[i] == false)
                {
                    pict.Image = Image.FromFile("HTH.jpg");
                    pict.Click += PictureBox_Click;
                }
                else
                {
                    BackColor = Color.DarkCyan;
                    pict.Image = Image.FromFile(ID[i] + ".jpg");
                }
                pict.Margin = new Padding(1, 1, 1, 1);
                pict.Width = 170;
                pict.Height = 170;
                table.Controls.Add(pict);
            }
            this.Controls.Add(table);
            
        }
        
        private void PictureBox_Click(object sender, EventArgs e)
        {
            PictureBox photo = (PictureBox)sender;
            
            if (ID1 == -1)
            {
                timer1.Enabled = true;
                timer1.Start();
                ID1 = Int32.Parse(photo.Name);
                photo.Image = Image.FromFile(ID[ID1] + ".jpg");
                photo.Click -= PictureBox_Click;
            }
            else
            {
               
                ID2 = Int32.Parse(photo.Name);
                photo.Image = Image.FromFile(ID[ID2] + ".jpg");
                Delay.Enabled = true;
                Delay.Start();
                
            }
        }

     
        private void Delay_Tick(object sender, EventArgs e)
        {
            Delay.Stop();
            if (ID[ID1] == ID[ID2])
            {
                    State[ID1] = true;
                    State[ID2] = true;
                    IDk1 = ID1;
                    IDk2 = ID2;
                   ID1 = -1; ID2 = -1;
                reDraw();
                if (time > 0)
                {
                    score = score + 1.25;
                    label5.Text = score.ToString();
                }
            }
            else
            {
                State[ID1] = false;
                State[ID2] = false;
                ID1 = -1; ID2 = -1;
                reDraw(); 
            }
            int Game = 1;
            for (int i = 0; i < 16; i++)
            {
                if (State[i] == false)
                {
                    Game = 0;
                    break;
                }
            }
            if (Game == 1 && time > 0)
            {
                timer1.Stop();
                MessageBox.Show("Win ^^!\n Hit R.S to Play Again", "HTH");
                
            }
        
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (time > 0)
            {
                time--;
                label1.Text = time.ToString();
            }
            if (time <= 0)
            {
                timer1.Stop();
                MessageBox.Show("Time Out! \nHit R.S or see this message again =]]");
            }

           
            
        }

        private void label2_Click(object sender, EventArgs e)
        {
            time = 20;
            score = 0;
            timer1.Stop();
            label1.Text = "30";
            for (int i = 0; i < 16; i++)
            {
                State[i] = false;
                ID1 = -1; ID2 = -1;
            }
            timer1.Stop();
            Init();
            reDraw(); 
        }

        private void label6_Click(object sender, EventArgs e)
        {
            ctime++;
            if(ctime == 1)
            {
                time = time + 10;
            }
            if(ctime > 1)
            {
                MessageBox.Show("I can only help you one time", "HTH");
            }
        }  
        }
    }
    
  