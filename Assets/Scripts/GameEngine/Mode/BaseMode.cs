using GameEngine.BoardSchema;
using GameEngine.Player;

namespace GameEngine.Mode
{
    public abstract class BaseMode : IMode
    {
        protected IBoardSchema _schema;
        private readonly IPlayer _whitePlayer;
        private readonly IPlayer _blackPlayer;

        public BaseMode(IBoardSchema schema, IPlayer whitePlayer, IPlayer blackPlayer)
        {
            _schema = schema;
            _whitePlayer = whitePlayer;
            _blackPlayer = blackPlayer;
        }

        public IBoardSchema GetPositions()
        {
            return _schema;
        }

        public IPlayer GetWhitePlayer()
        {
            return _whitePlayer;
        }

        public IPlayer GetBlackPlayer()
        {
            return _blackPlayer;
        }
    }
}