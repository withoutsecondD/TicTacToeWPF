using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
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
using System.Windows.Threading;

namespace Tic_Tac_Toe
{
    public partial class MainWindow : Window
    {
        private Random rand = new Random();
        private List<Button> buttons;
        private char[][] board = new char[3][];
        private bool gameOver = false;
        public int Difficulty { get; set; }
        private int counter = 0;
     
        public MainWindow()
        {
            InitializeComponent();

            board = new char[3][];

            buttons = new List<Button>();

            foreach (UIElement el in ButtonsGrid.Children)
            {
                if (el is Button && el.Uid != "New Game")
                    buttons.Add((Button)el);
            }

            for (int i = 0; i < 3; i++) {
                board[i] = new char[3];

                for (int j = 0; j < 3; j++) {
                    board[i][j] = ' ';
                    int row = i;
                    int col = j;
                    buttons[i * 3 + j].Click += (sender, e) => PlayerClickButton(sender, e, row, col);
                }
            }
        }
        
        private void PlayerClickButton(object sender, RoutedEventArgs e, int row, int column)
        {
            if (gameOver)
                return;

            Button button = sender as Button;
            
            if (board[row][column] == ' ') {
                button.Content = 'X';

                button.Background = Brushes.Transparent;
                button.Foreground = Brushes.White;
                board[row][column] = 'X';

                counter++;

                if (CheckWin('X')) {
                    gameOver = true;
                    GameLabel.Text = "Player Wins!";
                    return;
                }

                if (counter == 9) {
                    gameOver = true;
                    GameLabel.Text = "Draw!";
                    return;
                }

                switch (Difficulty) {
                    case 1:
                        BotFirstDifficultyMove();
                        break;
                    case 2:
                        BotSecondDifficultyMove();
                        break;
                    case 3:
                        BotThirdDifficultyMove();
                        break;
                    case 4:
                        BotFourthDifficultyMove();
                        break;
                }

                if (CheckWin('O')) {
                    gameOver = true;
                    GameLabel.Text = "AI Wins!";
                    return;
                }
            }
        }

        public void BotFirstDifficultyMove() {
            MakeMove(rand.Next(3), rand.Next(3));
        }

        public void BotSecondDifficultyMove() {
            int[] move = GetWinningMove('X');

            if (move != null) // CHECK IF BOT CAN BLOCK PLAYER'S WINNING MOVE
                MakeMove(move[0], move[1]);
            else
                BotFirstDifficultyMove(); // MAKE RANDOM MOVE OTHERWISE
        }

        public void BotThirdDifficultyMove() {
            int[] move = GetWinningMove('O');

            if (move != null) // CHECK IF THERE IS WINNING MOVE OR NOT
                MakeMove(move[0], move[1]);
            else
                BotSecondDifficultyMove(); // MAKE LOWER DIFFICULTY MOVE OTHERWISE
        }

        public void BotFourthDifficultyMove() {
            int[] move = GetWinningMove('O');

            if (move != null) { // CHECK IF THERE IS WINNING MOVE OR NOT
                MakeMove(move[0], move[1]);
                Debug.WriteLine($"Winning move for O found: [{move[0]}, {move[1]}]");
                return;
            }
            else {
                Debug.WriteLine("Winning move for O is not found");
            }

            move = GetWinningMove('X');

            if (move != null) { // CHECK IF THERE IS WINNING MOVE OF PLAYER TO BLOCK
                MakeMove(move[0], move[1]);
                return;
            }

            if (board[1][1] == ' ') { // MAKE A MOVE IN THE MIDDLE IF IT'S NOT TAKEN
                MakeMove(1, 1);
                return;
            }

            List<int> cornerIndexes = Enumerable.Range(0, 4).ToList();
            Shuffle(cornerIndexes);

            bool moveMade = false;

            foreach (int i in cornerIndexes) {
                if (moveMade)
                    return;

                switch (i) {
                    case 0:
                        if (board[0][0] == ' ') {
                            MakeMove(0, 0);
                            moveMade = true;
                        }
                        break;
                    case 1:
                        if (board[0][2] == ' ') {
                            MakeMove(0, 2);
                            moveMade = true;
                        }
                        break;
                    case 2:
                        if (board[2][0] == ' ') {
                            MakeMove(2, 0);
                            moveMade = true;
                        }
                        break;
                    case 3:
                        if (board[2][2] == ' ') {
                            MakeMove(2, 2);
                            moveMade = true;
                        }
                        break;
                }
            }

            BotThirdDifficultyMove(); // MAKE LOWER DIFFICULTY MOVE OTHERWISE

            Debug.WriteLine("End of move");
        }

