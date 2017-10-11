using UnityEngine;
using System.Collections;

public class GM : MonoBehaviour {

    public GameObject _enemySet;
    public GameObject _nearBgObj;
    public Transform _playerObjPool;
    public bool SpawnChk = true;
    public UILabel _ScoreText;

	// Use this for initialization
	void Start () {
	
	}

    public float _TimeForLevel = 0.0f;
    public float _TimeForLevelLim = 10.0f;
    public PlayerScript _PlayerSt;

	// Update is called once per frame
	void Update () {

        _TimeForLevel += Time.deltaTime;
        if (_TimeForLevel > _TimeForLevelLim)
        {
            if (Time.timeScale < 5.0f)
            {
                _PlayerSt._hpDam++;
                Time.timeScale *= 1.2f;
                _TimeForLevelLim *= 1.2f;
                _TimeForLevel = 0;
            }
        }

        if (_nearBgObj.transform.localPosition.x < -2460.0f && SpawnChk)
        {
            _ScoreText.text = (Time.timeSinceLevelLoad * 100.0f).ToString("N0");

            var Set1 = Instantiate(_enemySet, Vector3.zero, Quaternion.identity) as GameObject;
            Set1.transform.parent = _playerObjPool;
            Set1.transform.localScale = new Vector3(1, 1, 1);
            Set1.transform.localPosition = Vector3.zero;
            SpawnChk = false;
        }

        if (_nearBgObj.transform.localPosition.x > -1300.0f && !SpawnChk)
        {
            SpawnChk = true;
        }
	}

    void ReGame()
    {
        Time.timeScale = 1.0f;
        _ResultUI.SetActive(false);
        Application.LoadLevel("1_play");
    }

    public GameObject _ResultUI;
    public UILabel _ResultText;

    void GameOver()
    {
        _ResultText.text = "Your Score is\n" + _ScoreText.text;
        _ResultUI.SetActive(true);
        Time.timeScale = 0.0f;
    }
}
