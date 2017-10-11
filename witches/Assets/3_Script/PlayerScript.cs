using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public float _speed = 5.0f;
    private float _halfHeight;

    public start_GM _AudioGM;

	// Use this for initialization
	void Start () {
        _AudioGM = GameObject.Find("start_GM").GetComponent<start_GM>();
        _halfHeight = Screen.height * 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount > 0)
        {
            float _deltaPosY = Input.GetTouch(0).position.y - _halfHeight;
            float _Ypos = _deltaPosY - transform.localPosition.y;
            transform.Translate(0, _speed * Time.deltaTime * _Ypos * 0.01f, 0);
        }

        transform.localPosition = new Vector3(transform.localPosition.x,
            Mathf.Clamp(transform.localPosition.y, -270.0f, 250.0f),
            transform.localPosition.z);
	}

    public int _hp = 100;
    public int _hpDam = 1;
    public Animator _anim;
    public GameObject _DamageEff;
    public UISprite _GuageBarWidget;

    void OnTriggerEnter(Collider other)
    {
        _hp -= _hpDam;
        if (_hp <= 0)
        {
            GameObject.Find("GM").SendMessage("GameOver", SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            _GuageBarWidget.fillAmount = _hp * 0.01f;

            var _Eff1 = Instantiate(_DamageEff, transform.localPosition, Quaternion.identity) as GameObject;
            _Eff1.transform.parent = transform;
            _Eff1.transform.localPosition = Vector3.zero;
            _Eff1.transform.localScale = new Vector3(1, 1, 1);

            if (_anim != null)
            {
                _anim.SetBool("damageChk", true);
            }
        }
    }

    void DamageEnd()
    {
        _anim.SetBool("damageChk", false);
    }

    void DamageSound()
    {
        _AudioGM.audio.PlayOneShot(_AudioGM._damaEffSound);
    }
}
