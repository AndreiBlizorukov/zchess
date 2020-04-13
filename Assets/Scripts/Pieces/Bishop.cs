using UnityEngine;
using UnityEngine.UI;

namespace Pieces
{
    public class Bishop : BasePiece
    {
        public override void Setup(Color newTeamColor, Color32 newSpriteColor, PieceManager newPieceManager)
        {
            base.Setup(newTeamColor, newSpriteColor, newPieceManager);
            
            mMovement = new Vector3Int(0, 0, 7);
            GetComponent<Image>().sprite = Resources.Load<Sprite>("T_Bishop");
        }
    }
}