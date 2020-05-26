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
            ////inputs
            //OrGateDecorator orGate1 = new OrGateDecorator(new NandGate());
            //OrGateDecorator orGate2 = new OrGateDecorator(new NandGate());

            //orGate1.state = States.STATE_FALSE;
            //orGate2.state = States.STATE_TRUE;

            ////calctest
            //OrGateDecorator orGate = new OrGateDecorator(new NandGate());

            //orGate.state = States.STATE_UNDEFINED;
            //Console.WriteLine(orGate.state.ToString());


            //orGate.inputs.Add(orGate1);
            //orGate.inputs.Add(orGate2);

            //orGate.CalculateState();

            //Console.WriteLine(orGate.state.ToString());
            //Console.ReadKey();
            #endregion

            #region AndGate
            ////inputs
            //AndGateDecorator andGate1 = new AndGateDecorator(new NandGate());
            //AndGateDecorator andGate2 = new AndGateDecorator(new NandGate());

            //andGate1.state = States.STATE_FALSE;
            //andGate2.state = States.STATE_FALSE;

            ////calctest
            //AndGateDecorator andGate = new AndGateDecorator(new NandGate());

            //andGate.state = States.STATE_UNDEFINED;
            //Console.WriteLine(andGate.state.ToString());


            //andGate.inputs.Add(andGate1);
            //andGate.inputs.Add(andGate2);

            //andGate.CalculateState();

            //Console.WriteLine(andGate.state.ToString());
            //Console.ReadKey();
            #endregion

            #region NotGate
            //inputs
            NotGateDecorator notGate1 = new NotGateDecorator(new NandGate());

            notGate1.state = States.STATE_TRUE;

            //calctest
            NotGateDecorator notGate = new NotGateDecorator(new NandGate());

            notGate.state = States.STATE_UNDEFINED;
            Console.WriteLine(notGate.state.ToString());


            notGate.inputs.Add(notGate1);

            notGate.CalculateState();

            Console.WriteLine(notGate.state.ToString());
            Console.ReadKey();
            #endregion



        }
    }
}
