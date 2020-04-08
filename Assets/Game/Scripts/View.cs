using System;
using UnityEngine;

namespace TwoPm.TooltipDemo
{
    public static class View
    {
        public static void Hide(GameObject o) {
            if (o.activeSelf) {
                o.SetActive(false);
            }
        }

        public static void Hide(MonoBehaviour o) {
            Hide(o.gameObject);
        }

        public static void Show(GameObject o) {
            if (!o.activeSelf) {
                o.SetActive(true);
            }
        }

        public static void Show(MonoBehaviour o) {
            Show(o.gameObject);
        }

        public static void Destroy(MonoBehaviour o) {
            GameObject.Destroy(o.gameObject);
        }

        public static void ForEachChild(GameObject o, Action<int, GameObject> action) {
            for (var i = 0; i < o.transform.childCount; i++) {
                var child = o.transform.GetChild(i);
                action(i, child.gameObject);
            }
        }

        public static void ForEachChild(MonoBehaviour mb, Action<int, GameObject> action) {
            ForEachChild(mb.gameObject, action);
        }

        public static void ForEachChildComponent<T>(GameObject o, Action<int, T> action) {
            var components = o.gameObject.GetComponentsInChildren<T>();

            for (var i = 0; i < components.Length; i++) {
                action(i, components[i]);
            }
        }

        public static void ForEachChildComponent<T>(MonoBehaviour mb, Action<int, T> action) {
            ForEachChildComponent(mb.gameObject, action);
        }

        public static void ForEachChildComponent<T>(GameObject o, Action<int, T> action, bool includeInactive) {
            var components = o.gameObject.GetComponentsInChildren<T>(includeInactive);

            for (var i = 0; i < components.Length; i++) {
                action(i, components[i]);
            }
        }

        public static void ForEachChildComponent<T>(MonoBehaviour mb, Action<int, T> action, bool includeInactive) {
            var o = mb.gameObject;
            ForEachChildComponent(o, action, includeInactive);
        }
    }
}