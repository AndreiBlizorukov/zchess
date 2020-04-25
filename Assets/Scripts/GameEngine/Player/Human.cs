using UnityEngine;

namespace GameEngine.Player
{
    public class Human : IPlayer
    {
        private readonly Color _color;
        private float _timer;

        public Human(Color color, float timer = 0f)
        {
            _color = color;
            _timer = timer;
        }

        public Color GetColor()
        {
            return _color;
        }

        public string GetColorText()
        {
            return GetColor() == Color.black
                ? "black"
                : "white";
        }

        public float GetTimer()
        {
            return _timer;
        }

        public float DecrementTimer(float delta)
        {
            _timer -= delta;
            return _timer;
        }
    }
}