using UnityEngine;

namespace GameEngine.Player
{
    public class Human : IPlayer
    {
        private readonly Color _color;

        public Human(Color color)
        {
            _color = color;
        }

        public Color GetColor()
        {
            return _color;
        }
    }
}