using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class Cell : MonoBehaviour
    {
        public Image mOutlineImage;
        public RectTransform mRectTransform;
    
        public void Setup()
        {
            mRectTransform = GetComponent<RectTransform>();
        }
    }
}
