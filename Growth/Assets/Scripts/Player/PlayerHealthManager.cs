using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerHealthManager {

    private float m_fMaxHealth;
    private float m_fHealth;
   
    private float m_fTimeBeforeRecover = 2.0f;
    private float m_fTimeSinceLastHit;
    private float m_fRecoveryRate;
    private float m_fDamageMultiplier = 0.6f;

	public PlayerHealthManager(float maxHealth, float recoveryRate)
    {
        m_fMaxHealth = maxHealth;
        m_fHealth = m_fMaxHealth;
        m_fRecoveryRate = recoveryRate;
    }

   

    private void ChangeHealth(float amount)
    {
        m_fHealth += amount;
        if (m_fHealth > m_fMaxHealth)
            m_fHealth = m_fMaxHealth;
        if (m_fHealth < 0)
            m_fHealth = 0;
        WorldEffectsManager.instance.ChangeSaturationOfImages(m_fHealth);
        MusicManager.instance.SetLowPassCutoffFrequency(m_fHealth / m_fMaxHealth);
        if (m_fHealth == 0)
        {
            GameManager.instance.ResetLevel();
        }
    }

    public void ResetHealth()
    {
        ChangeHealth(m_fMaxHealth - m_fHealth);
    }

    public void TakeDamage(float damage)
    {
        m_fTimeSinceLastHit = 0;
        ChangeHealth(-damage * m_fDamageMultiplier);
    }

    public void Update(float deltaTime)
    {
        if (m_fTimeSinceLastHit > m_fTimeBeforeRecover)
        {
            ChangeHealth(m_fRecoveryRate * Time.deltaTime);
        }
        else
        {
            m_fTimeSinceLastHit += Time.deltaTime;
        }
    }
}
