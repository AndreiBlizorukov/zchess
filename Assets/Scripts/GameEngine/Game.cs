using GameEngine.GameMode;
namespace GameEngine
{
    public class Game
    {
        private Board _board;
        private readonly IMode _mode = new Default();

        public void Setup()
        {
            _board = new Board();
             var pieces = _mode.CreatePieces();
            _board.PlacePieces(pieces);
        }

        public Board GetBoard()
        {
            return _board;
        }
    }
}