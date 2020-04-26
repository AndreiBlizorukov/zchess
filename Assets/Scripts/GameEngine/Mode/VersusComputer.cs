using GameEngine.PiecesSchema;
using GameEngine.Player;

namespace GameEngine.Mode
{
    public class VersusComputer : BaseMode
    {
        public VersusComputer(IPiecesSchema schema, IPlayer white, IPlayer black) : base(schema, white, black)
        {
        }
    }
}