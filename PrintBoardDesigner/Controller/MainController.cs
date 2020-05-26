﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintBoardDesigner
{
    public class MainController
    {
        private Circuit circuit;
        //private CircuitBuilder circuitBuilder;

        public MainController()
        {
            //this.circuitBuilder = new CircuitBuilder();
        }


        public Circuit Circuit
        {
            get { return circuit; }
            set { circuit = value; }
        }


        public bool BuildCircuit(string fileLocation)
        {
            /// Build and retrieve the Circuit using the Builder Pattern
            CircuitBuilder circuitBuilder = new CircuitBuilder();
            circuitBuilder.PrepareCircuit(fileLocation);
            Circuit circuit = circuitBuilder.GetPreparedCircuit();

            //TODO Validate circuit

            this.circuit = circuit;
            return true;
        }

        public void init() //TODO Dit moet weg. Dit is het pakketje dat wordt gebruikt door de VIEW. Zelf dus niet uitvoerbaar.
        {
            Console.WriteLine("init");
            //this.circuit = circuitBuilder.buildCircuit();
        }
    }
}