using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour {
    public AudioSource source;
    public AudioClip hit;
    // Use this for initialization
    void Start () {
		
	}
    public void PlayHitSound() {
        source.PlayOneShot(hit);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
