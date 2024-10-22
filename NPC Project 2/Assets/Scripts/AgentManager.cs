using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    public static AgentManager Instance;
    public GameObject player;

    [HideInInspector]
    public List<BorderEnemy> borders = new List<BorderEnemy>();

    public List<Star> stars = new List<Star>();
    [HideInInspector]
    public Vector3 maxPosition;
    [HideInInspector]
    public Vector3 minPosition;

    public Vector3 cameraSize;
    [SerializeField]
    public Vector3 cameraEdge;
    [SerializeField]
    [Min(0f)]
    float screenEdgePercent = .05f;

    public BorderEnemy borderPrefab;
    public Star starPrefab;
    public int enemyCount = 15;
    public int starCoint = 40;
    float xPos = 0;
    float yPos = 0;
    float randSprite;

    float timer;
    float cooldownTimer;
    public SpriteRenderer srPlayer;
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioClip collectStarSound;
    [SerializeField]
    AudioClip playerHurtSound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (Instance == null)
        {
            Instance = this;
        }
        cameraSize.y = Camera.main.orthographicSize;
        cameraSize.x = cameraSize.y * Camera.main.aspect;

        cameraEdge.x = cameraSize.x * screenEdgePercent;
        cameraEdge.y = cameraSize.y * screenEdgePercent;

        for (int i = 0; i < enemyCount; i++)
        {
            borders.Add(Instantiate(borderPrefab, new Vector3(20f, 12f, 0), Quaternion.identity));
        }

        srPlayer = player.GetComponent<SpriteRenderer>();
        srPlayer.color = Color.white;
    }

    public void Update()
    {
         xPos = Random.Range(-cameraSize.x, cameraEdge.x);
         yPos = Random.Range(-cameraSize.y, cameraSize.y);
        randSprite = Random.Range(0, 5);
        timer += Time.deltaTime;
        RemoveNull(stars);
        RemoveNull(borders);
        if (timer > .5f && stars.Count < starCoint) 
        {
            timer = 0;
                stars.Add(Instantiate(starPrefab, new Vector3(xPos, yPos), Quaternion.identity));
        }
        
        if (srPlayer.color == Color.red)
        {
            cooldownTimer += 1 * Time.deltaTime;
        }
        if (cooldownTimer > 5f)
        {
            srPlayer.color = Color.white;
            cooldownTimer = 0;
        }

        for (int i = stars.Count - 1; i >= 0; i--) 
            {
            if (stars[i] != null) 
                {

                if (player.GetComponent<Player>().CircleCollision(stars[i])) 
                    {
                    player.GetComponent<Player>().score += 1;
                    Destroy(stars[i].gameObject);
                    audioSource.PlayOneShot(collectStarSound);
                    }
                }
                else
                {
                    stars[i] = stars[stars.Count - 1];
                    stars.RemoveAt(stars.Count - 1);
                }

            }
        if (srPlayer.color == Color.white)
        {
            for (int i = borders.Count - 1; i >= 0; i--)
            {

                if (player.GetComponent<Player>().CircleCollision(borders[i]))
                {
                    srPlayer.color = Color.red;
                    player.GetComponent<Player>().health -= 1;
                    audioSource.PlayOneShot(playerHurtSound);

                }

            }
        }
    }
    private T Spawn<T>(T prefabToSpawn) where T: Agent
    {
        float xPos = Random.Range(-cameraEdge.x, cameraEdge.x);
        float yPos = Random.Range(-cameraEdge.y, cameraEdge.y);
        return Instantiate(prefabToSpawn, new Vector3(xPos, yPos), Quaternion.identity);
    }

    static void RemoveNull<T>(List<T> list)
    {
        for (int i = list.Count - 1; i >= 0; i--)
        {
            if (list[i] == null)
            {
                list.RemoveAt(i); // O(n)
            }
        }
    }
}
