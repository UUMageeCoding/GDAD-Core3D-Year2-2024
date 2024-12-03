using UnityEngine;
using TMPro;

public class TextMeshProAnimator : MonoBehaviour
{
    public TMP_Text textMeshPro;
    public AnimationCurve animationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    public float animationSpeed = 2f;
    public float letterSpacing = 0.1f;

    private string originalText;
    private TMP_TextInfo textInfo;
    private float[] charOffset;

    void Start()
    {
        if (textMeshPro == null)
        {
            textMeshPro = GetComponent<TMP_Text>();
        }
        originalText = textMeshPro.text;
        textMeshPro.ForceMeshUpdate();
        textInfo = textMeshPro.textInfo;

        charOffset = new float[textInfo.characterCount];
    }

    void Update()
    {
        if (textMeshPro == null) return;

        textMeshPro.ForceMeshUpdate();
        textInfo = textMeshPro.textInfo;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            if (!textInfo.characterInfo[i].isVisible)
                continue;

            float time = Time.time * animationSpeed + i * letterSpacing;
            charOffset[i] = Mathf.Sin(time);
            AnimateCharacter(i, charOffset[i]);
        }
    }

    void AnimateCharacter(int index, float offset)
    {
        TMP_CharacterInfo charInfo = textInfo.characterInfo[index];

        int vertexIndex = charInfo.vertexIndex;
        Vector3[] vertices = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

        Vector3 charCenter = (vertices[vertexIndex + 0] + vertices[vertexIndex + 2]) / 2;

        for (int j = 0; j < 4; j++)
        {
            vertices[vertexIndex + j] -= charCenter;
            vertices[vertexIndex + j] += Vector3.up * offset * animationCurve.Evaluate(Time.time % 1);
            vertices[vertexIndex + j] += charCenter;
        }

        textMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices);
    }
}