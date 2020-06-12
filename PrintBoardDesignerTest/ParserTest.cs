using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrintBoardDesigner;
using System;

namespace PrintBoardDesignerTest
{
    [TestClass]
    public class ParserTest
    {

        [TestMethod]
        public void Parser_Can_Parse_FullAdder()
        {
            // Arrange
            FileReader fr = new FileReader();
            CircuitParser parser = new CircuitParser();
            var path = @"..\..\..\Circuits\Circuit1_FullAdder.txt";
            var componentsList = fr.ReadFile(path);

            // Act
            parser.ParseFile(componentsList);

            // Assert
            Assert.IsNotNull(parser.CircuitComponentDict);
            Assert.IsNotNull(parser.CircuitConnectionDict);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid File Path")]
        public void Parser_Throws_Exception_Invalid_Path()
        {
            // Arrange
            FileReader fr = new FileReader();
            CircuitParser parser = new CircuitParser();
            var path = @"..\..\..\Circuits\NonExistent";
            var componentsList = fr.ReadFile(path);

            // Act
            parser.ParseFile(componentsList);

            // Assert
        }
       
    }
}
