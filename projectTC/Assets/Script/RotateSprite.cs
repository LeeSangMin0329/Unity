using UnityEngine;
using System.Collections;

public class RotateSprite : MonoBehaviour {

    private float timer = 0;
    private bool dir = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (dir)
        {
            gameObject.transform.Rotate(new Vector3(0, 0, 0.1f));
        }
        else
        {
            gameObject.transform.Rotate(new Vector3(0, 0, -0.1f));
        }

        if(timer > 2)
        {
            timer = 0;
            if (dir)
                dir = false;
            else
                dir = true;
        }
	}
}
