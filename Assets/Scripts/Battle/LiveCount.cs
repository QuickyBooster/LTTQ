using TMPro;
using UnityEngine;

public class LiveCount : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txt;
    public void changeText(string text)
    {
        txt.text = text;
    }
}
