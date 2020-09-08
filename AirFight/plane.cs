using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirFight
{
    public class plane
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Speed { get; set; }
        public int Life { get; set; }
        public int Score { get; set; }
        public bool Stae { get; set; }
        public GameC GC { get; set; }
        public Direction Dir { get; set; }
        private bool key_up, key_down, key_right, key_left,key_fire=false;
        public plane(int x, int y, int width, int height, int speed, int life, int score, bool state, GameC gc)
        { this.X = x;this.Y = y;this.Width = width;this.Height = height;this.Speed = speed;
            this.Life = life;this.Score = score; this.Stae = state; this.GC = gc; }
        public void DrawPlane(Graphics g)
        {
            if (this.Stae)
            {
                if (!key_left && !key_right)
                    g.DrawImage(GC.img_plane, this.X, this.Y, this.Width, this.Height);
                else if (key_left && !key_right)
                    g.DrawImage(GC.left_plane, this.X, this.Y, this.Width, this.Height);
                else
                    g.DrawImage(GC.right_plane, this.X, this.Y, this.Width, this.Height);
                //g.DrawRectangle(new Pen(Color.Blue), this.X + 5, this.Y + 5, this.Width - 10, this.Height - 10);
                SetDirection();
                Move();
            }
            
            
            
        }
        
        public void KeyDown(Keys keys)
        {
            switch (keys)
            {
                case Keys.W: { key_up = true; }break;
                case Keys.A: { key_left=true; }break;
                case Keys.S: { key_down=true; }break;
                case Keys.D: { key_right=true; }break;
                case Keys.J: {
                        if (this.Stae)
                        {
                            if (!key_fire)
                            {
                                key_fire = true;
                                this.GC.bullets.Add(new Bullet(this.X + this.Width, this.Y + this.Height / 2, 8, 8, 15, 1, true, Direction.D, this.GC));
                            }
                        }
                    } break;
            }
           
        }
        public void KeyUp(Keys keys)
        {
            switch (keys)
            {
                case Keys.W: { key_up = false; }break;
                case Keys.A: { key_left = false; } break;
                case Keys.S: { key_down = false; } break;
                case Keys.D: { key_right = false; } break;
                case Keys.J: { key_fire = false; } break;
            }
        }
        public void SetDirection()
        {
            if (key_up && !key_down && !key_left && !key_right)
            { this.Dir = Direction.W; }
            else if (!key_up && key_down && !key_left && !key_right)
            { this.Dir = Direction.S; }
            else if (!key_up && !key_down && key_left && !key_right)
            { this.Dir = Direction.A; }
            else if (!key_up && !key_down && !key_left && key_right)
            { this.Dir = Direction.D; }
            else if (key_up && !key_down && key_left && !key_right)
            { this.Dir = Direction.LeftUp; }
            else if (key_up && !key_down && !key_left && key_right)
            { this.Dir = Direction.RightUp; }
            else if (!key_up && key_down && key_left && !key_right)
            { this.Dir = Direction.LeftDown; }
            else if (!key_up && key_down && !key_left && key_right)
            { this.Dir = Direction.RightDown; }
            else
            { this.Dir = Direction.Stop; }
        }
        public void Move()
        {
            switch (this.Dir)
            {
                case Direction.W: { this.Y -= this.Speed; }break;
                case Direction.A: { this.X -= this.Speed; }break;
                case Direction.S: { this.Y += this.Speed; }break;
                case Direction.D: { this.X += this.Speed; }break;
                case Direction.RightDown: { this.X += this.Speed;this.Y += this.Speed; } break;
                case Direction.LeftDown: { this.X -= this.Speed; this.Y += this.Speed; } break;
                case Direction.RightUp: { this.X += this.Speed; this.Y -= this.Speed; } break;
                case Direction.LeftUp: { this.X -= this.Speed; this.Y -= this.Speed; } break;
                case Direction.Stop: { } break;
            }
            if (this.X <= 0)
                this.X = 0;
            if (this.X >= 1000 - this.Width)
                this.X = 1000 - this.Width;
            if (this.Y <= 0)
                this.Y = 0;
            if (this.Y >= 460 - this.Height)
                this.Y = 460 - this.Height;
        }
        public Rectangle GetRectangle()
        {
            return new Rectangle(this.X, this.Y, this.Width, this.Height);
        }
    }
}
