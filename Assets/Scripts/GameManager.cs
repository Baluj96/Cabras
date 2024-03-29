using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Manager")]
    public static GameManager instance;

    [Header("Enemy Manager")]
    GameObject spawnGoat;
    public int level;
    [SerializeField] TextMeshProUGUI goatstextUI;
    [SerializeField] TextMeshProUGUI levelNumberText;
    [SerializeField] GameObject[] goatPrefabs;
    int numberGenerateGoats;
    int numGenerateGoats;
    float waitTime = 0.5f;

    [Header("UI GameOver")]
    [SerializeField] GameObject panelGameOver;
    private bool gameover;

    [Header("UI Victory")]
    [SerializeField] GameObject panelVictory;
    private bool gamevictory;


    private void Start()
    {
        instance = this;
        DesactivePanels();

        spawnGoat = GameObject.FindGameObjectWithTag("Spawner");

        WayManager.instance.StartGame();

        Invoke("Level0", 1);
        //Level0();
    }

    void DesactivePanels()
    {
        panelGameOver.SetActive(false);
        panelVictory.SetActive(false);
        gameover = false;
        gamevictory = false;
    }

    private void Update()
    {


    }

    public void Level0()
    {
        level = 1;
        numberGenerateGoats = 10;
        goatstextUI.text = "x " + numberGenerateGoats;
        levelNumberText.text = level.ToString();
        Invoke("CreateGoat", 1);
    }

    public void NextLevel()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (GameObject o in obstacles)
        {
            Destroy(o);
        }

        WayManager.instance.CreateObstacle();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector3(0, 0.5f, 0);
        player.GetComponent<PlayerMovement>().enabled = true;

        goatstextUI.enabled = true;
        level++;
        numberGenerateGoats = 10 + 2 * level + Mathf.RoundToInt(level * level * 0.5f);

        Destroy(GameObject.FindGameObjectWithTag("MainGoat"));
        GameObject[] goats = GameObject.FindGameObjectsWithTag("Goat");
        foreach (GameObject goat in goats)
        {
            Destroy(goat);
        }

        DesactivePanels();
        //Genera los enemigos
        //Debug.Log("A generar enemigos");
        Invoke("CreateGoat", 0.1f);
    }

    public void CheckUI()
    {
        //TODO
        numberGenerateGoats = GameObject.FindGameObjectsWithTag("Goat").Length;
        goatstextUI.text = "x " + numberGenerateGoats;
        levelNumberText.text = level.ToString();
    }

    void CreateGoat()
    {
        //Debug.Log(gameover + " y " + gamevictory);
        if (gameover == true || gamevictory == true)
        {
            return;
        }

        //Debug.Log("Generando cabras");
        StartCoroutine(CreateGoats());
    }
    IEnumerator CreateGoats()
    {
        numGenerateGoats = numberGenerateGoats;
        while (numGenerateGoats > 0)
        {
            int n = Random.Range(0, goatPrefabs.Length);
            GameObject g = Instantiate(goatPrefabs[n], spawnGoat.transform.position, spawnGoat.transform.rotation, gameObject.transform);

            int r = Random.Range(0, 2);
            if (r == 0)
            {
                g.GetComponent<GoatMovement>().player = true;
            }
            else
            {
                g.GetComponent<GoatMovement>().player = false;
            }

            numGenerateGoats--;
            yield return new WaitForSeconds(waitTime);
        }
        CheckUI();
    }

    public void ToVictory()
    {
        Invoke("Victory", 1);
    }

    public void Victory()
    {
        gamevictory = true;
        panelVictory.SetActive(true);
        Invoke("NextLevel", 3);
    }

    public void ToGameOver()
    {
        //Debug.Log("�han muerto todas las cabras?");
        if (numberGenerateGoats <= 0)
        {
            Invoke("GameOver", 1);
        }
    }

    public void GameOver()
    {
        gameover = true;
        panelGameOver.SetActive(true);
        Invoke("MainMenu", 3);
    }

    void MainMenu()
    {
        ManagerScenes.instance.LoadLevel(0);
    }
}
