using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerHealthManager {

    private float m_fMaxHealth;
    private float m_fHealth;
    private Dictionary<SpriteRenderer, Color> m_images;
    private float m_fTimeBeforeRecover = 2.0f;
    private float m_fTimeSinceLastHit;
    private float m_fRecoveryRate;

	public PlayerHealthManager(float maxHealth, float recoveryRate)
    {
        m_fMaxHealth = maxHealth;
        m_fHealth = m_fMaxHealth;
        m_fRecoveryRate = recoveryRate;
        m_images = new Dictionary<SpriteRenderer, Color>();
    }

    private void UpdateImages()
    {
        foreach (SpriteRenderer image in m_images.Keys)
        {
            image.color = Color.Lerp(Color.white, m_images[image], m_fHealth);
        }
    }

    private void ChangeHealth(float amount)
    {
        m_fHealth += amount;
        if (m_fHealth > m_fMaxHealth)
            m_fHealth = m_fMaxHealth;
        if (m_fHealth < 0)
            m_fHealth = 0;
        UpdateImages();
        if (m_fHealth == 0)
        {
            Debug.Log("Game over!");
        }
    }

    public void TakeDamage(float damage)
    {
        m_fTimeSinceLastHit = 0;
        ChangeHealth(-damage);
    }

    public void AddImage(SpriteRenderer image)
    {
        m_images.Add(image, image.color);
    }

    public void RemoveImage(SpriteRenderer image)
    {
        m_images.Remove(image);
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
