using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirFight
{
    public class Boom
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public bool State { get; set; }
        public GameC GC { get; set; }
        int index = 0;
        public Boom (int x, int y, int width, int height, bool state, GameC gc)
        {
            this.X = x; this.Y = y; this.Width = width; this.Height = height; 
             this.State = state;  this.GC = gc;
        }
        public void DrawBoom(Graphics g)
        {
            if (this.State)
            {
                
                    g.DrawImage(GC.imgboom[index], this.X, this.Y, this.Width, this.Height);
                index++;
                if(index==5)
                this.State = false;
            }
            else
                this.GC.booms.Remove(this);
        }
    }
}
