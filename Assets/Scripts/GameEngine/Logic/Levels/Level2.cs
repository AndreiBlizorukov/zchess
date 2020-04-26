using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameEngine.Logic.Levels
{
    public class Level2 : IlogicEngine
    {
        public Move GetNextMove(Color color, Game gameEngine)
        {
            var myPiecePositions = gameEngine.GetBoard().GetPiecesPositions(color).ToList();
            var availableMoves = new List<Move>();
            for (var i = 0; i < myPiecePositions.Count; i++)
            {
                var myPiecePosition = myPiecePositions[i];
                foreach (var availableMove in gameEngine.GetAvailableCells(myPiecePosition))
                {
                    availableMoves.Add(new Move(myPiecePosition, availableMove));
                }
            }
            
            if (availableMoves.Any())
            {
                var enemyPositions = gameEngine.GetBoard().GetPiecesPositions(gameEngine.GetOppositeColor(color)).ToList();
                foreach (var availableMove in availableMoves)
                {
                    foreach (var enemyPosition in enemyPositions)
                    {
                        if (enemyPosition.x == availableMove.Target.x && enemyPosition.y == availableMove.Target.y)
                        {
                            return availableMove;
                        }
                    }
                }
                
                var rand = new System.Random();
                return availableMoves.ElementAt(rand.Next(availableMoves.Count));
            }

            return new Move(Vector2Int.zero, Vector2Int.zero);
        }
    }
}