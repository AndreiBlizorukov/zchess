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
        
        public ViewManager viewManager;

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

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
            viewManager.HighLightCells(mCurrentCell);
        }

        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);
            transform.position += (Vector3) eventData.delta;
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            
            viewManager.ClearHighLightedCells();
            if (!viewManager.MovePiece(mCurrentCell, Input.mousePosition))
            {
                // return piece to the original place
                transform.position = mCurrentCell.transform.position;
                return;
            }
            
            viewManager.EndOfTurn();
        }
    }
}