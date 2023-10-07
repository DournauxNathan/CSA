using UnityEngine;

public class LevelController : MonoBehaviour
{
    //private bool isLevelCleared = false;
    
    public void LevelClear()
    {
        //isLevelCleared = true;
        GameManager.Instance.LoadLevel();
    }
}
