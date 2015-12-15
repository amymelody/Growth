using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    //=======Singleton implementation=======//
    private static GameManager _instance;
    public static GameManager instance
    {
        get { return _instance ?? (_instance = new GameObject("GameManager").AddComponent<GameManager>()); }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            if (this != _instance)
                Destroy(gameObject);
        }
        m_currentCursor = Textures.Cursor_Base_Texture;
        m_iCursorWidth = Screen.width / 16;
        m_iCursorHeight = m_iCursorWidth;
        DontDestroyOnLoad(gameObject);
    }
    //======================================//

    public static bool isShuttingDown;

    public int m_iNumLevels = 3;
    private int m_iNumEnemies;
    private int m_iCurrentLevel = 1;
    private float m_fWaitTimeBeforeLoadNextLevel = 2f;
    private Texture2D m_currentCursor;
    private int m_iCursorWidth = 32;
    private int m_iCursorHeight = 32;
    private bool m_bMouseInGame = false;
    private bool m_bGameEnded;

    public bool GameEnded()
    {
        return m_bGameEnded;
    }

    public void SetCursorFromCurrentSkill(Skill.SkillColor color)
    {
        switch(color)
        {
            case Skill.SkillColor.Red:
                m_currentCursor = Textures.Cursor_Red_Texture;
                break;
            case Skill.SkillColor.Blue:
                m_currentCursor = Textures.Cursor_Blue_Texture;
                break;
            case Skill.SkillColor.Yellow:
                m_currentCursor = Textures.Cursor_Yellow_Texture;
                break;
            case Skill.SkillColor.Green:
                m_currentCursor = Textures.Cursor_Green_Texture;
                break;
            default:
                m_currentCursor = Textures.Cursor_Base_Texture;
                break;
        }
    }

    public void ResetLevel()
    {
        m_iNumEnemies = 0;
        Application.LoadLevel(Application.loadedLevel);
        PlayerController.instance.ResetLevel();
    }

    private void StartGame()
    {
        Application.LoadLevel("level1");
    }

    private void EndGame()
    {
        m_bGameEnded = true;
        WorldEffectsManager.instance.StartBackgroundFadeToWhite();
    }

    private void LoadNextLevel()
    {
        if (m_iCurrentLevel == m_iNumLevels)
        {
            Application.LoadLevel("end");
            EndGame();
        }
        else
        {
            MusicManager.instance.ChangeGrowth(1f / (float)(m_iNumLevels - 1));
            m_iCurrentLevel++;
            Application.LoadLevel("level" + m_iCurrentLevel);
        }
    }

    public void ReceiveMouseInput(GameObject clicked)
    {
        PlayerController.instance.ReceiveMouseInput(clicked);

        if (clicked.name.Equals("play_button"))
        {
            StartGame();
        }
    }

    public void AddEnemy()
    {
        m_iNumEnemies++;
    }

    public void RemoveEnemy()
    {
        m_iNumEnemies--;
        if (m_iNumEnemies == 0)
        {
            Instantiate(Prefabs.Explosion_Rainbow_Large, Vector3.zero, Quaternion.identity);
            Invoke("LoadNextLevel", m_fWaitTimeBeforeLoadNextLevel);
        }
    }

    void OnApplicationQuit()
    {
        isShuttingDown = true;
    }

    void CheckMouseHover()
    {
        Rect screenRect = new Rect(0, 0, Screen.width, Screen.height);
        bool insideScreen = screenRect.Contains(Input.mousePosition);
        m_bMouseInGame = insideScreen;
        Cursor.visible = !insideScreen;
    }

    void OnGUI()
    {
        if (m_bMouseInGame)
        {
            GUI.DrawTexture(new Rect(Input.mousePosition.x-m_iCursorWidth/2, 
                Screen.height-Input.mousePosition.y-m_iCursorHeight/2, 
                m_iCursorWidth, m_iCursorHeight), m_currentCursor);
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        CheckMouseHover();
	}
}
