using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClicknMove : MonoBehaviour
{
	public Vector3 destination;
	public float time = 1.0f;

	private Vector3 curSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

	void OnMouseDown()
	{
		curSpeed = transform.position - destination /
			(Time.deltaTime * time);
	}

    // Update is called once per frame
    void Update()
    {
		if (transform.position == destination)
		{
			curSpeed = 0;
		}
 		transform.position += curSpeed;
    }
}
