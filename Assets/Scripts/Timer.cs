using System;
using GameEngine;
using UnityEngine;
using View;

public class Timer : MonoBehaviour
{
    public ViewManager _viewManager;

    // Update is called once per frame
    void Update()
    {
        var state = _viewManager.GameEngine.mState;
        if (state == Game.GameState.Checkmate || state == Game.GameState.Draw)
        {
            return;
        }
        
        var currentPlayer = _viewManager.GameEngine.mCurrentPlayer;
        var timer = currentPlayer.DecrementTimer(Time.deltaTime);
        
        if (timer <= 0)
        {
            _viewManager.GameEngine.SetState(Game.GameState.Checkmate, _viewManager.GameEngine.GetOppositePlayer());
            if (_viewManager.CheckStopGame())
            {
                return;
            }
            
            _viewManager.GameEngine.TogglePlayer();
            _viewManager.GameEngine.GetMode().NextTurn(_viewManager.GameEngine.mCurrentPlayer);
            return;
        }

        var minutes = ((int)timer / 60).ToString("00");
        var seconds = Math.Round(timer % 60).ToString("00");
        _viewManager.GetTimer(currentPlayer.GetColor()).text = $"{minutes}:{seconds}";
    }
}
