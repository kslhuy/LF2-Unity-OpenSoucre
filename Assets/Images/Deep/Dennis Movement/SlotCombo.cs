
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.InputSystem.Layouts;

////TODO: custom icon for OnScreenStick component


/// <summary>
/// A stick control displayed on screen and moved around by touch or other pointer
/// input.
/// </summary>
namespace UnityEngine.InputSystem.OnScreen
{
    public class SlotCombo : MonoBehaviour ,IPointerDownHandler,ISelectHandler,IPointerEnterHandler
    {

        private RectTransform rectTransform;
        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData == null)
                throw new System.ArgumentNullException(nameof(eventData));
            Debug.Log("OnpointerDown");
            // RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponentInParent<RectTransform>(), eventData.position, eventData.pressEventCamera, out m_PointerDownPos);
            // Debug.Log(m_PointerDownPos); 
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("OnPointerEnter");
        }

        // public void OnDrag(PointerEventData eventData)
        // {
        //     if (eventData == null)
        //         throw new System.ArgumentNullException(nameof(eventData));

        //     RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponentInParent<RectTransform>(), eventData.position, eventData.pressEventCamera, out var position);
        //     var delta = position - m_PointerDownPos;

        //     delta = Vector2.ClampMagnitude(delta, movementRange);
        //     ((RectTransform)transform).anchoredPosition = m_StartPos + (Vector3)delta;

        //     var newPos = new Vector2(delta.x / movementRange, delta.y / movementRange);
        //     SendValueToControl(newPos);
        //     Debug.Log(newPos); 

        // }

        // public void OnPointerUp(PointerEventData eventData)
        // {
        //     ((RectTransform)transform).anchoredPosition = m_StartPos;
        //     SendValueToControl(Vector2.zero);
        // }

        public void OnSelect(BaseEventData eventData)
        {
            Debug.Log("OnSelect");
        }
        private void Start()
        {
            // m_StartPos = ((RectTransform)transform).anchoredPosition;

        }


        // public float movementRange
        // {
        //     get => m_MovementRange;
        //     set => m_MovementRange = value;
        // }

        // [FormerlySerializedAs("movementRange")]
        // [SerializeField]
        // private float m_MovementRange = 50;

        // [InputControl(layout = "Vector2")]
        // [SerializeField]
        // private string m_ControlPath;

        // private Vector3 m_StartPos;
        // private Vector2 m_PointerDownPos;

        // protected override string controlPathInternal
        // {
        //     get => m_ControlPath;
        //     set => m_ControlPath = value;
        // }
    }
}