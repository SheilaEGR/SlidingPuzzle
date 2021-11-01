using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int value;
    public float speed = 5f;
    
    private bool clicked = false;
    private bool moveTile = false;
    private Vector2 movePos;
    

    public void SetPosition(Vector2 position)
    {
        transform.position = position;
    }

    public void MoveHorizontally(bool right)
    {
        movePos = transform.position;
        if(right)
            movePos.x++;
        else
            movePos.x--;
        moveTile = true;
    }

    public void MoveVertically(bool up)
    {
        movePos = transform.position;
        if(up)
            movePos.y++;
        else
            movePos.y--;
        moveTile = true;
    }

    public bool GetState()
    {
        return clicked;
    }

    public void ResetState()
    {
        clicked = false;
    }

    // ========== UNITY METHODS
    private void OnMouseDown() 
    {
        clicked = true;
    }

    private void Update() 
    {
        if(!moveTile)   return;

        Vector2 posNow = transform.position;
        Vector2 diff = posNow - movePos;
        if(diff.magnitude < 0.1f)
            transform.position = movePos;
        else
            transform.position = Vector2.Lerp(posNow, movePos, speed*Time.deltaTime);
    }
}
