using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Pieces
{
    public abstract class BasePiece : EventTrigger, IPiece
    {
        public Color mColor = Color.clear;
        public Cell mOriginalCell;
        public Cell mCurrentCell;
        public Cell mTargetCell;

        public RectTransform mRectTransform;
        public PieceManager mPieceManager;

        protected Vector3Int mMovement = Vector3Int.one;
        protected List<Cell> mHightLightedCells = new List<Cell>();

        public virtual void Setup(Color newTeamColor, Color32 newSpriteColor, PieceManager newPieceManager)
        {
            mColor = newTeamColor;
            mPieceManager = newPieceManager;
            GetComponent<Image>().color = newSpriteColor;
            mRectTransform = GetComponent<RectTransform>();
        }

        public void Place(Cell newCell)
        {
            mCurrentCell = newCell;
            mOriginalCell = newCell;
            mOriginalCell.mCurrentPiece = this;

            transform.position = newCell.transform.position;
            gameObject.SetActive(true);
        }

        public virtual void Move()
        {
            mTargetCell.RemovePiece();
            mCurrentCell.mCurrentPiece = null;
            mCurrentCell = mTargetCell;
            mCurrentCell.mCurrentPiece = this;

            transform.position = mCurrentCell.transform.position;
            mTargetCell = null;
        }

        private void CreateCellPath(int xDirection, int yDirection, int movement)
        {
            int currentX = mCurrentCell.mBoardPosition.x;
            int currentY = mCurrentCell.mBoardPosition.y;

            for (int i = 1; i <= movement; i++)
            {
                currentX += xDirection;
                currentY += yDirection;

                var state = mCurrentCell.mBoard.ValidateCell(currentX, currentY, this);
                if (state == CellState.Enemy)
                {
                    mHightLightedCells.Add(mCurrentCell.mBoard.mAllCells[currentX, currentY]);
                    break;
                }

                if (state != CellState.Free)
                {
                    break;
                }

                mHightLightedCells.Add(mCurrentCell.mBoard.mAllCells[currentX, currentY]);
            }
        }

        protected virtual void CheckPath()
        {
            // horizontal
            CreateCellPath(1, 0, mMovement.x);
            CreateCellPath(-1, 0, mMovement.x);

            // vertical
            CreateCellPath(0, 1, mMovement.y);
            CreateCellPath(0, -1, mMovement.y);

            // upper horizontal
            CreateCellPath(1, 1, mMovement.z);
            CreateCellPath(-1, 1, mMovement.z);

            // lower horizontal
            CreateCellPath(-1, -1, mMovement.z);
            CreateCellPath(1, -1, mMovement.z);
        }

        protected void ShowCells()
        {
            foreach (var cell in mHightLightedCells)
            {
                cell.mOutlineImage.enabled = true;
            }
        }

        protected void ClearCells()
        {
            foreach (var cell in mHightLightedCells)
            {
                cell.mOutlineImage.enabled = false;
            }

            mHightLightedCells.Clear();
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);

            CheckPath();
            ShowCells();
        }

        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);
            transform.position += (Vector3) eventData.delta;

            foreach (var cell in mHightLightedCells)
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(cell.mRectTransform, Input.mousePosition))
                {
                    mTargetCell = cell;
                    return;
                }
            }

            mTargetCell = null;
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            ClearCells();

            if (!mTargetCell)
            {
                transform.position = mCurrentCell.transform.position;
                return;
            }

            Move();
            mPieceManager.SwitchSides(mColor);
        }

        public void Reset()
        {
            Kill();
            Place(mOriginalCell);
        }


        public virtual void Kill()
        {
            mCurrentCell.mCurrentPiece = null;
            gameObject.SetActive(false);
        }

        public Color GetColor()
        {
            return mColor;
        }
    }
}