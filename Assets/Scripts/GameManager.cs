using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Board mBoard;

    public PieceManager PieceManager;
    // Start is called before the first frame update
    void Start()
    {
        mBoard.Create();
        PieceManager.Setup(mBoard);
        StartGame();
    }

    private void StartGame()
    {
        PieceManager.StartGame();
        // @todo set timer
    }
}
