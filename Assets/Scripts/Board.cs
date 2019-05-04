using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
    public static List<Cell> board = new List<Cell>();

    public static Cell CellAt(Vector3 pos)
    {
        Cell cell = null;
        foreach(var mcell in board)
        {
            if((mcell.transform.position - pos).magnitude < 1)
            {
                cell = mcell;
            }
        }
        return cell;
    }

    public static bool IsCellSpawnable(Vector3 pos)
    {
        var cell = CellAt(pos);
        if (!cell)
        {
            return false;
        }
        return IsCellSpawnable(cell);
    }

    public static bool IsCellSpawnable(Cell cell)
    {
        if (!cell)
        {
            return false;
        }
        var pos = cell.transform.position;
        return true;
    }

    public static void SetLinks()
    {
        foreach(var cell in board)
        {
            cell.linkedCells.Clear();
            var mcell = CellAt(cell.transform.position + (Vector3.up * 2));
            if (mcell)
            {
                cell.linkedCells.Add(mcell);
            }
            mcell = CellAt(cell.transform.position + (Vector3.down * 2));
            if (mcell)
            {
                cell.linkedCells.Add(mcell);
            }
            mcell = CellAt(cell.transform.position + (Vector3.left * 2));
            if (mcell)
            {
                cell.linkedCells.Add(mcell);
            }
            mcell = CellAt(cell.transform.position + (Vector3.right * 2));
            if (mcell)
            {
                cell.linkedCells.Add(mcell);
            }
        }
    }

    public static void SetPassages()
    {
        foreach(var mcell in board)
        {
            mcell.passages = 0;
            foreach(var cell in mcell.linkedCells)
            {
                if(!cell.Contains || !(cell.Contains is Tree))
                {
                    mcell.passages += 1;
                }
            }
        }
    }

    public static bool CheckPassages()
    {
        foreach(var mcell in board)
        {
            if(mcell.Contains && mcell.Contains is Tree)
            {
                continue;
            } 
            if(mcell.passages <= 0)
            {
                return false;
            }
            bool deadend = false;
            foreach(var cell in mcell.linkedCells)
            {
                if(cell.passages == 1)
                {
                    deadend = true;
                    continue;
                }
            }
            if (deadend)
            {
                return false;
            }
        }

        return true;
    }
}
