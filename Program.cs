using System;
using System.Runtime.InteropServices;


namespace BattleShips
{
    class Program
    {
        // code for maximizing console window
        [DllImport("kernel32.dll", ExactSpelling = true)]  
        private static extern IntPtr GetConsoleWindow();  
        private static IntPtr ThisConsole = GetConsoleWindow();  
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]  
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);  
        private const int HIDE = 0;  
        private const int MAXIMIZE = 3;  
        private const int MINIMIZE = 6;  
        private const int RESTORE = 9; 
        //end console max code
        
        static void Main(string[] args)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green; 
            
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);  
            ShowWindow(ThisConsole, MAXIMIZE); 
            
            
            //17 - 2d arrays: Battleships! The Rules: Two players; 10x10 grid per player; Ships: sizes 5,4,3,2,2;
            //Ships may be placed vertically or horizontally; 
            //ships may not overlap and may not touch each other,
            //there needs to be at least one empty grid cell around the ship (in every direction); 
            //players take alternating turns; player 1 starts; coordinates are given in the form:
            //g7, a2, d2, etc…; feedback “Hit” or “Miss”;
            //Feedback “Ship sunk!”; 
            //Feedback “Player1 Wins” / “Player2 Wins”;
            //Random AI; Smart AI;


            int playerNumber = 0;
            int noPlacedShips = 0;
            
            //Battleships.WelcomeToBattleships();
            //Console.Read();
            Battleships gameInstance = new Battleships();
            Battleships.DrawGrid();
            gameInstance.CreateShips();

            //keep running until all 10 ships have been placed
            for (var i = 0; i < 2; i++)
            {
                noPlacedShips = 0;
                playerNumber = i;
                while (noPlacedShips<5)
                {

                    var playerSelection = getIntInput(gameInstance, playerNumber, 1);
                
                    //if placement went well, change player
                    var outcome = 0;
                    outcome = gameInstance.PlaceShip(playerSelection, playerNumber);
                    //outcome == 1 => all well, change player, outcome == 0 => failed for some reason
                    //same player again
                    if (outcome != 1) continue;
                    noPlacedShips++;
                }
            }

            
            //hide all ships
            Battleships.DrawGrid();
   
            
            playerNumber = 0;
            //until one player is out of ships
            while (true)
            {
                if (gameInstance.AllShipsSunk(playerNumber))
                {
                    break;
                }
                //take turns shooting at the other player
                var playerSelection = getIntInput(gameInstance, playerNumber, 4);

                if (gameInstance.ShootAtPosition(playerSelection, playerNumber))
                {
                    playerNumber++;
                    playerNumber &= 0x1;
                }
            }
            
            gameInstance.PlayerInstructions(playerNumber, 5);
            
            
        }
        private static int getIntInput(in Battleships battleships, int playerNumber, int messageNumber )
        {
            bool test = false;
            int num = 0;
            while (!test)
            {
                battleships.PlayerInstructions(playerNumber, messageNumber);
                test = int.TryParse(Console.ReadLine(), out num);
                
                //set boundaries for numbers
                if (num > 99 || num < 0) test = false;
            }

            return num;
        }
        
    }
}
