using System.Collections.Generic;
using UnityEngine;

namespace View
{
    public class ColorSchema
    {
        public static readonly Dictionary<Color, Color32> PieceColors = new Dictionary<Color, Color32>
        {
            {Color.white, new Color32(80, 124, 159, 255)},
            {Color.black, new Color32(210, 95, 64, 255)}
        };
    }
}