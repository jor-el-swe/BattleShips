using System;
using System.Collections.Generic;
using System.Threading;
using System.Transactions;

namespace BattleShips
{
    public class Battleships
    {
        private static int cursorTopPosition;
        private static int cursorLeftPosition;
        //the grid will fit both players ships
        private static int[] playersGrid = new int[200];
        private Ship[] playersShips = new Ship[10];
        
        public static void DrawGrid()
        {
            Console.Clear();
            string grid = @"
                           A      B      C      D      E      F      G      H      I      J              A      B      C      D      E      F      G      H      I      J                                                                               
                         _____  _____  _____  _____  _____  _____  _____  _____  _____  _____          _____  _____  _____  _____  _____  _____  _____  _____  _____  _____ 
                    1   |     ||     ||     ||     ||     ||     ||     ||     ||     ||     |    1   |     ||     ||     ||     ||     ||     ||     ||     ||     ||     |
                        |_____||_____||_____||_____||_____||_____||_____||_____||_____||_____|        |_____||_____||_____||_____||_____||_____||_____||_____||_____||_____|
                         _____  _____  _____  _____  _____  _____  _____  _____  _____  _____          _____  _____  _____  _____  _____  _____  _____  _____  _____  _____ 
                    2   |     ||     ||     ||     ||     ||     ||     ||     ||     ||     |    2   |     ||     ||     ||     ||     ||     ||     ||     ||     ||     |
                        |_____||_____||_____||_____||_____||_____||_____||_____||_____||_____|        |_____||_____||_____||_____||_____||_____||_____||_____||_____||_____|
                         _____  _____  _____  _____  _____  _____  _____  _____  _____  _____          _____  _____  _____  _____  _____  _____  _____  _____  _____  _____ 
                    3   |     ||     ||     ||     ||     ||     ||     ||     ||     ||     |    3   |     ||     ||     ||     ||     ||     ||     ||     ||     ||     |
                        |_____||_____||_____||_____||_____||_____||_____||_____||_____||_____|        |_____||_____||_____||_____||_____||_____||_____||_____||_____||_____|
                         _____  _____  _____  _____  _____  _____  _____  _____  _____  _____          _____  _____  _____  _____  _____  _____  _____  _____  _____  _____ 
                    4   |     ||     ||     ||     ||     ||     ||     ||     ||     ||     |    4   |     ||     ||     ||     ||     ||     ||     ||     ||     ||     |
                        |_____||_____||_____||_____||_____||_____||_____||_____||_____||_____|        |_____||_____||_____||_____||_____||_____||_____||_____||_____||_____|
                         _____  _____  _____  _____  _____  _____  _____  _____  _____  _____          _____  _____  _____  _____  _____  _____  _____  _____  _____  _____ 
                    5   |     ||     ||     ||     ||     ||     ||     ||     ||     ||     |    5   |     ||     ||     ||     ||     ||     ||     ||     ||     ||     |
                        |_____||_____||_____||_____||_____||_____||_____||_____||_____||_____|        |_____||_____||_____||_____||_____||_____||_____||_____||_____||_____|
                         _____  _____  _____  _____  _____  _____  _____  _____  _____  _____          _____  _____  _____  _____  _____  _____  _____  _____  _____  _____ 
                    6   |     ||     ||     ||     ||     ||     ||     ||     ||     ||     |    6   |     ||     ||     ||     ||     ||     ||     ||     ||     ||     |
                        |_____||_____||_____||_____||_____||_____||_____||_____||_____||_____|        |_____||_____||_____||_____||_____||_____||_____||_____||_____||_____|
                         _____  _____  _____  _____  _____  _____  _____  _____  _____  _____          _____  _____  _____  _____  _____  _____  _____  _____  _____  _____ 
                    7   |     ||     ||     ||     ||     ||     ||     ||     ||     ||     |    7   |     ||     ||     ||     ||     ||     ||     ||     ||     ||     |
                        |_____||_____||_____||_____||_____||_____||_____||_____||_____||_____|        |_____||_____||_____||_____||_____||_____||_____||_____||_____||_____|
                         _____  _____  _____  _____  _____  _____  _____  _____  _____  _____          _____  _____  _____  _____  _____  _____  _____  _____  _____  _____ 
                    8   |     ||     ||     ||     ||     ||     ||     ||     ||     ||     |    8   |     ||     ||     ||     ||     ||     ||     ||     ||     ||     |
                        |_____||_____||_____||_____||_____||_____||_____||_____||_____||_____|        |_____||_____||_____||_____||_____||_____||_____||_____||_____||_____|
                         _____  _____  _____  _____  _____  _____  _____  _____  _____  _____          _____  _____  _____  _____  _____  _____  _____  _____  _____  _____ 
                    9   |     ||     ||     ||     ||     ||     ||     ||     ||     ||     |    9   |     ||     ||     ||     ||     ||     ||     ||     ||     ||     |
                        |_____||_____||_____||_____||_____||_____||_____||_____||_____||_____|        |_____||_____||_____||_____||_____||_____||_____||_____||_____||_____|
                         _____  _____  _____  _____  _____  _____  _____  _____  _____  _____          _____  _____  _____  _____  _____  _____  _____  _____  _____  _____ 
                    10  |     ||     ||     ||     ||     ||     ||     ||     ||     ||     |    10  |     ||     ||     ||     ||     ||     ||     ||     ||     ||     |
                        |_____||_____||_____||_____||_____||_____||_____||_____||_____||_____|        |_____||_____||_____||_____||_____||_____||_____||_____||_____||_____|
                                                                                                                                                                            
";

            Console.SetCursorPosition (56, 4);
            Console.Write("Player1");
            Console.SetCursorPosition (134, 4);
            Console.Write("Player2");
            Console.WriteLine();
            Console.WriteLine(grid);
            cursorLeftPosition = Console.CursorLeft;
            cursorTopPosition = Console.CursorTop;
        }
        
