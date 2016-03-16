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
        // Value displayed on right of calculator display, used to display the most recent value/operator entered and oputput of the calculation.
        String inVal = "0";
        // To be display above and to the left of the input values/result
        String expressionString = "";

        // TODO
        // CalculatorTree expression = new CalculatorTree();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void digit(String input)
        {
            if (inVal == "0")
            {
                inVal = "";
            }
            inVal += input;
            Display.Text = inVal;
        }

        private void reset(int input)
        {
            inVal = "0";
            Display.Text = inVal;
        }

        private void Numerical_Button_Click(object sender, RoutedEventArgs e)
        {
            String temp = ((Button)sender).Name;
            Debug.WriteLine(temp + " Clicked");
            temp = temp.Substring(temp.Length-1);
            digit(temp);
        }

        private void Calculator_Function_Button_Click(object sender, RoutedEventArgs e) {
            Button temp = (Button)sender;
            switch (temp.Name)
            {
                case "AllClear":
                    inVal = "0";
                    Display.Text = inVal;
                    break;
                case "Point":
                    inVal += ".";
                    Display.Text = inVal;
                    break;
                default:
                    Debug.WriteLine("Non Calculator Function Case reached in Calculator_Function_Click");
                    break;
            }
        }

        private void Operator_Button_Click(object sender, RoutedEventArgs e)
        {
            Button temp = (Button)sender;
            switch (temp.Name)
            {
                case "AllClear":
                    inVal = "0";
                    Display.Text = inVal;
                    break;
                default:
                    Debug.WriteLine("Non Operator Case reached in Operator_Click");
                    break;
            }
        }

    }
}
