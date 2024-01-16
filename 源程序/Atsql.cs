using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using static System.Windows.Forms.LinkLabel;
using System.Windows.Forms;

namespace CSharpSQL课设
{

    internal class Atsql
    {
    }
    //对数据库进行操作的类
    class directives
    {   //通过windows认证连接数据库
        //连接方法:Data Source后填服务器名称或localhost，Initial Catalog后填要连接的数据库

        public string linkString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\dell\\Documents\\火车票系统.mdf;Integrated Security=True;Connect Timeout=30";
        
        SqlConnection link;
        public SqlConnection Connsql()
        {
            try
            {
                link = new SqlConnection(linkString);
                link.Open();
            }
            catch (Exception){ MessageBox.Show("连接数据库失败，请检查您的数据库配置然后重新启动程序！\n"); Environment.Exit(0); }
            return link; //返回数据库对象
        }
        public SqlCommand Command(string sql)
        {
            SqlCommand cad = new SqlCommand(sql, Connsql());//数据库操作对象
            return cad;
        }

        //更改数据
        public int Update(string sql)//此"sql"变量应为sql语句
        {
            int a;
            try
            { a=Command(sql).ExecuteNonQuery(); }
            catch (SqlException e) { MessageBox.Show("抱歉，出现错误！\n这可能是由于您的输入值格式不正确所引发的\n详细信息：\n"+sql +"\n"+ e,"执行失败"); return 0; }
            return a;
        }

        //读取
        public SqlDataReader Read(string sql)
        { try
            {
                return Command(sql).ExecuteReader();
            }
            catch { return null; }
        }

        //检测是否打开数据库
        public Boolean linkon() { if (link.State == ConnectionState.Open) return true;return false; }
       
        //关闭数据库连接
        public void X() { link.Close(); }
    }
}
