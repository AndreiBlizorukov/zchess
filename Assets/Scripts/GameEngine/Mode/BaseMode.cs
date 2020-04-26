using GameEngine.BoardSchema;
using GameEngine.Player;
using View;

namespace GameEngine.Mode
{
    public abstract class BaseMode : IMode
    {
        protected IBoardSchema _schema;
        private readonly IPlayer _whitePlayer;
        private readonly IPlayer _blackPlayer;
        protected readonly ViewManager _viewManager;

        public BaseMode(IBoardSchema schema, IPlayer whitePlayer, IPlayer blackPlayer, ViewManager viewManager)
        {
            _schema = schema;
            _whitePlayer = whitePlayer;
            _blackPlayer = blackPlayer;
            _viewManager = viewManager;
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

        public virtual void EndTurn(IPlayer activePlayer)
        {
        }

        public virtual void NextTurn(IPlayer activePlayer)
        {
        }
    }
}