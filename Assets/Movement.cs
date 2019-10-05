using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private bool is_moving;
    public bool IsMoving {
        get {
            return is_moving;
        }
        set {
            is_moving = value;
        }
    }

    private bool is_flipped;
    public bool IsFlipped {
        get {
            return is_flipped;
        }
        set {
            is_flipped = value;
        }
    }
}
