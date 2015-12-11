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
    private float m_fCurrentScale;

    private void Die()
    {
        GameManager.instance.RemoveEnemy();
        Destroy(gameObject);
    }

    public bool TakeDamage(float damage)
    {
        if (m_fTimeSinceLastHit < 0 || m_fTimeSinceLastHit > m_fInvulnerabilityTime)
        {
            if (damage > 0)
            {
                m_fTimeSinceLastHit = 0;
                m_fHealth -= damage;
                if (m_fHealth <= 0)
                    Die();
                m_fCurrentShrinkRate = (damage * m_fCurrentScale) / m_fTimeToShrink;
                m_fCurrentScale -= m_fCurrentShrinkRate * m_fTimeToShrink;
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
            m_fCurrentScale += growthAmount;
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
        base.Start();
        GameManager.instance.AddEnemy();
        PlayerController.instance.AddImageToHealthManager(m_image.GetComponent<SpriteRenderer>());
        m_fHealth = m_fMaxHealth;
        m_fCurrentScale = m_fInitialScale;
        m_image.transform.localScale = new Vector3(m_fInitialScale, m_fInitialScale, m_fInitialScale);
        m_fInvulnerabilityTime = 0.6f * m_fTimeToShrink;
	}
	
	// Update is called once per frame
	void Update () {
        base.Update();
        UpdateGrowth();
	}

    void OnDestroy()
    {
        PlayerController.instance.RemoveImageFromHealthManager(m_image.GetComponent<SpriteRenderer>());
        if (!GameManager.isShuttingDown)
        {
            Instantiate(Prefabs.Explosion_Rainbow_Small, transform.position, transform.rotation);
        }
    }
}
