﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace xmlStructureEditor
{
    public partial class cruFunc : Form
    {
      bool paused = false;
        double moveX = 0.0;
        double moveY = 0.0;
        double X = 0.0;
        double Y = 0.0;
        double gravity = 0.1;
        Color col;
        Random rand;
        
        
        public cruFunc()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            rand = new Random(Environment.TickCount);
            col = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
        }

        private void BallForm_Paint(object sender, PaintEventArgs e)
        {
            Width = 240;
            Height = 240;
            
            Graphics g = e.Graphics;
            g.Clear(Color.Cyan);
            Point pnt = new Point();
            pnt.X = 1;
            pnt.Y = 1;
            Image newImage = this.imageList1.Images[0];
            Point ulCorner = new Point(0, 0);
            e.Graphics.DrawImage(newImage, ulCorner);
            g.DrawImage(newImage, ulCorner);
        }

        public void Tick()
        {
            if (moving)
                Bounce();
            if (!paused && !moving)
            {
                
                moveY += gravity;
                
                X += moveX;
                Y += moveY;
                Location = new Point((int)X, (int)Y);
                
                //Check Collision
                if (X < 0)
                {
                    X = 0;
                    moveX = -moveX;
                    moveX *= 0.75;
                    moveY *= 0.95;
                }
                if (X > Screen.PrimaryScreen.WorkingArea.Width - 1 - Width)
                {
                    X = Screen.PrimaryScreen.WorkingArea.Width - 1 - Width;
                    moveX = -moveX;
                    moveX *= 0.75;
                    moveY *= 0.95;
                }

                if (Y < 0)
                {
                    Y = 0;
                    moveY = -moveY;
                    moveY *= 0.75;
                    moveX *= 0.95;
                }
                if (Y > Screen.PrimaryScreen.WorkingArea.Height - 1 - Height)
                {
                    Y = Screen.PrimaryScreen.WorkingArea.Height - 1 - Height;
                    moveY = -moveY;
                    moveY *= 0.8;
                    moveX *= 0.95;
                }

                if (Math.Abs(moveX) < 0.1 && Math.Abs(moveY) < 0.5 && DateTime.Now.Second % 3 == 0 && Y > Screen.PrimaryScreen.WorkingArea.Height - 1 - Height - 40)
                {
                    Bounce();
                }
            }
        }

        bool moving = false;
        Point rel = new Point();
        private void BallForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Capture = false;
                moving = false;
            }
        }
        private void BallForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                rel = e.Location;
                Capture = true;
                moving = true;
            }
        }
        private void BallForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (moving)
            {
                X = Cursor.Position.X - rel.X;
                Y = Cursor.Position.Y - rel.Y;
                
                moveX += (X - Location.X)/2;
                moveY += (Y - Location.Y)/2;

                if (moveX > 2)
                    moveX = 2;
                if (moveY > 2)
                    moveY = 2;
                
                if (paused)
                {
                    moveX = 0;
                    moveY = 0;
                }
                
                Location = new Point((int)X, (int)Y);
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void pauseToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            paused = pauseToolStripMenuItem.Checked;
        }
        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            moveX = 0;
            moveY = 0;
        }
        private void bounceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bounce();
        }
        
        public void Bounce()
        {
            moveX = (rand.NextDouble() + rand.NextDouble()) - 1;
            moveY = -(rand.NextDouble());
            moveX *= 50;
            moveY *= 50;
            X += moveX;
            Y += moveY;
        }
    }
    }

