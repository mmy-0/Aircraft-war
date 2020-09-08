using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirFight
{
    /// <summary>
    /// 背景类
    /// </summary>
   public  class Background
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Speed { get; set; }
        public GameC GC { get; set; }
        public Background(int x, int y, int width, int height, int speed, GameC gc)
        { this.X = x;this.Y = y;this.Width = width;this.Height = height;this.Speed = speed; this.GC = gc; }
        public void DrawMe(Graphics g)
        { g.DrawImage(this.GC.img, this.X, this.Y, this.Width, this.Height);
          g.DrawImage(this.GC.img, this.X+998  , this.Y, this.Width, this.Height);
          Move();
        }
        public void Move()
        {
            this.X = this.X-this.Speed;
            if (this.X <= -1000) { this.X = 0; }
        }
    }
}
