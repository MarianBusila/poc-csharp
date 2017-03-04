using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using System.Reflection;

namespace TestNinject
{
    class Program
    {
        static void Main(string[] args)
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            var mailSender = kernel.Get<IMailSender>();
            //IMailSender mailSender = new MockMailSender(); //the type of mailSender comes from the bindings
            FormHandler formHandler = new FormHandler(mailSender);
            formHandler.Handle("test@test.com");
        }
    }
}
