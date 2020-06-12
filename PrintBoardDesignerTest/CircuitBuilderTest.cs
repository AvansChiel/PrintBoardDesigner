using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrintBoardDesigner;
using System;
using System.Collections.Generic;

namespace PrintBoardDesignerTest
{
    [TestClass]
    public class CircuitBuilderTest
    {

        [TestMethod]
        public void Builder_Can_Build_Circuit()
        {
            // Arrange
            CircuitBuilder circuitBuilder = new CircuitBuilder();
            Dictionary<string, string> componentsStringsDict = new Dictionary<string, string>();
            componentsStringsDict.Add("A", "INPUT_HIGH");
            componentsStringsDict.Add("B", "INPUT_LOW");
            componentsStringsDict.Add("NODE1", "AND");
            componentsStringsDict.Add("Cout", "PROBE");
            Dictionary<string, string[]> connectionsStringsDict = new Dictionary<string, string[]>();
            connectionsStringsDict.Add("A", new string[] { "NODE1" });
            connectionsStringsDict.Add("B", new string[] { "NODE1" });
            connectionsStringsDict.Add("NODE1", new string[] { "Cout" });


            // Act
            circuitBuilder.PrepareCircuit(componentsStringsDict, connectionsStringsDict);
            var circuit = circuitBuilder.GetPreparedCircuit();

            // Assert
            Assert.IsInstanceOfType(circuit, typeof(Circuit));
            Assert.IsNotNull(circuit.InputComposite);
            //Default: True, False
            var inode1 = (InputNode)circuit.InputComposite.GetChildren()[0];
            Assert.AreEqual(inode1.State.GetType(), new ConcreteTrueState().GetType());

            var inode2 = (InputNode)circuit.InputComposite.GetChildren()[1];
            Assert.AreEqual(inode2.State.GetType(), new ConcreteFalseState().GetType());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "File contains disconnected component: has no inputs")]
        public void Builder_Throws_Exception_Disconnected_Component()
        {
            // Arrange
            CircuitBuilder circuitBuilder = new CircuitBuilder();
            Dictionary<string, string> componentsStringsDict = new Dictionary<string, string>();
            componentsStringsDict.Add("A", "INPUT_HIGH");
            componentsStringsDict.Add("B", "INPUT_LOW");
            componentsStringsDict.Add("NODE1", "AND");
            componentsStringsDict.Add("Cout", "PROBE");
            Dictionary<string, string[]> connectionsStringsDict = new Dictionary<string, string[]>();
            connectionsStringsDict.Add("B", new string[] { "NODE1" });
            connectionsStringsDict.Add("NODE1", new string[] { "Cout" });

            // Act
            circuitBuilder.PrepareCircuit(componentsStringsDict, connectionsStringsDict);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "File contains inifinte loop")]
        public void Builder_Throws_Exception_On_Infinte_Loop()
        {
            // Arrange
            CircuitBuilder circuitBuilder = new CircuitBuilder();
            Dictionary<string, string> componentsStringsDict = new Dictionary<string, string>();
            componentsStringsDict.Add("A", "INPUT_HIGH");
            componentsStringsDict.Add("B", "INPUT_LOW");
            componentsStringsDict.Add("NODE1", "AND");
            componentsStringsDict.Add("Cout", "PROBE");
            Dictionary<string, string[]> connectionsStringsDict = new Dictionary<string, string[]>();
            connectionsStringsDict.Add("A", new string[] { "NODE1" });
            connectionsStringsDict.Add("B", new string[] { "NODE1" });
            connectionsStringsDict.Add("NODE1", new string[] { "Cout" });
            connectionsStringsDict.Add("NODE1", new string[] { "A" });

            // Act
            circuitBuilder.PrepareCircuit(componentsStringsDict, connectionsStringsDict);

            // Assert
        }

       
    }
}
