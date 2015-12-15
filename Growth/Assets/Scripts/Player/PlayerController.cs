using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
        m_skills = new Dictionary<Skill.SkillColor, Skill>();
        DontDestroyOnLoad(gameObject);
    }
    //======================================//

    public float m_fMaxHealth = 1;
    public float m_fRecoveryRate = 0.06f;
    private Skill m_currentSkill;
    private PlayerHealthManager m_healthManager;
    private Dictionary<Skill.SkillColor, Skill> m_skills;

    public void ReceiveMouseInput(GameObject clicked)
    {
        Enemy enemy = clicked.GetComponent<Enemy>();
        if (enemy)
        {
            bool success;
            if (m_currentSkill && enemy.m_skillWeakTo == m_currentSkill.m_color)
            {
                success = enemy.TakeDamage(m_currentSkill.Strength());
            }
            else
            {
                success = enemy.TakeDamage(0);
            }
            if (success && m_currentSkill)
            {
                m_currentSkill.Grow();
            }
        }
        Skill skill = clicked.GetComponent<Skill>();
        if (skill)
        {
            m_currentSkill = skill;
            GameManager.instance.SetCursorFromCurrentSkill(m_currentSkill.m_color);
        }
    }

    public void TakeDamage(float damage)
    {
        m_healthManager.TakeDamage(damage);
    }

    public void AddSkill(Skill skill)
    {
        m_skills.Add(skill.m_color, skill);
    }

    public void ResetLevel()
    {
        m_healthManager.ResetHealth();
        foreach (Skill skill in m_skills.Values)
        {
            skill.ResetStrength();
        }
    }

    public void AddEnemy(Enemy enemy)
    {
        WorldEffectsManager.instance.AddImage(enemy.m_image.GetComponent<SpriteRenderer>());
        Skill skill = m_skills[enemy.m_skillWeakTo];
        if (skill)
        {
            skill.Enable();
        }
    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        m_healthManager.Update(Time.deltaTime);
	}
}
