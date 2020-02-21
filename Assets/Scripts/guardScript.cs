using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guardScript : MonoBehaviour
{
    public float moveSpeed = 1.5f;
    //moveAxis: 0 = x, 1 = y, 2 = z
    public byte moveAxis = 0;
    public float moveDistance = 10f;
    public float rotateSpeed;
    
    private Rigidbody rb;
    private Vector3 startPos;
    private bool heenweg;

    private Quaternion startTurn;

    enum State{patrol, chase, turning}

    private State currentState;

    void Awake()
    {
        if (moveAxis > 2)
        {
            Debug.LogWarning("moveAxis invalid, setting default");
            moveAxis = 0;
        }
        currentState = State.patrol;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
        heenweg = true;
    }

    void FixedUpdate()
    {
    	switch(currentState) {
    		case State.patrol:
    			patrol_Do();
    			break;

    		case State.turning:
				turning_Do();
    			break;

			case State.chase:
				chase_Do();
    			break;
    	}
	}

	void patrol_Do(){
        var direction = Vector3.zero;
        direction[moveAxis] = 1f;
        
        var step = moveSpeed * Time.deltaTime * direction;
        if(heenweg){rb.MovePosition(transform.position + step);}
        else{rb.MovePosition(transform.position - step);}

        if (Vector3.Distance(transform.position, startPos) > moveDistance)
        {
        	print("a");
            currentState = State.turning;
            startTurn = transform.rotation;
        }
    }

    void turning_Do(){
        
        var step = rotateSpeed * Time.deltaTime;
        print("sep:"+step);
        transform.Rotate(0, step, 0);

        if (Quaternion.Angle(startTurn, transform.rotation) >= 179.9)
        {
            startPos = transform.position;
            heenweg = !heenweg;

            currentState = State.patrol;
        }
    }

    void chase_Do(){

    }
}
