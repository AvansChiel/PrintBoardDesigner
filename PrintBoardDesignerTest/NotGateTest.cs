using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrintBoardDesigner;

namespace PrintBoardDesignerTest
{
    [TestClass]
    public class NotGateTest
    {
        [TestMethod]
        public void Process_WithTrueInputs_AndGate()
        {
            // Arrange
            NandGate inputGate1 = new NandGate();
            inputGate1.state = States.STATE_TRUE;
            NotGateDecorator gateToTest = new NotGateDecorator(new NandGate());
            gateToTest.inputs.Add(inputGate1);
            var expected = States.STATE_FALSE;


            // Act
            gateToTest.CalculateState();

            // Assert
            var actual = gateToTest.state;
            Assert.AreEqual(expected, actual, "State not calculated correctly");
        }

        [TestMethod]
        public void Process_WithFalseInputs_AndGate()
        {
            // Arrange
            NandGate inputGate1 = new NandGate();
            inputGate1.state = States.STATE_FALSE;
            NotGateDecorator gateToTest = new NotGateDecorator(new NandGate());
            gateToTest.inputs.Add(inputGate1);
            var expected = States.STATE_TRUE;


            // Act
            gateToTest.CalculateState();

            // Assert
            var actual = gateToTest.state;
            Assert.AreEqual(expected, actual, "State not calculated correctly");
        }

    }
}
