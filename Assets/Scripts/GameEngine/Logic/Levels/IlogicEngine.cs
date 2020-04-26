using UnityEngine;

namespace GameEngine.Logic.Levels
{
    public interface IlogicEngine
    {
        Move GetNextMove(Color color, Game GameEngine);
    }
}