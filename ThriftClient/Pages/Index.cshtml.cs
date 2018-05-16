using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Thrift.Protocol;
using Common;
using Thrift.Transport;

namespace ThriftClient.Pages
{
    public class IndexModel : PageModel
    {
        public string Text1 { get; private set; } = " 123";        
        public string Text2 { get; private set; } = " ";
        public string Text3 { get; private set; } = " ";
        public string Text4 { get; private set; } = " ";

        static Dictionary<String, String> map = new Dictionary<String, String>();
        static List<Blog> blogs = new List<Blog>();

        //请求页面会访问的方法
        public void OnGet()
        {
            IndexModel ob1 = new IndexModel();
            Text1 = ob1.Click1();

            // 下面代码与thrift相关
            TTransport transport = new TSocket("localhost", 7911);
            TProtocol protocol = new TBinaryProtocol(transport);
            ThriftCase.Client client = new ThriftCase.Client(protocol);
            transport.Open();

            Text2 = "Client calls .....";
            //获取请求参数

            string blog1;
            blog1 = Request.Query["id"];//获取get请求参数
            if (blog1 == null)
            {
                blog1 = "blog";
            }
          // 获取post请求参数Request.Form["Key"]
            map.Add(blog1, "http://www.javabloger.com");

            int a = client.testCase1(10, 21, "3");
            Text3 = a.ToString();

            List<string> b = client.testCase2(map);
            Text4 = string.Join(",",b.ToArray());

            client.testCase3();

            Blog blog = new Blog();
            blog.CreatedTime = DateTime.Now.Ticks;
            blog.Id = "123456";
            blog.IpAddress = "127.0.0.1";
            blog.Topic = "this is blog topic";
            blogs.Add(blog);

            client.testCase4(blogs);

            transport.Close();





        }
        public string Click1()
        {
            Text1 = Text1 + "qwe";
            return Text1;

        }

       
    }
}
