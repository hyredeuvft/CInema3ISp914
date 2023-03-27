using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema.DB;

namespace Cinema.ClassHelper
{
    public class EFClass
    {
        public static Entities Contextmy { get; } = new Entities();
    }
}
