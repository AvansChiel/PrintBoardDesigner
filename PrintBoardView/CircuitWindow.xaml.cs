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
        List<CircuitComponent> probes;

        public CircuitWindow(MainController mainController)
        {
            InitializeComponent();
            this.mainController = mainController;
            this.circuit = mainController.Circuit;
            this.gates = new List<List<CircuitComponent>>();
            this.probes = new List<CircuitComponent>();
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

        private void CreateNode(CircuitComponent component, int xIterator, int yIterator)
        {

            int left = (80 * (xIterator + 1));
            int top = (80 * (yIterator + 1));

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
