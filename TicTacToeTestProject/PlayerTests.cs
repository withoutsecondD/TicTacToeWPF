using System.Diagnostics;
using NUnit.Framework;
using System.Windows;
using System.Windows.Controls;
using Tic_Tac_Toe;

namespace TestProject1;

[TestFixture]
public class PlayerTests {
    [SetUp]
    public void Setup() {
    }

    [Test]
    [Apartment(ApartmentState.STA)]
    public void PlayerClickButton_ShouldChangeBoardArray() {
        // ARRANGE

        MainWindow mainWindow = new MainWindow();
        Button button = new Button();
        
        char[][] testBoard = new char[3][];
        testBoard[0] = new char[] { ' ', ' ', ' ' };
        testBoard[1] = new char[] { ' ', 'X', ' ' };
        testBoard[2] = new char[] { ' ', ' ', ' ' };

        // ACT

        mainWindow.PlayerClickButton(button, null, 1, 1, 'X');

        // ASSERT

        Assert.AreEqual(testBoard, mainWindow.Board);
    }
    
    [Test]
    [Apartment(ApartmentState.STA)]
    public void CheckWin_ShouldReturnTrueForPlayerMove() {
        // ARRANGE
        
        MainWindow mainWindow = new MainWindow();
        char[][] testBoard = new char[3][];
        testBoard[0] = new char[] { 'X', ' ', ' ' };
        testBoard[1] = new char[] { ' ', 'X', ' ' };
        testBoard[2] = new char[] { ' ', ' ', 'X' };
        
        // ACT
        
        mainWindow.Board = testBoard;
        var result1 = mainWindow.CheckWin('X');
        var result2 = mainWindow.CheckWin('O');
        
        // ASSERT
        
        Assert.Multiple(() => {
            Assert.True(result1);
            Assert.False(result2);
        });
    }

    [Test]
    [Apartment(ApartmentState.STA)]
    public void PlayerClickButton_ShouldSetGameOverToTrueWhenWin() {
        // ARRANGE

        MainWindow mainWindow = new MainWindow();
        Button button = new Button();

        // ACT

        mainWindow.PlayerClickButton(button, null, 0, 0, 'X');
        mainWindow.PlayerClickButton(button, null, 1, 1, 'X');
        mainWindow.PlayerClickButton(button, null, 2, 2, 'X');

        // ASSERT

        Assert.True(mainWindow.GameOver);
    }

    [Test]
    [Apartment(ApartmentState.STA)]
    public void PlayerClickButton_ShouldSetGameOverToTrueWhenDraw() {
        // ARRANGE

        MainWindow mainWindow = new MainWindow();
        Button button = new Button();

        // ACT
        
        mainWindow.PlayerClickButton(button, null, 0, 0, 'X');
        mainWindow.PlayerClickButton(button, null, 0, 1, 'O');
        mainWindow.PlayerClickButton(button, null, 0, 2, 'X');
        mainWindow.PlayerClickButton(button, null, 1, 0, 'X');
        mainWindow.PlayerClickButton(button, null, 1, 1, 'X');
        mainWindow.PlayerClickButton(button, null, 1, 2, 'O');
        mainWindow.PlayerClickButton(button, null, 2, 0, 'O');
        mainWindow.PlayerClickButton(button, null, 2, 1, 'X');
        mainWindow.PlayerClickButton(button, null, 2, 2, 'O');

        // ASSERT

        Assert.True(mainWindow.GameOver);
    }
}