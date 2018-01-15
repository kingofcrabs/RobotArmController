using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RobotArm
{
    public partial class ZPanel : Control

    {
        public const int zLength = 300;
        private int arrowLength = 50;

        private float arrowHeight = 0;

        public void SetArrowHeight(float height)
        {
            arrowHeight = height;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;

            Pen pen = new Pen(Color.Black, 5);//定义了实线画笔

            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;//恢复实线  
            pen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;//定义线尾的样式为箭头 
            g.DrawLine(pen, arrowLength, 0, arrowLength, zLength);
            g.DrawLine(pen, 0, arrowHeight, arrowLength, arrowHeight);

        }

    }
}
