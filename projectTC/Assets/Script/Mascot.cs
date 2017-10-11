using UnityEngine;
using System.Collections;

public class Mascot : MonoBehaviour {

    public GameObject mCatObj;
    private float mBasicYPos = -10.4f;
    private float temp;
    private float mCatSpeed = 0.01f;

    private Vector3 mCurpos;
    private Vector3 mDespos;

    public GameObject Eff;
    private GameObject EffObj;
    private bool isFever;

    public Animator mAnimator;

	// Use this for initialization
	void Start () {
        mCurpos = new Vector3(0, mBasicYPos, -6);
        mDespos = new Vector3(Random.Range(-60, 60)*0.1f, mBasicYPos, -6);
        mAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        mCatObj.transform.position = mCurpos;

        if(mDespos.x < mCurpos.x)
        {
            temp = Random.Range(1, 20) *mCatSpeed;
            if (mCurpos.x - temp < mDespos.x)
                mCurpos.x = mDespos.x;
            else
                mCurpos.x -= temp;
        }
        else if(mDespos.x > mCurpos.x)
        {
            temp = Random.Range(1, 20)*mCatSpeed;
            if (mCurpos.x + temp > mDespos.x)
                mCurpos.x = mDespos.x;
            else
                mCurpos.x += temp;
        }
        else
        {
            ResetDestPos();
        }

        if (isFever)
        {
            EffObj.transform.position = new Vector3(mCurpos.x, mCurpos.y - 4, mCurpos.z);
        }
	}

    private void ResetDestPos()
    {
        mDespos = new Vector3(Random.Range(-60, 60)*0.1f, mBasicYPos, -6);
    }

    public void MascotFever(bool swt)
    {
        if (swt)
        {
            isFever = swt;
            EffObj = Instantiate(Eff, new Vector3( mCurpos.x, mCurpos.y -4, mCurpos.z), Quaternion.identity) as GameObject;
            EffObj.transform.Rotate(new Vector3(-90, 0, 0));
            
            mAnimator.SetBool("Fever", true);
            mCatSpeed = 0.1f;
        }

        else
        {
            isFever = swt;
            Destroy(EffObj);
            mAnimator.SetBool("Fever", false);
            mCatSpeed = 0.01f;
        }
            
    }
}
