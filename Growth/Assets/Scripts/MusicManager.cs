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

    public void ChangeGrowth(float amount)
    {
        m_growth.volume += amount;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
