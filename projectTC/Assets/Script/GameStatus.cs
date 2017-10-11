using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameStatus : MonoBehaviour {

    private int mScore = 0;
    public UILabel mScoreLabel;
    public test test;
    public UISprite mFeverGauge;
    
    private float mGameTime;
    private float mFeverToScore;
    private float mFeverScoreMax = 100000;
    private int mFeverTimeLength = 5;
    public PanelManager panelManager;
    public Mascot mascotScript;

    public bool isFever = false;

    private float mTimer = 0;

    public GameObject mFeverGuageEff;
    private GameObject mGuageEffObj;
    public GameObject mFeverWallEff;
    private GameObject mFeverWallEffObj0;
    private GameObject mFeverWallEffObj1;

    // -- audio--
    private AudioSource audio;
    
    public AudioClip NomalBgmClip;
    public AudioClip GameOverClip;
    public AudioClip ButtonClip;
    public AudioClip CellSound;

    // Use this for initialization
    void Start () {
        
        mGameTime = 60;
        mFeverToScore = 0;
        mFeverGauge.fillAmount = 0;
	}
	
	// Update is called once per frame
	void Update () {

        audio = GetComponent<AudioSource>();

        if (panelManager.isGameStart)
        {
                mScoreLabel.text = mScore.ToString();
                test.Current = mGameTime;
                mFeverGauge.fillAmount = mFeverToScore / mFeverScoreMax;

                if (mGameTime < 0)
                {
                    // GameOver ani
                    GameOver();
                }


                if (isFever && mFeverToScore <= 0)
                {
                    mFeverToScore = 0;
                    panelManager.FeverCellChange(false);
                    Destroy(mGuageEffObj);
                    Destroy(mFeverWallEffObj0);
                    Destroy(mFeverWallEffObj1);
                    mascotScript.MascotFever(false);
                audio.pitch = 1.0f;
                    isFever = false;
                }

                if (!isFever)
                {
                    mGameTime -= Time.deltaTime;
                }
                else
                {
                    mTimer += Time.deltaTime;
                    if (mTimer > 1)
                    {
                        mTimer = 0;
                        mFeverToScore -= mFeverScoreMax / mFeverTimeLength;
                    }
                }

            }   
        }

    public void ScoreUp(int score)
    {
        mScore += score;

        if(score != 0)
            GetComponent<AudioSource>().PlayOneShot(CellSound, 0.5f);


        if (!isFever)
        {
            if(mGameTime + 0.5f <= 60)
                mGameTime += 0.5f;

            mFeverToScore += score;
            if (mFeverToScore > mFeverScoreMax)
            {
                mFeverToScore = mFeverScoreMax;
                mGuageEffObj = Instantiate(mFeverGuageEff, new Vector2(3, 9.6f), Quaternion.identity) as GameObject;
                mGuageEffObj.transform.Rotate(new Vector3(0,180,0));
                mFeverWallEffObj0 = Instantiate(mFeverWallEff, new Vector3(-7.5f, -16, -7), Quaternion.identity) as GameObject;
                mFeverWallEffObj0.transform.Rotate(new Vector3(-90, 0, 0));
                mFeverWallEffObj1 = Instantiate(mFeverWallEff, new Vector3(7.5f, -16, -7), Quaternion.identity) as GameObject;
                mFeverWallEffObj1.transform.Rotate(new Vector3(-90, 0, 0));
                panelManager.FeverCellChange(true);
                audio.pitch = 1.5f;
                mascotScript.MascotFever(true);
                isFever = true;
            }
        }

    }


    // Game End Menu ===================================================

    public void ReStartGame()
    {
        audio.PlayOneShot(ButtonClip, 1.0f);
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public GameObject mResultUI;
    public UILabel mResultText;

    private void GameOver()
    {
        audio.Stop();
        audio.PlayOneShot(GameOverClip, 1.0f);
        panelManager.isGameStart = false;
        panelManager.isBlockFix = false;
        mResultUI.SetActive(true);
        mResultText.text = "Your Score is\n" + mScoreLabel.text;
    }

    // Pause Menu =====================================================
    public GameObject mPauseUI;

    public void PauseMenuOver()
    {
        audio.PlayOneShot(ButtonClip, 1.0f);
        audio.Stop();
        panelManager.isGameStart = false;
        mPauseUI.SetActive(true);
    }

    public void PushTheReturnBt()
    {
        audio.PlayOneShot(ButtonClip, 1.0f);
        
        audio.Play();
        panelManager.isGameStart = true;
        mPauseUI.SetActive(false);
    }

    public void PushTheTitle()
    {
        audio.PlayOneShot(ButtonClip, 1.0f);
        SceneManager.LoadScene("0.title", LoadSceneMode.Single);
    }
}
