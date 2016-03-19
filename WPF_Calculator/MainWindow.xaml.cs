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
using System.Windows.Navigation;
using System.Windows.Shapes;

// Debub
using System.Diagnostics;

namespace WPF_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Value displayed on right of calculator display, used to display the most recent value/operator entered and output of the calculation.
        String primaryDisplay = "0";
        String secondaryDisplay = "";
        BinaryExpressionTree expTree = null;
        bool justEvaluated = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void reset()
        {
            primaryDisplay = "0";
            secondaryDisplay = "";
            updateGUI();
        }

        private void Numerical_Button_Click(object sender, RoutedEventArgs e)
        {
            String temp = ((Button)sender).Name;
            Debug.WriteLine(temp + " Clicked");
            temp = temp.Substring(temp.Length-1);
            if (primaryDisplay == "0")
            {
                primaryDisplay = "";
            }
            primaryDisplay += temp;
            updateGUI();
        }

        private void Calculator_Function_Button_Click(object sender, RoutedEventArgs e) {
            Button temp = (Button)sender;
            switch (temp.Name)
            {
                case "AllClear":
                    reset();
                    break;
                case "Point":
                    primaryDisplay += ".";
                    break;
                case "Equals":
                    secondaryDisplay += primaryDisplay;
                    expTree = new BinaryExpressionTree(secondaryDisplay.Trim());
                    primaryDisplay = expTree.evaluateTree() + "";
                    justEvaluated = true;
                    break;
                case "LeftBrace":
                    if (primaryDisplay == "0")
                    {
                        primaryDisplay = "";
                    }
                    primaryDisplay += " ( ";
                    break;
                case "RightBrace":
                    primaryDisplay += " ) ";
                    break;
                default:
                    Debug.WriteLine("Non Calculator Function Case reached in Calculator_Function_Click");
                    break;
            }
            updateGUI();
        }

        private void Operator_Button_Click(object sender, RoutedEventArgs e)
        {
            if (justEvaluated)
            {
                justEvaluated = false;
                secondaryDisplay = "";
            }
            Button temp = (Button)sender;
            switch (temp.Name)
            {
                case "Add":
                    secondaryDisplay +=  primaryDisplay + " + ";
                    break;
                case "Subtract":
                    secondaryDisplay += primaryDisplay + " - ";
                    break;
                case "Multiply":
                    secondaryDisplay += primaryDisplay + " * ";
                    break;
                case "Divide":
                    secondaryDisplay += primaryDisplay + " / ";
                    break;
                case "Power":
                    secondaryDisplay += primaryDisplay + " ^ ";
                    break;
                default:
                    throw new Exception("Non Operator Case reached in Operator_Click." + "Operator: " + temp.Name);
            }
            primaryDisplay = "";
            updateGUI();
        }

        private void updateGUI()
        {
            SecondaryDisplay.Text = secondaryDisplay;
            Display.Text = primaryDisplay;
        }

    }
}
