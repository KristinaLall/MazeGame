using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KristinaLall_Assignment4_Graphics
{
    /// <summary>
    /// Class for the maze player
    /// </summary>
    public class Player
    {
        const int size = 15; //Player size compared to maze.
        public Rectangle innerRectangle;
        private Rectangle gameplayArea;
        private Cell currentPosition;
        private Image playerImage;

        public enum Direction {  Up, Down, Left, Right} //Move Directions

        public int xPosition = 0;
        public int yPosition = 0;

        /// <summary>
        /// Player Constructor which initializes the inner rectangle and player image.
        /// </summary>
        /// <param name="gameplayArea"></param>
        /// <param name="startingPosition"></param>
        public Player(Rectangle gameplayArea, Cell startingPosition)
        {
            this.gameplayArea = gameplayArea;
            currentPosition = startingPosition;

            int width = gameplayArea.Width / size;
            int height = gameplayArea.Height / size;

            //Initializes player rectangle.
            innerRectangle.Height = height;
            innerRectangle.Width = width;
            innerRectangle.X = xPosition;
            innerRectangle.Y = yPosition;

            //Create Image
            Image newImage = Image.FromFile("rabbit.png");
            playerImage = ScaleImage(newImage, height, width);
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
        /// Draws the Player Image.
        /// </summary>
        /// <param name="graphics"></param>
        public void Draw(Graphics graphics)
        { 
            graphics.DrawImage(playerImage, new Point(innerRectangle.X, innerRectangle.Y));
        }

       /// <summary>
       /// Method that will set the position of the player depending
       /// on the desired direction.
       /// </summary>
       /// <param name="direction"></param>
        public void Move(Direction direction)
        {
            Cell nextMove = null; //Create a new cell to store the move.

            //Sets the position according to direction.
            switch (direction)
            {              
                case Direction.Left:
                    {
                        if(currentPosition.leftCell != null)
                        {                         
                            innerRectangle.X -= innerRectangle.Width;
                            nextMove = currentPosition.leftCell; //set the nextcell
                        }                       
                        break;
                    }
                case Direction.Right:
                    {
                        if(currentPosition.rightCell != null)
                        {     
                            innerRectangle.X += innerRectangle.Width;
                            nextMove = currentPosition.rightCell;  //set the nextcell
                        }                       
                        break;
                    }
                case Direction.Up:
                    {
                        if(currentPosition.upCell != null)
                        {                           
                            innerRectangle.Y -= innerRectangle.Height;
                            nextMove = currentPosition.upCell;  //set the nextcell
                        }
                        break;
                    }
                case Direction.Down:
                    {
                        if(currentPosition.downCell != null)
                        {                          
                            innerRectangle.Y += innerRectangle.Height;
                            nextMove = currentPosition.downCell;  //set the nextcell
                        }                        
                        break;
                    }              
            }

            //Checks if the move was null in order to reset the isVisited property.
            if(nextMove != null)
            {
                currentPosition.isVisited = !nextMove.isVisited;
                currentPosition = nextMove;
            }                
        }
    }
      
}
