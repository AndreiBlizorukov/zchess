using UnityEngine;
using View;

public class GameManager : MonoBehaviour
{
    public PieceManager pieceManager;
    void Start()
    {
        var engine = new GameEngine.Game();
        engine.Setup();

        pieceManager.CreateBoard();
        pieceManager.Setup(engine);
    }
}