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

            //mc.init();


            #region Orgate
            //inputs
            Console.WriteLine("----start OrGate-----");

            OrGateDecorator orGate1 = new OrGateDecorator(new NandGate());
            OrGateDecorator orGate2 = new OrGateDecorator(new NandGate());

            orGate1.state = States.STATE_FALSE;
            orGate2.state = States.STATE_TRUE;

            //calctest
            OrGateDecorator orGate = new OrGateDecorator(new NandGate());

            orGate.state = States.STATE_UNDEFINED;
            Console.WriteLine(orGate.state.ToString());


            orGate.inputs.Add(orGate1);
            orGate.inputs.Add(orGate2);

            orGate.CalculateState();

            Console.WriteLine(orGate.state.ToString());
            Console.WriteLine("----end OrGate-----");
            Console.ReadKey();

            #endregion

            #region AndGate
            //inputs
            Console.WriteLine("----start AndGate-----");
            AndGateDecorator andGate1 = new AndGateDecorator(new NandGate());
            AndGateDecorator andGate2 = new AndGateDecorator(new NandGate());

            andGate1.state = States.STATE_FALSE;
            andGate2.state = States.STATE_FALSE;

            //calctest
            AndGateDecorator andGate = new AndGateDecorator(new NandGate());

            andGate.state = States.STATE_UNDEFINED;
            Console.WriteLine(andGate.state.ToString());


            andGate.inputs.Add(andGate1);
            andGate.inputs.Add(andGate2);

            andGate.CalculateState();

            Console.WriteLine(andGate.state.ToString());
            Console.WriteLine("----end AndGate-----");
            Console.ReadKey();
            #endregion

            #region NotGate
            //inputs
            Console.WriteLine("----start NotGate-----");
            NotGateDecorator notGate1 = new NotGateDecorator(new NandGate());

            notGate1.state = States.STATE_FALSE;

            //calctest
            NotGateDecorator notGate = new NotGateDecorator(new NandGate());

            notGate.state = States.STATE_UNDEFINED;
            Console.WriteLine(notGate.state.ToString());


            notGate.inputs.Add(notGate1);

            notGate.CalculateState();

            Console.WriteLine(notGate.state.ToString());
            Console.WriteLine("----End NotGate-----");
            Console.ReadKey();
            #endregion

            #region NorGate
            Console.WriteLine("----start NorGate-----");
            NorGateDecorator norGate1 = new NorGateDecorator(new NandGate());
            NorGateDecorator norGate2 = new NorGateDecorator(new NandGate());

            norGate1.state = States.STATE_TRUE;
            norGate2.state = States.STATE_FALSE;

            //calctest
            NorGateDecorator norGate = new NorGateDecorator(new NandGate());


            norGate.state = States.STATE_UNDEFINED;
            Console.WriteLine(norGate.state.ToString());


            norGate.inputs.Add(notGate1);
            norGate.inputs.Add(norGate2);

            norGate.CalculateState();

            Console.WriteLine(norGate.state.ToString());
            Console.WriteLine("----End NorGate-----");
            Console.ReadKey();
            #endregion

            #region XorGate
            Console.WriteLine("----start XorGate-----");
            XorGateDecorator xorGate1 = new XorGateDecorator(new NandGate());
            XorGateDecorator xorGate2 = new XorGateDecorator(new NandGate());

            xorGate1.state = States.STATE_TRUE;
            xorGate2.state = States.STATE_TRUE;

            //calctest
            XorGateDecorator xorGate = new XorGateDecorator(new NandGate());


            xorGate.state = States.STATE_UNDEFINED;
            Console.WriteLine(xorGate.state.ToString());


            xorGate.inputs.Add(xorGate1);
            xorGate.inputs.Add(xorGate2);

            xorGate.CalculateState();

            Console.WriteLine(xorGate.state.ToString());
            Console.WriteLine("----End XorGate-----");
            Console.ReadKey();
            #endregion


        }
    }
}
