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
        public CircuitWindow(MainController mainController)
        {
            InitializeComponent();
            this.mainController = mainController;
            this.circuit = mainController.Circuit;
            PaintWindow();
        }
        private void PaintWindow()
        {
            // Create a canvas sized to fill the window
            circuitCanvas = new Canvas();
            circuitCanvas.Background = Brushes.LightSteelBlue;

            for (int i = 0; i < circuit.InputNodes.Count; i++)
            {
                CircuitComponent node = circuit.InputNodes[i];
                switch (node.GetType().Name.ToString())
                {
                    case "InputNode":
                        CreateInput(node, i);
                        break;
                    default:
                        break;
                }
            }

            // Add a "Hello World!" text element to the Canvas
            //TextBlock txt1 = new TextBlock();
            //txt1.FontSize = 14;
            //txt1.Text = "Hello World!";
            //Canvas.SetTop(txt1, 100);
            //Canvas.SetLeft(txt1, 10);
            //myCanvas.Children.Add(txt1);

            // Add a second text element to show how absolute positioning works in a Canvas
            //TextBlock txt2 = new TextBlock();
            //txt2.FontSize = 22;
            //txt2.Text = "Isn't absolute positioning handy?";
            //Canvas.SetTop(txt2, 200);
            //Canvas.SetLeft(txt2, 75);
            //circuitCanvas.Children.Add(txt2);

            this.Content = circuitCanvas;
            this.Show();
        }

        private void CreateInput(CircuitComponent component, int iterator)
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
        }
    }
}
