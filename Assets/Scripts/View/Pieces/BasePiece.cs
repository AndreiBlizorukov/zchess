using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace View.Pieces
{
    public abstract class BasePiece : EventTrigger
    {
        public Color mColor = Color.clear;
        public Cell mOriginalCell;
        public Cell mCurrentCell;

        protected List<Cell> mHighlightedCells = new List<Cell>();

        public virtual void Setup(Color newTeamColor, Color32 newSpriteColor)
        {
            mColor = newTeamColor;
            GetComponent<Image>().color = newSpriteColor;
        }

        public void Place(Cell newCell)
        {
            mCurrentCell = newCell;
            mOriginalCell = newCell;

            transform.position = newCell.transform.position;
            gameObject.SetActive(true);
        }

        protected void ShowCells()
        {
            foreach (var cell in mHighlightedCells)
            {
                cell.mOutlineImage.enabled = true;
            }
        }

        protected void ClearCells()
        {
            foreach (var cell in mHighlightedCells)
            {
                cell.mOutlineImage.enabled = false;
            }

            mHighlightedCells.Clear();
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);

            //mHighlightedCells = mPieceManager.ValidateAvailableCells(this, CheckPath());
            ShowCells();
        }

        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);
            transform.position += (Vector3) eventData.delta;
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);

            /*
            foreach (var toCell in mHighlightedCells)
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(toCell.mRectTransform, Input.mousePosition))
                {
                    //mPieceManager.MovePiece(this, toCell);
                    
                    // trying to check if after enemy's move the king is under attack
                    mPieceManager.mIsKingUnderAttack = IsKingUnderAttack();
                    mPieceManager.SwitchSides(mColor);

                    ClearCells();
                    return;
                }
            }*/

            ClearCells();
            transform.position = mCurrentCell.transform.position;
        }
    }
}