using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager instance;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    public GameObject mFruit;
    public GameObject mTree;
    public GameObject mTiger;
    public Player player;
    public GameObject loseMenu;
    public GameObject cameraCanvas;
    public FloatData Orthosize;
    private List<Tiger> tigers = new List<Tiger>();
    private List<Fruit> fruits = new List<Fruit>();
    private List<Tree> trees = new List<Tree>();
    private List<RespawningObj> respawners = new List<RespawningObj>();
    private HungryTimer hungry;

    public int Points = 0;
    public int fruitCount = 0;
    public int bushCount = 0;
    public int tigerCount = 0;


    public int tigerActions = 0;

    public GameEvent stopEvent;

    public GameObject question;

    public GameObject EndScreen;

    public QuestionBank questions;

    private void Start()
    {
        hungry = GetComponent<HungryTimer>();
        Time.timeScale = 1;
        if (Orthosize.isSet)
        {
            Camera.main.orthographicSize = Orthosize.value;
        }
        Setup();
        stopEvent.Raise();
    }

    public void Setup()
    {
        foreach (var fruit in fruits)
        {
            fruit.currentCell.Contains = null;
            Destroy(fruit.gameObject);
        }
        foreach (var tree in trees)
        {
            tree.currentCell.Contains = null;
            Destroy(tree.gameObject);
        }
        foreach (var tiger in tigers)
        {
            tiger.currentCell.Contains = null;
            Destroy(tiger.gameObject);
        }
        fruits.Clear();
        trees.Clear();
        tigers.Clear();
        respawners.Clear();
        SpawnFruit(fruitCount);
        SpawnTree(bushCount);
        SpawnTiger(tigerCount);
        Board.SetPassages();
        if (!Board.CheckPassages())
        {
            Setup();
        }
    }

    public void Score(bool withPoints = true)
    {
        if (withPoints)
        {
            var point = (int)Mathf.Ceil((hungry.HungryLevel / 10f));
            Debug.Log(point);
            Points += point;
            hungry.Eat();
        }
        respawners.Add(new RespawningObj(mFruit, 5));
    }

    public void PassTurn()
    {
        hungry.Tick();
        foreach (var tiger in tigers)
        {
            tiger.Act();
        }
        if (tigers.Count <= 0)
        {
            RespawnCheck();
            player.SetInput();
        }
    }

    public void SpawnFruit(int amount = 1, bool withEffects = false)
    {
        if (amount <= 0) return;
        var index = Random.Range(0, Board.board.Count - 1);
        while (Board.board[index].Contains)
        {
            index = Random.Range(0, Board.board.Count - 1);
        }
        var targetCell = Board.board[index];
        var newFruit = Instantiate(mFruit);
        newFruit.transform.position = targetCell.transform.position;
        var fruit = newFruit.GetComponent<Fruit>();
        targetCell.Contains = fruit;
        fruit.currentCell = targetCell;
        fruits.Add(fruit);
        if (withEffects)
        {
            fruit.SpawnEffect();
        }
        SpawnFruit(amount - 1);
    }

    public void SpawnTree(int amount = 1)
    {
        if (amount <= 0) return;
        var index = Random.Range(0, Board.board.Count - 1);
        while (Board.board[index].Contains)
        {
            index = Random.Range(0, Board.board.Count - 1);
        }
        var targetCell = Board.board[index];
        var newTree = Instantiate(mTree);
        newTree.transform.position = targetCell.transform.position;
        var tree = newTree.GetComponent<Tree>();
        tree.currentCell = targetCell;
        targetCell.Contains = tree;
        trees.Add(tree);
        SpawnTree(amount - 1);
    }

    public void SpawnTiger(int amount = 1)
    {
        if (amount <= 0) return;
        var index = Random.Range(0, Board.board.Count - 1);
        while (Board.board[index].Contains)
        {
            index = Random.Range(0, Board.board.Count - 1);
        }
        var targetCell = Board.board[index];
        var newTiger = Instantiate(mTiger);
        newTiger.transform.position = targetCell.transform.position;
        var tiger = newTiger.GetComponent<Tiger>();
        tiger.currentCell = targetCell;
        targetCell.Contains = tiger;
        tigers.Add(tiger);
        SpawnTiger(amount - 1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Setup();
        }
    }

    public void Lose()
    {
        hungry.Stop();
        loseMenu.SetActive(true);
        Debug.Log("Lose");
        Time.timeScale = 0;
    }

    public void RespawnCheck()
    {
        List<RespawningObj> tempList = new List<RespawningObj>();
        foreach (var obj in respawners)
        {
            if (obj.Tick())
            {
                tempList.Add(obj);
                Entity ent = obj.objToSpawn.GetComponent<Entity>();
                if (ent is Fruit)
                {
                    SpawnFruit(1, true);
                }
                if (ent is Tree)
                {
                    SpawnTree();
                }
                if (ent is Tiger)
                {
                    SpawnTiger();
                }
            }
        }
        foreach (var obj in tempList)
        {
            respawners.Remove(obj);
        }
    }

    public void TigerPassTurn()
    {
        tigerActions -= 1;
        if (tigerActions <= 0)
        {
            RespawnCheck();
            player.SetInput();
        }
    }

    public void AskQuestion()
    {
        stopEvent.Raise();
        question.SetActive(true);
        question.GetComponentInChildren<Question>().SetQuest(questions.GetNewQuestion());
        player.EnableInput = false;
    }

    public void End(bool trueOrFalse)
    {
        stopEvent.Raise();
        EndScreen.SetActive(true);
    }

    class RespawningObj
    {
        public RespawningObj(GameObject obj, int turnsNeeded)
        {
            objToSpawn = obj;
            neededTurns = turnsNeeded;
        }

        public int currentTurns = 0;
        public int neededTurns = 0;
        public GameObject objToSpawn;

        public bool Tick()
        {
            currentTurns += 1;
            if (currentTurns >= neededTurns)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }


    public void PlayAgain()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        Board.board.Clear();
    }

}
