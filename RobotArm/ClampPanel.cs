using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RobotArm
{
    public partial class ClampPanel : Control
    {
        public const int clampRadius = 100;
        private float clampArrowX = 0;
        private float clampArrowY = clampRadius;

        public float GetClampArrowX() { return clampArrowX; }
        public float GetClampArrowY() { return clampArrowY; }
        public void SetClampArrow(float x, float y)
        {
            clampArrowX = x;
            clampArrowY = y;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            //g.DrawLine(Pens.Red, new Point(0, 0), new Point(this.Width, this.Height));
            Pen pen1 = new Pen(Color.Black, 2);
            Pen pen = new Pen(Color.Black, 5);//定义箭头画笔
            //int RADIUS = 10;
            g.DrawArc(pen1, 0, 0, 2 * clampRadius, 2 * clampRadius, 0, -180);//弧线
            g.DrawLine(pen1, new Point(0, clampRadius), new Point(2 * clampRadius, clampRadius));//在画板上画直线
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;//恢复实线  
            pen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;//定义线尾的样式为箭头 
            g.DrawLine(pen, clampRadius, clampRadius, clampArrowX, clampArrowY);

        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ClampPanel
            // 
            this.Name = "clampPanel";
            this.ResumeLayout(false);

        }
    }
}
