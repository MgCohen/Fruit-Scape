using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Tiger : Entity
{

    public AudioClip Eating;

    public void Act()
    {
        int index;
        Cell targetCell = null;
        do
        {
            index = Random.Range(0, currentCell.linkedCells.Count);
            targetCell = currentCell.linkedCells[index];
        } while (targetCell.Contains && (targetCell.Contains is Tree || targetCell.Contains is Tiger));
        foreach (var cell in currentCell.linkedCells)
        {
            if (cell.Contains && cell.Contains is Player)
            {
                targetCell = cell;
            }
        }

        if (targetCell != null)
        {
            Manager.instance.tigerActions += 1;
            currentCell.Contains = null;
            if (targetCell.Contains)
            {
                var obj = targetCell.Contains;
                if (obj is Fruit)
                {
                    (obj as Fruit).Stomp();
                }
                if (obj is Player)
                {
                    SoundSystem.Play(Eating, gameObject, 0.6f);
                    (obj as Player).Die();
                }
            }
            targetCell.Contains = this;
            currentCell = targetCell;
            transform.DOMove(targetCell.transform.position, 0.08f).OnComplete(Manager.instance.TigerPassTurn);
        }
    }
}
