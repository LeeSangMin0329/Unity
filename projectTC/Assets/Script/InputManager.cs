using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    public PanelManager panelManager;
    public GameStatus GM;

    private bool isRayShoot = false;


    // window resize value
    private float mLastScreenWidth;
    private float mLastScreenHeight;

    // Use this for initialization
    void Start () {
        mLastScreenHeight = Screen.height;
        mLastScreenWidth = Screen.width;
        Screen.SetResolution(Screen.height * 9 / 16, Screen.height, false);
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
            Screen.SetResolution(Screen.width, Screen.width * 16 / 9, false);
            mLastScreenWidth = Screen.width;
        }
        // ----------------------------------


        // mouse
        int mask = 1 << LayerMask.NameToLayer("GameUI");

        Vector3 m = Input.mousePosition;
        m = new Vector3(m.x, m.y, transform.position.z);
        Vector3 p = GetComponent<Camera>().ScreenToWorldPoint(m);

        
        Ray2D ray2D = new Ray2D(new Vector2(p.x, p.y), Vector3.down);
        

        if (panelManager.isGameStart)
        {
            Debug.DrawRay(ray2D.origin, ray2D.direction, Color.red, 1, false);
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(ray2D.origin, ray2D.direction, 10, mask);

                if (hit.collider != null)
                {
                    GM.PauseMenuOver();
                }
            }
            
            else if (Input.GetMouseButton(0))
            {
                Debug.DrawRay(ray2D.origin, ray2D.direction, Color.green, 5, false);
                
                    panelManager.AddSelectList(p);
                    
                
            }
            else if (Input.GetMouseButtonUp(0))
            {

                panelManager.Calculation();
                
            }



            
            // mobile
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        RaycastHit2D hit = Physics2D.Raycast(ray2D.origin, ray2D.direction, 10, mask);

                        if (hit.collider != null)
                        {
                            GM.PauseMenuOver();
                        }
                        break;
                    case TouchPhase.Moved:
                        panelManager.AddSelectList(p);
                        break;
                    case TouchPhase.Stationary:
                        break;
                    case TouchPhase.Ended:
                        panelManager.Calculation();
                        break;
                    case TouchPhase.Canceled:
                        break;
                    default:
                        break;
                }

            }
            
        }

        
    }
}
