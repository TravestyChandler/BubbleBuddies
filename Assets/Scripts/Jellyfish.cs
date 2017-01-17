using UnityEngine;
using System.Collections;

public class Jellyfish : MonoBehaviour {
    public Transform end;
    public Vector3 startPos, endPos;
    public float moveTime = 5f;
    public Rigidbody2D rig;
    public float delayTime = 2f;
    public float moveForce = 100f;
    public float maxVelocity = 1f;
	// Use this for initialization
	void Start () {
        startPos = transform.position;
        endPos = end.position;
        rig = this.GetComponent<Rigidbody2D>();
        StartCoroutine(MoveRoutine());
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator MoveRoutine()
    {
        int moveDirection = 1;
        while (true)
        {
            yield return null;
            //rig.AddForce(new Vector2(1f, 0f) * moveForce * moveDirection);
            //rig.velocity = new Vector2(Mathf.Clamp(rig.velocity.x, -maxVelocity, maxVelocity), rig.velocity.y);
            //yield return new WaitForSeconds(delayTime / 2f);
            //float timer = 0f;
            //while(timer < (delayTime / 2f))
            //{
            //    timer += Time.deltaTime;
            //    rig.AddForce(-moveDirection * new Vector2(1f, 0f) * (moveForce / 100f));
            //    if(moveDirection == -1)
            //    {
            //        rig.velocity = new Vector2(Mathf.Clamp(rig.velocity.x, -maxVelocity, 0), rig.velocity.y);

            //    }
            //    else if(moveDirection == 1)
            //    {
            //        rig.velocity = new Vector2(Mathf.Clamp(rig.velocity.x, 0f, maxVelocity), rig.velocity.y);

            //    }
            //    yield return null;
            //}
            //if(transform.position.x > endPos.x)
            //{
            //    moveDirection = -1;
            //}
            //else if(transform.position.x < startPos.x)
            //{
            //    moveDirection = 1;
            //}
            /*
            //Old routine: just straight up moves from one side to another.
            while(timer < moveTime)
            {
                timer += Time.deltaTime;
                Vector3 pos = Vector3.Lerp(startPos, endPos, timer / moveTime);
                this.transform.position = pos;
                yield return null;
            }
            timer = 0f;
            while(timer < moveTime)
            {
                timer += Time.deltaTime;
                Vector3 pos = Vector3.Lerp(endPos, startPos, timer / moveTime);
                this.transform.position = pos;
                yield return null;
            }
            */
        }
    }
}
