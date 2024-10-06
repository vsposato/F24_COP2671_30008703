using UnityEngine;
using System.Collections;

public class ScrollingObject : MonoBehaviour 
{
	// Use this for initialization
	public float scrollSpeed;

	//Update runs once per frame
	private void Update(){
		transform.position += new Vector3(scrollSpeed,0,0) * Time.deltaTime;
	}

}