using UnityEngine;

public class LevelController : MonoBehaviour
{
    private bool isLevelCleared;
    
    public void LevelClear()
    {
        isLevelCleared = true;
        GameManager.Instance.LoadLevel();
    }
}
