using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RobotArm
{
    public partial class RobotControlForm : Form
    {
        const int radius = PositionPanel.radius;
        const int clampRadius = ClampPanel.clampRadius;
        const int arm1 = PositionPanel.arm1;
        const int arm2 = PositionPanel.arm2;
        

        private float positionX;
        private float positionY;
        private float arm4Rotation = 0;
        private float clampX = 0;
        private float clampY = 0;
        private float zHeight = 0;
        private float arm1Rotation = 0;
        private float arm2Rotation = 0;
        private float arm1X;
        private float arm1Y;
        private float arm2X;
        private float arm2Y;

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

        public void CreatThreadForDraw()
        {
            Thread drawThread = new Thread(ArmDraw);
            drawThread.Start();
        }

        public  void ArmDraw()
        {
           
            while (!(control.robot1.is_robot_goto_target()))
            { 
                Arm1AngleToCoordination();
                positionPanel.Invalidate();
                Thread.Sleep(100);
            }

            positionPanel.UpdateNowCoordination();
            positionPanel.Invalidate();
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
            positionPanel.UpdateNowCoordination();

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
                        positionY -= positionTrackBar.Value / 2;
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
                        positionY += positionTrackBar.Value / 2;
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
                    arm4Rotation -= 5;
                    ClampAngelToCoordination();
                    clampPanel.Invalidate();
                    break;
                case Keys.D:
                    arm4Rotation += 5;
                    ClampAngelToCoordination();
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
            positionX = control.robot1.x / 2;
            positionY = control.robot1.y / 2;
            arm4Rotation = control.robot1.rotation;
            zHeight = control.robot1.z;
            arm1Rotation = control.robot1.angle1;
            arm2Rotation = control.robot1.angle2;

            ClampAngelToCoordination();
            clampPanel.Invalidate();

            Debug.WriteLine("arm1Rotation:{0}", arm1Rotation);
            Debug.WriteLine("arm2Rotation:{0}", arm2Rotation);
            Debug.WriteLine("arm4Rotation:{0}", arm4Rotation);
            Debug.WriteLine("zHeight:{0}", zHeight);
            Debug.WriteLine("x:{0}", positionX);
            Debug.WriteLine("y:{0}", positionY);

            PositionPanelToRobotArm();
            Arm1AngleToCoordination();
            positionPanel.UpdateNowCoordination();
            positionPanel.Invalidate();


            SetZHeight();
            zPanel.Invalidate();
        }

        public void Arm1AngleToCoordination()
        {
            control.robot1.get_scara_param();
            arm1Rotation = control.robot1.angle1;
            arm1X = radius - (float)Math.Cos(AngleToRadian(90-arm1Rotation)) * arm1;
            Debug.WriteLine("arm1rotation={0},cos(arm1rotation)={1}",arm1Rotation, (float)Math.Cos(AngleToRadian(90-arm1Rotation)));
            arm1Y = radius - (float)Math.Sin(AngleToRadian(90-arm1Rotation)) * arm1;
            Debug.WriteLine("arm1: x:{0},y:{1}", arm1X, arm1Y);
            positionPanel.SetArm1(arm1X,arm1Y);

            arm2Rotation = control.robot1.angle2;
            arm2X = arm1X - (float)Math.Cos(AngleToRadian(90 - arm1Rotation-arm2Rotation)) * arm1;
            Debug.WriteLine("arm1rotation={0},cos(arm1rotation)={1}", arm1Rotation, (float)Math.Cos(AngleToRadian(90 - arm1Rotation)));
            arm2Y = arm1Y - (float)Math.Sin(AngleToRadian(90 - arm1Rotation-arm2Rotation)) * arm1;
            Debug.WriteLine("arm1: x:{0},y:{1}", arm1X, arm1Y);
            positionPanel.SetArm2(arm2X, arm2Y);

        }

        public float AngleToRadian(float angle)
        {
            return angle * (float)Math.PI / 180;
        }

        public void SetZHeight()
        {
            zPanel.SetArrowHeight(-zHeight);
            string strZHeight = string.Format("{0}", zHeight);
            txtZHeight.Text = strZHeight;
        }
         public void ClampAngelToCoordination()
         {
             clampX = clampRadius - (float)Math.Cos(AngleToRadian(arm4Rotation)) * clampRadius;
             Debug.WriteLine("cos(rotation)={0}", (float)Math.Cos(AngleToRadian(arm4Rotation)));
             clampY = clampRadius - (float)Math.Sin(AngleToRadian(arm4Rotation)) * clampRadius;
             Debug.WriteLine("Clam: x:{0},y:{1}", clampX, clampY);
             clampPanel.SetClampArrow(clampX, clampY);
             string strClampAngle = string.Format("{0}", arm4Rotation % 360);
             txtrotation.Text = strClampAngle;

         }
         
        public void PositionPanelToRobotArm()
        {
            positionPanel.SetOrigin(radius - positionY, radius - positionX);
            string strPosition = string.Format("{0},{1}", positionX * 2, positionY * 2);
            txtPosition.Text = strPosition;


        }



        private void init_Click(object sender, EventArgs e)
        {
            control.RobotArmInit();
            Debug.WriteLine("init click");
            InitData();
        }

        private void move_Click(object sender, EventArgs e)
        {
            bool ret = control.robot1.is_robot_goto_target();
            control.MovePosition(positionX * 2, positionY * 2, zHeight, -arm4Rotation, 20, 1, 1, 1);
            //control.MoveArm4(rotation);
            CreatThreadForDraw();
            Arm1AngleToCoordination();
            positionPanel.UpdateNowCoordination();
            positionPanel.Invalidate();
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
