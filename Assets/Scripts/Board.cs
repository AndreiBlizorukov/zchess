using Pieces;
using UnityEngine;
using UnityEngine.UI;

public enum CellState
{
    None,
    Friendly,
    Enemy,
    Free,
    OutOfBounds
}

public class Board : MonoBehaviour
{
    public GameObject mCellPrefab;

    [HideInInspector]
    public Cell[,] mAllCells = new Cell[8, 8];
    
    // Start is called before the first frame update
    public void Create()
    {
        for (var y = 0; y < 8; y++)
        {
            for (var x = 0; x < 8; x++)
            {
                var newCell = Instantiate(mCellPrefab, transform);
                var rectTransform = newCell.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2((x * 100) + 50, (y * 100) + 50);

                mAllCells[x, y] = newCell.GetComponent<Cell>();
                mAllCells[x, y].Setup(new Vector2Int(x, y), this);

                if ((x + y) % 2 == 0)
                {
                    mAllCells[x, y].GetComponent<Image>().color = new Color32(230, 220, 187, 255);
                }
            }
        }
    }

    public CellState ValidateCell(int targetX, int targetY, IPiece piece)
    {
        if (targetX < 0 || targetX > 7)
        {
            return CellState.OutOfBounds;
        }
        
        if (targetY < 0 || targetY > 7)
        {
            return CellState.OutOfBounds;
        }

        Cell targetCell = mAllCells[targetX, targetY];
        if (targetCell.mCurrentPiece != null)
        {
            return piece.GetColor() == targetCell.mCurrentPiece.GetColor() 
                ? CellState.Friendly 
                : CellState.Enemy;
        }

        return CellState.Free;
    }
}
