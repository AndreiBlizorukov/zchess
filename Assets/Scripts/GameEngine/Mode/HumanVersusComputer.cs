using GameEngine.BoardSchema;
using GameEngine.Logic;
using GameEngine.Logic.Levels;
using GameEngine.Player;
using View;

namespace GameEngine.Mode
{
    public class HumanVersusComputer : BaseMode
    {
        private IlogicEngine _logic;

        public HumanVersusComputer(IBoardSchema schema, IPlayer white, IPlayer black, ViewManager viewManager) : base(
            schema, white, black, viewManager)
        {
        }

        public override void NextTurn(IPlayer activePlayer)
        {
            var logicEngine = activePlayer.GetLogicEngine();
            if (logicEngine != null)
            {
                var move = logicEngine.GetNextMove(activePlayer.GetColor(), _viewManager.GameEngine);
                _viewManager.MovePiece(
                    _viewManager.mBoard.mAllCells[move.Source.x, move.Source.y],
                    _viewManager.mBoard.mAllCells[move.Target.x, move.Target.y]
                );
                _viewManager.GameEngine.TogglePlayer();
                if (_viewManager.CheckStopGame())
                {
                    return;
                }
                
                _viewManager.SetInteractive(_viewManager.GameEngine.mCurrentPlayer.GetColor(), true);
                return;
            }
            
            _viewManager.GameEngine.TogglePlayer();
            _viewManager.GameEngine.GetMode().NextTurn(_viewManager.GameEngine.mCurrentPlayer);
        }

        public override void EndTurn(IPlayer activePlayer)
        {
            _viewManager.SetInteractive(activePlayer.GetColor(), false);
        }
    }
}