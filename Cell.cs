using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KristinaLall_Assignment4_Graphics
{
    /// <summary>
    /// Class for each individual Cell in the maze.
    /// </summary>
    public class Cell
    {
        //Represents the neighbouring cells of each cell.
        public Cell upCell = null;
        public Cell downCell = null;
        public Cell leftCell = null;
        public Cell rightCell = null;

        //Start Cell Position 
        //Used for the Randomized Maze
        public int row = 0;
        public int col = 0;

        public bool isVisited = false;

        public int xPosition;
        public int yPosition;

        //Rectangles for the Maze Walls.
        Rectangle up;
        Rectangle down;
        Rectangle left;
        Rectangle right;

        public Rectangle innerRectangle;
        public Rectangle gameplayArea;

        /// <summary>
        /// Cell Constructor
        /// </summary>
        /// <param name="gameplayArea">game dimensions from form screen</param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="xPosition"></param>
        /// <param name="yPosition"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public Cell(Rectangle gameplayArea, int width, int height, int xPosition, int yPosition, int row, int col)
        {
            this.gameplayArea = gameplayArea;

            this.xPosition = xPosition;
            this.yPosition = yPosition;

            //For Randomized Maze
            this.row = row;
            this.col = col;

            //Setting the Player innerRectangle
            innerRectangle = InitializeRectangles(innerRectangle, width, height, xPosition, yPosition);

            //Initializing the four Wall rectangles.
            up = InitializeRectangles(up, width, 1, xPosition, yPosition);
            down = InitializeRectangles(down, width, 1, xPosition, yPosition + height);
            left = InitializeRectangles(left, 1, height, xPosition, yPosition);
            right = InitializeRectangles(right, 1, height, xPosition + width, yPosition);
        }

        /// <summary>
        /// Initializes the width, height, and positions of the rectangles.
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="xPosition"></param>
        /// <param name="yPosition"></param>
        /// <returns></returns>
        public Rectangle InitializeRectangles(Rectangle rectangle, int width, int height, int xPosition, int yPosition)
        {
            rectangle.Height = height;
            rectangle.Width = width;
            rectangle.X = xPosition;
            rectangle.Y = yPosition;

            return rectangle;
        }

        /// <summary>
        /// Draws a cell with its walls.
        /// </summary>
        /// <param name="graphics"></param>
        public void Draw(Graphics graphics)
        {
            SolidBrush brush = new SolidBrush(Color.White);
            SolidBrush pathBrush = new SolidBrush(Color.SaddleBrown);

            //Check if the Wall is null before drawing it.
            if (upCell == null)
                graphics.FillRectangle(brush, up);
            if (downCell == null)
                graphics.FillRectangle(brush, down);
            if (leftCell == null)
                graphics.FillRectangle(brush, left);
            if (rightCell == null)
                graphics.FillRectangle(brush, right);

            //Draws the breadcrumb trail if the cell has been visited.
            if(isVisited)
                 graphics.FillEllipse(pathBrush, CreatePathRectangle(innerRectangle.X, innerRectangle.Y)); 
        }

        //Creates the breadcrumb trail path.
        public Rectangle CreatePathRectangle(int x, int y)
        {
            Rectangle pathRectangle = new Rectangle();

            //Initializes all of the path rectangles
            int pathWidth = innerRectangle.Width;
            int pathHeigth = innerRectangle.Height;

            pathRectangle.Height = pathHeigth /5;
            pathRectangle.Width = pathWidth /5;

            //Sets the path position.
            pathRectangle.X = x + pathRectangle.Width *2;
            pathRectangle.Y = y + pathRectangle.Height*2;

            return pathRectangle;
        }

    }
}
