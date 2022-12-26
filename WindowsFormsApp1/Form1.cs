using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Form startScreen;
        Random Enemy_count;
        bool w = true;
        bool d = true;
        bool a = true;
        bool s = true;
        int speed = 3;
        bool up = false;
        int hp = 300;
        List<PictureBox> monsters = new List<PictureBox>();
        Queue<PictureBox> puls = new Queue<PictureBox>();
        int maxPul = 5;
        System.Windows.Forms.Timer timerP;
        public Form1()
        {
            InitializeComponent();
            KeyPress += Form1_KeyPress;
            KeyDown += Form1_KeyDown;
            pictureBox3.Visible = false;
            pictureBox3.Location = new Point(2000, 2000);
            timer1.Enabled = false;
            timer1.Interval = 10;
            label1.Text = "HP: " + hp;
            timer2.Tick += timer2_Tick;
            timer2.Start();
            Enemy_count = new Random();
            timerP = new System.Windows.Forms.Timer();
            timerP.Tick += TimerP_Tick;
            timerP.Interval = 30;
            startScreen = new Form()
            {
                Width = 500,
                Height = 700,
                BackgroundImage = Properties.Resources.back__1_,
                BackgroundImageLayout = ImageLayout.Stretch
            };
            PictureBox startButton = new PictureBox()
            {
                Width = 300,
                Height = 100,
                Location = new Point(100, 300),
                BackColor = Color.Transparent,
                BackgroundImage = Properties.Resources.start,
                BackgroundImageLayout = ImageLayout.Stretch
            };
            startScreen.Controls.Add(startButton);
            PictureBox settingButton = new PictureBox()
            {
                Width = 300,
                Height = 100,
                Location = new Point(100, 410),
                BackColor = Color.Transparent,
                BackgroundImage = Properties.Resources.settings,
                BackgroundImageLayout = ImageLayout.Stretch
            };
            startScreen.Controls.Add(settingButton);
            PictureBox exitButton = new PictureBox()
            {
                Width = 300,
                Height = 100,
                Location = new Point(100, 520),
                BackColor = Color.Transparent,
                BackgroundImage = Properties.Resources.exit,
                BackgroundImageLayout = ImageLayout.Stretch
            };
            startScreen.Controls.Add(exitButton);
            startScreen.Focus();
            startButton.Click += StartButton_Click;
            exitButton.Click += ExitButton_Click;
            startScreen.Show();

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы собираитись их бросить?", "", MessageBoxButtons.OKCancel);
            if(result == DialogResult.OK)
            {
                startScreen.Close();
                startScreen.Dispose();
                this.Close();
                this.Dispose();
            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            startScreen.Close();
            startScreen.Dispose();
            this.Focus();
        }

        private void RemovePul(int ind)
        {
            List<PictureBox> temp = new List<PictureBox>();
            for (int k = 0; k<puls.Count; k++)
            {
                temp.Add(puls.Dequeue());
            }
            foreach(PictureBox pb in temp)
            {
                if(temp.IndexOf(pb) != ind)
                {
                    puls.Enqueue(pb);
                }
            }
        }

        private void TimerP_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i<puls.Count; i++)
            {
                for (int j = 0; j<monsters.Count; j++)
                {
                    if (puls.ElementAt(i).Bounds.IntersectsWith(
                        monsters[j].Bounds))
                    {
                        Controls.Remove(monsters[j]);
                        monsters.RemoveAt(j);
                        return;
                    }
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Up:
                    Pul(0, -1);
                    break;
                case Keys.Down:
                    Pul(0, 1);
                    break;
                case Keys.Left:
                    Pul(-1, 0);
                    break;
                case Keys.Right:
                    Pul(1, 0);
                    break;
            }
        }

        private void Pul(int x, int y)
        {
            PictureBox pulBox = new PictureBox()
            {
                Width = 30,
                Height = 30,
                BackgroundImage = Properties.Resources.pul,
                BackgroundImageLayout = ImageLayout.Zoom,
                BackColor = Color.Transparent,
                Location = pictureBox1.Location
            };
            puls.Enqueue(pulBox);
            Controls.Add(pulBox);
            if (puls.Count > maxPul)
            {
                Controls.Remove(puls.Dequeue());
            }
            if (puls.Count == 0)
            {
                timerP.Enabled = false;
            }
            else
            {
                timerP.Enabled = true;
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (pictureBox1.Bounds.IntersectsWith(pictureBox4.Bounds))
            {
                w = false;
            }
            if (pictureBox1.Bounds.IntersectsWith(pictureBox5.Bounds))
            {
                s = false;
            }
            if (pictureBox1.Bounds.IntersectsWith(pictureBox7.Bounds))
            {
                a = false;
            }
            if (pictureBox1.Bounds.IntersectsWith(pictureBox6.Bounds))
            {
                d = false;
            }
            if (pictureBox1.Bounds.IntersectsWith(pictureBox2.Bounds))
            {
                pictureBox1.Location = new Point(512, 179);
                BackgroundImage = Properties.Resources.room__1_;
                pictureBox3.Visible = true;
                timer1.Enabled = true;
                pictureBox3.Location = new Point(49, 28);
                int enemy_cound2 = Enemy_count.Next(1, 6);
                if(enemy_cound2 == 1)
                {
                    monsters.Add(pictureBox3);
                }
                else
                {
                    monsters.Add(pictureBox3);
                    for (int i = 0; i < enemy_cound2 - 1; i++)
                    {
                        int t = Enemy_count.Next(2);
                        var img = t == 0 ? pictureBox3.BackgroundImage : Properties.Resources.enemu;
                        monsters.Add(new PictureBox() 
                        { 
                            Location = pictureBox3.Location,
                            BackgroundImage = img,
                            BackgroundImageLayout = pictureBox3.BackgroundImageLayout,
                            BackColor = pictureBox3.BackColor,
                            Size = pictureBox3.Size
                        });
                        monsters.Last().Location = new Point(Enemy_count.Next(50, 500) ,pictureBox3.Location.Y);
                    }
                    Controls.AddRange(monsters.ToArray());
                }
            }
            {
                if (w == true)
                {
                    if (e.KeyChar == 'w')
                    {
                        pictureBox1.Location = new Point(pictureBox1.Location.X, pictureBox1.Location.Y - speed);
                    }
                }
            }
            if (d == true)
            {
                if (e.KeyChar == 'd')
                {
                    pictureBox1.Location = new Point(pictureBox1.Location.X + speed, pictureBox1.Location.Y);
                }
            }
            if (a == true)
            {
                if (e.KeyChar == 'a')
                {
                    pictureBox1.Location = new Point(pictureBox1.Location.X - speed, pictureBox1.Location.Y);
                }
            }
            if (s == true)
            {
                if (e.KeyChar == 's')
                {
                    pictureBox1.Location = new Point(pictureBox1.Location.X, pictureBox1.Location.Y + speed);
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
        private void EnemyMove(PictureBox enemy)
        {
            if (up)
            {
                if (enemy.Location.Y < 28)
                {
                    up = false;
                    return;
                }
                else
                {
                    enemy.Location = new Point(enemy.Location.X, enemy.Location.Y - speed);
                }
            }
            else
            {
                if (enemy.Location.Y > 361)
                {
                    up = true;
                    return;
                }
                else
                {
                    enemy.Location = new Point(enemy.Location.X, enemy.Location.Y + speed);
                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (monsters.Count == 0)
            {
                EnemyMove(pictureBox3);
            }
            else
            {
                foreach (PictureBox monster in monsters)
                {
                    if (monster.BackgroundImage == pictureBox3.BackgroundImage)
                    {
                        EnemyMove(monster);
                    }
                    else
                    {
                        EnemyMove2(monster);
                    }
                }
            }
        }
        private void EnemyMove2(PictureBox enemy)
        {
            Point p1, p2;
            p1 = enemy.Location;
            p2 = pictureBox1.Location;
            
            Point p3 = new Point();

            if(p1.X > p2.X)
            {
                p3.X = p1.X - 2;
            }
            else if (p1.X < p2.X)
            {
                p3.X = p1.X + 2;
            }
            int left = 0;
            if (p2.X - p1.X != 0)
            {
               left = (p3.X - p1.X) / (p2.X - p1.X);
               p3.Y = left * (p2.Y - p1.Y) + p1.Y;
               enemy.Location = p3;
            }
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (monsters.Count == 0)
            {
                if (pictureBox1.Bounds.IntersectsWith(pictureBox3.Bounds))
                {
                    timer2.Stop();
                    hp--;
                    label1.Text = "HP: " + hp;
                    if (hp > 0)
                    {
                        timer2.Start();
                    }
                    else
                    {
                        KeyPress -= Form1_KeyPress;
                        timer1.Stop();
                        MessageBox.Show("GAME OVER");
                    }
                }
            }
            else
            {
                foreach (PictureBox monster in monsters)
                {
                    if (pictureBox1.Bounds.IntersectsWith(monster.Bounds))
                    {
                        timer2.Stop();
                        hp--;
                        label1.Text = "HP: " + hp;
                        if (hp > 0)
                        {
                            timer2.Start();
                        }
                        else
                        {
                            KeyPress -= Form1_KeyPress;
                            timer1.Stop();
                            MessageBox.Show("GAME OVER");
                        }
                    }
                }
            }
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}