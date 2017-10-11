using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleInput : MonoBehaviour {
    private float mLastScreenWidth;
    private float mLastScreenHeight;

	// Use this for initialization
	void Start () {
        mLastScreenHeight = Screen.height;
        mLastScreenWidth = Screen.width;
        Screen.SetResolution(Screen.height * 9/16, Screen.height, false);
	}
	
	// Update is called once per frame
	void Update () {

        // window resize----------------
        if (mLastScreenHeight != Screen.height)
        {
            Screen.SetResolution(Screen.height * 9 / 16, Screen.height, false);
            mLastScreenHeight = Screen.height;
        }
        else if (mLastScreenWidth != Screen.width)
        {
            Screen.SetResolution(Screen.width, Screen.width * 16/9, false);
            mLastScreenWidth = Screen.width;
        }
        // ----------------------------------

        int mask = 1 << LayerMask.NameToLayer("GameUI");

        Vector3 m = Input.mousePosition;
        m = new Vector3(m.x, m.y, transform.position.z);
        Vector3 p = GetComponent<Camera>().ScreenToWorldPoint(m);


        Ray2D ray2D = new Ray2D(new Vector2(p.x, p.y), Vector3.down);

        if (Input.GetMouseButtonDown(0))
        {
            if(p.y < -2.5f)
            {
                RaycastHit2D hit = Physics2D.Raycast(ray2D.origin, ray2D.direction, 5, mask);
                Debug.DrawRay(ray2D.origin, ray2D.direction, Color.green, 5, false);

                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.tag == "GameStart")
                    {
                        SceneManager.LoadScene("1.game", LoadSceneMode.Single);
                    }
                    else if (hit.collider.gameObject.tag == "Exit")
                    {
                        Application.Quit();
#if !UNITY_EDITOR
            System.Diagnostics.Process.GetCurrentProcess().Kill(); 
#endif
                    }
                }
            }
           
        }


        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (p.y < -2.5f)
                    {
                        RaycastHit2D hit = Physics2D.Raycast(ray2D.origin, ray2D.direction, 10, mask);

                        if (hit.collider != null)
                        {
                            if (hit.collider.gameObject.tag == "GameStart")
                            {
                                SceneManager.LoadScene("1.game", LoadSceneMode.Single);
                            }
                            else if (hit.collider.gameObject.tag == "Exit")
                            {
                                Application.Quit();
#if !UNITY_EDITOR
            System.Diagnostics.Process.GetCurrentProcess().Kill(); 
#endif
                            }
                        }
                    }
                   
                    break;
                case TouchPhase.Moved:
                    
                    break;
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Ended:
                    
                    break;
                case TouchPhase.Canceled:
                    break;
                default:
                    break;
            }

        }
    }
}
