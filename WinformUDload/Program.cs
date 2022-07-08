using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformUDload
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.Run(new Form1());
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.ExceptionObject as Exception, "");
            MessageBox.Show(str);
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.Exception, "");
            MessageBox.Show(str);
        }
        /// <summary>
        /// 生成自定义异常消息
        /// </summary>
        /// <param name="ex">异常对象</param>
        /// <param name="backStr">备用异常消息：当ex为null时有效</param>
        /// <returns>异常字符串文本</returns>
        static string GetExceptionMsg(Exception ex, string backStr)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("****************************异常文本****************************" + Environment.NewLine);
            sb.AppendLine("【出现时间】：" + DateTime.Now.ToString() + Environment.NewLine);
            if (ex != null)
            {
                sb.AppendLine("【异常类型】：" + ex.GetType().Name + Environment.NewLine);
                sb.AppendLine("【异常信息】：" + ex.Message + Environment.NewLine);
                sb.AppendLine("【堆栈调用】：" + ex.StackTrace + Environment.NewLine);
            }
            else
            {
                sb.AppendLine("【未处理异常】：" + backStr + Environment.NewLine);
            }
            sb.AppendLine("***************************************************************");
            return sb.ToString();

        }
    }
}

