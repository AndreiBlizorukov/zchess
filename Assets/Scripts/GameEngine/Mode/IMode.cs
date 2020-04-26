using GameEngine.BoardSchema;
using GameEngine.Player;

namespace GameEngine.Mode
{
    public interface IMode
    {
        IBoardSchema GetPositions();
        IPlayer GetWhitePlayer();
        IPlayer GetBlackPlayer();
        void NextTurn(IPlayer activePlayer);
        void EndTurn(IPlayer activePlayer);
    }
}