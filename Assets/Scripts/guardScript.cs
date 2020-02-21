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

    public GameObject target;
    
    private Rigidbody rb;
    private Vector3 startPos;
    private bool heenweg;

    private Quaternion startTurn;

    enum State{patrol, chase, turning, lostPlayer}

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

    		case State.lostPlayer:
    			lost_Do();
    			break;
    	}
	}

	void patrol_Do(){
        var direction = Vector3.zero;
        direction[moveAxis] = 1f;
        
        var step = moveSpeed * Time.deltaTime * direction;
        if(heenweg){rb.MovePosition(transform.position + step);}
        else{rb.MovePosition(transform.position - step);}

        if(seePlayer()){
        	currentState = State.chase;
        }

        if (Vector3.Distance(transform.position, startPos) > moveDistance)
        {
            currentState = State.turning;
            startTurn = transform.rotation;
        }
    }

    void turning_Do(){
        
        var step = rotateSpeed * Time.deltaTime;
        transform.Rotate(0, step, 0);

        if (Quaternion.Angle(startTurn, transform.rotation) >= 179.9)
        {
            startPos = transform.position;
            heenweg = !heenweg;

            currentState = State.patrol;
        }
    }

    void chase_Do(){
    	//move
        var positionDelta = target.transform.position - transform.position;
        var temp = moveSpeed * Time.deltaTime * positionDelta.normalized;
        rb.MovePosition(transform.position + temp);

        if(!seePlayer()){
        	print("lost him");
    		currentState = State.lostPlayer;
    	}

    	//rotate
	    Quaternion rot = Quaternion.FromToRotation(transform.forward, target.transform.position - transform.position);
	    transform.rotation *= rot;
    }

    void lost_Do(){
    	if(seePlayer()){
    		currentState = State.chase;
    	}
    }

    bool seePlayer(){
    	var targetDistance = Vector3.Distance(target.transform.position, transform.position);

		Quaternion rot = Quaternion.FromToRotation(transform.forward, target.transform.position - transform.position);
		var deHoek = Mathf.Abs(rot.eulerAngles.y);
		// Debug.Log("deHoek: "+deHoek);

	    // Vector3 directionToTarget = transform.position - target.transform.position;
	    // float angle = Vector3.Angle(transform.forward, directionToTarget);

        if (targetDistance <= 5 && deHoek < 90)
        {
        	print("spotted");
            return true;
        } else {
        	return false;
        }
    }
}
