﻿using PrintBoardDesigner;
using PrintBoardView.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PrintBoardView
{
    /// <summary>
    /// Interaction logic for CircuitView.xaml
    /// </summary>
    public partial class CircuitWindow : Window
    {
        private static int CircleRadius = 20;

        MainController mainController;
        Circuit circuit;
        Canvas circuitCanvas;

        List<List<CircuitComponent>> gates;
        List<CircuitComponent> probes;
        List<CircuitComponentDrawing> drawingList;

   

        public CircuitWindow(MainController mainController)
        {
            InitializeComponent();
            this.mainController = mainController;
            this.circuit = mainController.Circuit;
            this.gates = new List<List<CircuitComponent>>();
            this.probes = new List<CircuitComponent>();
            this.drawingList = new List<CircuitComponentDrawing>();
            PaintWindow();
        }
        private void PaintWindow()
        {
            // Create a canvas sized to fill the window
            circuitCanvas = new Canvas();
            circuitCanvas.Background = Brushes.LightSteelBlue;


            //take node
            //for each output do the same
            this.gates.Add(new List<CircuitComponent>());
            
            for (int i = 0; i < circuit.InputNodes.Count; i++)
            {
                CircuitComponent node = circuit.InputNodes[i];
                this.gates[0].Add(node);
                recursiveTestMethod(node, 1);

            }


            this.gates.Add(probes);
            Console.WriteLine(this.gates[this.gates.Count - 1].Count);
            this.logStuff();
            
            this.addGatesToCanvas();
            this.DrawLines();
            this.Content = circuitCanvas;
            this.Show();
        }

        private void logStuff()
        {

            for (int i = 0; i < this.gates.Count; i++)
            {
                for(int j = 0; j < this.gates[i].Count; j++)
                {
                    Console.WriteLine(this.gates[i][j].name);
                }
            }
        }

        private void recursiveTestMethod(CircuitComponent circuitComponent, int passedThroughTimes) 
        {
            foreach (CircuitComponent output in circuitComponent.outputs)
            {
                if(output.outputs.Count == 0)
                {
                    if (!this.probes.Contains(output))
                    {
                        this.probes.Add(output);
                        return;
                    }
                }
                else
                {
                    if (!this.isPresent(output))
                    {
                         while(this.gates.Count <= passedThroughTimes)
                         {
                            this.gates.Add(new List<CircuitComponent>());
                         }

                         this.gates[passedThroughTimes].Add(output);
                    }

                }
                this.recursiveTestMethod(output, passedThroughTimes + 1);
            }

        }

        private bool isPresent(CircuitComponent component)
        {
            foreach (var gatesList in gates)
            {
                if(gatesList.Contains(component))
                {
                    return true;
                }
            }
            return false;
        }

        private void addGatesToCanvas()
        {
            for(int i = 0; i < this.gates.Count; i++)
            {
                for(int j = 0; j < this.gates[i].Count; j++)
                {
                    CreateNode(this.gates[i][j], i, j);
                    
                }
            }
        }

        private void DrawLines()
        {
           
            foreach (var item in this.drawingList)
            {
                
                foreach(var output in item.Component.outputs)
                {

                    int lineStartX = item.X + 10;
                    int lineStartY = item.Y + 10;

                    int lineEndX = 0;
                    int lineEndY = 0;
                    foreach (var drawing in this.drawingList)
                    {
                        if(output == drawing.Component)
                        {
                            lineEndX = drawing.X + 10;
                            lineEndY = drawing.Y + 10;
                            break;
                        }
                    }
                    Line line = new Line();

                    line.Stroke = Brushes.Black;

                    line.X1 = lineStartX;
                    line.X2 = lineEndX;
                    line.Y1 = lineStartY;
                    line.Y2 = lineEndY;

                    line.MouseEnter += (s, e) => line.Stroke = Brushes.Red;
                    line.MouseLeave += (s, e) => line.Stroke = Brushes.Black;

                    line.StrokeThickness = 4;
                    circuitCanvas.Children.Add(line);
                    //draw line between coords
                }

            }
        }

        private void CreateNode(CircuitComponent component, int xIterator, int yIterator)
        {

            int left = (80 * (xIterator + 1));
            int top = (80 * (yIterator + 1));

            CircuitComponentDrawing cDrawing = new CircuitComponentDrawing(component, left, top);
            drawingList.Add(cDrawing);

            //save in object
            //add object to array

            Ellipse e1 = new Ellipse();
            e1.Height = e1.Width = CircleRadius;
            e1.ToolTip = component.name;
            e1.Fill = Brushes.Black;

            TextBlock t1 = new TextBlock();
            t1.FontSize = 14;
            t1.Text = component.name;

            Canvas.SetTop(e1, top);
            Canvas.SetTop(t1, top + 20);

            Canvas.SetLeft(e1, left);
            Canvas.SetLeft(t1, left);

            circuitCanvas.Children.Add(e1);
            circuitCanvas.Children.Add(t1);

        }


       


    }
}
