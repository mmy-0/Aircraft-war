using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirFight
{
   public class LifeBar
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public GameC GC { get; set; }
        public LifeBar (int x, int y, int width, int height, GameC gc)
        {
            this.X = x; this.Y = y; this.Width = width; this.Height = height;
             this.GC = gc;
        }
        public void DrawLife(Graphics g)
        {
            g.DrawString("生命值：",new Font("宋体",20),new SolidBrush(Color.Red),this.X,this.Y+3);
            g.DrawRectangle(new Pen(Color.Black),this.X+100,this.Y+5,202,this.Height);
            g.FillRectangle(new SolidBrush(Color.Green),this.X+102,this.Y+7,this.GC.p.Life*2,this.Height-2);
            string s = this.GC.p.Life.ToString() + "%";
            g.DrawString(s,new Font("宋体",13),new SolidBrush(Color.Red),this.X+185,this.Y+7);
            g.DrawString("积  分：",new Font("宋体",20),new SolidBrush(Color.Red),this.X,this.Y+53);
            string jf = this.GC.p.Score.ToString();
            g.DrawString(jf, new Font("宋体", 20), new SolidBrush(Color.Red), this.X+100, this.Y + 50);
        }
    }

}