        public static void WelcomeToBattleships()
        {
            Console.Title = "Battleships!";

            string title = @"







                                                                                       __    __     _                            _          
                                                                                      / / /\ \ \___| | ___ ___  _ __ ___   ___  | |_ ___    
                                                                                      \ \/  \/ / _ \ |/ __/ _ \| '_ ` _ \ / _ \ | __/ _ \   
                                                                                       \  /\  /  __/ | (_| (_) | | | | | |  __/ | || (_) |  
                                                                                        \/  \/ \___|_|\___\___/|_| |_| |_|\___|  \__\___/   
                                                                                           
                                                                                       ___       _   _   _           _     _              _ 
                                                                                      / __\ __ _| |_| |_| | ___  ___| |__ (_)_ __  ___   / \
                                                                                     /__\/// _` | __| __| |/ _ \/ __| '_ \| | '_ \/ __| /  /
                                                                                    / \/  \ (_| | |_| |_| |  __/\__ \ | | | | |_) \__ \/\_/ 
                                                                                    \_____/\__,_|\__|\__|_|\___||___/_| |_|_| .__/|___/\/   
                                                                                                                            |_|             
                                                                                           
                                                                                           
                                                                                           
                                                                                           
                                                                                           
                                                                                           
                                                                                      _                 _                      _            
                                                                                     | |__  _   _ _    (_) ___  _ __       ___| |           
                                                                                     | '_ \| | | (_)   | |/ _ \| '__|____ / _ \ |           
                                                                                     | |_) | |_| |_    | | (_) | | |_____|  __/ |           
                                                                                     |_.__/ \__, (_)  _/ |\___/|_|        \___|_|           
                                                                                            |___/    |__/                                   ";
            Console.ForegroundColor = System.ConsoleColor.DarkBlue;
            for (var i = 0; i < 10; i++)
            {
                Console.Clear();
                Console.WriteLine(title);
                Thread.Sleep(100);
                Console.ForegroundColor++;
            }
            Console.ForegroundColor = System.ConsoleColor.Green;
        }

