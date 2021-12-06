using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float m_BulletSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBulletPosition();
    }

    private void UpdateBulletPosition()
    {
        transform.position += transform.up * m_BulletSpeed * Time.deltaTime;

        Vector3 BulletPosOnScreen = Camera.main.WorldToScreenPoint(transform.position);

        if(BulletPosOnScreen.y > Screen.height)
        {
            Destroy(gameObject);
        }
    }
}
