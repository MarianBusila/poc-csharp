﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNancyTopshelf
{
    public interface IConfigurationService
    {
        string GetValue(string key);
    }
}
