using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public interface IPiece
    {
        void Setup(Color newTeamColor, Color32 newSpriteColor, PieceManager newPieceManager);
        void Place(Cell newCell);
        void Kill();
        Color GetColor();
    }
}