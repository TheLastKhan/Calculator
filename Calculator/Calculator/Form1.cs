using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        // Variables required for calculation
        private double previousValue = 0;
        private double currentValue = 0;
        private string operation = "";
        private bool operationPressed = false;
        private bool equalsPressed = false;

        public Form1()
        {
            InitializeComponent();

            // Enable form keyboard events
            this.KeyPreview = true;

            // Bind event handlers
            this.KeyPress += Form1_KeyPress;
            this.KeyDown += Form1_KeyDown;

            // Form settings
            this.AcceptButton = null; // Remove default button
            this.CancelButton = null; // Remove cancel button
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "0";
            label1.Text = "";

            // Block manual typing into the TextBox (allow only programmatic input)
            textBox1.ReadOnly = true;
        }

        // Keyboard support - KeyPress event
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Number keys (0-9) - only normal keys, numpad is handled in KeyDown
            if (char.IsDigit(e.KeyChar))
            {
                NumberButton_Click(e.KeyChar.ToString());
                e.Handled = true;
            }
            // Operator keys
            else if (e.KeyChar == '+')
            {
                OperatorButton_Click("+");
                e.Handled = true;
            }
            else if (e.KeyChar == '-')
            {
                OperatorButton_Click("-");
                e.Handled = true;
            }
            else if (e.KeyChar == '*')
            {
                OperatorButton_Click("*");
                e.Handled = true;
            }
            else if (e.KeyChar == '/')
            {
                OperatorButton_Click("/");
                e.Handled = true;
            }
            // Equals key only for '=' character
            else if (e.KeyChar == '=')
            {
                button24_Click(sender, EventArgs.Empty);
                e.Handled = true;
            }
            // Decimal point (, or .)
            else if (e.KeyChar == ',' || e.KeyChar == '.')
            {
                button23_Click(sender, EventArgs.Empty);
                e.Handled = true;
            }
            // Percent (%)
            else if (e.KeyChar == '%')
            {
                button1_Click(sender, EventArgs.Empty);
                e.Handled = true;
            }
        }

        // Keyboard support - KeyDown event (for special keys)
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            bool handled = true;

            switch (e.KeyCode)
            {
                case Keys.Back:
                case Keys.Delete:
                    button4_Click(sender, EventArgs.Empty); // Backspace
                    break;

                case Keys.Escape:
                    button3_Click(sender, EventArgs.Empty); // Clear
                    break;

                case Keys.C:
                    if (!e.Control) // If not Ctrl+C
                        button3_Click(sender, EventArgs.Empty); // Clear
                    else
                        handled = false;
                    break;

                case Keys.R:
                    if (!e.Control)
                        button5_Click(sender, EventArgs.Empty); // 1/x (Reciprocal)
                    else
                        handled = false;
                    break;

                case Keys.Q:
                    if (!e.Control)
                        button6_Click(sender, EventArgs.Empty); // x² (Square)
                    else
                        handled = false;
                    break;

                case Keys.S:
                    if (!e.Control) // If not Ctrl+S
                        button7_Click(sender, EventArgs.Empty); // √x (Square root)
                    else
                        handled = false;
                    break;

                case Keys.F9:
                    button21_Click(sender, EventArgs.Empty); // Plus/Minus toggle
                    break;

                // Numpad keys - only handled here
                case Keys.NumPad0:
                    NumberButton_Click("0");
                    break;
                case Keys.NumPad1:
                    NumberButton_Click("1");
                    break;
                case Keys.NumPad2:
                    NumberButton_Click("2");
                    break;
                case Keys.NumPad3:
                    NumberButton_Click("3");
                    break;
                case Keys.NumPad4:
                    NumberButton_Click("4");
                    break;
                case Keys.NumPad5:
                    NumberButton_Click("5");
                    break;
                case Keys.NumPad6:
                    NumberButton_Click("6");
                    break;
                case Keys.NumPad7:
                    NumberButton_Click("7");
                    break;
                case Keys.NumPad8:
                    NumberButton_Click("8");
                    break;
                case Keys.NumPad9:
                    NumberButton_Click("9");
                    break;

                // Numpad operators
                case Keys.Add:
                    OperatorButton_Click("+");
                    break;
                case Keys.Subtract:
                    OperatorButton_Click("-");
                    break;
                case Keys.Multiply:
                    OperatorButton_Click("*");
                    break;
                case Keys.Divide:
                    OperatorButton_Click("/");
                    break;
                case Keys.Decimal:
                    button23_Click(sender, EventArgs.Empty);
                    break;

                case Keys.Enter:
                    // For debugging - temporary
                    System.Diagnostics.Debug.WriteLine("Enter key pressed!");
                    button24_Click(sender, EventArgs.Empty); // Equals
                    break;

                default:
                    handled = false;
                    break;
            }

            if (handled)
            {
                e.Handled = true;
                e.SuppressKeyPress = true; // Suppress KeyPress event
            }
        }

        // Alternative Enter solution - ProcessCmdKey override
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter || keyData == Keys.Return)
            {
                System.Diagnostics.Debug.WriteLine("ProcessCmdKey: Enter key pressed!");
                button24_Click(this, EventArgs.Empty);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void NumberButton_Click(string number)
        {
            if (textBox1.Text == "0" || operationPressed || equalsPressed)
            {
                textBox1.Text = number;
                operationPressed = false;
                equalsPressed = false;
            }
            else
            {
                if (textBox1.Text.Length < 15) // Maximum digit limit
                {
                    textBox1.Text += number;
                }
            }
        }

        // Number button events
        private void button17_Click(object sender, EventArgs e) // 1
        {
            NumberButton_Click("1");
        }

        private void button18_Click(object sender, EventArgs e) // 2
        {
            NumberButton_Click("2");
        }

        private void button19_Click(object sender, EventArgs e) // 3
        {
            NumberButton_Click("3");
        }

        private void button13_Click(object sender, EventArgs e) // 4
        {
            NumberButton_Click("4");
        }

        private void button14_Click(object sender, EventArgs e) // 5
        {
            NumberButton_Click("5");
        }

        private void button15_Click(object sender, EventArgs e) // 6
        {
            NumberButton_Click("6");
        }

        private void button9_Click(object sender, EventArgs e) // 7
        {
            NumberButton_Click("7");
        }

        private void button10_Click(object sender, EventArgs e) // 8
        {
            NumberButton_Click("8");
        }

        private void button11_Click(object sender, EventArgs e) // 9
        {
            NumberButton_Click("9");
        }

        private void button22_Click(object sender, EventArgs e) // 0
        {
            if (textBox1.Text != "0")
            {
                NumberButton_Click("0");
            }
        }

        // Common method for operator buttons
        private void OperatorButton_Click(string op)
        {
            try
            {
                if (!operationPressed && !string.IsNullOrEmpty(operation))
                {
                    // Complete previous operation first
                    CalculateResult();
                }

                previousValue = Convert.ToDouble(textBox1.Text);
                operation = op;
                operationPressed = true;
                equalsPressed = false;

                // Update label
                label1.Text = FormatNumber(previousValue) + " " + GetOperatorSymbol(op) + " ";
            }
            catch (Exception ex)
            {
                textBox1.Text = "Error";
                label1.Text = "";
            }
        }

        private string GetOperatorSymbol(string op)
        {
            switch (op)
            {
                case "+": return "+";
                case "-": return "−";
                case "*": return "×";
                case "/": return "÷";
                default: return op;
            }
        }

        private string FormatNumber(double number)
        {
            if (number == (long)number)
                return ((long)number).ToString();
            else
                return number.ToString();
        }

        // Operator button events
        private void button20_Click(object sender, EventArgs e) // +
        {
            OperatorButton_Click("+");
        }

        private void button16_Click(object sender, EventArgs e) // -
        {
            OperatorButton_Click("-");
        }

        private void button12_Click(object sender, EventArgs e) // *
        {
            OperatorButton_Click("*");
        }

        private void button8_Click(object sender, EventArgs e) // /
        {
            OperatorButton_Click("/");
        }

        // Equals button
        private void button24_Click(object sender, EventArgs e) // =
        {
            if (!string.IsNullOrEmpty(operation) && !operationPressed)
            {
                CalculateResult();
                label1.Text = "";
                operation = "";
                equalsPressed = true;
            }
        }

        // Calculation logic
        private void CalculateResult()
        {
            try
            {
                currentValue = Convert.ToDouble(textBox1.Text);
                double result = 0;

                switch (operation)
                {
                    case "+":
                        result = previousValue + currentValue;
                        break;
                    case "-":
                        result = previousValue - currentValue;
                        break;
                    case "*":
                        result = previousValue * currentValue;
                        break;
                    case "/":
                        if (currentValue != 0)
                            result = previousValue / currentValue;
                        else
                        {
                            textBox1.Text = "Cannot divide by zero";
                            return;
                        }
                        break;
                }

                textBox1.Text = FormatNumber(result);
                previousValue = result;
            }
            catch (Exception)
            {
                textBox1.Text = "Error";
                label1.Text = "";
            }
        }

        // Clear button (C)
        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "0";
            label1.Text = "";
            previousValue = 0;
            currentValue = 0;
            operation = "";
            operationPressed = false;
            equalsPressed = false;
        }

        // Clear Entry button (CE)
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "0";
        }

        // Backspace button
        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 1 && textBox1.Text != "Error")
            {
                textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1);
            }
            else
            {
                textBox1.Text = "0";
            }
        }

        // Decimal point button
        private void button23_Click(object sender, EventArgs e)
        {
            if (operationPressed || equalsPressed)
            {
                textBox1.Text = "0.";
                operationPressed = false;
                equalsPressed = false;
            }
            else if (!textBox1.Text.Contains("."))
            {
                textBox1.Text += ".";
            }
        }

        // Plus/Minus button
        private void button21_Click(object sender, EventArgs e)
        {
            try
            {
                double value = Convert.ToDouble(textBox1.Text);
                value *= -1;
                textBox1.Text = FormatNumber(value);
            }
            catch (Exception)
            {
                textBox1.Text = "Error";
            }
        }

        // Percent button
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                double value = Convert.ToDouble(textBox1.Text);
                value /= 100;
                textBox1.Text = FormatNumber(value);
            }
            catch (Exception)
            {
                textBox1.Text = "Error";
            }
        }

        // 1/x button
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                double value = Convert.ToDouble(textBox1.Text);
                if (value != 0)
                {
                    value = 1 / value;
                    textBox1.Text = FormatNumber(value);
                }
                else
                {
                    textBox1.Text = "Cannot divide by zero";
                }
            }
            catch (Exception)
            {
                textBox1.Text = "Error";
            }
        }

        // x² button
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                double value = Convert.ToDouble(textBox1.Text);
                value = Math.Pow(value, 2);
                textBox1.Text = FormatNumber(value);
            }
            catch (Exception)
            {
                textBox1.Text = "Error";
            }
        }

        // √x button
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                double value = Convert.ToDouble(textBox1.Text);
                if (value >= 0)
                {
                    value = Math.Sqrt(value);
                    textBox1.Text = FormatNumber(value);
                }
                else
                {
                    textBox1.Text = "Invalid input";
                }
            }
            catch (Exception)
            {
                textBox1.Text = "Error";
            }
        }
    }
}
