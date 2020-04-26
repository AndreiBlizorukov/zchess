using GameEngine.BoardSchema;
using GameEngine.Player;
using View;

namespace GameEngine.Mode
{
    public class HumanVersusHuman : BaseMode
    {
        public HumanVersusHuman(IBoardSchema schema, IPlayer white, IPlayer black, ViewManager viewManager) : base(schema,
            white, black, viewManager)
        {
        }

        public override void NextTurn(IPlayer activePlayer)
        {
            _viewManager.SetInteractive(activePlayer.GetColor(), true);
        }

        public override void EndTurn(IPlayer activePlayer)
        {
            _viewManager.SetInteractive(activePlayer.GetColor(), false);
        }
    }
}