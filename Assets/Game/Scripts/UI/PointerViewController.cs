using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace TwoPm.TooltipDemo
{
    public enum PointerMode
    {
        Mouse,
        Controller
    }

    public enum PointerDisplayMode
    {
        Normal,
        Locked
    }

    public class PointerTooltipUpdated
    {
        public string Message { get; set; }
    }

    public class PointerDisplayModeChanged
    {
        public PointerDisplayMode Mode { get; set; }
    }

    public class PointerDisplayModeReset { }

    public class PointerViewController : MonoBehaviour
    {
        public PointerMode Mode;
        public ControlSystem Control;

        public PointerDisplayMode DisplayMode = PointerDisplayMode.Normal;
        public GameObject NormalPointerDisplay;
        public GameObject LockedPointerDisplay;
        public float PointerSpeed;
        public Vector3 Offset;
        public string TooltipMessage;
        public TMP_Text TooltipText;
        public GameObject TooltipDisplay;

        private Vector3 _pointerPosition;

        public Vector2 Position => _pointerPosition;
        private PointerDisplayModeChanged _defaultConfig = new PointerDisplayModeChanged()
        {
            Mode = PointerDisplayMode.Normal,
        };

        // Start is called before the first frame update
        void Start() {
            Assert.IsNotNull(Control);
            Assert.IsNotNull(TooltipText);
            Assert.IsNotNull(TooltipDisplay);
            Assert.IsNotNull(NormalPointerDisplay);
            Assert.IsNotNull(LockedPointerDisplay);

            Control.PointerTooltipUpdated += (msg) => {
                TooltipMessage = msg;
            };

            Control.PointerDisplayModeChanged += ChangeDisplayMode;
            Control.PointerDisplayModeReset += () => {
                ChangeDisplayMode(_defaultConfig);
            };

            ChangeDisplayMode(_defaultConfig);
        }

        private void ChangeDisplayMode(PointerDisplayModeChanged change) {
            View.Hide(NormalPointerDisplay);
            View.Hide(LockedPointerDisplay);

            switch (change.Mode) {
                case PointerDisplayMode.Normal:
                    View.Show(NormalPointerDisplay);
                    break;
                case PointerDisplayMode.Locked:
                    View.Show(LockedPointerDisplay);
                    break;
            }
        }

        // Update is called once per frame
        void LateUpdate() {
            switch (Mode) {
                case PointerMode.Mouse:
                    _pointerPosition = Input.mousePosition;
                    Cursor.visible = false;
                    break;
                case PointerMode.Controller:
                    _pointerPosition += Time.deltaTime * PointerSpeed * new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
                    Cursor.visible = true;
                    break;
            }

            transform.position = _pointerPosition + Offset; // TODO: consider  * _settings.Settings.UiScale here

            if (!string.IsNullOrEmpty(TooltipMessage)) {
                View.Show(TooltipDisplay);
                TooltipText.text = TooltipMessage;
            }
            else {
                View.Hide(TooltipDisplay);
            }
        }
    }
}