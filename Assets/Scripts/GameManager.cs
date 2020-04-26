using GameEngine;
using GameEngine.Mode;
using GameEngine.BoardSchema;
using GameEngine.Logic;
using GameEngine.Player;
using UnityEngine;
using View;

public class GameManager : MonoBehaviour
{
    public ViewManager ViewManager;

    void Start()
    {
        var gameEngine = Game.GetInstance();
        var mode = new HumanVersusComputer(
            new Default(),
            new Human(Color.white, 300), 
            new EasiestBot(Color.black,  new Easiest(), 300),
            ViewManager
        );
        
        gameEngine.Setup(mode);

        ViewManager.CreateBoard();
        ViewManager.Setup(gameEngine);
    }
}