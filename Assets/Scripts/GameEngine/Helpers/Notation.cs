using System;
using System.Collections.Generic;
using GameEngine.Pieces;

namespace GameEngine.Helpers
{
    public class Notation
    {
        public const string Pawn = "P";
        public const string Rook = "R";
        public const string Knight = "Kn";
        public const string Bishop = "B";
        public const string King = "K";
        public const string Queen = "Q";

        private Dictionary<string, Type> _map = new Dictionary<string, Type>()
        {
            {"P", typeof(Pawn)},
            {"R", typeof(Pawn)},
            {"Kn", typeof(Pawn)},
            {"B", typeof(Pawn)},
            {"K", typeof(Pawn)},
            {"Q", typeof(Pawn)},
        };

    }
}