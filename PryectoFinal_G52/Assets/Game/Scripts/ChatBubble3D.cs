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
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class ChatBubble3D : MonoBehaviour
{
    [SerializeField] private GameObject chatBubblePrefab = null;

    public static void Create(Transform parent, Vector3 localPosition, IconType iconType, string text, GameObject prefab)
    {
        Transform chatBubbleTransform = Instantiate(prefab, parent).transform;
        chatBubbleTransform.localPosition = localPosition;
        chatBubbleTransform.GetComponent<ChatBubble3D>().Setup(iconType, text);
        Destroy(chatBubbleTransform.gameObject, 6f);
    }

    public enum IconType
    {
        Happy,
        Neutral,
        Angry,
    }

    [SerializeField] private Sprite happyIconSprite = null;
    [SerializeField] private Sprite neutralIconSprite = null;
    [SerializeField] private Sprite angryIconSprite = null;

    private SpriteRenderer backgroundSpriteRenderer;
    private Transform backgroundCube;
    private SpriteRenderer iconSpriteRenderer;
    private TextMeshPro textMeshPro;

    private void Awake()
    {
        backgroundSpriteRenderer = transform.Find("Background").GetComponent<SpriteRenderer>();
        backgroundCube = transform.Find("BackgroundCube");
        iconSpriteRenderer = transform.Find("Icon").GetComponent<SpriteRenderer>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
    }

    private void Setup(IconType iconType, string text)
    {
        textMeshPro.SetText(text);
        textMeshPro.ForceMeshUpdate();
        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 padding = new Vector2(7f, 3f);

        backgroundSpriteRenderer.size = textSize + padding;
        backgroundCube.localScale = textSize + padding * .5f;

        Vector3 offset = new Vector3(-3f, 0f);
        backgroundSpriteRenderer.transform.localPosition =
            new Vector3(backgroundSpriteRenderer.size.x / 2f, 0f) + offset;
        backgroundCube.localPosition =
            new Vector3(backgroundSpriteRenderer.size.x / 2f, 0f, +.1f) + offset;

        iconSpriteRenderer.sprite = GetIconSprite(iconType);

        // Reemplazamos TextWriter por una corrutina interna
        StartCoroutine(TypeText(text, 0.03f));
    }

    private IEnumerator TypeText(string fullText, float timePerChar)
    {
        textMeshPro.text = "";

        foreach (char c in fullText)
        {
            textMeshPro.text += c;
            yield return new WaitForSeconds(timePerChar);
        }
    }

    private Sprite GetIconSprite(IconType iconType)
    {
        switch (iconType)
        {
            default:
            case IconType.Happy: return happyIconSprite;
            case IconType.Neutral: return neutralIconSprite;
            case IconType.Angry: return angryIconSprite;
        }
    }
}