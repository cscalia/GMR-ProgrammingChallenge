using UnityEngine;
using UnityEngine.UI;

namespace Code.Table
{
    public class UITable : MonoBehaviour
    {
        [Header("Prefabs")]
        public Transform rowContainer;
        public Text textPrefab;

        public void AddTitle(string titleText)
        {
            Transform titleContainer = CreateRow();
            AddText(titleText, titleContainer, FontStyle.Bold);
        }

        public void AddHeaders(string[] headerElements)
        {
            AddElements(headerElements, CreateRow(), FontStyle.Bold);
        }

        public void AddContent(string[] rowElements)
        {
            AddElements(rowElements, CreateRow(), FontStyle.Normal);
        }

        private void AddElements(string[] elements, Transform rowContainer, FontStyle fontStyle)
        {
            for (int i = 0; i < elements.Length; i++)
            {
                AddText(elements[i], rowContainer, fontStyle);
            }
        }

        private Transform CreateRow()
        {
            return Instantiate(rowContainer, transform);
        }

        private void AddText(string text, Transform container, FontStyle fontStyle)
        {
            Text textInstance = Instantiate(textPrefab, container);
            textInstance.text = text;
            textInstance.fontStyle = fontStyle;
        }
    }
}