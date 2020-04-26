using GameEngine.PiecesSchema;
using GameEngine.Player;

namespace GameEngine.Mode
{
    public class VersusHuman : BaseMode
    {
        public VersusHuman(IPiecesSchema schema, IPlayer white, IPlayer black) : base(schema, white, black)
        {
        }
    }
}