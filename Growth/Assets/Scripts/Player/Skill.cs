using UnityEngine;
using System.Collections;

public class Skill : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerController.instance.AddImageToHealthManager(GetComponent<SpriteRenderer>());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDestroy()
    {
        PlayerController.instance.RemoveImageFromHealthManager(GetComponent<SpriteRenderer>());
    }
}
