using PrintBoardDesigner;
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

        public CircuitWindow(MainController mainController)
        {
            InitializeComponent();
            this.mainController = mainController;
            this.circuit = mainController.Circuit;
            this.gates = new List<List<CircuitComponent>>();
            PaintWindow();
        }
        private void PaintWindow()
        {
            // Create a canvas sized to fill the window
            circuitCanvas = new Canvas();
            circuitCanvas.Background = Brushes.LightSteelBlue;


            //take node
            //for each output do the same
            this.gates[0] = new List<CircuitComponent>();
            
            for (int i = 0; i < circuit.InputNodes.Count; i++)
            {
                CircuitComponent node = circuit.InputNodes[i];
                this.gates[0].Add(node);

                recursiveTestMethod(node, 1);

            }

            this.Content = circuitCanvas;
            this.Show();
        }

        private void recursiveTestMethod(CircuitComponent circuitComponent, int passedThroughTimes) 
        {
            this.gates[passedThroughTimes] = new List<CircuitComponent>();
            foreach(CircuitComponent output in circuitComponent.outputs)
            {
                this.gates[passedThroughTimes].Add(output);
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

        private void CreateNode(CircuitComponent component, int iterator)
        {
            int top = 100 + (50 * (iterator + 1));

            Ellipse e1 = new Ellipse();
            e1.Height = e1.Width = CircleRadius;
            e1.ToolTip = component.name;
            e1.Fill = Brushes.Yellow;

            TextBlock t1 = new TextBlock();
            t1.FontSize = 14;
            t1.Text = component.name;

            Canvas.SetTop(e1, top);
            Canvas.SetTop(t1, top + 20);

            Canvas.SetLeft(e1, 40);
            Canvas.SetLeft(t1, 45);

            circuitCanvas.Children.Add(e1);
            circuitCanvas.Children.Add(t1);

            ////
            //bool isGate = true;
            //List<CircuitComponent> outputs = component.outputs;
            //while (isGate)
            //{
            //    foreach (var comp in outputs)
            //    {
            //        outputs = comp.outputs
            //    }
            //}
        }

        private void CreateGate(CircuitComponent component, int iterator)
        {
            int top = (50 * (iterator + 1));

            Ellipse e1 = new Ellipse();
            e1.Height = e1.Width = CircleRadius;
            e1.ToolTip = component.name;
            e1.Fill = Brushes.Black;

            TextBlock t1 = new TextBlock();
            t1.FontSize = 14;
            t1.Text = component.name;

            Canvas.SetTop(e1, top);
            Canvas.SetTop(t1, top + 20);

            Canvas.SetLeft(e1, 40 * (iterator + 1));
            Canvas.SetLeft(t1, 45 * (iterator + 1));

            circuitCanvas.Children.Add(e1);
            circuitCanvas.Children.Add(t1);
        }
    }
}
