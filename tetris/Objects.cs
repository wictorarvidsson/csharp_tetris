using System;
using System.Collections.Generic;
using System.Text;

namespace tetris
{
    class Objects
    {
        public string type;
        public ConsoleColor objectColor;

        public bool[,] objectSquares = new bool[4, 4];
        public int xPos = 5;
        public int yPos = 0;
        public void setObjectSquares()
        {
            // sets random 

            Random rand = new Random();
            int randomNumber = rand.Next(0, 7);

            switch (randomNumber)
            {
                case 0:
                    //OOXO
                    //OOXO
                    //OOXO
                    //OOXO

                    type = "I";
                    objectColor = ConsoleColor.Cyan;

                    objectSquares[2, 0] = true;
                    objectSquares[2, 1] = true;
                    objectSquares[2, 2] = true;
                    objectSquares[2, 3] = true;
                    break;
                case 1:
                    //OOXO
                    //OXXX
                    //OOOO
                    //OOOO

                    type = "PYRAMID";
                    objectColor = ConsoleColor.Magenta;


                    objectSquares[2, 0] = true;
                    objectSquares[1, 1] = true;
                    objectSquares[2, 1] = true;
                    objectSquares[3, 1] = true;
                    break;
                case 2:
                    //OOXO
                    //OOXO
                    //OXXO
                    //OOOO

                    type = "L2";
                    objectColor = ConsoleColor.Blue;


                    objectSquares[2, 0] = true;
                    objectSquares[2, 1] = true;
                    objectSquares[2, 2] = true;
                    objectSquares[1, 2] = true;
                    break;
                case 3:
                    //OXOO
                    //OXOO
                    //OXXO
                    //OOOO

                    type = "L1";
                    objectColor = ConsoleColor.DarkYellow;


                    objectSquares[1, 0] = true;
                    objectSquares[1, 1] = true;
                    objectSquares[1, 2] = true;
                    objectSquares[2, 2] = true;
                    break;
                case 4:
                    //OOOO
                    //OXXO
                    //OXXO
                    //OOOO

                    type = "box";
                    objectColor = ConsoleColor.Yellow;


                    objectSquares[1, 1] = true;
                    objectSquares[2, 1] = true;
                    objectSquares[2, 2] = true;
                    objectSquares[1, 2] = true;
                    break;
                case 5:
                    //OOOO
                    //OXXO
                    //XXOO
                    //OOOO

                    type = "S1";
                    objectColor = ConsoleColor.Green;


                    objectSquares[1, 1] = true;
                    objectSquares[2, 1] = true;
                    objectSquares[0, 2] = true;
                    objectSquares[1, 2] = true;
                    break;
                case 6:
                    //OOOO
                    //XXOO
                    //OXXO
                    //OOOO

                    type = "S2";
                    objectColor = ConsoleColor.Red;


                    objectSquares[0, 1] = true;
                    objectSquares[1, 1] = true;
                    objectSquares[1, 2] = true;
                    objectSquares[2, 2] = true;
                    break;
            }




            //objectSquares[0, 0] = true;

            xPos = 5;
            yPos = 0;
        }

        public void newObjects()
        {
            setObjectSquares();

        }
    }
}
