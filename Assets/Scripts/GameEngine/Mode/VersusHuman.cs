using GameEngine.BoardSchema;
using GameEngine.Player;

namespace GameEngine.Mode
{
    public class VersusHuman : BaseMode
    {
        public VersusHuman(IBoardSchema schema, IPlayer white, IPlayer black) : base(schema, white, black)
        {
        }
    }
}