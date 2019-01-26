using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropScript : MonoBehaviour {
	public int fullHealth;

	private float health;
	private bool beingEaten;
	private int sheepOnCrops;
	// Use this for initialization
	void Start () {
		beingEaten = false;
		sheepOnCrops = 0;
		health = 100;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if(!beingEaten && fullHealth < fullHealth)
		{
			health *= .1f;
			fullHealth %= fullHealth+1;
		}
	}

	void eat(int damage)
	{
		health -= damage;
		if(health <= 0)
		{
			GameObject.Destroy(this);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Sheep")
		{
			sheepOnCrops++;
			beingEaten = true;
		}
	}

	private void OnCollisionExit(Collision collision)
	{
		if(collision.gameObject.tag == "Sheep")
		{
			sheepOnCrops--;
			if(sheepOnCrops == 0)
			{
				beingEaten = false;
			}
		}
	}
}
