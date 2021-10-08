using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;
public class MyJoyStick : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public RectTransform BackRect;
    public RectTransform SelfRect;
    public float Radius;  //ҡ�˱����İ뾶
    public Vector2 Direction;  //ҡ�˵ķ���
    public bool IsMoving = false;
    public bool IsClicking = false;
    public bool isDraging = false;
    public PointerEventData.InputButton button;
    public Action<bool> JumpEvent;
    public Action<bool> DragEvent;
    public Action<Vector2,float> MoveEvent; //��Ϊ�����ƶ����¼�����
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
        SelfRect.anchoredPosition = eventData.position - BackRect.anchoredPosition;  //����ҡ�˵���ȷλ��

        if ((BackRect.position - SelfRect.position).magnitude > Radius)  //�����Ƿ���ڰ뾶
            SelfRect.anchoredPosition = Vector2.ClampMagnitude(SelfRect.anchoredPosition, Radius);  //����ҡ��λ��

        Direction = SelfRect.anchoredPosition.normalized;  //ҡ��λ�õ�λ����Ϊҡ�˷���
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