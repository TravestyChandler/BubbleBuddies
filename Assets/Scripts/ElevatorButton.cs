using UnityEngine;
using System.Collections;

public class ElevatorButton : MonoBehaviour {
    public Elevator connectedElevator;
    int playerOnCount = 0;
    bool platformStay = false;
    public Sprite up, down;
	// Use this for initialization
	void Start () {
        connectedElevator.enabled = false;

    }

    // Update is called once per frame
    void Update () {
	
	}

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Player"))
        {
            playerOnCount++;
            if (playerOnCount > 0)
            {
                connectedElevator.enabled = true;
                this.GetComponent<SpriteRenderer>().sprite = down;
                connectedElevator.MoveUp();
            }
        }
        else
        {
            return;
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag.Equals("Player"))
        {
            if(playerOnCount == 2)
            {
                platformStay = true;
            }
            playerOnCount--;
            if (playerOnCount == 0)
            {
                if (!platformStay)
                {
                    connectedElevator.enabled = false;
                    this.GetComponent<SpriteRenderer>().sprite = up;
                    connectedElevator.MoveDown();
                }
            }
        }
        else
        {
            return;
        }
    }
}
