using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform player1;
    public Transform player2;
    public float  previousY;
    public float yDistance = 0.1f;
    public float moveDistance = 0.025f;
	// Use this for initialization
	void Start () {
        previousY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        float currentY = (player1.position.y + player2.position.y) / 2f;
        float difference = Mathf.Abs(currentY - previousY);
        if (difference > yDistance)
        {
            AdjustCamera(previousY, currentY);
            previousY = currentY;
        }
        
	}

    public void AdjustCamera(float y1, float y2)
    {
        if(y1 > y2)
        {
            float moveDist = Mathf.Clamp(y1 - y2, 0f, moveDistance);
            transform.Translate(new Vector3(0f, -moveDist, 0f));
        }
        else
        {
            float moveDist = Mathf.Clamp(y2 - y1, 0f, moveDistance);
            transform.Translate(new Vector3(0f, moveDist, 0f));
        }
    }
}
