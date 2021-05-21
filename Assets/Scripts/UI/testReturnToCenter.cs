using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class testReturnToCenter : MonoBehaviour
{
    private Animator _myAN = null;
    [SerializeField] private MoveToLocation _myRTC = null;

    void Start()
    {
        _myAN = GetComponent<Animator>();
    }

    

    public void OpenPanel()
    {
        _myRTC.TravelToCenter(OpenComms);
    }

    private void OpenComms()
    {
        _myAN.SetBool("Appear", true);
    }
}
