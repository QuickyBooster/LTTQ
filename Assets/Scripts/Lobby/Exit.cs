using UnityEngine;

public class Exit : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
        Debug.Log("quit");
    }
}
