using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Entity Contains;

    public bool notSpawnable;

    private void OnEnable()
    {
        Board.board.Add(this);
        Board.SetLinks();
    }

    public List<Cell> linkedCells = new List<Cell>();

    public int passages = 0;
}
