using UnityEngine;

namespace Services.Input
{
    public class InputController : MonoBehaviour
    {
        public bool IsFireRequired() => 
            UnityEngine.Input.GetKeyDown(KeyCode.Space);

        public int GetMoveDirection()
        {
            if (UnityEngine.Input.GetKey(KeyCode.LeftArrow))
                return -1;
            
            if (UnityEngine.Input.GetKey(KeyCode.RightArrow))
                return 1;
            
            return 0;
        }
    }
}