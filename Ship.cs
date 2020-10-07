namespace BattleShips
{
    public class Ship
    {
        public int Hits { get; set; } = 0;
        public bool IsSunk { get; set; } = false;
        public int Size { get; set; }
        public int StartPos { get; set; }
        public int EndPos { get; set; }
        public bool IsPlaced { get; set; } = false;
    }
}