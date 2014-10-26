using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

	void Update () {
        if (Input.GetKeyDown("up"))
        {
            transform.position = new Vector3(transform.position.x,transform.position.y+1,transform.position.z);
            Debug.Log("up");
        }
        else if (Input.GetKeyDown("down"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y-1, transform.position.z);
            Debug.Log("down");
        }
        else if (Input.GetKeyDown("left"))
        {
            transform.position = new Vector3(transform.position.x-1, transform.position.y, transform.position.z);
            Debug.Log("left");
        }
        else if (Input.GetKeyDown("right"))
        {
            transform.position = new Vector3(transform.position.x+1, transform.position.y, transform.position.z);
            Debug.Log("right");
        }
	}
}