        public void PlayerInstructions(int playerNumber, int messageNumber)
        {
            //first clear the lines for the messages:
            Console.SetCursorPosition(cursorLeftPosition + 10, cursorTopPosition - 1);
            Console.WriteLine("                                                           ");
            Console.WriteLine("                                                           ");
            
            
            Console.SetCursorPosition(cursorLeftPosition + 10, cursorTopPosition - 1);
            switch (messageNumber)
            {
                //placement message:ships left
                case 1:
                    Console.Write("Ships left: ");
                    for (var i = (0 + playerNumber * 5); i < (5 + playerNumber * 5); i++)
                    {
                        if (!playersShips[i].IsPlaced)
                        {
                            Console.Write($"{playersShips[i].Size} ");
                        }
                    }
                    Console.WriteLine();
                    Console.CursorLeft += 10;
                    Console.Write($"Player{playerNumber + 1}, place your ships:");
                    break;
                case 2:
                    Console.Write("Choose a starting position (0-99):");
                    break;
                case 3:
                    Console.Write("Choose an ending position (0-99):");
                    break;
                case 4:
                    Console.Write("Shoot at position(0-99)");
                    Console.WriteLine();
                    Console.CursorLeft += 10;
                    Console.Write($"Player{playerNumber + 1}:");
                    break;
                case 5:
                    Console.Write("Game over!");
                    Console.WriteLine();
                    Console.CursorLeft += 10;
                    Console.Write($"Player{playerNumber + 1} wins!");
                    break;
                
                default:
                    break;
            }

           
        }

        public void CreateShips()
        {
            //Ships: sizes 5,4,3,2,2;
            playersShips[0] = new Ship {Size = 5};
            playersShips[1] = new Ship {Size = 4};
            playersShips[2] = new Ship {Size = 3};
            playersShips[3] = new Ship {Size = 2};
            playersShips[4] = new Ship {Size = 2};

            
            playersShips[5] = new Ship {Size = 5};
            playersShips[6] = new Ship {Size = 4};
            playersShips[7] = new Ship {Size = 3};
            playersShips[8] = new Ship {Size = 2};
            playersShips[9] = new Ship {Size = 2};
            
        }

        //outcome == 1 => all well, change player, outcome == 0 => failed for some reason
        //same player again
        public int PlaceShip(in int playerSelection, in int playerNumber)
        {

            //the ship we are about to place
            int currentShip = 999;
            
            //look in the player ship array if a ship of a correct size is still there
            //if not, return 0
            for(var i = (0 + playerNumber*5); i < (5 + playerNumber*5); i++ )
            {
                if (!playersShips[i].IsPlaced)
                {
                    if (playersShips[i].Size == playerSelection)
                    {
                        currentShip = i;
                        continue;
                    }
                }
            }
            //if we found a free ship, currentShip contains its index
            if(currentShip==999) return 0;
            
            //if the ship is there, ask for a starting and ending position
            this.PlayerInstructions(playerNumber, 2);
            int startPos = Int32.Parse(Console.ReadLine());
            this.PlayerInstructions(playerNumber, 3);
            int endPos = Int32.Parse(Console.ReadLine());

            //if the position is not ok //return 0
            //out of bounds
            
            //this should already have been taken care of in main program input reading 
            //if ((startPos < 0 || endPos < 0) || (startPos > 99 || endPos > 99)) return 0;
            
            //if ship is not horizontal or vertical
            if ((startPos % 10 != endPos % 10) && (startPos / 10 != endPos/10)) return 0;
            
            //if ship length is different from positions entered
            if( playersShips[currentShip].Size != (Math.Abs(startPos-endPos) + 1) &&
                 playersShips[currentShip].Size != (Math.Abs(startPos/10-endPos/10) +1)
                 )
            {
                return 0;
            }


            //if placement of ship goes wrong, return 0
            if (!DrawShipOnGrid(startPos, endPos, playersShips[currentShip].Size, playerNumber)) return 0;
            
            playersShips[currentShip].IsPlaced = true;
            playersShips[currentShip].StartPos = startPos;
            playersShips[currentShip].EndPos = endPos;
            
            //if position is ok, place the ship and return 1
            return 1;

        }
        
