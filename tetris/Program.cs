using System;
using System.Threading;

namespace tetris
{
    class Program
    {

        static void Main(string[] args)
        {
            int updateTime = 10;
            int updateGravity = 500;
            int callUpdateGravityFunc = updateGravity / updateTime;
            int callUpdateGravityFuncCounter = 0;
            
            Render render = new Render();
            render.setWindowSize();
            render.drawMap();
            render.intro();

            while (true)
            {
                callUpdateGravityFuncCounter++;
                if(callUpdateGravityFuncCounter == callUpdateGravityFunc)
                {
                    render.addGravityOnObjects();
                    callUpdateGravityFuncCounter = 0;

                    //checkcollision

                    //render.renderSquares();

                }

                // KOLLA BARA ROW CLEAR NÄR OBJEKT LANDAR

                render.checkCollision();
                render.checkForKeyPress();

                Thread.Sleep(updateTime);
            }
        }
    }
}
