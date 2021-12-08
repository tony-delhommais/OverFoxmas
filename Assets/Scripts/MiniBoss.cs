using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : Entity
{
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Bullet"))
        {
            m_CurrentPV -= 5;

            if (m_CurrentPV <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public override void Spawn()
    {
        print("Spawn MiniBoss");
    }
}
