using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

    //=======Singleton implementation=======//
    private static MusicManager _instance;
    public static MusicManager instance
    {
        get { return _instance ?? (_instance = new GameObject("MusicManager").AddComponent<MusicManager>()); }
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
        DontDestroyOnLoad(gameObject);

        m_struggle.volume = 1;
        m_growth.volume = 0;
    }
    //======================================//

    public AudioSource m_struggle;
    public AudioSource m_growth;
    public AudioLowPassFilter m_lowPassFilter;

    public void SetGrowthVolume(float volume)
    {
        m_growth.volume = volume;
    }

    public void SetLowPassCutoffFrequency(float normalizedFreq)
    {
        m_lowPassFilter.cutoffFrequency = 22000f * normalizedFreq;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
