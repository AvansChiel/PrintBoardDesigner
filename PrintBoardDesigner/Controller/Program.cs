using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintBoardDesigner
{
    class Program
    {
        
        static void Main(string[] args)
        {
            MainController mc = new MainController();

            mc.CircuitParser.ParseFile();
        }
    }
}
