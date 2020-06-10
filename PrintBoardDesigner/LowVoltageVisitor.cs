﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintBoardDesigner
{
    public class LowVoltageVisitor : IVisitor
    {
        public void Visit(CircuitComponent element)
        {
            element.state = States.STATE_FALSE;
        }
    }
}