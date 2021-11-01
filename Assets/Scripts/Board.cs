using System.Collections;
using System.Collections.Generic;
using UnityEngine;


struct Element
{
    public Tile tile;
    public int position;
}

public class Board : MonoBehaviour
{
    public GameObject[] tiles;
    public Tile printSignal;

    private int numTiles = 15;
    private Element[] elements;
    private int[] answer = {3, 7, 11, 15, 2, 6, 10, 14, 1, 9, 5, 13, 0, 4, 8};
    private int emptyPos;

    // ===== GAME MECHANICS
    private void Update() 
    {
        if(Input.GetMouseButtonDown(0)) // Left click
        {
            if(printSignal.GetState())
            {
                for(int i=0; i<numTiles; i++)
                    Debug.Log(elements[i].position);
                printSignal.ResetState();
                return;
            }
            
            int clickedElem = FindClickedTile();
            if(clickedElem < 0) return;
            UpdateTile(clickedElem);
            if(CheckFinished())
                Debug.Log("Puzzle finished");
        }
    }

    private int FindClickedTile()
    {
        for(int i=0; i<numTiles; i++)
            if(elements[i].tile.GetState())
                return i;
        return -1;
    }

    private void UpdateTile(int index)
    {
        int pos = elements[index].position;
        if(emptyPos == (pos+1))
        {
            elements[index].tile.MoveVertically(true);            
            elements[index].position = emptyPos;
            emptyPos = pos;
        }
        if(emptyPos == (pos-1))
        {
            elements[index].tile.MoveVertically(false);
            elements[index].position = emptyPos;
            emptyPos = pos;
        }
        if(emptyPos == (pos+4))
        {
            elements[index].tile.MoveHorizontally(true);
            elements[index].position = emptyPos;
            emptyPos = pos;
        }
        if(emptyPos == (pos-4))
        {
            elements[index].tile.MoveHorizontally(false);
            elements[index].position = emptyPos;
            emptyPos = pos;
        }
        elements[index].tile.ResetState();
    }

    private bool CheckFinished()
    {
        for(int i=0; i<numTiles; i++)
        {
            if(elements[i].position != answer[i])
                return false;
        }
        return true;
    }
    

    // ===== START BOARD
    private void Start() 
    {
        int[] posList = new int[numTiles+1];
        for(int i=0; i<numTiles; i++)
            posList[i] = i;

        elements = new Element[numTiles];
        for(int i=0; i<numTiles; i++)
            PlaceElement(posList, i);

        for(int i=0; i<numTiles; i++)
            if(posList[i] >= 0)
            {
                emptyPos = posList[i];
                break;
            }
    }

    private void PlaceElement(int[] posList, int index)
    {
        int pos;
        do
        {
            pos = Random.Range(0, numTiles+1);
        } while (posList[pos] < 0);
        posList[pos] = -1;

        Vector2 vPos;
        vPos.x = pos/4 - 1.5f;
        vPos.y = pos%4 - 1.5f;

        GameObject tt = Instantiate(tiles[index], vPos, Quaternion.identity);
        elements[index].tile = tt.transform.GetComponent<Tile>();
        elements[index].position = pos;
    }
}
