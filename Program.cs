using FlyFF.DmSoft;
using System;
using System.Windows.Forms;
using FlyFF.Config;
using System.IO;

namespace FlyFF
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 免注册调用大漠插件
            var registerDmSoftDllResult = RegisterDmSoft.RegisterDmSoftDll();
            Console.WriteLine($"免注册调用大漠插件返回：{registerDmSoftDllResult}");
            if (!registerDmSoftDllResult)
            {
                throw new Exception("免注册调用大漠插件失败");
            }

            // 创建对象 
            DmSoftCustomClassName dmSoft = new DmSoftCustomClassName();

            // 收费注册
            var regResult = dmSoft.Reg(DmConfig.DmRegCode, DmConfig.DmVerInfo);
            Console.WriteLine($"收费注册返回：{regResult}");
            if (regResult != 1)
            {
                throw new Exception("收费注册失败");
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
    
}
