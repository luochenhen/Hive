using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;
public class MyJoyStick : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public RectTransform BackRect;
    public RectTransform SelfRect;
    public float Radius;  //摇杆背景的半径
    public Vector2 Direction;  //摇杆的方向
    public bool IsMoving = false;
    public bool IsClicking = false;
    public bool isDraging = false;
    public PointerEventData.InputButton button;
    public Action<bool> JumpEvent;
    public Action<bool> DragEvent;
    public Action<Vector2,float> MoveEvent; //作为人物移动的事件调用
    float DragTime;
    float ClickTime;

    private bool isJumping;

    private void Awake()
    {
        Radius = BackRect.sizeDelta.x / 2;
    }

    void Update()
    {
        
            if (DragEvent != null)
            {
                DragEvent.Invoke(isDraging);
                
            }
                
        

        if(IsClicking)
        {
            if (JumpEvent != null)
            {
                //Debug.Log(IsClicking);
                JumpEvent.Invoke(IsClicking);
                IsClicking = false;
            }
        }
            
                
        if (IsMoving)
        {
            if(MoveEvent != null)
            {
                MoveEvent.Invoke(Direction, DragTime);
                //Debug.Log(IsMoving);
                IsMoving = false;
            }
        }
                     
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        //if (eventData.dragging == true)
        //{
        DragTime = Time.time;
        eventData.clickTime = 0;
    
        SelfRect.anchoredPosition = eventData.position - BackRect.anchoredPosition;
           
        //}
        //else if (eventData.dragging == false&&eventData.clickTime < 500 )
        //{
        //    //Debug.Log(eventData.pointerId);
        //    IsClicking = true;
        //} 
    }

    public void OnDrag(PointerEventData eventData)
    {
        SelfRect.anchoredPosition = eventData.position - BackRect.anchoredPosition;  //计算摇杆的正确位置

        if ((BackRect.position - SelfRect.position).magnitude > Radius)  //计算是否大于半径
            SelfRect.anchoredPosition = Vector2.ClampMagnitude(SelfRect.anchoredPosition, Radius);  //限制摇杆位置

        Direction = SelfRect.anchoredPosition.normalized;  //摇杆位置单位化后为摇杆方向
        isDraging = true;
        //Debug.Log(Direction);

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragTime = Time.time-DragTime;
        SelfRect.anchoredPosition = Vector2.zero;
        //Debug.Log(DragTime);
      
        //Debug.Log(Direction);
        IsMoving = true;
        isDraging = false;
        Debug.Log(isDraging);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ClickTime = Time.time;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ClickTime = Time.time - ClickTime;

        //Debug.Log(ClickTime);
        if (ClickTime < 0.2)
        {
            IsClicking = true;
        }

    }
}