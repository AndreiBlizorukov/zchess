using GameEngine.BoardSchema;
using GameEngine.Player;
using View;

namespace GameEngine.Mode
{
    public class VersusComputer : BaseMode
    {
        public VersusComputer(IBoardSchema schema, IPlayer white, IPlayer black, ViewManager viewManager) : base(schema, white, black, viewManager)
        {
        }
    }
}