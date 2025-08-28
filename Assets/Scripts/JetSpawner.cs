using UnityEngine;

public class JetSpawner : MonoBehaviour
{
    public JetSpawnerSO jetSpawnerSO;
    public GameObject jet;
    GameObject[] jetSpawnerList;
    int difficultyJetQuantity;

    private void Awake()
    {
        jetSpawnerList = GameObject.FindGameObjectsWithTag("spawner");

        // jumlah jet yang spawn per difficulty
        int difficulty = PlayerPrefs.GetInt("difficulty", 0);
        switch (difficulty)
        {
            case 0:
                difficultyJetQuantity = jetSpawnerSO.EasyJetQuantity;
                break;
            case 1:
                difficultyJetQuantity = jetSpawnerSO.MediumJetQuantity;
                break;
            case 2:
                difficultyJetQuantity = jetSpawnerSO.HardJetQueantity;
                break;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int x=0; x<difficultyJetQuantity; x++)
        {
            // pilih spawner mana yang spawn jet
            int chosenSpawnerIndex = Random.Range(0, jetSpawnerList.Length);
            Instantiate(jet, jetSpawnerList[chosenSpawnerIndex].transform.position, jetSpawnerList[chosenSpawnerIndex].transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
