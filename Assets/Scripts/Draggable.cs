using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DOW
{
    public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public bool IsDraggable { get; set; } = false;
        public Transform Beacon {get; set;} // drag할 때 위치
        private Transform parentTransform; // drag끝났을 때 원래 위치

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!IsDraggable) return;
            Debug.Log("OnBeginDrag");
            parentTransform = transform.parent;
            transform.SetParent(Beacon);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!IsDraggable) return;
            Debug.Log("OnDrag");
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!IsDraggable) return;
            Debug.Log("OnEndDrag");
            transform.SetParent(parentTransform);
        }
    }
}