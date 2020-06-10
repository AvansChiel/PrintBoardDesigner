using PrintBoardDesigner;
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
using System.Windows.Forms;
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
        System.Windows.Controls.Button resetButton;


   

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

        void resetCircuit(object sender, RoutedEventArgs e)
        {
            // reset circuit.
        }

        private void OnTimerEvent(object sender, EventArgs e)
        {
            PaintWindow();
        }

        private void PaintWindow()
        {
            // Create a canvas sized to fill the window
            circuitCanvas = new Canvas();
            circuitCanvas.Background = Brushes.LightSteelBlue;
            //circuitCanvas.Height = this.Height - 50;


            //take node
            //for each output do the same
            this.gates.Add(new List<CircuitComponent>());
            
            for (int i = 0; i < circuit.InputComposite.getChildren().Count; i++)
            {
                CircuitComponent node = circuit.InputComposite.getChildren()[i];
                this.gates[0].Add(node);
                recursiveTestMethod(node, 1);

            }


            this.gates.Add(probes);
            Console.WriteLine(this.gates[this.gates.Count - 1].Count);
            this.logStuff();
            
            this.addGatesToCanvas();
            this.DrawLines();
            this.addButtons();
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

                    var normalStroke = Brushes.Black;

                    if (item.Component.state == States.STATE_TRUE)
                    {
                        normalStroke = Brushes.Green;
                    }

                    line.Stroke = normalStroke;

                    line.X1 = lineStartX;
                    line.X2 = lineEndX;
                    line.Y1 = lineStartY;
                    line.Y2 = lineEndY;

                    line.MouseEnter += (s, e) =>
                    {
                        Canvas.SetZIndex(line, 99);
                        line.Stroke = Brushes.Red;
                    };

                    line.MouseLeave += (s, e) =>
                    {
                        Canvas.SetZIndex(line, 1);
                        line.Stroke = normalStroke;
                    };

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
            if(component.state == States.STATE_TRUE)
            {
                e1.Fill = Brushes.Green;
            }
            else
            {
                e1.Fill = Brushes.Black;
            }
            
            e1.Name = component.name;


            TextBlock t1 = new TextBlock();
            t1.FontSize = 14;
            t1.Text = component.name;

            if (xIterator == 0)
            {
                e1.MouseUp += (s, e) =>
                {
                    if (component.state != States.STATE_TRUE)
                    {
                        component.state = States.STATE_TRUE;
                        component.CalculateState();
                    }
                    else
                    {
                        component.state = States.STATE_FALSE;
                        component.CalculateState();
                    }
                    redrawCanvasWithExistingData();
                };
            }

            Canvas.SetTop(e1, top);
            Canvas.SetTop(t1, top + 20);

            Canvas.SetLeft(e1, left);
            Canvas.SetLeft(t1, left);

            circuitCanvas.Children.Add(e1);
            circuitCanvas.Children.Add(t1);

        }

        private void redrawCanvasWithExistingData()
        {
            circuitCanvas = new Canvas();
            circuitCanvas.Background = Brushes.LightSteelBlue;

            this.addGatesToCanvas();
            this.DrawLines();

            this.addButtons();

            this.Content = circuitCanvas;
            this.Show();
        }

        private void addButtons()
        {
            resetButton = new System.Windows.Controls.Button();
            resetButton.Content = "reset";
            resetButton.Click += resetCircuit;
            this.circuitCanvas.Children.Add(resetButton);
        }


        //private void toggleState(object sender, MouseButtonEventArgs e)
        //{
        //    //foreach(CircuitComponentDrawing comp in drawingList)
        //    //{
        //    //    if
        //    //}
        //    Console.WriteLine(sender);
        //    Console.WriteLine(e);
        //}


       


    }
}
