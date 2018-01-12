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
        
        private float robotArmX;
        private float robotArmY;
        private float robotRotation = 0;
        private float robotClampX = 0;
        private float robotClampY = 0;

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
            //trackBar1.ValueChanged += trackBar1_ValueChanged;//获取trackBar1的值
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
                positionPanel.SetOriginX(ranOrigin.Next(0, radius * 2));
                positionPanel.SetOriginY(ranOrigin.Next(0, radius));
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
                        positionPanel.SetOriginX(positionPanel.GetOriginX() + positionTrackBar.Value);
                        positionPanel.Invalidate();
                        FormToRobotArm();
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
                        positionPanel.SetOriginX(positionPanel.GetOriginX() - positionTrackBar.Value);
                        positionPanel.Invalidate();
                        FormToRobotArm();
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
                        positionPanel.SetOriginY(positionPanel.GetOriginY() - positionTrackBar.Value);
                        positionPanel.Invalidate();
                        FormToRobotArm();
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
                        positionPanel.SetOriginY(positionPanel.GetOriginY() + positionTrackBar.Value);
                        positionPanel.Invalidate();
                        FormToRobotArm();
                    }
                    //else
                    //{
                    //    robotArmPositionPanel.SetOriginY(radius);
                    //    robotArmPositionPanel.Invalidate();
                    //    FormToRobotArm();
                    //}
                    break;
                case Keys.A:
                    robotRotation -= 90;
                    ClampAngelToOrigin();
                    clampPanel.Invalidate();
                    break;
                case Keys.D:
                    robotRotation += 90;
                    ClampAngelToOrigin();
                    clampPanel.Invalidate();
                    break;

            }
            return true;
        }

        public void InitOrigin()
        {
            robotArmX = control.robot1.x/2;
            robotArmY = control.robot1.y/2;
            robotRotation = control.robot1.rotation;

            ClampAngelToOrigin();

            Debug.WriteLine("rotation:{0}", robotRotation);
            Debug.WriteLine("angle1:{0}", control.robot1.angle1);
            Debug.WriteLine("angle2:{0}", control.robot1.angle2);
            Debug.WriteLine("x:{0}", robotArmX);
            Debug.WriteLine("y:{0}", robotArmY);

            positionPanel.SetOriginX(radius - robotArmY);
            positionPanel.SetOriginY(radius - robotArmX);
            positionPanel.UpdateNowOrigin();
            positionPanel.Invalidate();
            FormToRobotArm();
            //string robotArmOrigin = string.Format("{0},{1}", robot1.x, robot1.y);
            //txtInfo.Text = robotArmOrigin;

        }
        public float RadianToAngel(float angel)
        {
            return angel * (float)Math.PI / 180;
        }

         public void ClampAngelToOrigin()
         {
             robotClampX = clampRadius - (float)Math.Cos(RadianToAngel(robotRotation)) * clampRadius;
            Debug.WriteLine("cos(rotation)={0}", (float)Math.Cos(RadianToAngel(robotRotation)));
             robotClampY = clampRadius - (float)Math.Sin(RadianToAngel(robotRotation)) * clampRadius;
             Debug.WriteLine("Clam: x:{0},y:{1}", robotClampX, robotClampY);
             clampPanel.SetClampArrow(robotClampX,robotClampY);
             string robotClampOrigin = string.Format("{0}", robotRotation);
             txtrotation.Text = robotClampOrigin;

         }
         
        public void FormToRobotArm()
        {
            //robotArmPositionPanel.SetOriginX(radius - robotArmY);
            //robotArmPositionPanel.SetOriginY(radius - robotArmX);
            //robotArmPositionPanel.UpdateNowOrigin();
            //robotArmPositionPanel.Invalidate();
            //robotArmX = radius - robotArmPositionPanel.GetOriginY();
            //robotArmY = radius - robotArmPositionPanel.GetOriginX();
            string robotArmOrigin = string.Format("{0},{1}", robotArmX*2, robotArmY*2);
            txtPosition.Text = robotArmOrigin;


        }



        private void init_Click_1(object sender, EventArgs e)
        {
            control.RobotArmInit();
            Debug.WriteLine("init click");
            InitOrigin();
        }

        private void move_Click_1(object sender, EventArgs e)
        {
            //robotArmPositionPanel.UpdateNowOrigin();
            //robotArmPositionPanel.Invalidate();
            // myScaraController.Init();
            //control.RobotArmInit();
            // FormToRobotArm();
            // string robotArmOrigin = string.Format("{0},{1}", RobotArmX, RobotArmY);
            //  txtInfo.Text = robotArmOrigin;
            control.RobotArmMove(robotArmX*2, robotArmY*2, 0, 0, 30, 1, 1, 1);
        }
    }


}
