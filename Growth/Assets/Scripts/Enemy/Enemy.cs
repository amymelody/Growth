using UnityEngine;
using System.Collections;

public class Enemy : Entity {

    public Skill.SkillColor m_skillWeakTo = Skill.SkillColor.Red;
    public GameObject m_image;
    public float m_fTimeBeforeGrowth = 3.0f;
    public float m_fGrowthRate = 0.1f;
    public float m_fTimeToShrink = 0.8f;
    public float m_fInitialScale = 0.3f;
    public float m_fMaxScale = 1.5f;
    private float m_fTimeSinceLastHit = -1;
    private const float m_fMaxHealth = 1;
    private float m_fHealth;
    private float m_fCurrentShrinkRate;
    private float m_fInvulnerabilityTime;

    public bool TakeDamage(float damage)
    {
        if (m_fTimeSinceLastHit < 0 || m_fTimeSinceLastHit > m_fInvulnerabilityTime)
        {
            if (damage > 0)
            {
                m_fTimeSinceLastHit = 0;
                m_fHealth -= damage;
                if (m_fHealth <= 0)
                    Destroy(gameObject);
                m_fCurrentShrinkRate = (damage * m_fInitialScale) / m_fTimeToShrink;
            }   
            return true;
        }
        return false;
    }

    private void UpdateGrowth()
    {
        if (m_fTimeSinceLastHit >= m_fTimeBeforeGrowth && m_image.transform.localScale.x < m_fMaxScale)
        {
            float growthAmount = m_fGrowthRate * Time.deltaTime;
            m_image.transform.localScale += new Vector3(growthAmount, growthAmount, growthAmount);
            PlayerController.instance.TakeDamage(growthAmount);
        }
        else
        {
            if (m_fTimeSinceLastHit < m_fTimeToShrink)
            {
                float shrinkAmount = m_fCurrentShrinkRate * Time.deltaTime;
                m_image.transform.localScale -= new Vector3(shrinkAmount, shrinkAmount, shrinkAmount);
            }
            else
            {
                m_fCurrentShrinkRate = 0;
            }
            m_fTimeSinceLastHit += Time.deltaTime;
        }
    }

	// Use this for initialization
	void Start () {
        PlayerController.instance.AddImageToHealthManager(m_image.GetComponent<SpriteRenderer>());
        m_fHealth = m_fMaxHealth;
        m_image.transform.localScale = new Vector3(m_fInitialScale, m_fInitialScale, m_fInitialScale);
        m_fInvulnerabilityTime = 0.7f * m_fTimeToShrink;
	}
	
	// Update is called once per frame
	void Update () {
        UpdateGrowth();
	}

    void OnDestroy()
    {
        PlayerController.instance.RemoveImageFromHealthManager(m_image.GetComponent<SpriteRenderer>());
    }
}
