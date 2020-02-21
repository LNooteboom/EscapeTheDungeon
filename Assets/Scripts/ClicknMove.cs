using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClicknMove : MonoBehaviour
{
	public Vector3 speed;

	[Range(0.0f, 5.0f)]
	public float time = 1.0f;

	private Vector3 curSpeed;
	private float curTime;
	private bool reverse = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

	void OnMouseDown()
	{
		curSpeed = reverse? -speed : speed;
		curTime = time;
		reverse = !reverse;
	}

    // Update is called once per frame
    void Update()
    {
		if (curTime <= 0.0f)
		{
			curSpeed = Vector3.zero;

		} else {
			curTime -= Time.deltaTime;
		}
 		transform.position += curSpeed * Time.deltaTime;;
    }
}