        private bool DrawShipOnGrid(in int startPos, in int endPos, int size, int playerNumber)
        {
            //starting position top/left in grid
            const int origoX = 27;
            const int origoY = 9;
            
            //increment between boxes in X and Y
            const int deltaX = 7;
            const int deltaY = 3;
            
            //player1 or player 2 grid?
            int fieldDiff = playerNumber * 78;
            
            //draw ship forward or backwards?
            int direction = 1;
            
           //check if we are drawing backwards
            if ((endPos - startPos) < 0)
            {
                direction = -1;
            } 
            
            //are we incrementing on Y or X (drawing horizontally or vertically)?
            int incrementX = ((endPos % 10 - startPos % 10) != 0) ? 1 : 0;
            int incrementY = ((endPos / 10 - startPos / 10) != 0) ? 1 : 0;
 
            
            //now check if the grid position is already occupied, then return false
            //also check if the ship is placed too close to another ship
            if (!CheckPositionAndNeighbours(size, startPos, playerNumber, direction, incrementX, incrementY )) return false;
           
            
            Console.ForegroundColor = ConsoleColor.Blue;
            //draw the actual ship, and enter it in the playersGrid array
            for (var i = 0; i < size; i++)
            {  
                Console.SetCursorPosition (origoX + fieldDiff + deltaX*(startPos%10) + i * deltaX * incrementX * direction , origoY + deltaY*(startPos/10) + i * deltaY*incrementY* direction );
                Console.Write(size);

                //storing ships in grid seems to work. not tested for edge cases
                playersGrid[startPos + (playerNumber)*100 + i*direction*incrementX + i*direction*incrementY*10] = size;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            return true;

        }

        private bool CheckPositionAndNeighbours(int size, int startPos, int playerNumber, int direction, int incrementX, int incrementY)
        {
            for (var i = 0; i < size; i++)
            {
                int currentIndex = startPos + (playerNumber) * 100 + i * direction * incrementX +
                                   i * direction * incrementY * 10;
                
                //for checking neighbours
                int right = currentIndex + 1;
                int left = currentIndex -1;
                int down = currentIndex + 10;
                int up = currentIndex-10;
                
                //check the actual spot for empty slot
                if (playersGrid[currentIndex] != 0) return false;
                
                //check neighbors, but first check boundaries
                if( (right < 200) && (right % 10 !=0))
                {
                    if (playersGrid[right] != 0) return false;
                }
                
                if( (left >= 0) && (left % 10 !=9))
                {
                    if (playersGrid[left] != 0) return false;
                }

                if ( (up >= 0) && !(playerNumber==1 && up < 100) )
                {
                    if (playersGrid[up] != 0) return false; 
                }

                if ((down < 200) && (playerNumber==0 && down < 100) )
                {
                    if (playersGrid[down] != 0) return false; 
                }
            }
            return true;
        }

        public bool AllShipsSunk(in int playerNumber)
        {
            var allAreSunk = true;
            for (var i = (0 + playerNumber * 5); i < (5 + playerNumber * 5); i++)
            {
                if (!playersShips[i].IsSunk)
                {
                    allAreSunk = false;
                }
            }
            return allAreSunk;
        }

        //return false if user makes an error
        //returns true if position is valid
        public bool ShootAtPosition(in int playerSelection, in int playerNumber)
        {
            int shootingIndex = playerSelection + playerNumber * 100;
            
            //first check if user already shot at that position
            //8 means already shot at this position
            if (playersGrid[shootingIndex] == 8)
            {
                return false;
            }
            
            //then check if no ship is present there
            //mark as shot
            if (playersGrid[shootingIndex] == 0)
            {
                playersGrid[shootingIndex] = 8;
                //mark the position on the UI grid
                markGridPosition(shootingIndex,8);
            }
            
            
            return true;
        }

        private void markGridPosition(in int shootingIndex, int i)
        {
            //paint the grid at shootingIndex with marker i
        }
    }
    
}