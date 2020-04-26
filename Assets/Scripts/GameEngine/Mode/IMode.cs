using GameEngine.PiecesSchema;
using GameEngine.Player;

namespace GameEngine.Mode
{
    public interface IMode
    {
        IPiecesSchema GetPositions();
        IPlayer GetWhitePlayer();
        IPlayer GetBlackPlayer();
    }
}