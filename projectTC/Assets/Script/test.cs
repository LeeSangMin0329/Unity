using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {
    public GameObject dtest;
    

    private Vector3 mObjLocalPos;
    private Vector3 mObjLocalScale;


    public float Max;
    public float Current;
	// Use this for initialization
	void Start () {
        Debug.Log("길이" + dtest.transform.localScale.x);
        mObjLocalPos = dtest.transform.localPosition;
        mObjLocalScale = dtest.transform.localScale;

    }
	
	// Update is called once per frame
	void Update () {
        dtest.transform.localPosition = new Vector3(mObjLocalPos.x * Current/ Max, dtest.transform.localPosition.y, 0);

        dtest.transform.localScale = new Vector3(mObjLocalScale.x * Current / Max, dtest.transform.localScale.y);

        
    }
}
