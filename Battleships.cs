using System;
using System.Threading;


namespace BattleShips
{
    public class Battleships
    {
        private static int cursorTopPosition;
        private static int cursorLeftPosition;

        public int NumberOfPlayers { get; set; } = 0;
        public bool CheatMode { get; set; } = false;

        private int AIcyclesOfThinking = 0;

        //the grid will fit both players ships
        private static int[] playersGrid = new int[200];
        private Ship[] playersShips = new Ship[10];
        
        //starting position top/left in grid
        const int origoX = 27;
        const int origoY = 9;
        
        //increment between boxes in X and Y
        const int deltaX = 7;
        const int deltaY = 3;

        const int delta12 = 78;
        
        public static void DrawGrid(int numberPlayers)
        {
            Console.Clear();
            string grid = @"
                           0      1      2      3      4      5      6      7      8      9              0      1      2      3      4      5      6      7      8      9                                                                               
                         _____  _____  _____  _____  _____  _____  _____  _____  _____  _____          _____  _____  _____  _____  _____  _____  _____  _____  _____  _____ 
                    0   |     ||     ||     ||     ||     ||     ||     ||     ||     ||     |    0   |     ||     ||     ||     ||     ||     ||     ||     ||     ||     |
                        |_____||_____||_____||_____||_____||_____||_____||_____||_____||_____|        |_____||_____||_____||_____||_____||_____||_____||_____||_____||_____|
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
                                                                                                                                                                            
";

            Console.SetCursorPosition (56, 4);
            Console.Write("Player1");
            Console.SetCursorPosition (134, 4);

            if (numberPlayers == 2)
            {
                Console.Write("Player2");
            }
            else
            {
                Console.Write("AI player");
            }
            
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
                                                                                            |___/    |__/                                   
";
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
            Console.WriteLine("                                                                    ");
            Console.WriteLine("                                                                    ");
            Console.WriteLine("                                                                    ");
            
            
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
                    Console.WriteLine($"Player{playerNumber + 1}, place your ships.");
                    Console.CursorLeft += 10;
                    Console.Write("Choose one of the ships above:");
                    break;
                case 2:
                    Console.CursorLeft += 10;
                    Console.Write("Choose a starting position (0-99):");
                    break;
                case 3:
                    Console.CursorLeft += 10;
                    Console.Write("Choose an ending position (0-99):");
                    break;
                case 4:
                    Console.Write("Shoot at position(0-99)");
                    Console.WriteLine();
                    Console.CursorLeft += 10;
                    Console.Write($"Player{playerNumber + 1}:");
                    break;
                case 5:
                    Console.CursorLeft += 10;
                    Console.Write("Game over!");
                    Console.WriteLine();
                    Console.CursorLeft += 20;
                    if (playerNumber == 1 && AIcyclesOfThinking != 0)
                    {
                        Console.Write($"AI wins!");
                    }
                    else
                    {
                        Console.Write($"Player{playerNumber + 1} wins!");  
                    }
                    
                    break;
                case 6:
                    Console.Write("Ship Sunk!");
                    Console.WriteLine();
                    Console.CursorLeft += 10;
                    Console.Write($"Well Done Player{playerNumber + 1}!");
                    break;
                case 7: 
                    Console.Write("How many players?");
                    Console.WriteLine();
                    Console.CursorLeft += 10;
                    Console.Write($"Enter 1 or 2:");
                    break;
                case 8:
                    Console.CursorLeft += 10;
                    Console.Write($"AI calculating possible ship placements...: {AIcyclesOfThinking}");
                    Thread.Sleep(50);
                    break;
                case 9:
                    Console.CursorLeft += 10;
                    Console.WriteLine($"AI done placing ships in {AIcyclesOfThinking} cycles.");
                    Console.CursorLeft += 20;
                    Console.Write("Press enter to begin the battle.");
                    break;
                case 10:
                    string answer = null;
                    while (answer != "yes" && answer != "no")
                    {
                        Console.SetCursorPosition(cursorLeftPosition + 10, cursorTopPosition - 1);
                        Console.Write("                                   ");
                        Console.SetCursorPosition(cursorLeftPosition + 10, cursorTopPosition - 1);
                        Console.Write("Do you want cheats (yes/no)?");
                        answer = Console.ReadLine();
                    }

                    if (answer == "yes")
                    {
                        CheatMode = true;
                    }
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
        public int PlaceShip(in int shipSize, in int playerNumber)
        {

            //the ship we are about to place
            int currentShip = 999;
            int startPos = 0;
            int endPos = 0;
            
            //look in the player ship array if a ship of a correct size is still there
            //if not, return 0
            for(var i = (0 + playerNumber*5); i < (5 + playerNumber*5); i++ )
            {
                if (!playersShips[i].IsPlaced)
                {
                    if (playersShips[i].Size == shipSize)
                    {
                        currentShip = i;
                        continue;
                    }
                }
            }
            //if we found a free ship, currentShip contains its index
            if(currentShip==999) return 0;

            if (NumberOfPlayers == 1 && playerNumber == 1)
            {
                //AI placer's turn
                //random the positions
                Random rnd = new Random((int)DateTime.Now.Ticks);
                startPos = rnd.Next(0, 100);
                endPos = rnd.Next(0, 100);
                
                //count how many times the AI has to try (just because I'm curious! :) )
                AIcyclesOfThinking++;
                PlayerInstructions(1,8);
            }
            else
            {            
                //if the ship is there, ask for a starting and ending position
                startPos = Program.getIntInput(this, playerNumber, 2 );
                endPos = Program.getIntInput(this, playerNumber, 3 );
            }
            
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

            
            //player1 or player 2 grid?
            int fieldDiff = playerNumber * delta12;
            
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
                
                if (!CheatMode && playerNumber == 1)
                {
                    
                }
                else
                {
                    Console.Write(size);
                }


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
                markGridPosition(playerSelection,playerNumber, 8);
                return true;
            }
            
            //if a ship is present there (it is, because is is neither 0 or 8)
            int shipSize = 0;
            //check which ship has been hit
            for (var i = (0 + playerNumber * 5); i < (5 + playerNumber * 5); i++)
            {
                if (CheckIsInRange(playerSelection, playersShips[i].StartPos, playersShips[i].EndPos))
                {
                    //register a hit at ship
                    playersShips[i].Hits++;
                    shipSize = playersShips[i].Size;
                    if (playersShips[i].Hits == playersShips[i].Size)
                    {
                        playersShips[i].IsSunk = true;
                        
                        PlayerInstructions(playerNumber, 6);
                        
                    }
                }
            }
            
            //mark found ship at position
            markGridPosition(playerSelection,playerNumber, shipSize);
            //update grid to be 8
            playersGrid[shootingIndex] = 8;
            return true;
        }

