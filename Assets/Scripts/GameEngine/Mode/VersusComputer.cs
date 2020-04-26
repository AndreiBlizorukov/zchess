using GameEngine.BoardSchema;
using GameEngine.Player;

namespace GameEngine.Mode
{
    public class VersusComputer : BaseMode
    {
        public VersusComputer(IBoardSchema schema, IPlayer white, IPlayer black) : base(schema, white, black)
        {
        }
    }
}