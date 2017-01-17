using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    public enum PlayerColor
    {
        Red,
        Blue,
        Invalid
    }

    public bool aboveWater = false;
    public bool alive = true;
    public PlayerColor color = PlayerColor.Invalid;
    public string Left, Right, BlowBubble;
    public Rigidbody2D rig;
    public float moveSpeed = 100f;
    public bool inBubble = false;
    public GameObject bubblePrefab;
    public Transform bubbleLocation;
    public float startXScale = 1f;
    public bool bubbleBlowing = false;
    public float bubbleDelay = 2f;
    public float forcePower = 10f;
    public float surfaceY = 0f;
    public bool canTakeDamage = false;
    public float damageDelay = 0.5f;
    public Image[] hearts;
    public int health = 3;
    public AudioSource source;
    public AudioClip hit;
	// Use this for initialization
	void Start () {
        rig = this.GetComponent<Rigidbody2D>();
        startXScale = transform.localScale.x;
        surfaceY = GameObject.Find("Surface").transform.position.y;
	    if(color == PlayerColor.Blue)
        {
            Left = "BLeft";
            Right = "BRight";
            BlowBubble = "BBubble";
        }
        else if(color == PlayerColor.Red)
        {
            Left = "RLeft";
            Right = "RRight";
            BlowBubble = "RBubble";
        }
	}
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Enemy"))
        {
            if (canTakeDamage) {
                Debug.Log("Enemy hit");
                col.SendMessage("PlayHitSound");
                TakeDamage();
            }
        }
    }

    public void EnterBubble(Bubble bub)
    {
        inBubble = true;
        rig.velocity = Vector3.zero;
        rig.gravityScale = 0f;
        this.GetComponent<Collider2D>().isTrigger = true;
        transform.SetParent(bub.transform);
    }

    public void ExitBubble(Bubble bub)
    {
        canTakeDamage = false;
       StartCoroutine( allowDamage());
        inBubble = false;
        rig.velocity = Vector3.zero;
        this.GetComponent<Collider2D>().isTrigger = false;
        rig.gravityScale = 1f;
        transform.SetParent(null);
    }

    IEnumerator allowDamage()
    {
        float timer = 0f;
        while(timer < damageDelay)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        canTakeDamage = true;
    }

    public void TakeDamage()
    {
        hearts[health-1].color = Color.black;
        source.PlayOneShot(hit);
        health--;
        canTakeDamage = false;
        if(health == 0)
        {
            alive = false;
        }
       StartCoroutine(allowDamage());
    }

	// Update is called once per frame
	void Update () {
        if (!inBubble && GameManager.Instance.gameRunning)
        {
            Vector2 moveDirection = new Vector2(1f, 0f);
            if (Input.GetButton(Left))
            {
                rig.AddForce(-moveDirection * forcePower);
                rig.velocity = new Vector2(Mathf.Clamp(rig.velocity.x, -moveDirection.x, moveDirection.x), rig.velocity.y);
                //rig.velocity = -moveDirection;
                Vector3 vect = transform.localScale;
                vect.x = startXScale;
                transform.localScale = vect;
            }
            if (Input.GetButton(Right))
            {
                rig.AddForce(moveDirection * forcePower);
                rig.velocity = new Vector2(Mathf.Clamp(rig.velocity.x, -moveDirection.x, moveDirection.x), rig.velocity.y);
                //rig.velocity = moveDirection;
                Vector3 vect = transform.localScale;
                vect.x = -startXScale;
                transform.localScale = vect;
            }
            if (Input.GetButtonDown(BlowBubble))
            {
                if (!bubbleBlowing)
                {
                    bubbleBlowing = true;
                    SpawnBubble();
                }
            }
        }
        else
        {

        }
        if(transform.position.y > surfaceY)
        {
            aboveWater = true;
        }
        else
        {
            aboveWater = false;
        }
    }

    public void SpawnBubble()
    {
        StartCoroutine(SpawnBubbleRoutine());
    }
    public IEnumerator SpawnBubbleRoutine()
    {
        Instantiate(bubblePrefab, bubbleLocation.position, Quaternion.identity);
        yield return new WaitForSeconds(bubbleDelay);
        bubbleBlowing = false;
    }
}
