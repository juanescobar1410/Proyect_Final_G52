
using System.Collections;
using TMPro;
using UnityEngine;


/// <summary>
/// Muestra un mensaje en un chat bubble 3D.
/// Actualiza el texto, ajusta el tamaño del fondo según el contenido
/// y reposiciona el fondo para que encaje correctamente.
/// El chat bubble se destruye automáticamente después de unos segundos.
/// </summary>


public class ChatBubble3D : MonoBehaviour
{
    [SerializeField] private GameObject chatBubblePrefab;

    private SpriteRenderer backgroundSpriteRenderer;
    private TextMeshPro textMeshPro;

    public void ShowMessage(string text)
    {
        // 1. Buscar los componentes necesarios
        backgroundSpriteRenderer = transform.Find("Background").GetComponent<SpriteRenderer>();
        textMeshPro = transform.Find("Text (TMP)").GetComponent<TextMeshPro>();

        textMeshPro.fontSize = 10;

        
        textMeshPro.SetText(text);
        textMeshPro.ForceMeshUpdate();

        
        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 padding = new Vector2(3f, 1f);

        
        backgroundSpriteRenderer.size = textSize + padding;


        
        Vector3 offset = new Vector3(0f, 0f); // ← Cambia -3f a 0f para centrarlo
        backgroundSpriteRenderer.transform.localPosition =
            new Vector3(backgroundSpriteRenderer.size.x / 2f, 0f) + offset;


        Destroy(gameObject, 6f);
    }
}

