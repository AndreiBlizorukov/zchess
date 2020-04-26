using GameEngine.Logic;
using UnityEngine;

namespace GameEngine.Player
{
    public class EasiestBot : BasePlayer
    {
        private readonly IlogicEngine _engine;

        public EasiestBot(Color color, IlogicEngine engine, float timer = 0) : base(color, timer)
        {
            _engine = engine;
        }

        public override IlogicEngine GetLogicEngine()
        {
            return _engine;
        }
    }
}