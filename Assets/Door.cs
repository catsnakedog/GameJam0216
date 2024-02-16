using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool DoorOpended=false;
    public int ThisDoor = 0;  // 0 ����, 1 �ڵ���
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame  
    void Update()
    {

    }
    public void OnMouseDown()
    {
        Debug.Log("Door Opened");
        DoorOpended = true;
        if (ThisDoor == 0)
        {
            Debug.Log("����");
            this.GetComponent<SpriteRenderer>().color = new Color(0, 0, 255, 255);
        }
        else if(ThisDoor == 1)
        {
            Debug.Log("�ڵ���");
            this.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 255);
        }
    }
 
}
