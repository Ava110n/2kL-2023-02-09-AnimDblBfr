using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace _2kL_2023_02_09_AnimDblBfr
{
    public class Animator
    {
        private Circle c;
        private Thread? t = null;
        public bool IsAlive => t == null || t.IsAlive;
        public Size ContainerSize { get; set; }

        public Animator(Size containerSize)
        {
            Random rnd = new Random();
            int diam = 50;
            ContainerSize = containerSize;
            c = new Circle(diam, rnd.Next(0, containerSize.Width-diam), 
                rnd.Next(0, containerSize.Height-diam));
        }

        public void Start()
        {
            Random rnd = new Random();
            c.Dx = rnd.Next(-5, 6);
            int sign = rnd.Next(0, 2);
            if(sign==0) { sign = -1; }
            c.Dy = sign*Convert.ToInt32(Math.Sqrt(25- c.Dx * c.Dx));
            t = new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(30);
                    c.Move();
                    wall_check();
                }
            });
            t.IsBackground = true;
            t.Start();
        }

        public void wall_check()
        {
            if (c.X+c.Diam >= ContainerSize.Width || c.X <=0) 
            { 
                c.Dx = - c.Dx;
            }
            if (c.Y + c.Diam >= ContainerSize.Height || c.Y <= 0) 
            { 
                c.Dy = - c.Dy;
            }
        }

        public void PaintCircle(Graphics g)
        {
            c.Paint(g);
        }
    }
}
