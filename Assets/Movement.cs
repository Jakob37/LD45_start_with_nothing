using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;

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

    public Vector2 LimitPositionToScreen(Vector2 start_pos) {
        var edges = GetCurrentEdgePositions();
        float clamp_x = Mathf.Clamp(start_pos.x, edges.w, edges.y);
        float clamp_y = Mathf.Clamp(start_pos.y, edges.z, edges.x);
        return new Vector2(clamp_x, clamp_y);
    }

    public Vector4 GetCurrentEdgePositions() {
        Vector2 left_positions = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector2 right_positions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        return new Vector4(right_positions.y, right_positions.x, left_positions.y, left_positions.x);
    }
}
