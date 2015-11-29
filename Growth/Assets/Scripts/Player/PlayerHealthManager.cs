using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerHealthManager {

    private const float m_fMaxHealth = 1;
    private float m_fHealth;
    private Dictionary<SpriteRenderer, Color> m_images;

	public PlayerHealthManager()
    {
        m_fHealth = m_fMaxHealth;
        m_images = new Dictionary<SpriteRenderer, Color>();
    }

    public void TakeDamage(float damage)
    {
        m_fHealth -= damage;
        if (m_fHealth <= 0)
        {
            Debug.Log("Game over!");
        }
        foreach (SpriteRenderer image in m_images.Keys) 
        {
            image.color = Color.Lerp(Color.white, m_images[image], m_fHealth);
        }
    }

    public void AddImage(SpriteRenderer image)
    {
        m_images.Add(image, image.color);
    }

    public void RemoveImage(SpriteRenderer image)
    {
        m_images.Remove(image);
    }
}
