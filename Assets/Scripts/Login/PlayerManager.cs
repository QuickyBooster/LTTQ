using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public string userUID { get;  set; }
    public string userName { get; set; }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    
}
