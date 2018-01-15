using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RobotArm
{
    public partial class RobotControlForm: Form
    {
        const int radius = PositionPanel.radius;
        const int clampRadius = ClampPanel.clampRadius;
        
        private float positionX;
        private float positionY;
        private float rotation = 0;
        private float clampX = 0;
        private float clampY = 0;
        private float zHeight = 0;

        Controller control = new Controller();


        public RobotControlForm()
        {
            InitializeComponent();
            GetOrigin();
            positionPanel.Invalidate();
            
        }


        private void RobotControlForm_Load(object sender, EventArgs e)
        {
            positionTrackBar.ValueChanged += PositionTrackBar_ValueChanged;
            control.Display += Control_Display;
        }

        void Control_Display(string message)
        {
            txtMessage.Text = message;
        }
        void PositionTrackBar_ValueChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }
        private void GetOrigin()//获取随机值
        {
            System.Random raniSeed = new Random();
            int iSeed = raniSeed.Next();
            Random ranOrigin = new Random(iSeed);
            while ((positionPanel.GetOriginX() - radius) * (positionPanel.GetOriginX() - radius) + (positionPanel.GetOriginY() - radius) * (positionPanel.GetOriginY() - radius) > radius * radius)
            {
                positionPanel.SetOrigin(ranOrigin.Next(0, radius * 2), ranOrigin.Next(0, radius));
            }
            positionPanel.UpdateNowOrigin();
            Debug.WriteLine(positionPanel.GetOriginX());
            Debug.WriteLine(positionPanel.GetOriginY());

        }

        enum Direction
        {
            Right,
            Left,
            Up,
            Down
        };

        private int GetRange(int side)
        {
            return (int)(Math.Sqrt(radius * radius - side * side));

        }
        //private bool CheckOrigin(Direction key)//检查移动位置是否过界
        //{
        //    bool result = false;
        //    switch (key)
        //    {
        //        case Direction.Right:
        //            result = ((robotArmPositionPanel.GetOriginX() + positionTrackBar.Value) >= 0 && (robotArmPositionPanel.GetOriginX() + positionTrackBar.Value) <= radius * 2 && robotArmPositionPanel.GetOriginY() >= 0 && robotArmPositionPanel.GetOriginY() <= radius && ((robotArmPositionPanel.GetOriginX() + positionTrackBar.Value - radius) * (robotArmPositionPanel.GetOriginX() + positionTrackBar.Value - radius) + (robotArmPositionPanel.GetOriginY() - radius) * (robotArmPositionPanel.GetOriginY() - radius) <= radius * radius));
        //            break;
        //        case Direction.Left:
        //            result = ((robotArmPositionPanel.GetOriginX() - positionTrackBar.Value) >= 0 && (robotArmPositionPanel.GetOriginX() - positionTrackBar.Value) <= radius * 2 && robotArmPositionPanel.GetOriginY() >= 0 && robotArmPositionPanel.GetOriginY() <= radius && ((robotArmPositionPanel.GetOriginX() - positionTrackBar.Value - radius) * (robotArmPositionPanel.GetOriginX() - positionTrackBar.Value - radius) + (robotArmPositionPanel.GetOriginY() - radius) * (robotArmPositionPanel.GetOriginY() - radius) <= radius * radius));
        //            break;
        //        case Direction.Up:
        //            result = (robotArmPositionPanel.GetOriginX() >= 0 && robotArmPositionPanel.GetOriginX() <= radius * 2 && (robotArmPositionPanel.GetOriginY() - positionTrackBar.Value) >= 0 && (robotArmPositionPanel.GetOriginY() - positionTrackBar.Value) <= radius && ((robotArmPositionPanel.GetOriginX() - radius) * (robotArmPositionPanel.GetOriginX() - radius) + (robotArmPositionPanel.GetOriginY() - positionTrackBar.Value - radius) * (robotArmPositionPanel.GetOriginY() - positionTrackBar.Value - radius) <= radius * radius));
        //            break;
        //        case Direction.Down:
        //            result = (robotArmPositionPanel.GetOriginX() >= 0 && robotArmPositionPanel.GetOriginX() <= radius * 2 && (robotArmPositionPanel.GetOriginY() + positionTrackBar.Value) >= 0 && (robotArmPositionPanel.GetOriginY() + positionTrackBar.Value) <= radius && ((robotArmPositionPanel.GetOriginX() - radius) * (robotArmPositionPanel.GetOriginX() - radius) + (robotArmPositionPanel.GetOriginY() + positionTrackBar.Value - radius) * (robotArmPositionPanel.GetOriginY() + positionTrackBar.Value - radius) <= radius * radius));
        //            break;
        //    }
        //    return result;
        //}

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)//方向键触发
        {

            switch (keyData)
            {
                case Keys.Right:
                    //if(CheckOrigin(Direction.Right))
                    {
                        positionY -= positionTrackBar.Value/2;
                        PositionPanelToRobotArm();
                        positionPanel.Invalidate();
                    }
                    //else
                    //{
                    //    robotArmPositionPanel.SetOriginX(GetRange(radius - robotArmPositionPanel.GetOriginY()) +radius);
                    //    robotArmPositionPanel.Invalidate();
                    //    FormToRobotArm();
                    //}
                    break;
                case Keys.Left:
                    //if (CheckOrigin(Direction.Left))
                    {
                        positionY += positionTrackBar.Value/2;
                        PositionPanelToRobotArm();
                        positionPanel.Invalidate();
                    }
                    //else
                    //{
                    //    robotArmPositionPanel.SetOriginX(radius - GetRange(radius-robotArmPositionPanel.GetOriginY()));
                    //    robotArmPositionPanel.Invalidate();
                    //    FormToRobotArm();
                    //}
                    break;
                case Keys.Up:
                    //if (CheckOrigin(Direction.Up))
                    {
                        positionX += positionTrackBar.Value;
                        PositionPanelToRobotArm();
                        positionPanel.Invalidate();
                    }
                    //else
                    //{
                    //    robotArmPositionPanel.SetOriginY(radius-GetRange(Math.Abs(radius-robotArmPositionPanel.GetOriginX())));
                    //    robotArmPositionPanel.Invalidate();
                    //    FormToRobotArm();
                    //}
                    break;
                case Keys.Down:
                    //if (CheckOrigin(Direction.Down))
                    {
                        positionX -= positionTrackBar.Value;
                        PositionPanelToRobotArm();
                        positionPanel.Invalidate();
                    }
                    //else
                    //{
                    //    robotArmPositionPanel.SetOriginY(radius);
                    //    robotArmPositionPanel.Invalidate();
                    //    FormToRobotArm();
                    //}
                    break;
                case Keys.A:
                    rotation -= 5;
                    ClampAngelToOrigin();
                    clampPanel.Invalidate();
                    break;
                case Keys.D:
                    rotation += 5;
                    ClampAngelToOrigin();
                    clampPanel.Invalidate();
                    break;
                case Keys.W:
                    zHeight += 5;
                    SetZHeight();
                    zPanel.Invalidate();
                    break;
                case Keys.S:
                    zHeight -= 5;
                    SetZHeight();
                    zPanel.Invalidate();
                    break;
            }
            return true;
        }

        public void InitData()
        {
            positionX = control.robot1.x/2;
            positionY = control.robot1.y/2;
            rotation = control.robot1.rotation;
            zHeight = control.robot1.z;

            ClampAngelToOrigin();
            clampPanel.Invalidate();

            Debug.WriteLine("rotation:{0}", rotation);
            Debug.WriteLine("angle1:{0}", control.robot1.angle1);
            Debug.WriteLine("angle2:{0}", control.robot1.angle2);
            Debug.WriteLine("x:{0}", positionX);
            Debug.WriteLine("y:{0}", positionY);

            PositionPanelToRobotArm();
            positionPanel.Invalidate();

            SetZHeight();
            zPanel.Invalidate();
        }
        
        public float RadianToAngel(float angel)
        {
            return angel * (float)Math.PI / 180;
        }

        public void SetZHeight()
        {
            zPanel.SetArrowHeight(-zHeight);
            string strZHeight = string.Format("{0}", zHeight);
            txtZHeight.Text = strZHeight;
        }
         public void ClampAngelToOrigin()
         {
             clampX = clampRadius - (float)Math.Cos(RadianToAngel(rotation)) * clampRadius;
             Debug.WriteLine("cos(rotation)={0}", (float)Math.Cos(RadianToAngel(rotation)));
             clampY = clampRadius - (float)Math.Sin(RadianToAngel(rotation)) * clampRadius;
             Debug.WriteLine("Clam: x:{0},y:{1}", clampX, clampY);
             clampPanel.SetClampArrow(clampX, clampY);
             string strClampAngle = string.Format("{0}", rotation%360);
             txtrotation.Text = strClampAngle;

         }
         
        public void PositionPanelToRobotArm()
        {
            positionPanel.SetOrigin(radius - positionY, radius - positionX);
            positionPanel.UpdateNowOrigin();
            string strPosition = string.Format("{0},{1}", positionX * 2, positionY * 2);
            txtPosition.Text = strPosition;


        }



        private void init_Click_1(object sender, EventArgs e)
        {
            control.RobotArmInit();
            Debug.WriteLine("init click");
            InitData();
        }

        private void move_Click_1(object sender, EventArgs e)
        {
            control.MovePosition(positionX * 2, positionY * 2, zHeight, 60/*rotation*/, 20, 1, 1, 1);
            //control.MoveArm4(rotation);
        }

        private void clampOn_Click(object sender, EventArgs e)
        {
            control.ClampOn();
        }

        private void clampOff_Click(object sender, EventArgs e)
        {
            control.ClampOff();
        }
    }


}
