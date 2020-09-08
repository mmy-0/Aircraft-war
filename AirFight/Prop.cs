using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirFight
{
    public class Prop
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
        public Prop(int x, int y, int width, int height, int speed, int type, bool state, Direction dir, GameC gc)
        {
            this.X = x; this.Y = y; this.Width = width; this.Height = height; this.Speed = speed;
            this.Type = type; this.State = state; this.Dir = dir; this.GC = gc;
        }
        public void DrawProp(Graphics g)
        {
            if (this.State)
            {
                
                    g.DrawImage(this.GC.img_Prop, this.X, this.Y, this.Width, this.Height);
                

            }
            else
                this.GC.Props.Remove(this);
            Move();
            carsh();
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
            if (this.X < -this.Width - 10 || this.X > this.GC.Width + this.Width  || this.Y > this.GC.Height + this.Height)
            {
                this.State = false;
            }
            
        }
        public Rectangle GetRectangle()
        {
            return new Rectangle(this.X, this.Y, this.Width, this.Height);
        }
        public void carsh()
        {
            bool flag = this.GetRectangle().IntersectsWith(this.GC.p.GetRectangle());
            if (flag && this.State && this.GC.p.Stae)
            {
                this.State = false;
                this.GC.p.Life += 20;
                if (this.GC.p.Life >= 100)
                    this.GC.p.Life = 100;
                this.GC.p.Score += 1500;
            }
        }
    }
}
