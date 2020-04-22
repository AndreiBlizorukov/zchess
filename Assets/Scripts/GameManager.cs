using GameEngine;
using GameEngine.GameMode;
using GameEngine.Player;
using UnityEngine;
using View;

public class GameManager : MonoBehaviour
{
    public ViewManager ViewManager;

    void Start()
    {
        var gameEngine = Game.GetInstance();
        var mode = new Default();
        gameEngine.Setup(
            mode,
            new Human(Color.white),
            new Human(Color.black)
        );

        ViewManager.CreateBoard();
        ViewManager.Setup(gameEngine);
    }
}