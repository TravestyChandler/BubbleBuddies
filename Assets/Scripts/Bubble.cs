﻿using UnityEngine;
using System.Collections;

public class Bubble : MonoBehaviour {
    public PlayerController.PlayerColor bubbleColor = PlayerController.PlayerColor.Invalid;
    public PlayerController attachedPlayer;
    public GameObject highlight;
    float liveTimer = 0f;
    public float maxLife = 3f;
    public bool floating = false;
    public float floatTime = 3f;
    public float floatHeight = 6f;
    public GameObject bubbleParticles;
    public bool popped = false;
    public AudioClip pop;
    public AudioSource source;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        liveTimer += Time.deltaTime;
        if(liveTimer >= maxLife && !floating &&!popped)
        {
            popped = true;
            PopBubble();
        }
	}
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Enemy"))
        {
            Debug.Log("Enemy hit");
            attachedPlayer.ExitBubble(this);
            PopBubble();
        }
        if (col.tag.Equals("Player"))
        {
            if (col.GetComponent<PlayerController>().color != bubbleColor)
            {
                floating = true;
                attachedPlayer = col.GetComponent<PlayerController>();
                attachedPlayer.inBubble = true;
                attachedPlayer.EnterBubble(this);
                col.transform.SetParent(transform);
                BubbleFloat();
            }
        }
        
    }

    public void BubbleFloat()
    {
        StartCoroutine(FloatRoutine());
    }

    public IEnumerator FloatRoutine()
    {
        //Move player to center, speed based on distance, up to max time
        Vector3 start = attachedPlayer.transform.position;
        Vector3 end = transform.position;
        float maxMoveTime = .25f;
        float maxDistance = 2f;
        float clampDistance = Mathf.Clamp(Vector3.Distance(start, end), 0f, maxDistance);
        float clampTimer = Mathf.Clamp(clampDistance / maxDistance, 0f, maxMoveTime);
        float moveTimer = 0f;
        while (moveTimer < clampTimer)
        {
            moveTimer += Time.deltaTime;
            Vector3 current = Vector3.Lerp(start, end, moveTimer / clampTimer);
            attachedPlayer.transform.position = current;
            yield return null;
        }
        Vector3 startLoc = transform.position;
        Vector3 endLoc = transform.position + new Vector3(0f, floatHeight, 0f);
        float timer = 0f;
        while(timer < floatTime)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(startLoc, endLoc, timer / floatTime);
            yield return null;
        }
        attachedPlayer.ExitBubble(this);
        PopBubble();        
    }

    public void PopBubble()
    {
        StopAllCoroutines();
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, 0);
        GameObject parts = GameObject.Instantiate(bubbleParticles, pos, Quaternion.identity);
        this.GetComponent<SpriteRenderer>().enabled = false;
        source.PlayOneShot(pop);
        Destroy(highlight);
        Destroy(parts, 0.5f);
        Destroy(this.gameObject, 1f);
    }
}
