using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Assertions;

namespace TwoPm.TooltipDemo
{
    public delegate void PointerReactor();

    public class ReactToPointer : MonoBehaviour
    {
        // public EventManager2 Events;
        public ControlSystem Control;
        public PointerReactor OnPointerEntered;
        public PointerReactor OnPointerExited;
        public PointerReactor OnPointerSelected;

        public string TooltipMessage;
        public bool Disabled = false;

        public bool _focused = false;
        public bool HasFocus => _focused;

        private float _hoverDelayTimer = 0;

        public float HoverDelay = 0;
        private bool _delayElapsed;
        private bool _focusTriggered = false;

        // Start is called before the first frame update
        void Start() {
            Assert.IsNotNull(Control);

            Control.PrimaryMouseClicked += () => {
                if (!_focused || !gameObject.activeInHierarchy) return;
                OnPointerSelected?.Invoke();
            };
        }

        void OnDisable() {
            OnPointerExited?.Invoke();
            Control.OnPointerTooltipUpdated("");
        }

        // Update is called once per frame
        void Update() {
            var focused = false;
            if (Control.PrimaryHoveredUiObject == null && Control.PrimaryHoveredObject == gameObject) {
                focused = true;
            }

            if (Control.HoveredUiObjects.Contains(gameObject)) {
                focused = true;
            }

            // Gained focus
            if (_delayElapsed && focused && !_focusTriggered) {
                _focusTriggered = true;
                OnPointerEntered?.Invoke();

                if (!string.IsNullOrEmpty(TooltipMessage)) {
                    Control.OnPointerTooltipUpdated(TooltipMessage);
                }

                if (Disabled) {
                    Control.OnPointerDisplayModeChanged(new PointerDisplayModeChanged() {
                        Mode = PointerDisplayMode.Locked
                    });
                }
            }


            // Lost focus
            if (_focused && !focused) {
                OnPointerExited?.Invoke();

                if (!string.IsNullOrEmpty(TooltipMessage)) {
                    Control.OnPointerTooltipUpdated("");
                }

                Control.OnPointerDisplayModeReset();
            }

            if (focused) {
                _hoverDelayTimer += Time.deltaTime;

                if (_hoverDelayTimer >= HoverDelay) {
                    _delayElapsed = true;
                }
            }
            else {
                _hoverDelayTimer = 0;
                _delayElapsed = false;
                _focusTriggered = false;
            }

            _focused = focused;
        }
    }
}