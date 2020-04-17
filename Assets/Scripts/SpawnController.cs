using UnityEngine;

[RequireComponent(typeof(RainPool))]
public class SpawnController : MonoBehaviour
{
    [SerializeField]
    private float spawnRate = 1f;

    [SerializeField]
    private float firstSpawnDelay = 0f;

    private Vector3 spawnPoint;
    private RainPool rain;

    private bool IsThereAtLeastOneObjectToSpawn
    {
        get
        {
            bool result = false;

            for (int i = 0; i < rain.DropsPref.Length; i++)
            {
                result = rain.DropsPref[i] != null;

                if (result)
                {
                    break;
                }
            }

            return result;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        rain = GetComponent<RainPool>();

        if (rain.DropsPref.Length > 0 && IsThereAtLeastOneObjectToSpawn)
        {
            InvokeRepeating("SpawnObject", firstSpawnDelay, spawnRate);
            
            Player.OnPlayerDied += StopSpawning;
        }
    }

    private void SpawnObject()
    {
        GameObject spawnGO = rain.RandomDrop();

        if (spawnGO != null)
        {
            spawnPoint = Camera.main.ViewportToWorldPoint(new Vector3(
                Random.Range(0F, 1F), 1F, transform.position.z));

            spawnGO.transform.position = spawnPoint;
            spawnGO.transform.rotation = Quaternion.identity;
        }
        
    }

    private void StopSpawning()
    {
        CancelInvoke();
        Player.OnPlayerDied -= StopSpawning;
    }
}

/*
//  ADVERTENCIA: Esta es una clase aparte de las demás, pero la alojé aquí para que no fuese necesario crear otro objeto :v
public class Control : MonoBehaviour
{
    private ICommand playerHit;
    private ICommand addScore;
    private ICommand playerDied;

    public void Awake()
    {
        playerHit = new PlayerHit();
        playerDied = new PlayerDied();
    }

    private void Start()
    {
        Player.OnPlayerHit -= PHit;
        Player.OnPlayerScoreChanged -= PScoreAdd;
        Player.OnPlayerDied -= PDied;

        Player.OnPlayerHit += PHit;
        Player.OnPlayerScoreChanged += PScoreAdd;
        Player.OnPlayerDied += PDied;
    }

    private void PHit()
    {
        if(playerHit != null) playerHit.Execute();
    }

    private void PScoreAdd(int scoreAdd)
    {
        addScore = new AddScore(scoreAdd);
        addScore.Execute();
    }
    private void PDied()
    {
        if(playerDied != null) playerDied.Execute();
    }
}
*/