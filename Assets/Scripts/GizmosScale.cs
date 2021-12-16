using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosScale : MonoBehaviour
{
    [SerializeField]
    private Color color = Color.red;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = color;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
