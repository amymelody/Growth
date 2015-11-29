using UnityEngine;
using System.Collections;

public class Enemy : Entity {

    public SkillsManager.SkillColor m_skillWeakTo = SkillsManager.SkillColor.Red;
    public GameObject m_image;
    public float m_fTimeBeforeGrowth = 3.0f;
    public float m_fGrowthRate = 0.1f;
    public float m_fMaxScale = 1.5f;
    private float m_fTimeSinceLastHit = 0;

    public void TakeDamage(float damage)
    {
        Debug.Log("Ow! Damage: " + damage);
        MusicManager.instance.ChangeGrowth(0.1f);
        m_fTimeSinceLastHit = 0;
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
            m_fTimeSinceLastHit += Time.deltaTime;
        }
    }

	// Use this for initialization
	void Start () {
        PlayerController.instance.AddImageToHealthManager(m_image.GetComponent<SpriteRenderer>());
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
