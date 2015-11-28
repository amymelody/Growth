using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    //=======Singleton implementation=======//
    private static PlayerController _instance;
    public static PlayerController instance
    {
        get { return _instance ?? (_instance = new GameObject("PlayerController").AddComponent<PlayerController>()); }
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
    }
    //======================================//

    public void ReceiveMouseInput(GameObject clicked)
    {
        if (clicked.tag.Equals("Enemy"))
        {
            Debug.Log(":D");
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
}
