using UnityEngine;
using System.Collections;

public class Skill : Entity {

    public enum SkillColor { Red, Blue };

    public SkillColor m_color;
    public SpriteRenderer m_nameImage;
    public int m_iNumUsesBeforeGrowth = 5;
    public float m_fTimeBeforeWeaken = 3.0f;
    public float m_fWeakenRate = 0.1f;
    public float m_fGrowthRate = 0.1f;
    public float m_fBaseStrength;
    public float m_fMaxStrength = 1;
    private float m_fStrength;
    private float m_fTimeSinceLastUse;
    private int m_iNumUses;

    public void Enable()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
    }

    public float Strength()
    {
        return m_fStrength;
    }

    public void ResetStrength()
    {
        m_fStrength = m_fBaseStrength;
    }

    public void Grow()
    {
        m_fTimeSinceLastUse = 0;
        if (m_iNumUses < m_iNumUsesBeforeGrowth)
            m_iNumUses++;
        if (m_iNumUses >= m_iNumUsesBeforeGrowth)
        {
            m_fStrength += m_fGrowthRate;
            MusicManager.instance.ChangeGrowth(m_fGrowthRate);
            if (m_fStrength > m_fMaxStrength)
                m_fStrength = m_fMaxStrength;
        }
    }

    private void UpdateStrength()
    {
        if (m_fTimeSinceLastUse >= m_fTimeBeforeWeaken)
        {
            m_iNumUses = 0;
            float weakenAmount = m_fWeakenRate * Time.deltaTime;
            m_fStrength -= weakenAmount;
            if (PlayerController.instance.m_currentSkill == this)
                MusicManager.instance.ChangeGrowth(-weakenAmount);
            if (m_fStrength < 0)
                m_fStrength = 0;
        }
        else
        {
            m_fTimeSinceLastUse += Time.deltaTime;
        }
        Color currentColor = m_nameImage.color;
        currentColor.a = m_fStrength / m_fMaxStrength;
        m_nameImage.color = currentColor;
    }

	// Use this for initialization
	void Start () {
        PlayerController.instance.AddSkill(this);
        PlayerController.instance.AddImageToHealthManager(GetComponent<SpriteRenderer>());
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
        UpdateStrength();
	}

    void OnDestroy()
    {
        PlayerController.instance.RemoveImageFromHealthManager(GetComponent<SpriteRenderer>());
    }
}
