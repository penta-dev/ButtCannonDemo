using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raptor : MonoBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Sprite[] _sprites;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CheckHeadTouch(Vector2 pos)
    {
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Raptor_Head")
                return true;
        }
        return false;
    }
    public void ApplyForce(float force)
    {
        if (0 < force && force < 1) _spriteRenderer.sprite = _sprites[0];
        if (1 < force && force < 2) _spriteRenderer.sprite = _sprites[1];
        if (2 < force && force < 3) _spriteRenderer.sprite = _sprites[2];
        if (3 < force && force < 4) _spriteRenderer.sprite = _sprites[3];
    }
    public void ShowShotAnimation()
    {
        _spriteRenderer.sprite = _sprites[4];
        StartCoroutine(AfterShot(0.5f));
    }
    IEnumerator AfterShot(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        _spriteRenderer.sprite = _sprites[0];
    }
}
