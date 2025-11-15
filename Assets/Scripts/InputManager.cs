using UnityEngine;

public class InputManager : MonoBehaviour
{

    public bool canClick = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
        
    }

    private bool CheckClickLockouts()
    {
        if (canClick) return false;
        else
            return true;
    }

}
