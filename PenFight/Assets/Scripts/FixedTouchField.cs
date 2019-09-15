using UnityEngine;
using UnityEngine.EventSystems;
using Cinemachine;

public class FixedTouchField : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector]
    public Vector2 TouchDist;
    [HideInInspector]
    public Vector2 PointerOld;
    [HideInInspector]
    protected int PointerId;
    [HideInInspector]
    public bool Pressed;

    public CinemachineVirtualCamera PenfollowVcam;

    public int PenID = 0; //0 - First player, 1 - Second Player ; By default let player 0 start first

    public GameObject[] PenToFlick; //Stores both the pens to flick

    // Update is called once per frame
    void Update()
    {
        if (Pressed)
        {
            if (PointerId >= 0 && PointerId < Input.touches.Length)
            {
                TouchDist = Input.touches[PointerId].position - PointerOld;
                PointerOld = Input.touches[PointerId].position;
            }
            else
            {
                TouchDist = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - PointerOld;
                PointerOld = Input.mousePosition;
            }
        }
        else
        {
            TouchDist = new Vector2();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
        PointerId = eventData.pointerId;
        PointerOld = eventData.position;

        //When touchscreen is pressed down, assign the PenID

        GameManager.GMStaticInstance.CurrentPenID = PenID;
        //When the button is pressed zoom the camera back to the pen
        PenfollowVcam.m_Follow = PenToFlick[PenID].transform;
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        //When our finger is released, we want the pen to flick
        PenToFlick[PenID].GetComponent<PenRot>().FlickPen(); //Flick the pen whose Pen ID is given

        //After the flick is done, change the penID
        if(PenID == 1 )
        {
            PenID = 0;
        }
        else if(PenID == 0)
        {
            PenID = 1;
        }

        Pressed = false;
    }

}