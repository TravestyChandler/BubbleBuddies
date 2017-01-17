using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour {

    public Transform endTrans;
    public Vector3 startPostion, endPosition;
    public float moveTime = 1f;
	// Use this for initialization
	void Start () {
        startPostion = transform.position;
        endPosition = endTrans.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Player"))
        {
            PlayerController play = col.gameObject.GetComponent<PlayerController>();
            if (!play.inBubble)
            {
                col.transform.SetParent(this.transform);
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
            col.transform.SetParent(null);
        }
        else
        {
            return;
        }
    }

    public void MoveUp()
    {
        StopAllCoroutines();
        StartCoroutine(UpRoutine(transform.position));
    }

    IEnumerator UpRoutine(Vector3 start)
    {
        float timer = 0f;
        while (timer < moveTime)
        {
            timer += Time.deltaTime;
            Vector3 pos = Vector3.Lerp(start, endPosition, timer / moveTime);
            transform.position = pos;
            yield return null;
        }
        yield return null;
    }

    public void MoveDown()
    {
        StopAllCoroutines();
        StartCoroutine(DownRoutine(transform.position));
    }

    IEnumerator DownRoutine(Vector3 start)
    {
        float timer = 0f;
        while(timer < moveTime)
        {
            timer += Time.deltaTime;
            Vector3 pos = Vector3.Lerp(start, startPostion, timer / moveTime);
            transform.position = pos;
            yield return null;
        }
        yield return null;
    }
}
