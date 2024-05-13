using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    private void Start()
    {
        Instance = this;
    }

    public Transform ItemWorld;   // Prefab for the item world

    public Sprite AxeSprite;
    public Sprite WoodSprite;
}
