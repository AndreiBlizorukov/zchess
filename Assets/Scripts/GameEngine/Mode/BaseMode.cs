using GameEngine.PiecesSchema;
using GameEngine.Player;

namespace GameEngine.Mode
{
    public abstract class BaseMode : IMode
    {
        protected IPiecesSchema _schema;
        private readonly IPlayer _whitePlayer;
        private readonly IPlayer _blackPlayer;

        public BaseMode(IPiecesSchema schema, IPlayer whitePlayer, IPlayer blackPlayer)
        {
            _schema = schema;
            _whitePlayer = whitePlayer;
            _blackPlayer = blackPlayer;
        }

        public IPiecesSchema GetPositions()
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