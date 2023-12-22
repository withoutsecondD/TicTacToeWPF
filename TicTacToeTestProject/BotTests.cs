using System.Windows.Controls;
namespace TestProject1; 
using Tic_Tac_Toe;

[TestFixture]
public class BotTests {
    [SetUp]
    public void Setup() {
    }
    
    [Test]
    [Apartment(ApartmentState.STA)]
    public void BotSecondDifficulty_ShouldBlockPlayerWinningCombination() {
        // ARRANGE

        MainWindow mainWindow = new MainWindow();
        Button button = new Button();
        
        char[][] initialBoard = new char[3][];
        initialBoard[0] = new char[] { 'X', ' ', ' ' };
        initialBoard[1] = new char[] { ' ', 'X', ' ' };
        initialBoard[2] = new char[] { ' ', ' ', ' ' };
        
        char[][] testBoard = new char[3][];
        testBoard[0] = new char[] { 'X', ' ', ' ' };
        testBoard[1] = new char[] { ' ', 'X', ' ' };
        testBoard[2] = new char[] { ' ', ' ', 'O' };

        mainWindow.Board = initialBoard;
        
        // ACT

        mainWindow.BotSecondDifficultyMove();
        
        // ASSERT

        Assert.AreEqual(testBoard, mainWindow.Board);
        
    }
    
    [Test]
    [Apartment(ApartmentState.STA)]

    public void BotThirdDifficulty_ShouldMakeWinningMove() {
        // ARRANGE

        MainWindow mainWindow = new MainWindow();
        Button button = new Button();
        
        char[][] initialBoard = new char[3][];
        initialBoard[0] = new char[] { 'O', ' ', ' ' };
        initialBoard[1] = new char[] { 'O', ' ', ' ' };
        initialBoard[2] = new char[] { ' ', ' ', ' ' };
        
        char[][] testBoard = new char[3][];
        testBoard[0] = new char[] { 'O', ' ', ' ' };
        testBoard[1] = new char[] { 'O', ' ', ' ' };
        testBoard[2] = new char[] { 'O', ' ', ' ' };

        mainWindow.Board = initialBoard;
        
        // ACT

        mainWindow.BotThirdDifficultyMove();
        
        // ASSERT

        Assert.AreEqual(testBoard, mainWindow.Board);
        
    }

    [Test]
    [Apartment(ApartmentState.STA)]
    public void BotFourthDifficultyMove_ShouldOccupyCenter() {
        // ARRANGE

        MainWindow mainWindow = new MainWindow();
        Button button = new Button();
        
        char[][] initialBoard = new char[3][];
        initialBoard[0] = new char[] { ' ', ' ', ' ' };
        initialBoard[1] = new char[] { ' ', ' ', ' ' };
        initialBoard[2] = new char[] { ' ', ' ', ' ' };
        
        char[][] testBoard = new char[3][];
        testBoard[0] = new char[] { ' ', ' ', ' ' };
        testBoard[1] = new char[] { ' ', 'O', ' ' };
        testBoard[2] = new char[] { ' ', ' ', ' ' };

        mainWindow.Board = initialBoard;
        
        // ACT

        mainWindow.BotFourthDifficultyMove();
        
        // ASSERT

        Assert.AreEqual(testBoard, mainWindow.Board);
    }
    
    [Test]
    [Apartment(ApartmentState.STA)]
    public void BotFourthDifficultyMove_ShouldOccupyCorners() {
        // ARRANGE

        MainWindow mainWindow = new MainWindow();
        Button button = new Button();
        
        char[][] initialBoard = new char[3][];
        initialBoard[0] = new char[] { 'A', ' ', 'D' };
        initialBoard[1] = new char[] { ' ', 'C', ' ' };
        initialBoard[2] = new char[] { 'B', ' ', ' ' };
        
        char[][] testBoard = new char[3][];
        testBoard[0] = new char[] { 'A', ' ', 'D' };
        testBoard[1] = new char[] { ' ', 'C', ' ' };
        testBoard[2] = new char[] { 'B', ' ', 'O' };

        mainWindow.Board = initialBoard;
        
        // ACT

        mainWindow.BotFourthDifficultyMove();
        
        // ASSERT

        Assert.AreEqual(testBoard, mainWindow.Board);
    }
    
}