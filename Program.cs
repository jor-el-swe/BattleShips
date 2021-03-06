﻿using System;
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
            //17 - 2d arrays: Battleships! The Rules: Two players; 10x10 grid per player; Ships: sizes 5,4,3,2,2;
            //Ships may be placed vertically or horizontally; 
            //ships may not overlap and may not touch each other,
            //there needs to be at least one empty grid cell around the ship (in every direction); 
            //players take alternating turns; player 1 starts; coordinates are given in the form:
            //g7, a2, d2, etc…; feedback “Hit” or “Miss”;
            //Feedback “Ship sunk!”; 
            //Feedback “Player1 Wins” / “Player2 Wins”;
            //Random AI; Smart AI;
            
            InitConsoleWindow();
            
            int playerNumber = 0;
            int noPlacedShips = 0;
            int noPlayers = 0;

            Battleships gameInstance = new Battleships();
            
            //draw first time for initialization
            Battleships.DrawGrid(noPlayers);
            
            while (noPlayers != 1 && noPlayers != 2)
            {
                noPlayers = getIntInput(gameInstance, playerNumber, 7);
                gameInstance.NumberOfPlayers = noPlayers;
            }

            if (noPlayers == 1)
            {
                gameInstance.PlayerInstructions(playerNumber, 10);
            }
            //draw first time for correct AI/player labels
            Battleships.DrawGrid(noPlayers);
            gameInstance.CreateShips();



            //place ships
            //keep running until all 10 (or 5 player, 5  AI ) ships have been placed
            for (var i = 0; i < 2; i++)
            {
                //hide all ships
                Battleships.DrawGrid(noPlayers);
                noPlacedShips = 0;
                playerNumber = i;
                
                //place 5 ships
                while (noPlacedShips<5)
                {
                    int shipSize = 0;
                    
                    //let AI random its ship's number
                    if (noPlayers == 1  && playerNumber == 1)
                    {
                        Random rnd = new Random((int)DateTime.Now.Ticks);
                        shipSize = rnd.Next(2, 6);
                    }
                    else
                    {
                        //only read input for human players
                        shipSize = getIntInput(gameInstance, playerNumber, 1);
                    }
                    
                    var outcome = gameInstance.PlaceShip(shipSize, playerNumber);
                    
                    //outcome == 1 => all well, change player, outcome == 0 => failed for some reason
                    //same player again
                    if (outcome != 1) continue;
                    noPlacedShips++;
                }
            }

            gameInstance.PlayerInstructions(playerNumber, 9);
            Console.ReadLine();

            //hide all ships
            Battleships.DrawGrid(noPlayers);
   
            
            playerNumber = 0;
            //until one player is out of ships
            int playerShootAt = 0;
            int AIShootAT = 0;
            
            while (true)
            {
                
                if (gameInstance.AllShipsSunk(playerNumber))
                {
                    break;
                }
                //take turns shooting at the other player
                if (noPlayers == 1 && playerNumber == 1)
                {
                    //AI:s turn to shoot
                    //AI totally randoms guesses
                    Random rnd = new Random((int)DateTime.Now.Ticks);
                    playerShootAt = rnd.Next(0,99);
                }
                else
                {
                    playerShootAt = getIntInput(gameInstance, playerNumber, 4);
                }

                //shoot at the other player's grid
                int otherPlayer = playerNumber == 0 ? 1 : 0;
                
                if (gameInstance.ShootAtPosition(playerShootAt, otherPlayer))
                {
                    playerNumber++;
                    playerNumber &= 0x1;
                }
            }
            int otherWinner = playerNumber == 0 ? 1 : 0;
            gameInstance.PlayerInstructions(otherWinner, 5);
            
            
        }

        private static void InitConsoleWindow()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);  
            ShowWindow(ThisConsole, MAXIMIZE); 
            
            Battleships.WelcomeToBattleships();
            Console.WriteLine("press enter to start");
            Console.Read();
        }

        public static int getIntInput(in Battleships battleships, int playerNumber, int messageNumber )
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
