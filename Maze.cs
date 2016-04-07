using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KristinaLall_Assignment4_Graphics
{
    /// <summary>
    /// Class that represents the application Maze.
    /// </summary>
    public class Maze
    {
        const int mazeSize = 15; //represents the desired maze size.

        public Cell startCell; 
        public Cell endCell;
        public Cell currentCell;

        private Cell[,] cellArray = new Cell[mazeSize, mazeSize]; //Cell array to store the maze. 

        private enum Direction { Up, Down, Left, Right } //Move Directions

        private Image goalImage;
        private Rectangle gameplayArea;
        private Random rand = new Random();

        /// <summary>
        /// Maze Constructor to Initialize the cells in the Maze.
        /// </summary>
        /// <param name="gameplayArea"></param>
        public Maze(Rectangle gameplayArea)
        {
            this.gameplayArea = gameplayArea;
            int cellWidth = gameplayArea.Width / cellArray.GetLength(0);
            int cellHeight = gameplayArea.Height / cellArray.GetLength(1);

            for (int i = 0; i < cellArray.GetLength(0); i++)
            {
                for (int j=0; j<cellArray.GetLength(1); j++)
                    cellArray[i, j] = new Cell(gameplayArea, cellWidth, cellHeight, i *cellWidth, j* cellHeight,j, i);
            }

            ClearAllVisited(); //Clear the isVisited property of all cells to false.
            //CreateStaticMaze(); //Creates the Maze.
            CreateMaze(); //Creates the randomly generated maze.
        }

        /// <summary>
        /// Scales the Image of the Endpoint in the maze. 
        /// Code sample taken from "BouncyBall - Complete" code from class.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="maxWidth"></param>
        /// <param name="maxHeight"></param>
        /// <returns></returns>
        public Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);

            return newImage; //returns the scaled image.
        }

        /// <summary>
        /// Creates a randomly Generated Maze.
        /// </summary>
        public void CreateMaze()
        {
            int limit = cellArray.GetLength(0);
            int row = 0;
            int col = 0;

            startCell = cellArray[row, col];
            currentCell = startCell;
            Stack<Cell> cellStack = new Stack<Cell>();
            List<Cell> neighbourCells = new List<Cell>();

            cellStack.Push(currentCell);

            PerformRecursiveBacktracking(row, col);

            ClearAllVisited();
        }

        /// <summary>
        /// Performs Recursive Backtracking to generate a random maze.
        /// Reference: http://weblog.jamisbuck.org/2011/1/27/maze-generation-growing-tree-algorithm#
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public void PerformRecursiveBacktracking(int row, int col)
        {
            Cell currentCell = cellArray[col, row];
            List <Direction> validMoves = new List<Direction>() { Direction.Up, Direction.Down, Direction.Left, Direction.Right };
            var randomDirections = validMoves.OrderBy(item => rand.Next());

            currentCell.isVisited = true;

            //Reference: http://stackoverflow.com/questions/5383498/shuffle-rearrange-randomly-a-liststring
            foreach (Direction move in randomDirections)
            {
                if (move == Direction.Up && row - 1 >= 0 && !cellArray[col, row - 1].isVisited)
                {
                    BreakWall(currentCell, cellArray[col, row - 1], Direction.Up);
                    PerformRecursiveBacktracking(row - 1, col);
                }
                else if(move == Direction.Right && col + 1 < cellArray.GetLength(0) && !cellArray[col + 1, row].isVisited)
                {
                    BreakWall(currentCell, cellArray[col + 1, row], Direction.Right);
                    PerformRecursiveBacktracking(row, col + 1);
                }
                else if (move == Direction.Down && row + 1 < cellArray.GetLength(0) && !cellArray[col, row + 1].isVisited)
                {
                    BreakWall(currentCell, cellArray[col, row + 1], Direction.Down);
                    PerformRecursiveBacktracking(row + 1, col);
                }
                else if (move == Direction.Left && col - 1 >= 0 && !cellArray[col - 1, row].isVisited)
                {
                    BreakWall(currentCell, cellArray[col - 1, row], Direction.Left);
                    PerformRecursiveBacktracking(row, col -1);
                }
            }
        }


        /// <summary>
        /// Creates a hardcoded maze.
        /// </summary>
        public void CreateStaticMaze()
        {
            startCell = cellArray[0, 0]; //Initializes the start cell of the maze.

            //Break down walls to create the maze.
            BreakWall(cellArray[0, 0], cellArray[0,1], Direction.Down);
            BreakWall(cellArray[0, 1], cellArray[1, 1], Direction.Right);
            BreakWall(cellArray[1, 1], cellArray[1,0], Direction.Up);
            BreakWall(cellArray[1,0], cellArray[2,0], Direction.Right);
            BreakWall(cellArray[2, 0], cellArray[3,0], Direction.Right);

            BreakWall(cellArray[3,0], cellArray[4,0], Direction.Right);
            BreakWall(cellArray[4,0], cellArray[4,1], Direction.Down);
            BreakWall(cellArray[4,1], cellArray[4,2], Direction.Down);
            BreakWall(cellArray[4,2], cellArray[3,2], Direction.Left);
            BreakWall(cellArray[3,0], cellArray[3,1], Direction.Down);

            BreakWall(cellArray[3,1], cellArray[2,1], Direction.Left);
            BreakWall(cellArray[2,1], cellArray[2,2], Direction.Down);
            BreakWall(cellArray[2,2], cellArray[2,3], Direction.Down);
            BreakWall(cellArray[2,3], cellArray[3,3], Direction.Right);
            BreakWall(cellArray[3,3], cellArray[4,3], Direction.Right);

            BreakWall(cellArray[4,3], cellArray[4,4], Direction.Down);
            BreakWall(cellArray[4,4], cellArray[3,4], Direction.Left);
            BreakWall(cellArray[3,4], cellArray[2,4], Direction.Left);
            BreakWall(cellArray[2,4], cellArray[1,4], Direction.Left);
            BreakWall(cellArray[1,4], cellArray[0,4], Direction.Left);

            BreakWall(cellArray[1,4], cellArray[1,3], Direction.Up);
            BreakWall(cellArray[1,3], cellArray[1,2], Direction.Up);
            BreakWall(cellArray[1,2], cellArray[0,2], Direction.Left);
            BreakWall(cellArray[0, 2], cellArray[0,3], Direction.Down);
           
        }

        /// <summary>
        /// Generates a random endpoint for the maze.
        /// </summary>
        public void GenerateEndPoint()
        {
            Random rand = new Random();

            int endRow = rand.Next(0, mazeSize);
            int endCol = rand.Next(1, mazeSize);

            endCell = cellArray[endRow, endCol];

            //Sets the Image of the endpoint.
            Image newImage = Image.FromFile("carrots.png");
            goalImage = ScaleImage(newImage, endCell.innerRectangle.Width, endCell.innerRectangle.Height);
        }
     
        /// <summary>
        /// This method breaks down the appropriate walls of the cells
        /// depending on the direction chosen by the user.
        /// </summary>
        /// <param name="currentCell"></param>
        /// <param name="nextCell"></param>
        /// <param name="direction"></param>
        private void BreakWall(Cell currentCell, Cell nextCell, Direction direction)
        {
            if (direction == Direction.Up)
            {
                currentCell.upCell = nextCell;
                nextCell.downCell = currentCell;
            }
            else if(direction ==Direction.Right)
            {
                currentCell.rightCell = nextCell;
                nextCell.leftCell = currentCell;
            }
            else if(direction ==Direction.Down)
            {
                currentCell.downCell = nextCell;
                nextCell.upCell = currentCell;
            }
            else if(direction ==Direction.Left)
            {
                currentCell.leftCell = nextCell;
                nextCell.rightCell = currentCell;
            }
        }
      
        /// <summary>
        /// Clears all the isVisited property of each cell to false.
        /// </summary>
        public void ClearAllVisited()
        {
            for (int i = 0; i < cellArray.GetLength(0); i++)
            {
                for (int j = 0; j < cellArray.GetLength(1); j++)                
                    cellArray[i, j].isVisited = false;             
            }
        }

        /// <summary>
        /// Draws the endpoint image.
        /// </summary>
        /// <param name="graphics"></param>
        public void DrawEnd(Graphics graphics)
        {
            graphics.DrawImage(goalImage, new Point(endCell.innerRectangle.X, endCell.innerRectangle.Y));
        }

        /// <summary>
        /// Loops through each cell in the array and draws them.
        /// </summary>
        /// <param name="graphics"></param>
        public void Draw(Graphics graphics)
        {
            SolidBrush brush = new SolidBrush(Color.Red);

            foreach (Cell cell in cellArray)
                cell.Draw(graphics);
        }
    }

}
