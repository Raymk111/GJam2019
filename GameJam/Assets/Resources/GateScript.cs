using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Colour
{
	Red,
	White,
	Blue
}

public class GateScript : MonoBehaviour
{
	private Colour gateColour;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	void initGate(Colour c)
	{
		gateColour = c;
		gatePoints = 0;
	}
	// Update is called once per frame
	void Update ()
    {

	}

    void OnCollisionEnter(Collision collision)
    {
		GameObject hitGObj;
		if(hitGObj.tag == "Sheep")
		{
			if(hitGObj.colour == gateColour)
			{
				gatePoints += 10;
			}
			else
			{
				gatePoints -= 2;
			}
			GameObject.Destroy(hitGObj);
		}
    }
}