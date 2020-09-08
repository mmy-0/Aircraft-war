using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirFight
{
    public class Bullet
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Speed { get; set; }
        public int Type { get; set; }
        public bool State { get; set; }
        public GameC GC { get; set; }
        public Direction Dir { get; set; }
        public Bullet(int x, int y, int width, int height, int speed, int type, bool state, Direction dir, GameC gc)
        {
            this.X = x; this.Y = y; this.Width = width; this.Height = height; this.Speed = speed;
            this.Type = type; this.State = state; this.Dir = dir; this.GC = gc;
        }
        public void DrawBullet(Graphics g)
        {
            if (this.State)
            {
                if (this.Type == 1)
                {
                    g.DrawImage(this.GC.fire, this.X, this.Y, this.Width, this.Height);
                }
                else if (this.Type == 2)
                {
                    g.DrawImage(this.GC.enemyfire, this.X, this.Y, this.Width, this.Height);
                }
            }
            Move();
        }
        public void Move()
        {
            switch (this.Dir)
            {
                case Direction.W: { this.Y -= this.Speed; } break;
                case Direction.A: { this.X -= this.Speed; } break;
                case Direction.S: { this.Y += this.Speed; } break;
                case Direction.D: { this.X += this.Speed; } break;
                case Direction.RightDown: { this.X += this.Speed; this.Y += this.Speed; } break;
                case Direction.LeftDown: { this.X -= this.Speed; this.Y += this.Speed; } break;
                case Direction.RightUp: { this.X += this.Speed; this.Y -= this.Speed; } break;
                case Direction.LeftUp: { this.X -= this.Speed; this.Y -= this.Speed; } break;
                case Direction.Stop: { } break;
            }
            if (this.X < -this.Width - 10 || this.X > this.GC.Width + this.Width || this.Y < this.Height || this.Y > this.GC.Height + this.Height)
            {
                this.State = false;
                this.GC.bullets.Remove(this);
            }
            Hit();
        }
        public Rectangle GetRectangle()
        {
            return new Rectangle(this.X, this.Y, this.Width, this.Height);
        }
        public void Hit()
        {
            if (this.Type == 1)
            {
                for (int i = 0; i < this.GC.ep.Count; i++)
                {
                    bool flag = this.GC.ep[i].GetRectangle().IntersectsWith(this.GetRectangle());
                    if (flag)
                    {
                        this.GC.ep[i].State = false;
                        this.State = false;
                        //爆炸
                        this.GC.booms.Add(new Boom(this.GC.ep[i].X, this.GC.ep[i].Y - 10,80,80, true, this.GC));
                        this.GC.p.Score += 1000;
                    }
                }
            }
            else if (this.Type == 2)
            {
                bool flag = this.GetRectangle().IntersectsWith(this.GC.p.GetRectangle());
                if(flag&&this.State&&this.GC.p.Stae)
                {
                    this.State = false;
                    this.GC.p.Life -= 1;
                    if (this.GC.p.Life <= 0)
                    {
                        this.GC.p.Stae = false;
                        //我军飞机爆炸
                        this.GC.booms.Add(new Boom(this.GC.p.X, this.GC.p.Y - 20, 120, 120, true, this.GC));
                    }
                }
            }
        }
    }
}
