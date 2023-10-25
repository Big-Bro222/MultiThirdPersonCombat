using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BigBro
{
    [CreateAssetMenu(fileName = "SO_CInputProvider", menuName = "BigBro/ScriptableObjects/CharacterInputProvider", order = 1)]
    public class CharacterInputProvider : ScriptableObject
    {
        [Header("Character Input Values")]
        public Vector2 move;
        public Vector2 look;
        public bool jump;
        public bool sprint;
        public bool dash;
        public bool fire;
        public bool aim;
   
        [Header("Movement Settings")]
        public bool analogMovement;

        [Header("Mouse Cursor Settings")]
        public bool cursorLocked = true;
        public bool cursorInputForLook = true;
        

        public void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }

    }
}
