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
    [SerializeField] GameObject[] goatPrefabs;
    int numberGenerateGoats;
    public int numGenerateGoats;
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

        Level0();
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
        level = 0;
        numberGenerateGoats = 10;
        CheckUI();
        Invoke("CreateGoat", 1);
    }

    public void NextLevel()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector3(0, 0.5f, 0);
        player.GetComponent<PlayerMovement>().enabled = true;

        goatstextUI.enabled = true;
        level++;
        numberGenerateGoats = 10 + 2 * level + Mathf.RoundToInt(level * level * 0.5f);

        CheckUI();
        DesactivePanels();
        //Genera los enemigos
        //Debug.Log("A generar enemigos");
        Invoke("CreateGoat", 1);
    }

    public void CheckUI()
    {
        goatstextUI.text = "Cabras: " + numberGenerateGoats;
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
        while (numberGenerateGoats > 0)
        {
            int n = Random.Range(0, goatPrefabs.Length);
            Instantiate(goatPrefabs[n], spawnGoat.transform.position, spawnGoat.transform.rotation, gameObject.transform);
            numberGenerateGoats--;
            yield return new WaitForSeconds(waitTime);
        }
    }

    public void Victory()
    {
        gamevictory = true;
        panelVictory.SetActive(true);
        Invoke("NextLevel", 2);
    }

    public void ToGameOver()
    {
        //Debug.Log("�han muerto todas las cabras?");
        if (numGenerateGoats <= 0)
        {
            Invoke("GameOver", 1);
        }
    }

    public void GameOver()
    {
        gameover = true;
        panelGameOver.SetActive(true);
        Invoke("MainMenu", 2);
    }

    void MainMenu()
    {
        ManagerScenes.instance.LoadLevel(0);
    }
}
