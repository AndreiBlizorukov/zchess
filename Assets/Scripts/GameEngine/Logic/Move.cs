using UnityEngine;

namespace GameEngine.Logic
{
    public class Move
    {
        public Vector2Int Source;
        public Vector2Int Target;

        public Move(Vector2Int source, Vector2Int target)
        {
            Source = source;
            Target = target;
        }
    }
}