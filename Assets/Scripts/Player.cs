using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : Entity
{


    public bool EnableInput = true;

    public AudioClip moveSound;
    public AudioClip wrongMoveSound;

    private void Start()
    {
        currentCell = Board.CellAt(transform.position);
        currentCell.Contains = this;
        Manager.instance.player = this;
        TouchSystem.Swipe.AddListener(SwipeMove);
    }

    private void Update()
    {
        if (EnableInput)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Move(Vector3.left);
            }
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                Move(Vector3.right);
            }
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                Move(Vector3.up);
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                Move(Vector3.down);
            }
        }

    }

    public void SwipeMove()
    {
        var dir = TouchSystem.swipeDirection.Unidirectional();
        Move(dir);
    }

    public void Move(Vector3 dir)
    {
        Cell targetCell = Board.CellAt(transform.position + (dir * 2));
        if (targetCell)
        {
            if (!targetCell.Contains)
            {
                targetCell.Contains = this;
                EnableInput = false;
                transform.DOMove(targetCell.transform.position, 0.15f).OnComplete(Manager.instance.PassTurn);
                SoundSystem.Play(moveSound, gameObject);
                currentCell.Contains = null;
                currentCell = targetCell;
                Camera.main.GetComponent<CameraEffect>().BumpInOut(0.07f, 0.1f);
            }
            else if (targetCell.Contains)
            {
                var target = targetCell.Contains;
                if (target is Fruit)
                {
                    (target as Fruit).PickUp();
                    Move(dir);
                }
                else if (target is Tiger)
                {
                    transform.DOMove(((targetCell.transform.position - transform.position) / 2) + transform.position, 0.07f).OnComplete(Die);
                    SoundSystem.Play(moveSound, gameObject);
                }
                else if (target is Tree)
                {
                    EnableInput = false;
                    transform.DOPunchPosition((targetCell.transform.position - transform.position) / 2, 0.2f).OnComplete(SetInput);
                    SoundSystem.Play(wrongMoveSound);
                    Camera.main.GetComponent<CameraEffect>().ShakeOut(0.15f, 0.1f);
                }
            }
        }
        else
        {
            EnableInput = false;
            SoundSystem.Play(wrongMoveSound);
            transform.DOPunchPosition(dir / 2, 0.2f).OnComplete(SetInput);
            Camera.main.GetComponent<CameraEffect>().ShakeOut(0.15f, 0.1f);
        }

    }

    public void SetInput()
    {
        EnableInput = true;
    }

    public void Die()
    {
        Destroy(this.gameObject);
        ActionDelayer.DelayAction(Manager.instance.Lose, 0.5f);
    }
}
