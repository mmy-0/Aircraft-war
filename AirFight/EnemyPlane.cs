using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirFight
{
    public class EnemyPlane
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
        public EnemyPlane(int x,int y,int width,int height,int speed,int type,bool state,Direction dir,GameC gc)
        {
            this.X = x; this.Y = y; this.Width = width; this.Height = height; this.Speed = speed;
            this.Type = type;this.State = state; this.Dir = dir;this.GC = gc;
        }
        public void DrawEP(Graphics g)
        {
            if (this.State)
            {
                if (this.Type == 1)
                    g.DrawImage(GC.enemyplane1, this.X, this.Y, this.Width, this.Height);
                else if(this.Type==2)
                    g.DrawImage(GC.enemyplane2, this.X, this.Y, this.Width, this.Height);
                Move();
                //g.DrawRectangle(new Pen(Color.Red), this.X+5, this.Y+5, this.Width-10, this.Height-10);
            }
            else
                this.GC.ep.Remove(this);    
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
            if (this.X <= -100 || this.Y <= -100 || this.Y >= 600)
                this.State = false;
            Crash();
            GreateBullet();
        }
        public Rectangle GetRectangle()
        {
            return new Rectangle(this.X,this.Y,this.Width,this.Height);
        }
        public void Crash()
        {
            bool flag = this.GetRectangle().IntersectsWith(this.GC.p.GetRectangle());
            if (flag&&this.State&&this.GC.p.Stae)
            {
                this.State = false;
                //爆炸
                this.GC.booms.Add(new Boom(this.X,this.Y-10,80,80,true,this.GC));
                this.GC.p.Score += 1000;
                this.GC.p.Life -= 2;
                if (this.GC.p.Life <= 0)
                {   
                    this.GC.p.Stae = false;
                    //我军飞机爆炸
                    this.GC.booms.Add(new Boom(this.GC.p.X,this.GC.p.Y-20,120,120,true,this.GC));
                }
            }
        }
        public void GreateBullet()
        {
            if (this.GC.rd.Next(100) % 59 == 0)
            {
                this.GC.bullets.Add(new Bullet(this.X,this.Y+this.Height/2,8,8,10,2,true,Direction.A,this.GC));
            }
        }
    }
}
