using UnityEngine;

namespace Pieces
{
    public interface IPiece
    {
        void Setup(Color newTeamColor, Color32 newSpriteColor, PieceManager newPieceManager);
        void Place(Cell newCell);
        void Kill();
        Color GetColor();
    }
}