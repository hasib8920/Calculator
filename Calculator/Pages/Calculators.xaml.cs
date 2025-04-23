using System;
using Microsoft.Maui.Controls;

namespace Calculator.Pages
{
    public partial class Calculators : ContentPage
    {
        private string currentInput = "0";
        private double firstNumber = 0;
        private string currentOperator = "";
        private bool isNewInput = true;

        public Calculators()
        {
            InitializeComponent(); // Critical for XAML connectivity
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var buttonText = button.Text;

            switch (buttonText)
            {
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    HandleNumberInput(buttonText);
                    break;

                case ".":
                    if (!currentInput.Contains("."))
                        currentInput += ".";
                    break;

                case "+":
                case "-":
                case "×":
                case "÷":
                    if (!string.IsNullOrEmpty(currentOperator))
                        CalculateResult();
                    firstNumber = double.Parse(currentInput);
                    currentOperator = buttonText;
                    isNewInput = true;
                    break;

                case "=":
                    CalculateResult();
                    break;

                case "C":
                    currentInput = "0";
                    firstNumber = 0;
                    currentOperator = "";
                    isNewInput = true;
                    break;

                case "+/-":
                    currentInput = currentInput.StartsWith("-") ?
                        currentInput.Substring(1) : "-" + currentInput;
                    break;

                case "%":
                    currentInput = (double.Parse(currentInput) / 100).ToString();
                    break;
            }

            lblDisplay.Text = currentInput;
        }

        private void HandleNumberInput(string number)
        {
            if (currentInput == "0" || isNewInput)
            {
                currentInput = number;
                isNewInput = false;
            }
            else
            {
                currentInput += number;
            }
        }

        private void CalculateResult()
        {
            if (string.IsNullOrEmpty(currentOperator)) return;

            var secondNumber = double.Parse(currentInput);

            currentInput = currentOperator switch
            {
                "+" => (firstNumber + secondNumber).ToString(),
                "-" => (firstNumber - secondNumber).ToString(),
                "×" => (firstNumber * secondNumber).ToString(),
                "÷" => (firstNumber / secondNumber).ToString(),
                _ => currentInput
            };

            currentOperator = "";
            isNewInput = true;
        }

        private void ContentPage_Loaded(object sender, EventArgs e)
        {
            // Initialization if needed
        }
    }
}