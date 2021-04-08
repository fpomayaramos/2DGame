using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Item Assets
 */
public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Transform pfItemWorld;

    public Sprite appleSprite;
    public Sprite bookSprite;
}
