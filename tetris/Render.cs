using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace tetris
{
    class Render
    {

        Objects objects;

        private static int offSet = 10;
        private static int gridSizeX = 10;
        private static int gridSizeY = 20;

        private bool[,] placedSquares = new bool[gridSizeX, gridSizeY];
        private ConsoleColor[,] placedSquaresColors = new ConsoleColor[gridSizeX, gridSizeY];



        public Render()
        {
            objects = new Objects();
            objects.setObjectSquares();
            Console.CursorVisible = false;

        }

        public void addGravityOnObjects()
        {
            int oldX = objects.xPos;
            int oldY = objects.yPos;
            objects.yPos++;
            renderFallingObject(oldX, oldY, objects.xPos, objects.yPos);


        }
        public void setWindowSize()
        {
            Console.SetWindowSize(gridSizeX * 2 + 2*offSet, gridSizeY + 2*offSet);

        }

        public bool checkForKeyPress()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                int lastX = objects.xPos;
                int lastY = objects.yPos;

                bool canMove = true;
                switch (key.KeyChar)
                {
                    case 'a':

                        for (int i = 0; i < 4; i++)
                        {
                            for (int j = 0; j < 4; j++)
                            {
                                if (objects.objectSquares[i, j] && objects.xPos + i == 0)
                                {
                                    canMove = false;                                   
                                }
                            }
                        }

                        if (canMove)
                        {
                            objects.xPos--;
                            renderFallingObject(lastX, lastY, objects.xPos, objects.yPos);
                        }

                        break;

                    case 'd':

                        for (int i = 0; i < 4; i++)
                        {
                            for (int j = 0; j < 4; j++)
                            {
                                if (objects.objectSquares[i, j] && objects.xPos + i + 1 == 10)
                                {
                                    canMove = false;
                                }
   
                            }
                        }

                        if (canMove)
                        {
                            objects.xPos++;
                            renderFallingObject(lastX, lastY, objects.xPos, objects.yPos);
                        }

                        break;

                    case 's':
                        objects.yPos++;
                        renderFallingObject(lastX, lastY, objects.xPos, objects.yPos);

                        break;

                    case 'w':

                        bool [,] oldGrid = rotateObject();
                        renderRotateObject(lastX, lastY, objects.xPos, objects.yPos, oldGrid);

                        break;


                }


                /*
                Console.SetCursorPosition(lastX, lastY);
                Console.Write("  ");
                Console.SetCursorPosition(objects.xPos, objects.yPos);
                Console.Write("[]");*/
                //renderSquares();


                return true;
            }

            return false;
    

        }

        public bool[,] rotateObject()
        {
            bool[,] oldGrid = objects.objectSquares;
            bool[,] tempGrid = new bool[4,4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (objects.objectSquares[i, j])
                    {
                        tempGrid[1 - (j - 2), i] = true;
                    }

                }
            }

            objects.objectSquares = tempGrid;
            return oldGrid;
        }

        public void checkCollision()
        {

            //check floor
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (objects.objectSquares[i, j] && objects.yPos + j == 19)
                    {
                        afterCollision(objects);
                    }

                }
            }

            //check other squares 
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
         
                    if (objects.objectSquares[i, j] && placedSquares[i + objects.xPos, j + objects.yPos + 1])
                    {
                        afterCollision(objects);
                    }
                }
            }
        }


        public void checkCompleteRow()
        {
            int[] rowNumbersToClear = new int[20];
            int rowNumbersToClearCounter = 0;

            //check which rows to clear
            for (int j = 19; j >= 0; j--)
            {
                int squaresInRow = 0;

                for (int i = 0; i < 10; i++)
                {
                    if (placedSquares[i, j])
                    {
                        squaresInRow++;
                    }
                }

                if (squaresInRow == 10)
                {
                    rowNumbersToClear[rowNumbersToClearCounter] = j;
                    rowNumbersToClearCounter++;
                }
            }

            //clear all rows
            for (int i = 0; i < rowNumbersToClearCounter + 1; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    //CLEAR ROW
                    placedSquares[j, rowNumbersToClear[i]] = false;
                    Console.SetCursorPosition(j * 2 + offSet, rowNumbersToClear[i] + offSet);
                    Console.Write("  ");
                }
            }


            //animation
            if (rowNumbersToClearCounter > 0)
            {
                for (int u = 0; u < 3; u++)
                {
                    for (int k = 0; k < rowNumbersToClearCounter; k++)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            Console.SetCursorPosition(i * 2 + offSet, rowNumbersToClear[k] + offSet);
                            Console.Write("++");
                        }
                    }

                    Thread.Sleep(85);



                    for (int k = 0; k < rowNumbersToClearCounter; k++)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            Console.SetCursorPosition(i * 2 + offSet, rowNumbersToClear[k] + offSet);
                            Console.Write("  ");
                        }
                    }

                    Thread.Sleep(85);
                }

            }
            





            /*for (int i = 0; i < rowNumbersToClearCounter; i++)
            {
                animateClearedRow(rowNumbersToClear[i]);

            }

            void animateClearedRow(int rowY)
            {
                for (int u = 0; u < 5; u++)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Console.SetCursorPosition(i * 2 + offSet, rowY + offSet);
                        Console.Write("  ");
                    }

                    Thread.Sleep(100);

                    for (int i = 0; i < 10; i++)
                    {
                        Console.SetCursorPosition(i * 2 + offSet, rowY + offSet);
                        Console.Write("##");
                    }

                    Thread.Sleep(100);
                }


            }*/


            Console.SetCursorPosition(0, 0);
            Console.WriteLine(rowNumbersToClearCounter);

            //move blocks down
            for (int i = rowNumbersToClearCounter; i > -1; i--)
            {
                if (i != rowNumbersToClearCounter - 1)
                {
                    moveDownSquaresBetweenYvalues(rowNumbersToClear[i], 0);

                }
                else
                {
                    
                    moveDownSquaresBetweenYvalues(rowNumbersToClear[i], 0);

                }
            }


            void moveDownSquaresBetweenYvalues(int currentY, int nextY)
            {
                for (int j = currentY - 1; j > nextY; j--)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        if (placedSquares[i, j])
                        {
                            placedSquares[i, j] = false;
                            Console.SetCursorPosition(i * 2 + offSet, j + offSet);
                            Console.Write("  ");

                            Console.SetCursorPosition(i * 2 + offSet, j + 1 + offSet);
                            Console.ForegroundColor = placedSquaresColors[i, j];
                            Console.Write("[]");
                            placedSquares[i, j + 1] = true;

                        }
                    }
                }
            }


        }

        /*public void checkCompleteRow()
        {
            int lowestClearedRowNumber = 0;
            int[] rowNumbersToClear = new int[20];
            int rowNumbersToClearCounter = 0;

            for(int j = 19; j >= 0; j--)
            {
                int squaresInRow = 0;

                for (int i = 0; i < 10; i++)
                {
                    if (placedSquares[i, j])
                    {
                        squaresInRow++;
                    }
                }

                if (squaresInRow == 10)
                {
                    rowNumbersToClear[rowNumbersToClearCounter] = j;
                    rowNumbersToClearCounter++;
                }                      
            }


            //CLEAR ROWS

            for (int i = 0; i < rowNumbersToClearCounter + 1; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    //CLEAR ROW
                    placedSquares[j, rowNumbersToClear[i]] = false;
                    Console.SetCursorPosition(j * 2 + offSet, rowNumbersToClear[i] + offSet);
                    Console.Write("  ");
                }
            }

            //MOVE DOWN ROWS ABOVE
            int timesMovedDown = 0;
            for (int i = 0; i < rowNumbersToClearCounter; i++)
            {
                int moveDownRowsAboveThis = rowNumbersToClear[i] + timesMovedDown;

                //MOVE DOWN
                for (int j = moveDownRowsAboveThis - 1; j >=0; j--)
                {
                    for (int x = 0; x < 10; x++)
                    {
                        if (placedSquares[x, j])
                        {
                            placedSquares[x, j] = false;
                            placedSquares[x, j + 1] = true;
                            placedSquaresColors[x, j + 1] = placedSquaresColors[x, j]; 
                        }
                    }
                }
                timesMovedDown++;
            }

            // RE-RENDER PLACED SQUARES

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (placedSquares[i, j])
                    {
                        Console.SetCursorPosition((i) * 2 + offSet, j + offSet);
                        Console.Write("  ");
                    }

                }
            }

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (placedSquares[i, j])
                    {
                        Console.SetCursorPosition((i) * 2 + offSet, j + offSet);
                        Console.ForegroundColor = placedSquaresColors[i, j];
                        Console.Write("[]");
                    }

                }
            }


        }*/

        public void afterCollision(Objects objects)
        {
            //set new squares 
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (objects.objectSquares[i, j])
                    {
                        placedSquares[i + objects.xPos, j + objects.yPos] = true;
                        placedSquaresColors[i + objects.xPos, j + objects.yPos] = objects.objectColor;

                        objects.objectSquares[i, j] = false;

                    }
                }
            }

            checkCompleteRow();
            objects.setObjectSquares();

        }

        public void renderFallingObject(int lastX, int lastY, int newX, int newY)
        {
            //Console.SetCursorPosition(lastX, lastY);
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (objects.objectSquares[i, j])
                    {
                        Console.SetCursorPosition((i + lastX) * 2 + offSet, j + lastY + offSet);
                        Console.Write("  ");
                    }

                }
            }

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (objects.objectSquares[i, j])
                    {
                        Console.SetCursorPosition((i + newX) * 2 + offSet, j + newY + offSet);
                        Console.ForegroundColor = objects.objectColor;
                        Console.Write("[]");
                    }

                }
            }

        }

        public void renderRotateObject(int lastX, int lastY, int newX, int newY, bool[,] oldGrid)
        {
            //Console.SetCursorPosition(lastX, lastY);
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (oldGrid[i, j])
                    {
                        Console.SetCursorPosition((i + lastX) * 2 + offSet, j + lastY + offSet);
                        Console.Write("  ");
                    }

                }
            }

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (objects.objectSquares[i, j])
                    {
                        Console.SetCursorPosition((i + newX) * 2 + offSet, j + newY + offSet);

                        switch (objects.type)
                        {
                            case "I":
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                break;
                            case "PYRAMID":
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                break;
                            case "L2":
                                Console.ForegroundColor = ConsoleColor.Blue;
                                break;
                            case "L1":
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                break;
                            case "box":
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                break;
                            case "S1":
                                Console.ForegroundColor = ConsoleColor.Green;
                                break;
                            case "S2":
                                Console.ForegroundColor = ConsoleColor.Red;
                                break;


                        }
                        Console.Write("[]");
                    }

                }
            }

        }

        public void drawMap()
        {
            for (int i = 0; i < (gridSizeX + 1) * 2; i++)
            {
                Console.SetCursorPosition(offSet + i - 1, offSet - 1);
                Console.Write("#");
                Console.SetCursorPosition(offSet + i - 1, offSet + gridSizeY);
                Console.Write("#");

            }

            for (int i = 0; i < gridSizeY + 2; i ++)
            {
                Console.SetCursorPosition(offSet - 1, offSet - 1 + i);
                Console.Write("#");
                Console.SetCursorPosition(offSet + gridSizeX * 2, offSet - 1 + i);
                Console.Write("#");


            }


        }

        /*public void renderSquares()
        {

            Console.Clear();


            //check where to put squares
            for (int i = 0; i < gridSizeX; i++)
            {
                for (int j = 0; j < gridSizeY; j++)
                {
                    if (placedSquares[i, j])
                    {
                        Console.SetCursorPosition(i * 2, j);
                        Console.Write("[]");
                    }
                }
            }

            //render object squares
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (objects.objectSquares[i, j])
                    {
                        Console.SetCursorPosition((i + objects.xPos) * 2, j + objects.yPos);
                        Console.Write("[]");
                    }
                }
            }
        }*/
    }
}
