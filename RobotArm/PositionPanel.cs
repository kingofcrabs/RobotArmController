using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RobotArm
{
    public partial class PositionPanel : Control

    {
        public const int radius = 200;
        public const int arm1 = 100;
        public const int arm2 = 100;

        private float nowOriginX = 0;//当前十字坐标
        private float nowOriginY = 0;
        private float originX = 0;//移动十字坐标
        private float originY = 0;
        private float nowArm1X = 0;
        private float nowArm1Y = 0;
        private float arm1X = radius;
        private float arm1Y = radius;

        //private float nowArm2X = 0;
        //private float nowArm2Y = 0;
        private float arm2X = radius;
        private float arm2Y = radius;

        public float GetOriginX() { return originX; }
        public float GetOriginY() { return originY; }
        public void SetOrigin(float x,float y)
        {
            originX = x;
            originY = y;
        }

        public void SetArm1(float x, float y)
        {
            arm1X = x;
            arm1Y = y;
        }

        public void SetArm2(float x, float y)
        {
            arm2X = x;
            arm2Y = y;
        }

        public void UpdateNowCoordination()
        {
            nowOriginX = originX;
            nowOriginY = originY;
            //arm1X = arm2X;
            //arm1Y = arm2Y;
            nowArm1X = arm1X;
            nowArm1Y = arm1Y;
            
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            //g.DrawLine(Pens.Red, new Point(0, 0), new Point(this.Width, this.Height));

            Pen pen = new Pen(Color.Black, 2);//定义了实线画笔
            Pen pen1 = new Pen(Color.Black, 2);//定义虚线画笔

            pen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            pen1.DashPattern = new float[] { 1f, 1f };
            g.DrawArc(pen, 0, 0, 2 * radius, 2 * radius, 0, -180);//弧线
            g.DrawLine(pen, new Point(0, radius), new Point(2 * radius, radius));//在画板上画直线

            g.DrawLine(pen, nowOriginX - 10, nowOriginY, nowOriginX + 10, nowOriginY);//构造实线十字
            g.DrawLine(pen, nowOriginX, nowOriginY - 10, nowOriginX, nowOriginY + 10);
            g.DrawLine(pen1, originX - 10, originY, originX + 10, originY);//构造虚线十字
            g.DrawLine(pen1, originX, originY - 10, originX, originY + 10);

            pen1.Color = Color.Red;
            g.DrawLine(pen, radius, radius, nowArm1X, nowArm1Y);
            g.DrawLine(pen, nowArm1X, nowArm1Y, nowOriginX, nowOriginY);
            //if (!(arm1X == nowArm1X && arm1Y == nowArm1Y && originX == nowOriginX && originY == nowOriginX))
            //{
            //g.DrawLine(pen1, radius, radius, arm1X, arm1Y);
            //g.DrawLine(pen1, arm1X, arm1Y, originX, originY);
            //}
            g.DrawLine(pen1, arm2X - 10, arm2Y, arm2X + 10, arm2Y);//构造虚线十字
            g.DrawLine(pen1, arm2X, arm2Y - 10, arm2X, arm2Y + 10);
            g.DrawLine(pen1, radius, radius, arm1X, arm1Y);
            g.DrawLine(pen1, arm1X, arm1Y, arm2X, arm2Y);
        }

    }
}
