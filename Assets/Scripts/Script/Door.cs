using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool DoorOpended=false;
    public int ThisDoor = 0;  // 0 염소, 1 자동차
    public DoorHandler _doorhandler;

   
    // Start is called before the first frame update
    void Start()
    {
        _doorhandler = DoorHandler.DoorH;
    }

    // Update is called once per frame  
    void Update()
    {

    }
    public void OnMouseDown()
    {

        if(!_doorhandler.FirstClicked)
        {
            _doorhandler.MohntiholAction();
            return;
        }
        Debug.Log("Door Opened");
        DoorOpended = true;
        if (ThisDoor == 0)
        {
            Debug.Log("염소");
            this.GetComponent<SpriteRenderer>().color = new Color(0, 0, 255, 255);
        }
        else if(ThisDoor == 1)
        {
            Debug.Log("자동차");
            this.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 255);
        }

        if(_doorhandler.FirstClicked==true)
        {
            Debug.Log("게임오버");
        }
    }
 
}
