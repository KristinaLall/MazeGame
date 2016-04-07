using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace KristinaLall_Assignment4_Graphics
{
    public partial class MazeGame : Form
    {
        //Create the maze and the player.
        Maze maze; 
        Player player;

        /// <summary>
        /// MazeGame Constructor
        /// </summary>
        public MazeGame()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Load Event for Maze
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Maze_Load(object sender, EventArgs e)
        {
            StartGame();
        }

        /// <summary>
        /// Starts a new game.
        /// </summary>
        private void StartGame()
        {   
            //Display all objects.        
            maze = new Maze(this.DisplayRectangle);
            maze.GenerateEndPoint();
            player = new Player(this.DisplayRectangle, maze.startCell);
            this.Refresh(); //reference: http://stackoverflow.com/questions/2376998/force-form-to-redraw
        }

        /// <summary>
        /// Paints the maze and player.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MazeGame_Paint(object sender, PaintEventArgs e)
        {
            maze.Draw(e.Graphics);
            maze.DrawEnd(e.Graphics);
            player.Draw(e.Graphics);
        }

        /// <summary>
        /// Depending on Keystrokes, Moves the player the in desired position.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MazeGame_KeyDown(object sender, KeyEventArgs e)
        {
            if (!CheckIfWin())
            {
                switch (e.KeyCode)
                {
                    case Keys.Right:
                        {
                            //Moves RIGHT
                            player.Move(Player.Direction.Right);
                            break;
                        }
                    case Keys.Left:
                        {
                            //Moves LEFT
                            player.Move(Player.Direction.Left);
                            break;
                        }
                    case Keys.Up:
                        {
                            //Moves UP
                            player.Move(Player.Direction.Up);
                            break;
                        }
                    case Keys.Down:
                        {
                            //Moves DOWN
                            player.Move(Player.Direction.Down);
                            break;
                        }
                }

                Invalidate();
            }

            //Check if there was a win to display the win message.
            if (CheckIfWin())
            {
                label_WinMessage.Visible = true;
                new Task(HideWinMessage).Start();  //to prevent cross-threading exceptions.            
            } 
        }

        /// <summary>
        /// Hides the win message and starts a new game after 5 seconds.
        /// </summary>
        public void HideWinMessage()
        {
            Thread.Sleep(5000); //5 second wait.
            
            //Reference: http://stackoverflow.com/questions/142003/cross-thread-operation-not-valid-control-accessed-from-a-thread-other-than-the
            if (label_WinMessage.InvokeRequired)
            {
                label_WinMessage.Invoke(new MethodInvoker(
                    delegate
                    {
                        StartGame(); //Start new game
                        label_WinMessage.Visible = false;  //sets the win message to not be visible.                    
                    }));
            }         
        }

        /// <summary>
        /// Method that returns a boolean if the game was won or not.
        /// </summary>
        /// <returns></returns>
        public bool CheckIfWin()
        {
            return (player.innerRectangle.X == maze.endCell.innerRectangle.X && player.innerRectangle.Y == maze.endCell.innerRectangle.Y) ;      
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
