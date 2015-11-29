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
        m_healthManager = new PlayerHealthManager(m_fMaxHealth, m_fRecoveryRate);
        DontDestroyOnLoad(gameObject);
    }
    //======================================//

    public Skill m_currentSkill;
    public const float m_fMaxHealth = 1;
    public const float m_fRecoveryRate = 0.06f;
    private PlayerHealthManager m_healthManager;

    public void ReceiveMouseInput(GameObject clicked)
    {
        Enemy enemy = clicked.GetComponent<Enemy>();
        if (enemy)
        {
            bool success;
            if (enemy.m_skillWeakTo == m_currentSkill.m_color)
            {
                success = enemy.TakeDamage(m_currentSkill.Strength());
            }
            else
            {
                success = enemy.TakeDamage(0);
            }
            if (success)
            {
                m_currentSkill.Grow();
            }
        }
        Skill skill = clicked.GetComponent<Skill>();
        if (skill)
        {
            m_currentSkill = skill;
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
        m_healthManager.Update(Time.deltaTime);
	}
}
