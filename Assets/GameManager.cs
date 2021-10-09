using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform firstBg;
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }
}
