﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Modules;

namespace TestNinject
{
    public class NinjectBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IMailSender>().To<MailSender>();
            Bind<ILogging>().To<ConsoleLogging>();
            this.Bind<IEnvelopeBuilder>().To<EnvelopeBuilder>().WithConstructorArgument("color", "Pink");
        }
    }
}
