using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(BulletPool))]
public class Player : MonoBehaviour
{
    #region Singleton

    public static Player instance;
    public static Player Instance
    {
        get
        {
            if(instance == null) instance = new Player();
            return instance;
        } 
    }

    private void Awake()
    {
        if(Instance != null) 
        {
            Destroy(Instance.gameObject);
        }

        instance = this;
    }

    #endregion

    public const int PLAYER_LIVES = 3;

    private const float PLAYER_RADIUS = 0.4F;

    [Header("Movement")]
    [SerializeField]
    private float moveSpeed = 1F;

    private float hVal;

    #region Bullet

    [Header("Bullet")]
    [SerializeField]
    private Transform bulletSpawnPoint;

    [SerializeField]
    private float bulletSpeed = 3F;

    private BulletPool charger; 

    #endregion Bullet

    #region BoundsReferences

    private float referencePointComponent;
    private float leftCameraBound;
    private float rightCameraBound;

    #endregion BoundsReferences

    #region StatsProperties

    public int Score { get; set; }
    public int Lives { get; set; }

    #endregion StatsProperties

    #region MovementProperties

    public bool ShouldMove
    {
        get =>
            (hVal != 0F && InsideCamera) ||
            (hVal > 0F && ReachedLeftBound) ||
            (hVal < 0F && ReachedRightBound);
    }

    private bool InsideCamera
    {
        get => !ReachedRightBound && !ReachedLeftBound;
    }

    private bool ReachedRightBound { get => referencePointComponent >= rightCameraBound; }
    private bool ReachedLeftBound { get => referencePointComponent <= leftCameraBound; }

    private bool CanShoot { get => bulletSpawnPoint != null; }

    #endregion MovementProperties

    public static Action OnPlayerDied;
    public static Action OnPlayerHit;
    public static Action<int> OnPlayerScoreChanged;

    // Start is called before the first frame update
    private void Start()
    {
        leftCameraBound = Camera.main.ViewportToWorldPoint(new Vector3(
            0F, 0F, 0F)).x + PLAYER_RADIUS;

        rightCameraBound = Camera.main.ViewportToWorldPoint(new Vector3(
            1F, 0F, 0F)).x - PLAYER_RADIUS;

        Lives = PLAYER_LIVES;

        charger = GetComponent<BulletPool>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(Lives > 0)
        {
            hVal = Input.GetAxis("Horizontal");

            if (ShouldMove)
            {
                transform.Translate(transform.right * hVal * moveSpeed * Time.deltaTime);
                referencePointComponent = transform.position.x;
            }

            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                && CanShoot)
            {
                GameObject b = charger.Next();
                b.transform.position = bulletSpawnPoint.position;
                b.transform.rotation = bulletSpawnPoint.rotation;
                b.GetComponent<Rigidbody>().AddForce(transform.up * bulletSpeed, ForceMode.Impulse);
            }
        }
    }
    public void TakeDamage()
    {
        //  Decrease health.
        Lives -= 1;
                
        //  If there is no health, the player dies.
        
        if (Lives <= 0)
        {
            if(OnPlayerDied != null) Player.OnPlayerDied();
            this.enabled = false;
            gameObject.SetActive(false);
        }
        
    }
    public void AddScore(int scoreAdd)
    {
        //  Increase score count.
        Score += scoreAdd;
    }
}