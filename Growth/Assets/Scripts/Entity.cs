using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

    private GameObject m_hoverOutline;
    private float m_fOutlineSize = 1.2f;

    private void CreateHoverOutline()
    {
        m_hoverOutline = Instantiate(Prefabs.Empty_Object, transform.position, transform.rotation) as GameObject;
        m_hoverOutline.transform.parent = transform;
        SpriteRenderer outlineSprite = m_hoverOutline.AddComponent<SpriteRenderer>();
        outlineSprite.sprite = GetComponent<SpriteRenderer>().sprite;
        outlineSprite.color = Color.white;
        outlineSprite.material = Materials.Outline;
        outlineSprite.enabled = false;
        m_hoverOutline.transform.localScale = Vector3.one * m_fOutlineSize;
    }

	// Use this for initialization
	protected void Start () {
        if (GetComponent<SpriteRenderer>())
        {
            CreateHoverOutline();
        }
	}
	
	// Update is called once per frame
	protected void Update () {
	
	}

    void OnMouseOver()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer && spriteRenderer.enabled)
        {
            m_hoverOutline.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    void OnMouseExit()
    {
        m_hoverOutline.GetComponent<SpriteRenderer>().enabled = false;
    }

    void OnMouseDown()
    {
        GameManager.instance.ReceiveMouseInput(gameObject);
    }
}
