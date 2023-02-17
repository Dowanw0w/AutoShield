using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using FlyFF.Config;

namespace FlyFF
{
    public partial class Form1 : Form
    {
        // 创建dmSoft对象
        DmSoftCustomClassName dmSoft = new DmSoftCustomClassName();

        public Form1()
        {
            InitializeComponent();
        }

        private void function(int hwnd, int x1, int y1, int x2, int y2, string picname)
        {
            int x, y;
            while (true)
            {
                if (dmSoft.GetWindowState(hwnd, 1) == 1)
                {
                    dmSoft.FindPic(x1, y1, x2, y2, picname, "050505", 0.9, 2, out x, out y);
                    if (x < 0 && y < 0)
                    {
                        SendKeys.Send("{F8}");
                    }
                }
                dmSoft.delay(500);
            }
        }

        private void Form1_Load(object sender, EventArgs e) { }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            // 获取窗口句柄
            var hwnd = dmSoft.EnumWindowByProcess("Neuz.exe", "新飞飞", "D3D Window", 1 + 8 + 16);

            //获取多个窗口的句柄并转换为数组，使用逗号分割获取想要的窗口句柄
            var hwndArray = Regex.Split(hwnd, ",", RegexOptions.IgnoreCase);
            int ret = dmSoft.BindWindow(int.Parse(hwndArray[0]), "normal", "normal", "windows", 0);
            if (ret == 1)
            {
                MessageBox.Show("绑定成功！");
            }
            else
            {
                MessageBox.Show("绑定失败！");
                return;
            }

            int _HWND = int.Parse(hwndArray[0]);
            int x1, y1, x2, y2;
            int dm_ret = dmSoft.GetWindowRect(_HWND, out x1, out y1, out x2, out y2);

            // 设置全局路径,设置了此路径后,所有接口调用中,相关的文件都相对于此路径. 比如图片,字库等
            dmSoft.SetPath(DmConfig.DmGlobalPath);

            function(int.Parse(hwndArray[0]), x1, y1, x2, y2, "魔法盾小图标.bmp");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
