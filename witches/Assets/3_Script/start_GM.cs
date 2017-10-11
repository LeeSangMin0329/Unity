using UnityEngine;
using System.Collections;

public class start_GM : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void StartGame()
    {
        audio.clip = _playBgm;
        audio.Play();
        Application.LoadLevel("1_play");
    }

    public AudioClip _playBgm;
    public AudioClip _damaEffSound;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
