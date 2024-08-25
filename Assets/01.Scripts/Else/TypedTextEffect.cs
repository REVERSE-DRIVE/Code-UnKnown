using System;
using UnityEngine;
using TMPro;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

public class TypedTextEffect : MonoBehaviour
{
    public float typingSpeed = 0.05f; // 타이핑 속도
    private TMP_Text textMeshPro;
    private string beforeText;
    private string fullText;
    private string lastText;

    private void Start()
    {
        textMeshPro = GetComponent<TMP_Text>();
        beforeText = GetBeforeText(textMeshPro.text);
        fullText = GetAfterText(textMeshPro.text);
        lastText = GetSlashTagText(textMeshPro.text);
        textMeshPro.text = beforeText;
        StartCoroutine(ApplyTypingEffect(fullText));
    }
    
    /// <summary>
    /// typing 태그 이전의 텍스트를 반환하는 메서드
    /// </summary>
    /// <param name="text">찾을 텍스트</param>
    /// <returns>찾은 텍스트</returns>
    private string GetBeforeText(string text)
    {
        var index = text.IndexOf("<typing>", StringComparison.Ordinal);
        return text.Substring(0, index);
    }
    
    /// <summary>
    /// <typing> 태그 이후의 텍스트를 </typing> 태그까지 반환
    /// </summary>
    /// <param name="text">찾을 텍스트</param>
    /// <returns>찾은 텍스트</returns>
    private string GetAfterText(string text)
    {
        var index = text.IndexOf("<typing>", StringComparison.Ordinal);
        var last = text.IndexOf("</typing>", StringComparison.Ordinal);
        return text.Substring(index, last - index + 9);
    }
    
    /// <summary>
    /// typing 닫는 태그 이후의 텍스트를 반환하는 메서드
    /// </summary>
    /// <param name="text">찾을 텍스트</param>
    /// <returns>찾은 텍스트</returns>
    private string GetSlashTagText(string text)
    {
        var last = text.IndexOf("</typing>", StringComparison.Ordinal);
        return text.Substring(last + 9);
    }

    /// <summary>
    /// 타이핑 효과를 적용하는 메서드
    /// </summary>
    /// <param name="text">타이핑 할 텍스트</param>
    private IEnumerator ApplyTypingEffect(string text)
    {
        var parts = Regex.Split(text, @"(<typing>.*?</typing>)", RegexOptions.IgnoreCase);

        foreach (var part in parts)
        {
            if (part.StartsWith("<typing>") && part.EndsWith("</typing>"))
            {
                var innerText = part.Substring(8, part.Length - 17); // <typing>와 </typing>를 제거
                yield return StartCoroutine(TypeText(innerText));
            }
            else
            {
                textMeshPro.text += part;
                yield return null; // 타이핑 효과를 적용하지 않고 바로 텍스트 추가
            }
        }

        yield return null;
        textMeshPro.text += lastText;
    }

    /// <summary>
    /// 실질적으로 타이핑 효과를 적용하는 메서드
    /// </summary>
    /// <param name="text">붙여줄 텍스트</param>
    private IEnumerator TypeText(string text)
    {
        // 타이핑 애니메이션을 적용할 부분의 텍스트
        foreach (char letter in text)
        {
            textMeshPro.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        
    }
}