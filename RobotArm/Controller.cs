using ControlBeanExDll;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TcpserverExDll;

namespace RobotArm
{
    public class Controller
    {
        public ControlBeanEx robot1;
        public Controller()
        {
            robot1 = TcpserverEx.get_robot(114);
        }

        public void Arminit()
        {


            TcpserverEx.net_port_initial();

            //int card_number = int.Parse("114");
            //robot1 = TcpserverEx.get_robot(card_number);
            if (robot1.is_connected())
            {
                int ret = robot1.joint_home(1);
                robot1.joint_home(3);
                ret = robot1.joint_home(2);
                ret = robot1.joint_home(4);
                if (ret == 0)
                    MessageBox.Show("ARM init error");
                else
                    MessageBox.Show(string.Format("Arm init succes {0}", ret));
            }
            else
                MessageBox.Show("Arm not connected");

        }
        public void RobotArmInit()
        {
            TcpserverEx.net_port_initial();
            //TcpserverEx.card_number_connect(114);


            if (robot1.is_connected())
            {
                int ret = robot1.initial(3, 310);//修改自己机器的型号，参数具体意义参考sdk说明文档

                if (ret == 1)
                {
                    robot1.unlock_position();//解锁
                    MessageBox.Show(robot1.get_card_num().ToString() + "初始化完成");
                    robot1.get_scara_param();
                    Debug.WriteLine(robot1.x);
                    Debug.WriteLine(robot1.y);
                }
                else
                {
                    MessageBox.Show(robot1.get_card_num().ToString() + "初始化失败，返回值 = " + ret.ToString());
                }
            }
            else
            {
                MessageBox.Show(robot1.get_card_num().ToString() + "未连接");
            }
        }

        public void RobotArmMove(float goal_x, float goal_y, float goal_z, float rotation, float speed, float acceleration, int interpolation, int move_mode)
        {
            int ret = robot1.set_position_move(goal_x, goal_y, goal_z, rotation, speed, acceleration, interpolation, move_mode);
            if (ret != 1)
            {
                MessageBox.Show(robot1.get_card_num().ToString() + "调用set_position_move失败，返回值 = " + ret.ToString());

            }
        }
    }
}
