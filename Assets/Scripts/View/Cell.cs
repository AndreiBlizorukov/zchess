using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class Cell : MonoBehaviour
    {
        public Image mOutlineImage;
        public RectTransform mRectTransform;
        public Vector2Int position;
        public Pieces.BasePiece mCurrentPiece;

        public void Setup()
        {
            mRectTransform = GetComponent<RectTransform>();
        }

        public void SetPosition(int x, int y)
        {
            position.x = x;
            position.y = y;
        }
    }
}