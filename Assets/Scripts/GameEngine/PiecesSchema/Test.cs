using GameEngine.Pieces;
using static UnityEngine.Color;

namespace GameEngine.PiecesSchema
{
    public class Test : IPiecesSchema
    {
        public IPiece[,] CreatePieces()
        {
            return new IPiece[,]
            {
                {null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null},
                {new Pawn(white), new Pawn(white), new Pawn(white), null, null, null, null, null},
            };
        }
    }
}