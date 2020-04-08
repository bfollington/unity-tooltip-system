using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace TwoPm.TooltipDemo {
    public class TextPanelViewController : MonoBehaviour
    {
        public TMP_Text Text;
        public UnityEngine.UI.Image Image;

        public string Label
        {
            get
            {
                return Text.text;
            }

            set
            {
                Text.text = value;
            }
        }

        public float Padding = 10f;

        public bool Center = false;
        private Vector2 _initialPosition;
        private Vector2 _initialSize;


        // Start is called before the first frame update
        void Start() {
            Assert.IsNotNull(Text);
            Assert.IsNotNull(Image);

            _initialPosition = (transform as RectTransform).anchoredPosition;
            _initialSize = Image.rectTransform.sizeDelta - Vector2.right * Padding;
        }

        // Update is called once per frame
        void Update() {
            Image.rectTransform.sizeDelta = new Vector2(Text.renderedWidth + Padding, 30);

            if (Center) {
                var sizeDiff = Image.rectTransform.sizeDelta - _initialSize;
                (transform as RectTransform).anchoredPosition = _initialPosition - new Vector2(sizeDiff.x / 2f, 0);
            }
        }
    }
}