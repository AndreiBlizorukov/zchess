using UnityEngine;
using UnityEngine.UI;

namespace Pieces
{
    public class Pawn : BasePiece
    {
        public bool IsFirstMove = true;

        public override void Setup(Color newTeamColor, Color32 newSpriteColor, PieceManager newPieceManager)
        {
            base.Setup(newTeamColor, newSpriteColor, newPieceManager);
            IsFirstMove = true;

            mMovement = mColor == Color.white
                ? new Vector3Int(0, 1, 1)
                : new Vector3Int(0, -1, -1);

            GetComponent<Image>().sprite = Resources.Load<Sprite>("T_Pawn");
        }

        private bool MatchesState(int targetX, int targetY, CellState targetState)
        {
            var state = mCurrentCell.mBoard.ValidateCell(targetX, targetY, this);
            if (state == targetState)
            {
                mHightLightedCells.Add(mCurrentCell.mBoard.mAllCells[targetX, targetY]);
                return true;
            }

            return false;
        }

        protected override void CheckPath()
        {
            int currentX = mCurrentCell.mBoardPosition.x;
            int currentY = mCurrentCell.mBoardPosition.y;

            // top left
            MatchesState(currentX - mMovement.z, currentY - mMovement.z, CellState.Enemy);

            //forward
            if (MatchesState(currentX, currentY + mMovement.y, CellState.Free))
            {
                if (IsFirstMove)
                {
                    MatchesState(currentX, currentY + mMovement.y * 2, CellState.Free);
                }
            }

            // top right
            MatchesState(currentX + mMovement.z, currentY + mMovement.z, CellState.Enemy);
        }

        public override void Move()
        {
            base.Move();
            IsFirstMove = false;
        }
    }
}