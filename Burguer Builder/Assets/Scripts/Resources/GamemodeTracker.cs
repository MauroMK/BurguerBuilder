using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamemodeTracker : MonoBehaviour
{
    public bool randomOrderMode;

    private void Awake() 
    {
        DontDestroyOnLoad(gameObject);   
    }
}
