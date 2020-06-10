using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrintBoardDesigner;
using System;

namespace PrintBoardDesignerTest
{
    [TestClass]
    public class CircuitBuilderTest
    {

        [TestMethod]
        public void Builder_Can_Build_FullAdder()
        {
            // Arrange
            CircuitBuilder circuitBuilder = new CircuitBuilder();
            var path = @"..\..\..\Circuits\Circuit1_FullAdder.txt";

            // Act
            circuitBuilder.PrepareCircuit(path);
            var circuit = circuitBuilder.GetPreparedCircuit();

            // Assert
            Assert.IsInstanceOfType(circuit, typeof(Circuit));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "File contains disconnected component: has no inputs")]
        public void Builder_Throws_Exception_Disconnected_Component()
        {
            // Arrange
            CircuitBuilder circuitBuilder = new CircuitBuilder();
            var path = @"..\..\..\Circuits\Circuit5_NotConnected.txt";

            // Act
            circuitBuilder.PrepareCircuit(path);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "File contains inifinte loop")]
        public void Builder_Throws_Exception_On_Infinte_Loop()
        {
            // Arrange
            CircuitBuilder circuitBuilder = new CircuitBuilder();
            var path = @"..\..\..\Circuits\Circuit4_InfiniteLoop.txt";

            // Act
            circuitBuilder.PrepareCircuit(path);

            // Assert
        }

       
    }
}