        public void MakeMove(int row, int column) {
            if (board[row][column] != ' ') {
                switch (Difficulty) {
                    case 1:
                        BotFirstDifficultyMove();
                        break;
                    case 2:
                        BotSecondDifficultyMove();
                        break;
                    case 3:
                        BotThirdDifficultyMove();
                        break;
                    case 4:
                        BotFourthDifficultyMove();
                        break;
                }
            }
            else {
                GetButton(row, column).Content = "O";
                GetButton(row, column).Background = Brushes.Transparent;
                GetButton(row, column).Foreground = Brushes.Red;
                board[row][column] = 'O';
                counter++;
            }
        }

        public int[] GetWinningMove(char player) {
            // CHECK FOR ROWS 

            for (int i = 0; i < board.Length; i++) {
                if (board[i][0] == player && board[i][1] == player) {
                    if (board[i][2] == ' ')
                        return new int[2] { i, 2 };
                }

                if (board[i][1] == player && board[i][2] == player) {
                    if (board[i][0] == ' ')
                        return new int[2] { i, 0 };
                }

                if (board[i][0] == player && board[i][2] == player) {
                    if (board[i][1] == ' ')
                        return new int[2] { i, 1 };
                }
            }

            // CHECK FOR COLUMNS

            for (int i = 0; i < board.Length; i++) {
                if (board[0][i] == player && board[1][i] == player) {
                    if (board[2][i] == ' ')
                        return new int[2] { 2, i };
                }

                if (board[1][i] == player && board[2][i] == player) {
                    if (board[0][i] == ' ')
                        return new int[2] { 0, i };
                }

                if (board[0][i] == player && board[2][i] == player) {
                    if (board[1][i] == ' ')
                        return new int[2] { 1, i };
                }
            }

            // CHECK FOR DIAGONALS

            if (board[0][0] == player) {
                if (board[1][1] == player) {
                    if (board[2][2] == ' ')
                        return new int[2] { 2, 2 };
                }
                else if (board[2][2] == player) {
                    if (board[1][1] == ' ')
                        return new int[2] { 1, 1 };
                }
            }

            if (board[0][2] == player) {
                if (board[1][1] == player) {
                    if (board[2][0] == ' ')
                        return new int[2] { 2, 0 };
                }
                else if (board[2][0] == player) {
                    if (board[1][1] == ' ')
                        return new int[2] { 1, 1 };
                }
            }

            if (board[2][0] == player) {
                if (board[1][1] == player) {
                    if (board[0][2] == ' ')
                        return new int[2] { 0, 2 };
                }
                else if (board[0][2] == player) {
                    if (board[1][1] == ' ')
                        return new int[2] { 1, 1 };
                }
            }

            if (board[2][2] == player) {
                if (board[1][1] == player) {
                    if (board[0][0] == ' ')
                        return new int[2] { 0, 0 };
                }
                else if (board[0][0] == player) {
                    if (board[1][1] == ' ')
                        return new int[2] { 1, 1 };
                }
            }

            return null;
        }

        public bool CheckWin(char player) {
            // CHECK FOR ROWS AND COLUMNS

            for (int i = 0; i < board.Length; i++) {
                if (
                    (board[i][0] == player && board[i][1] == player && board[i][2] == player) ||
                    (board[0][i] == player && board[1][i] == player && board[2][i] == player)
                ) {
                    return true;
                }
            }

            // CHECK FOR DIAGONALS

            if (
                (board[0][0] == player && board[1][1] == player && board[2][2] == player) ||
                (board[0][2] == player && board[1][1] == player && board[2][0] == player)
            ) {
                return true;
            }

            return false;
        }

        public Button GetButton(int row, int col) {
            return ButtonsGrid.Children
                .OfType<Button>()
                .FirstOrDefault(b => Grid.GetRow(b) == row && Grid.GetColumn(b) == col);
        }

        private void New_Game(object sender, RoutedEventArgs e) {
            ChooseDifficulty chooseDifficulty = new ChooseDifficulty();
            chooseDifficulty.Show();
            this.Close();
        }

        public static void Shuffle(List<int> list) {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1) {
                n--;
                int k = rng.Next(n + 1);
                int value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}