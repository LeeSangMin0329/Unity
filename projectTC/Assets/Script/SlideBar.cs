using UnityEngine;
using System.Collections;

public class SlideBar : MonoBehaviour {

    public float maxBar = 100.0f;
    public float justBar = 100.0f;

    public Texture2D barTexture;
    public Texture2D bgbarTexture;

    public float barLength;

    float bgjustBar = 100.0f;
    float bgbarLength;
    //float bgbarheight;


    // Use this for initialization
    void Start () {
        barLength = 12; // 바의 유니티 상 크기
        bgbarLength = 12; // 바 외곽의 유니티상...
        
    }

    // Update is called once per frame
    void Update()
    {
        AddjustCurrentBar(-3); // 1초에 얼마씩 줄일 것인지....
    }

    void OnGUI()
    {
        //simplemode = 세가지 제공.. 그중 아래는 사간 화면 꽉 제운것...  true = 알파 브랜딩 오케이
        GUI.DrawTexture(new Rect(13, 10, barLength, 30), barTexture, ScaleMode.StretchToFill, true, 0.0F);
        GUI.DrawTexture(new Rect(10, 10, bgbarLength, 30), bgbarTexture, ScaleMode.StretchToFill, true, 0.0F);


        // 박스로 작업한것에서.... 위에 드로우 텍스쳐로 바꾼 것은... 그림이 맘대로 제어되지 않았기 때문입니다.
        //GUI.Box(new Rect(10,10,bgbarLength,20),bgbarTexture);
        //GUI.Box(new Rect(10,10,barLength,20),barTexture);

    }

    public void AddjustCurrentBar(float adj)
    {

        justBar += adj * Time.deltaTime; // 1초에 얼마씩 줄일것인지.... 하는 변수

        if (justBar < 0) // 현재가 0보다 작으면...
            justBar = 0;
        if (justBar > maxBar) // 현재가 맥스보다 크면..
            justBar = maxBar; // 둘은 동일...
        if (maxBar < 1) // 맥스가 1보다 작으면...
            maxBar = 1;


        barLength = (Screen.width / 2) * (justBar / (float)maxBar);
        // 빠지는 수치만큼 줄여나간다...
        // 원래 크기에서...

    }
}
