using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {           
            string sendAddress = "2290937298@qq.com";
            string receiveAddress = "1281001690@qq.com";
            string imgAddress = @"E:\IT\Exercise\C#SendEmail\ConsoleApp1\IMG\wallhaven-142977.jpg";
            string text = "This is ChenHanSong mail test for fuck you 小鸡鸡";
            string stmppawd = "sxzakvvdqxctebjc";

            TestEmail testEmail = new TestEmail();
            testEmail.SendImgEmail(sendAddress, receiveAddress, imgAddress, text, stmppawd);

            Console.ReadLine();
        }
    }

    public class TestEmail
    {
        /// <summary>
        /// 发送带有图片的邮件
        /// </summary>
        /// <param name="sendAddress">发送方地址</param>
        /// <param name="receiveAddress">接收方地址</param>
        /// <param name="imgAddress">图片地址</param>
        /// <param name="text">文本内容</param>
        /// /// <param name="stmppawd">semp授权码</param>
        public void SendImgEmail(string sendAddress, string receiveAddress, string imgAddress,string text,string stmppawd)
        {
            Bitmap bmp = new Bitmap(imgAddress);

            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] arr = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(arr, 0, (int)ms.Length);
            ms.Close();

            String strbaser64 = Convert.ToBase64String(arr);
            MailMessage mm = new MailMessage(sendAddress, receiveAddress);//发送方(需要开启smpt) 接收方

            mm.Subject = "Test Email" + DateTime.Now;//标题
            mm.IsBodyHtml = true;

            string mailBody = "<BODY style=\"MARGIN: 10px\"><DIV><p>"+text+"</p> <IMG src=\"data:image/png;base64," + strbaser64 + "\"> </IMG></DIV></BODY> ";
            mm.Body = mailBody;

            SmtpClient sc = new SmtpClient("smtp.qq.com");//利用QQ邮箱发送
            sc.Credentials = new NetworkCredential(sendAddress, stmppawd);//用户名和密码  密码应为开启stmp时给的授权码
            try
            {
                sc.Send(mm);
                Console.WriteLine("发送成功");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// 发送纯文本邮件
        /// </summary>
        /// <param name="sendAddress"></param>
        /// <param name="receiveAddress"></param>
        public void SendTextEmail(string sendAddress, string receiveAddress)
        {
            MailMessage mm = new MailMessage(sendAddress, receiveAddress);//发送方(需要开启smpt) 接收方

            mm.Subject = "Test Email    " + DateTime.Now;//标题
            mm.Body = "This is ChenHanSong Test Mail";
            SmtpClient sc = new SmtpClient("smtp.qq.com");//利用QQ邮箱发送
            sc.Credentials = new NetworkCredential(sendAddress, "****");//用户名和密码  密码应为开启stmp时给的授权码
            sc.Send(mm);
        }
    }
}