        private bool CheckIsInRange(in int playerSelection, in int startPos, in int endPos)
        {
            // check for x-axis
            int startX = startPos % 10;
            int endX = endPos % 10;
            int diffX = endX - startX;
            
            //check for y-axis
            int startY = startPos / 10;
            int endY = endPos / 10;
            int diffY = endY - startY;

            if (diffX != 0)
            {
                //get the lowest of the two
                int beginX = diffX > 0 ? startX: endX;
                
                if( (playerSelection % 10 >= beginX) && (playerSelection % 10 <= beginX + Math.Abs(diffX)) )
                {
                    //it is in X-range, also check that is has correct y-position
                    if(playerSelection/10 == startY)
                        return true;
                }
            }

            if (diffY != 0)
            {
                //get the lowest of the two
                int beginY = diffY > 0 ?  startY: endY;
                
                if( (playerSelection / 10 >= beginY) && (playerSelection / 10 <= beginY + Math.Abs(diffY)) )
                {
                    //it is in Y-range, also check that is has correct x-position
                    if(playerSelection%10 == startX)
                        return true;
                }
            }

            //both x and y coords must be correct
            return false;
        }

        private void markGridPosition(in int playerSelection, int playerNumber,  int i)
        {
            //mark hits on ships with red
            //mark misses with blue
            if( i == 8) Console.ForegroundColor = ConsoleColor.Blue;
            else Console.ForegroundColor = ConsoleColor.Red;
            
            //paint the grid at shootingIndex with marker i
            Console.SetCursorPosition (origoX + playerNumber*delta12 +(playerSelection%10) * deltaX   , origoY + (playerSelection/10 ) * deltaY );
            Console.Write(i);
            Console.ForegroundColor = ConsoleColor.Green;
        }
    }
    
}