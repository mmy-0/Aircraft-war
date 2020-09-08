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

namespace AirFight
{
    public partial class GameC : Form
    {
        //背景图片
        public Image img = Image.FromFile(@"..\..\image\bg.png");
        //我的飞机
        public Image img_plane = Image.FromFile(@"..\..\image\plane.png");
        public Image left_plane = Image.FromFile(@"..\..\image\left.png");
        public Image right_plane = Image.FromFile(@"..\..\image\right.png");
        //子弹
        public Image fire = Image.FromFile(@"..\..\image\bossbullet.png");
        public Image enemyfire = Image.FromFile(@"..\..\image\enemybullet.png");
        //敌人飞机
        public Image enemyplane1 = Image.FromFile(@"..\..\image\enemyplane1.png");
        public Image enemyplane2 = Image.FromFile(@"..\..\image\enemyplane2.png");
        //boom
        public Image[] imgboom = { Image.FromFile(@"..\..\image\explode1.png"),
                                   Image.FromFile(@"..\..\image\explode2.png"),
                                   Image.FromFile(@"..\..\image\explode3.png"),
                                   Image.FromFile(@"..\..\image\explode4.png"),
                                   Image.FromFile(@"..\..\image\explode5.png")};
        //道具
        public Image img_Prop= Image.FromFile(@"..\..\image\bulletbox3.png");
        Background bg;
        public plane p;Bullet b;
        public Random rd = new Random();
        public LifeBar lifeBar;
        public List< EnemyPlane> ep=new List<EnemyPlane>();
        public List<Bullet> bullets = new List<Bullet>();
        public List<Boom> booms = new List<Boom>();
        public List<Prop> Props = new List<Prop>();
        public GameC()
        {
            InitializeComponent();
            //窗体属性
            bg = new Background(0,0,1000,500,5,this);
            p = new plane(0,250,90,50,10,100,0,true,this);
            lifeBar = new LifeBar(20,20,200,20,this);
            b = new Bullet(10, 10, 10, 10,20,1,true,Direction.D,this);
            this.Width = 1000;//窗口大小
            this.Height = 500;
            this.BackColor = Color.Black;//窗口背景色
            this.Text = "雷霆战机";//窗口标题
            this.MaximizeBox = false;//无最大化按钮
            this.MinimizeBox = false;//无最小化按钮
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;//不能拖动来变化窗口大小
            this.StartPosition = FormStartPosition.CenterScreen;//使窗口在屏幕中央出现
            this.DoubleBuffered = true; //双缓冲，使重绘减少或避免闪烁；
        }
            // 窗体重绘
            protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            bg.DrawMe(e.Graphics);
            p.DrawPlane(e.Graphics);
            lifeBar.DrawLife(e.Graphics);
            b.DrawBullet(e.Graphics);
            //画敌机列表
            for(int i=0;i<ep.Count;i++)
            {
                ep[i].DrawEP(e.Graphics);
            }
            //画子弹列表
            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].DrawBullet(e.Graphics);
            }
            //画爆炸列表
            for (int i = 0; i < booms.Count; i++)
            {
                booms[i].DrawBoom(e.Graphics);
            }
            //画道具列表
            for (int i = 0; i < Props.Count; i++)
            {
                Props[i].DrawProp(e.Graphics);
            }
        }

        private void GameC_Load(object sender, EventArgs e)
        {
            //创建一个子线程；
            //Thread thread = new Thread(new ThreadStart(run));
            Thread thread = new Thread(run);
            thread.IsBackground = true;//设置为后台线程
            thread.Start();
            
        }
        private void run()
        {
            //不断重绘窗体；
            while (true) { 
            this.Invalidate();//让窗体无效，使其重绘；
                Thread.Sleep(30);//间隔50毫秒；
                DrawEP();
                DrawProps();
            }
        }

        private void GameC_KeyDown(object sender, KeyEventArgs e)
        {
            p.KeyDown(e.KeyCode);

        }
        private void GameC_KeyUp(object sender, KeyEventArgs e)
        {
            p.KeyUp(e.KeyCode);
           
        }
        //产生敌机
        private void DrawEP()
        {
            
            if(rd.Next(100)%59==0)
            ep.Add(new EnemyPlane(1000, rd.Next(10, 450), 60, 20, rd.Next(4, 10), rd.Next(1, 3),true, Direction.A, this));
        }
        //产生道具
        private void DrawProps()
        {

            if (rd.Next(1000) % 514 == 0)
                Props.Add(new Prop(rd.Next(100,900), -50, 50, 50, rd.Next(3,8),1, true, Direction.S, this));
        }
    }
    
}
