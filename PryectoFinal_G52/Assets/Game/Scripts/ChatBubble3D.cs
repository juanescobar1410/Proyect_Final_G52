/* 
    ------------------- Code Monkey -------------------
    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!
               unitycodemonkey.com
    --------------------------------------------------
 */
using System.Collections;
using TMPro;
using UnityEngine;

public class ChatBubble3D : MonoBehaviour
{
    [SerializeField] private GameObject chatBubblePrefab;

    private SpriteRenderer backgroundSpriteRenderer;
    private TextMeshPro textMeshPro;

    public void ShowMessage(string text)
    {
        // Inicializar aquí para asegurar que existan
        backgroundSpriteRenderer = transform.Find("Background").GetComponent<SpriteRenderer>();
        textMeshPro = transform.Find("Text (TMP)").GetComponent<TextMeshPro>();

        textMeshPro.SetText(text);
        textMeshPro.ForceMeshUpdate();
        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 padding = new Vector2(7f, 3f);

        backgroundSpriteRenderer.size = textSize + padding;

        Vector3 offset = new Vector3(-3f, 0f);
        backgroundSpriteRenderer.transform.localPosition =
            new Vector3(backgroundSpriteRenderer.size.x / 2f, 0f) + offset;

        // Auto destruir después de 6 segundos
        Destroy(gameObject, 6f);
    }
}