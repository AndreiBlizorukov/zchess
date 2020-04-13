using UnityEngine;
using UnityEngine.UI;

namespace Pieces
{
    public class Rook : BasePiece
    {
        public override void Setup(Color newTeamColor, Color32 newSpriteColor, PieceManager newPieceManager)
        {
            base.Setup(newTeamColor, newSpriteColor, newPieceManager);
            
            mMovement = new Vector3Int(7, 7, 0);
            GetComponent<Image>().sprite = Resources.Load<Sprite>("T_Rook");
        }
    }
}