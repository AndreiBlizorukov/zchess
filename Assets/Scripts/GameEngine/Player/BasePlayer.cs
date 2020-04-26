using GameEngine.Logic;
using GameEngine.Logic.Levels;
using UnityEngine;

namespace GameEngine.Player
{
    public abstract class BasePlayer : IPlayer
    {
        private readonly Color _color;
        private float _timer;

        public BasePlayer(Color color, float timer = 0f)
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

        public abstract IlogicEngine GetLogicEngine();
    }
}