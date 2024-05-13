using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{
    // spawn items into the scene
    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {
        Transform transform = Instantiate(ItemAssets.Instance.ItemWorld, position, Quaternion.identity);
    
        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;
    }

    private Item item;
    private SpriteRenderer spriteRenderer; //change this to mesh

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // ask item to return sprite
    public void SetItem(Item item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
