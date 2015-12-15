using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldEffectsManager : MonoBehaviour {

    //=======Singleton implementation=======//
    private static WorldEffectsManager _instance;
    public static WorldEffectsManager instance
    {
        get { return _instance ?? (_instance = new GameObject("WorldEffectsManager").AddComponent<WorldEffectsManager>()); }
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
        m_images = new Dictionary<SpriteRenderer, Color>();
        DontDestroyOnLoad(gameObject);
    }
    //======================================//

    private Dictionary<SpriteRenderer, Color> m_images;
    private const float m_fTimeToFade = 6f;
    private float m_fTimePassed;
    private bool m_bFading;

    public void AddImage(SpriteRenderer image)
    {
        m_images.Add(image, image.color);
    }

    public void RemoveImage(SpriteRenderer image)
    {
        m_images.Remove(image);
    }

    public void ChangeSaturationOfImages(float normalizedSaturation)
    {
        foreach (SpriteRenderer image in m_images.Keys)
        {
            image.color = Color.Lerp(Color.white, m_images[image], normalizedSaturation);
        }
    }

    public void StartBackgroundFadeToWhite()
    {
        m_bFading = true;
    }

    private void UpdateFadeToWhite()
    {
        if (m_bFading)
        {
            m_fTimePassed += Time.deltaTime;
            Color color = Color.black;
            color.r = MathHelpers.QuadraticInterpolation(Color.black.r, Color.white.r, m_fTimePassed / m_fTimeToFade);
            color.g = MathHelpers.QuadraticInterpolation(Color.black.g, Color.white.g, m_fTimePassed / m_fTimeToFade);
            color.b = MathHelpers.QuadraticInterpolation(Color.black.b, Color.white.b, m_fTimePassed / m_fTimeToFade);
            Camera.main.backgroundColor = color;
            if (m_fTimePassed >= m_fTimeToFade)
            {
                m_bFading = false;
            }
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        UpdateFadeToWhite();
	}
}
