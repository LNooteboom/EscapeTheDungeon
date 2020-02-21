using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTeleporter : MonoBehaviour
{
	public string destination = "SampleScene";

    // Start is called before the first frame update
    void Start()
    {
        
    }

	void OnMouseDown()
	{
		SceneManager.LoadScene(destination);
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
