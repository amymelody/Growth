using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    //=======Singleton implementation=======//
    private static PlayerController _instance;
    public static PlayerController instance
    {
        get { return _instance ?? (_instance = new GameObject("PlayerController").AddComponent<PlayerController>()); }
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
        m_skillsManager = new SkillsManager();
        m_healthManager = new PlayerHealthManager();
        DontDestroyOnLoad(gameObject);
    }
    //======================================//

    private SkillsManager m_skillsManager;
    private PlayerHealthManager m_healthManager;

    public void ReceiveMouseInput(GameObject clicked)
    {
        Enemy enemy = clicked.GetComponent<Enemy>();
        if (enemy)
        {
            if (enemy.m_skillWeakTo == m_skillsManager.CurrentSkill())
            {
                enemy.TakeDamage(m_skillsManager.CurrentSkillStrength());
            }
        }
    }

    public void TakeDamage(float damage)
    {
        m_healthManager.TakeDamage(damage);
    }

    public void AddImageToHealthManager(SpriteRenderer image)
    {
        m_healthManager.AddImage(image);
    }

    public void RemoveImageFromHealthManager(SpriteRenderer image)
    {
        m_healthManager.RemoveImage(image);
    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}
}
