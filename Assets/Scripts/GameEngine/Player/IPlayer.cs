using GameEngine.Logic;
using GameEngine.Logic.Levels;
using UnityEngine;

namespace GameEngine.Player
{
    public interface IPlayer
    {
        Color GetColor();
        string GetColorText();
        float GetTimer();
        float DecrementTimer(float delta);
        IlogicEngine GetLogicEngine();
    }
}