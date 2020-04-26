using GameEngine.BoardSchema;
using GameEngine.Player;
using View;

namespace GameEngine.Mode
{
    public class VersusHuman : BaseMode
    {
        public VersusHuman(IBoardSchema schema, IPlayer white, IPlayer black,  ViewManager viewManager) : base(schema, white, black, viewManager)
        {
        }

        public override void NextTurn(IPlayer activePlayer)
        {
            base.NextTurn(activePlayer);
            _viewManager.SetInteractive(activePlayer.GetColor(), true);
        }
    }
}