using GameEngine;
using GameEngine.Mode;
using GameEngine.PiecesSchema;
using GameEngine.Player;
using UnityEngine;
using View;

public class GameManager : MonoBehaviour
{
    public ViewManager ViewManager;

    void Start()
    {
        var gameEngine = Game.GetInstance();
        var mode = new VersusHuman(
            new Default(), 
            new Human(Color.white, 300),
            new Human(Color.black, 300)
        );
        
        gameEngine.Setup(mode);

        ViewManager.CreateBoard();
        ViewManager.Setup(gameEngine);
    }
}