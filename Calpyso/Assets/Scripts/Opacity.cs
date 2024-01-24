using System;
using UnityEngine;

public class Opacity : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    private int SpriteRenderer()
    {
        throw new NotImplementedException();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("eve girdim");
            Color newColor = spriteRenderer.color;
            newColor.a = 0.5f;
            spriteRenderer.color = newColor;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            spriteRenderer.color = originalColor;
        }
    }
}
