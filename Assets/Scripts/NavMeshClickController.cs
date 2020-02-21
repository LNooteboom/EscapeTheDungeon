using UnityEngine;
using UnityEngine.AI;

public class NavMeshClickController : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                GetComponent<NavMeshAgent>().SetDestination(hitInfo.point);
            }
        }
    }
}