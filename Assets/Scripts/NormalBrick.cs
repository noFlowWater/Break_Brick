using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBrick : Brick
{
    SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    int maxLife;
    int randomInt;

    protected override void Awake()
    {
        randomInt = Random.Range(1, 3);
        base.Awake(); 
        maxLife = life;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = sprites[0];
    }

    protected override void OnDamaged(int damage)
    {
        base.OnDamaged(damage);
        if (life <= maxLife/2 && !(life <= maxLife / 5)) { spriteRenderer.sprite = sprites[randomInt]; }
        else if (life <= maxLife / 5) { spriteRenderer.sprite = sprites[3]; }
    }
}
