using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System.Linq;

namespace TwoPm.TooltipDemo
{
    public class ObjectPrimaryClicked
    {
        public GameObject Object { get; set; }
    }

    public delegate void PointerEvent<T>(T data);
    public delegate void PointerAction();

    public class ControlSystem : MonoBehaviour
    {
        public GraphicRaycaster Raycaster;
        public EventSystem EventSystem;
        private PointerEventData _pointerEventData;

        public Camera Camera;
        public PointerViewController Pointer;

        public List<GameObject> HoveredObjects { get; private set; }
        public GameObject PrimaryHoveredObject => HoveredObjects.FirstOrDefault();
        public List<GameObject> HoveredUiObjects { get; private set; }
        public GameObject PrimaryHoveredUiObject => HoveredUiObjects.FirstOrDefault();

        public List<GameObject> DebugList;

        public PointerEvent<string> PointerTooltipUpdated;
        public PointerEvent<PointerDisplayModeChanged> PointerDisplayModeChanged;
        public PointerAction PointerDisplayModeReset;
        public PointerAction PrimaryMouseClicked; 
        public PointerAction SecondaryMouseClicked; 

        public PointerEvent<ObjectPrimaryClicked> ObjectPrimaryClicked;

        // Start is called before the first frame update
        void Start() {
            Assert.IsNotNull(Pointer);
            Assert.IsNotNull(Camera);
            Assert.IsNotNull(Raycaster);
            Assert.IsNotNull(EventSystem);

            HoveredObjects = new List<GameObject>();
            HoveredUiObjects = new List<GameObject>();

            PrimaryMouseClicked += () => {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Pointer.Position);

                if (Physics.Raycast(ray, out hit, 100)) {
                    var objectHit = hit.transform;

                    ObjectPrimaryClicked?.Invoke(new ObjectPrimaryClicked()
                    {
                        Object = objectHit.gameObject
                    });
                };
            };
        }

        public void OnPointerTooltipUpdated(string msg) {
            PointerTooltipUpdated?.Invoke(msg);
        }

        public void OnPointerDisplayModeChanged(PointerDisplayModeChanged e) {
            PointerDisplayModeChanged?.Invoke(e);
        }

        public void OnPointerDisplayModeReset() {
            PointerDisplayModeReset?.Invoke();
        }

        void FixedUpdate() {
            HoveredObjects.Clear();
            HoveredUiObjects.Clear();

            // Scene objects

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Pointer.Position);

            if (Physics.Raycast(ray, out hit, 100)) {
                HoveredObjects.Add(hit.transform.gameObject);
            };

            // UI objects

            _pointerEventData = new PointerEventData(EventSystem)
            {
                position = Pointer.Position
            };

            //Create a list of Raycast Results
            List<UnityEngine.EventSystems.RaycastResult> results = new List<UnityEngine.EventSystems.RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            Raycaster.Raycast(_pointerEventData, results);

            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            foreach (UnityEngine.EventSystems.RaycastResult result in results) {
                HoveredUiObjects.Add(result.gameObject);
            }

            DebugList = HoveredUiObjects;
        }

        // Update is called once per frame
        void Update() {
            if (Input.GetMouseButtonUp(0)) {
                PrimaryMouseClicked?.Invoke();
            }

            if (Input.GetMouseButtonUp(1)) {
                SecondaryMouseClicked?.Invoke();
            }
        }
    }

}