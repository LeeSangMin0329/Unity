using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PanelManager : MonoBehaviour {

    public GameStatus gameStatus;
    private int OneCellPoint = 200;


    public int mValueHeight = 14;
    public int mValueWidth = 6;
    private float mCellCreateHeight = 30f;

    public GameObject panelManagerObj;
    public GameObject[] mPanelSet = new GameObject[5];
    private GameObject mSelectedTemp;
    public GameObject mSelectedCell;
    public GameObject SelectedCellParent;

    private Cell[,] mCells;
    

    public bool isGameStart = false;
    public bool isBlockFix = false;
    public bool isDestroyed = false;
    public bool isFever = false;

    private Vector3 tempVec;


    public List<Cell> mSelectList = new List<Cell>();


    // effect
    public GameObject mCellDestroyEffect;
    public GameObject mCellDestroyAni;

    public GameObject mGamestartSeal;
    private GameObject mGamestartSealObj;

    // time value
    float timer;
    int waitingTime;

    // -- sound --
    
    public AudioClip StartSoundClip;

    // Use this for initialization
    IEnumerator Start () {

        mGamestartSealObj = Instantiate(mGamestartSeal, new Vector3(0, 5, -4), Quaternion.identity) as GameObject;

        mCells = new Cell[mValueHeight, mValueWidth];

        Debug.Log(mPanelSet[0].transform.localScale.x);
        for (int i=mValueHeight-1; i>=0; --i)
        {
            for(int j=0; j<mValueWidth; ++j)
            {
                mCells[i, j] = new Cell();
                mCells[i, j].value = Random.Range(1, 5);
                mCells[i, j].x = j;
                mCells[i, j].y = i;
                mCells[i, j].obj = Instantiate(mPanelSet[mCells[i, j].value - 1], new Vector3(j * 2.4f - 6, mCellCreateHeight, -1), Quaternion.identity) as GameObject;
                mCells[i, j].obj.transform.parent = panelManagerObj.transform;
                yield return new WaitForSeconds(0.02f);
            }
        }
        yield return new WaitForSeconds(0.3f);


       
        for(int i=0; i<mValueHeight; i++)
        {
            for(int j=0; j<mValueWidth; j++)
            {
                mCells[i, j].position = new Vector3(-6+2.4f*j, 22.8f-2.4f*i ,-1);
            }
        }

        
        
        

        // init complete
        isGameStart = true;
        isBlockFix = true;
        GetComponent<AudioSource>().PlayOneShot(StartSoundClip, 1.0f);
        Destroy(mGamestartSealObj);
        for (int i = 0; i < mValueHeight; i++)
        {
            for (int j = 0; j < mValueWidth; j++)
            {
                mCells[i, j].obj.GetComponent<BoxCollider2D>().isTrigger = true;
            }
        }

        //FeverCellChange(true);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isGameStart || isBlockFix)
            {
            
            for (int i = 0; i < mValueHeight; i++)
                {
                    for (int j = 0; j < mValueWidth; j++)
                    {
                        if(mCells[i,j].obj != null)
                            if (mCells[i, j].obj.transform.position.y < mCells[i, j].position.y)
                            {
                                mCells[i, j].obj.transform.position = mCells[i, j].position;
                            }
                     
                    }
                }
            }

           
        
        
        

    }

    public void DestroyPanel(int y, int x)
    {
        
        Destroy(mCells[y,x].obj);

        SortingmPanel(y , x);

        
        Debug.Log(mCells[0, x].value);

        // re create panel
        if (isFever)
        {
            mCells[0, x].value = 10;
            mCells[0, x].obj = Instantiate(mPanelSet[4], new Vector3(mCells[0, x].position.x, mCellCreateHeight, -1), Quaternion.identity) as GameObject;
            //mCells[0, x].obj.GetComponent<BoxCollider2D>().isTrigger = true;
            mCells[0, x].obj.transform.parent = panelManagerObj.transform;
            
        }
        else
        {
            mCells[0, x].value = Random.Range(1, 5);
            mCells[0, x].obj = Instantiate(mPanelSet[mCells[0, x].value - 1], new Vector3(mCells[0, x].position.x, mCellCreateHeight, -1), Quaternion.identity) as GameObject;
            mCells[0, x].obj.GetComponent<BoxCollider2D>().isTrigger = true;
            mCells[0, x].obj.transform.parent = panelManagerObj.transform;
            
        }


    }

    private void SortingmPanel(int y, int x)
    {
        for(int i=y; i>0; --i)
        {
            
            mCells[i, x].obj = mCells[i - 1, x].obj;
            
            mCells[i, x].value = mCells[i - 1, x].value;
        }
        
        //mCells[0, x].obj = null;
            
    }

    public void AddSelectList(Vector2 pos)
    {
        
        Cell cell = FindCellOfObject(pos);

        if(cell == null)
        {
            Debug.Log("exceptional select or Cell is bug");
            return;
        }

        if(cell != null)
        {
            

            if (mSelectList.Count == 0)
            {
                SelectedCellControl(true, cell.position);
                mSelectList.Add(cell);
            }
            else
            {
                if (isFever && mSelectList.Count >1)
                {
                    Calculation();
                    return;
                }
                    

                if ((Mathf.Abs( mSelectList[mSelectList.Count-1].x - cell.x) == 0 && Mathf.Abs( mSelectList[mSelectList.Count-1].y - cell.y) == 1)
                    || (Mathf.Abs(mSelectList[mSelectList.Count - 1].y - cell.y) == 0 && Mathf.Abs(mSelectList[mSelectList.Count - 1].x - cell.x) == 1))
                {
                    foreach (Cell oneCell in mSelectList)
                    {
                        if (oneCell.Equals(cell))
                            return;
                    }
                    SelectedCellControl(true, cell.position);
                    mSelectList.Add(cell);
                }
            }
                
        }
            

    }

    public void SelectedCellControl(bool swt, Vector3 position)
    {
        if (swt)
        {
            mSelectedTemp = Instantiate(mSelectedCell, position, Quaternion.identity) as GameObject;
            mSelectedTemp.transform.parent = SelectedCellParent.transform;
        }
        else
        {
            Destroy(SelectedCellParent);
            SelectedCellParent = new GameObject();
        }
    }
    

    

    private Cell FindCellOfObject(Vector2 pos)
    {
        

        for(int i=7; i<mValueHeight; i++)
        {
            for(int j=0; j<mValueWidth; j++)
            {
                if (-7.2 + 2.4 * j < pos.x && -7.2 + 2.4 * (j + 1) > pos.x)
                    if (24 - 2.4 * i > pos.y && 24 - 2.4 * (i + 1) < pos.y)
                        return mCells[i, j];
  
            }

        }

        return null;
    }

    public void FeverCellChange(bool start)
    {
        if (start)
        {
            for (int i = 0; i < mValueHeight; ++i)
            {
                for (int j = 0; j < mValueWidth; ++j)
                {
                    mCells[i, j].value = 10;
                    Destroy(mCells[i, j].obj);
                    mCells[i, j].obj = Instantiate(mPanelSet[4], mCells[i, j].position, Quaternion.identity) as GameObject;
                    //mCells[i,j].obj.GetComponent<BoxCollider2D>().isTrigger = true;
                }
            }

            this.isFever = true;
        }
        else
        {
            for (int i = 0; i < mValueHeight; ++i)
            {
                for (int j = 0; j < mValueWidth; ++j)
                {
                    mCells[i, j].value = Random.Range(1,5);
                    Destroy(mCells[i, j].obj);
                    mCells[i, j].obj = Instantiate(mPanelSet[mCells[i,j].value-1], mCells[i, j].position, Quaternion.identity) as GameObject;
                    mCells[i,j].obj.GetComponent<BoxCollider2D>().isTrigger = true;
                }
            }
            this.isFever = false;
        }
           
    }

    public void Calculation()
    {
        int check = 0;

        isGameStart = false;

        if (mSelectList != null)
        {
            SelectedCellControl(false, Vector3.zero);
            foreach (Cell oneCell in mSelectList)
            {
                Debug.Log(oneCell.value);
                Debug.Log(oneCell.y + " " + oneCell.x);
                check += oneCell.value;
            }

            if (check % 10 == 0 && check != 0)
            {
                
                if (isFever)
                {
                    foreach (Cell oneCell in mSelectList)
                    {
                        Instantiate(mCellDestroyEffect, oneCell.position, Quaternion.identity);
                        Instantiate(mCellDestroyAni, oneCell.position, Quaternion.identity);

                    }
                    
                    gameStatus.ScoreUp(OneCellPoint * mSelectList.Count * 10);
                }
                else
                {
                    mSelectList.Sort(CompareCellToY);
                    foreach (Cell oneCell in mSelectList)
                    {
                        Instantiate(mCellDestroyEffect, oneCell.position, Quaternion.identity);
                        Instantiate(mCellDestroyAni, oneCell.position, Quaternion.identity);
                        DestroyPanel(oneCell.y, oneCell.x);
                        Debug.Log("처리순서 " + oneCell.y + " " + oneCell.x);
                    }
                    

                    gameStatus.ScoreUp(OneCellPoint * mSelectList.Count * check);
                }

            }
            mSelectList.Clear();
            
        }
        isGameStart = true;
        
    }

    static int CompareCellToY(Cell c1, Cell c2)
    {
        if (c1.y > c2.y)
            return 1;
        else if (c1.y < c2.y)
            return -1;
        else
            return 0;
    }
}
