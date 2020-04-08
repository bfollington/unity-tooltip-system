using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TwoPm.TooltipDemo
{
    public class Tooltip : MonoBehaviour
    {
        public Vector2 Offset;
        public PointerViewController Pointer;

        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void LateUpdate() {
            transform.position = Pointer.Position + Offset;
        }
    }
}