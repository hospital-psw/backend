﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Model
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}