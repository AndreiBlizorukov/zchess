using UnityEngine;

namespace GameEngine.Logic
{
    public interface IlogicEngine
    {
        Move GetNextMove(Color color, Game GameEngine);
    }
}