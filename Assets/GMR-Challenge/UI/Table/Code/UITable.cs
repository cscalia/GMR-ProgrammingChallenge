using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Table
{
    public class UITable : MonoBehaviour
    {
        [Header("References")]
        [SerializeField]
        private Transform content;
        
        [Header("Prefabs")] 
        [SerializeField]
        private Transform rowContainer;
        [SerializeField]
        private Text contentTextPrefab;
        [SerializeField]
        private Text headerTextPrefab;
        
        private List<GameObject> childs = new List<GameObject>();

        public void AddTitle(string titleText)
        {
            Transform titleContainer = CreateRow();
            AddText(titleText, titleContainer, headerTextPrefab);
        }

        public void AddHeaders(string[] headerElements)
        {
            AddElements(headerElements, CreateRow(), headerTextPrefab);
        }

        public void AddContent(string[] rowElements)
        {
            AddElements(rowElements, CreateRow(), contentTextPrefab);
        }

        public void Clear()
        {
            foreach (var item in childs)
            {
                Destroy(item);
            }
            childs.Clear();
        }

        private void AddElements(string[] elements, Transform rowContainer, Text textStyle)
        {
            for (int i = 0; i < elements.Length; i++)
            {
                AddText(elements[i], rowContainer, textStyle);
            }
        }

        private Transform CreateRow()
        {
            Transform container = Instantiate(rowContainer, content);
            childs.Add(container.gameObject);
            return container;
        }

        private void AddText(string text, Transform container, Text textStyle)
        {
            Text textInstance = Instantiate(textStyle, container);
            textInstance.text = text;
        }
    }
}