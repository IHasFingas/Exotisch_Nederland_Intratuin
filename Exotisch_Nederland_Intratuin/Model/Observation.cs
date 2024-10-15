﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Exotisch_Nederland_Intratuin.Model
{
    internal class Observation
    {
        private int id;
        private string name;
        private string description;
        private Image picture;
        private string location;
        private Specie specie;
        private Area area;
        private User user;
    }
}