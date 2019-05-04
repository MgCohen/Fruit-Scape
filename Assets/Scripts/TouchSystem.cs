using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;


/// <summary>
/// USE FOR GENERIC SCREEN COMMANDS
/// FOR TARGET SPECIFIC COMMANDS CUSTOMIZE TO OWN CONTROLLER
/// </summary>
public class TouchSystem : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler
{
    public static TouchSystem instance;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    public bool Enabled;

    [Header("Triggers")]
    public static UnityEvent Swipe = new UnityEvent();
    public static UnityEvent Touch = new UnityEvent();
    public static UnityEvent Doubletouch = new UnityEvent();
    public static UnityEvent Hold = new UnityEvent();

    [Header("last touch info")]
    public float lastTouchTime;
    public Vector2 lastTouchPos;
    public float touchTime;

    [Header("touch parameters")]
    public static int touchCount;
    public static bool swipped;
    public static Vector2 swipeDirection = new Vector2();
    public static Vector2 currentPointerPos = new Vector2();

    public void OnDrag(PointerEventData eventData)
    {
        currentPointerPos = eventData.position;
        swipped = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        lastTouchTime = Time.time;
        lastTouchPos = eventData.position;
        touchCount = eventData.clickCount;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!swipped)
        {
            if (lastTouchTime + touchTime <= Time.time)
            {
                //Debug.Log("Holded");
                Hold.Invoke();
            }
            else
            {
                if (eventData.clickCount == 1)
                {
                    //Debug.Log("Click");
                    Touch.Invoke();
                }
                else
                {
                    //Debug.Log("Double Click");
                    Doubletouch.Invoke();
                }
            }
        }
        else
        {
            //Debug.Log("Swiped");
            swipeDirection = eventData.position - lastTouchPos;
            Swipe.Invoke();
        }
        swipped = false;
    }


}
