using System;
using System.Collections.Generic;
using POCOs;
using UnityEngine;

public class Animate : MonoBehaviour
{
    [SerializeField] List<SpriteInfo> sprites;
    private Dictionary<string, SpriteInfo> spriteDictionary = new Dictionary<string, SpriteInfo>();
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        foreach (var spriteInfo in sprites)
        {
            spriteDictionary.Add(spriteInfo.ID, spriteInfo);
        }
    }

    public void SetFlip(bool flipped)
    {
        spriteRenderer.flipX = flipped;
        //Can we send this my pig?
    }

    public void SetSprite(string id)
    {
        if (object.ReferenceEquals(spriteRenderer, null)) return;
        
        spriteDictionary.TryGetValue(id, out var found);
        if (object.ReferenceEquals(found, null)) return;

        spriteRenderer.sprite = found.sprite;
    }
}