using UnityEngine;
using UnityEngine.UI;

namespace Pieces
{
    public class Knight : BasePiece
    {
        public override void Setup(Color newTeamColor, Color32 newSpriteColor, PieceManager newPieceManager)
        {
            base.Setup(newTeamColor, newSpriteColor, newPieceManager);
            GetComponent<Image>().sprite = Resources.Load<Sprite>("T_Knight");
        }

        protected override void CheckPath()
        {
            CreateCellPath(1);
            CreateCellPath(-1);
        }
        
        private void CreateCellPath(int flipper)
        {
            int currentX = mCurrentCell.mBoardPosition.x;
            int currentY = mCurrentCell.mBoardPosition.y;

            MatchesState(currentX - 2, currentY + flipper);
            MatchesState(currentX - 1, currentY + 2 * flipper);
            MatchesState(currentX + 1, currentY + 2 * flipper);
            MatchesState(currentX + 2, currentY + flipper);
        }

        private void MatchesState(int targetX, int targetY)
        {
            var state = mCurrentCell.mBoard.ValidateCell(targetX, targetY, this);
            if (state != CellState.Friendly && state != CellState.OutOfBounds)
            {
                mHightLightedCells.Add(mCurrentCell.mBoard.mAllCells[targetX, targetY]);
            }
        }
    }
}