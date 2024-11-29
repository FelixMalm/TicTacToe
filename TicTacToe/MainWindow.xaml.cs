using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    public class Score
    {
        public int User { get; set; }
        public int Computer { get; set; }
    }
    public partial class MainWindow : Window
    {
        private Score score = new Score();  
        private Button[,] buttons = new Button[3, 3];

        public MainWindow()
        {
            InitializeComponent();

            buttons[0, 0] = btn1;
            buttons[0, 1] = btn2;
            buttons[0, 2] = btn3;
            buttons[1, 0] = btn4;
            buttons[1, 1] = btn5;
            buttons[1, 2] = btn6;
            buttons[2, 0] = btn7;
            buttons[2, 1] = btn8;
            buttons[2, 2] = btn9;

            ResetBoard();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton != null && clickedButton.Tag != null && (bool)clickedButton.Tag)
                return; 

            clickedButton.Background = Brushes.Blue;
            clickedButton.Tag = true; 

            if (CheckForWin(Brushes.Blue))
            {
                UpdateUserScore();
                ResetBoard();
                return;
            }

            if (IsBoardFull())
            {
                MessageBox.Show("It's a draw!");
                ResetBoard();
                return;
            }

            ComputerMove();
        }

        private void ComputerMove()
        {
            Random random = new Random();
            Button computerButton = null;

            while (computerButton == null)
            {
                int row = random.Next(0, 3);
                int col = random.Next(0, 3);
                computerButton = buttons[row, col];

                if (computerButton.Background == Brushes.Blue || computerButton.Background == Brushes.Red)
                {
                    computerButton = null;
                }
            }

            computerButton.Background = Brushes.Red;
            computerButton.Tag = true; 

            if (CheckForWin(Brushes.Red))
            {
                UpdateComputerScore();
                ResetBoard();
            }
        }

        private bool CheckForWin(Brush color)
        {
            for (int i = 0; i < 3; i++)
            {
                if (buttons[i, 0].Background == color && buttons[i, 1].Background == color && buttons[i, 2].Background == color)
                    return true;
                if (buttons[0, i].Background == color && buttons[1, i].Background == color && buttons[2, i].Background == color)
                    return true;
            }
            if (buttons[0, 0].Background == color && buttons[1, 1].Background == color && buttons[2, 2].Background == color)
                return true;
            if (buttons[0, 2].Background == color && buttons[1, 1].Background == color && buttons[2, 0].Background == color)
                return true;

            return false;
        }

        private bool IsBoardFull()
        {
            foreach (var button in buttons)
            {
                if (button.Background == Brushes.White)
                    return false;
            }
            return true;
        }

        private void UpdateUserScore()
        {
            score.User += 1;
            UserScore.Text = $"User: {score.User}";
            MessageBox.Show("User wins!");
        }

        private void UpdateComputerScore()
        {
            score.Computer += 1;
            ComputerScore.Text = $"Computer: {score.Computer}";
            MessageBox.Show("Computer wins!");
        }

        private void ResetBoard()
        {
            foreach (var button in buttons)
            {
                button.Background = Brushes.White;
                button.IsEnabled = true;
                button.Tag = null;
            }
        }
    }
}
