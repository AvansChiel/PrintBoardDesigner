using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrintBoardDesigner;
namespace PrintBoardDesignerTest
{
    [TestClass]
    public class NandGateTests
    {
        [TestMethod]
        public void Process_WithTrueInputs_NandGate()
        {
            // Arrange
            NandGate inputGate1 = new NandGate();
            NandGate inputGate2 = new NandGate();
            inputGate1.state = States.STATE_TRUE;
            inputGate2.state = States.STATE_TRUE;
            NandGate gateToTest = new NandGate();
            gateToTest.inputs.Add(inputGate1);
            gateToTest.inputs.Add(inputGate2);
            var expected = States.STATE_FALSE;


            // Act
            gateToTest.CalculateState();

            // Assert
            var actual = gateToTest.state;
            Assert.AreEqual(expected, actual, "State not calculated correctly");
        }

        [TestMethod]
        public void Process_WithFalseInputs_NandGate()
        {
            // Arrange
            NandGate inputGate1 = new NandGate();
            NandGate inputGate2 = new NandGate();
            inputGate1.state = States.STATE_FALSE;
            inputGate2.state = States.STATE_FALSE;
            NandGate gateToTest = new NandGate();
            gateToTest.inputs.Add(inputGate1);
            gateToTest.inputs.Add(inputGate2);
            var expected = States.STATE_TRUE;


            // Act
            gateToTest.CalculateState();

            // Assert
            var actual = gateToTest.state;
            Assert.AreEqual(expected, actual, "State not calculated correctly");
        }

        [TestMethod]
        public void Process_WithFalseAndTrueInputs_NandGate()
        {
            // Arrange
            NandGate inputGate1 = new NandGate();
            NandGate inputGate2 = new NandGate();
            inputGate1.state = States.STATE_TRUE;
            inputGate2.state = States.STATE_FALSE;
            NandGate gateToTest = new NandGate();
            gateToTest.inputs.Add(inputGate1);
            gateToTest.inputs.Add(inputGate2);
            var expected = States.STATE_TRUE;


            // Act
            gateToTest.CalculateState();

            // Assert
            var actual = gateToTest.state;
            Assert.AreEqual(expected, actual, "State not calculated correctly");
        }
    }
}
