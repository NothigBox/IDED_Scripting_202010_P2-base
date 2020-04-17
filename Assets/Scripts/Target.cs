using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Target : MonoBehaviour
{
    [SerializeField]
    private int maxHP = 1;

    private int currentHP;

    [SerializeField]
    private int scoreAdd = 10;

    private void Start()
    {
        currentHP = maxHP;
    }

    private void OnCollisionEnter(Collision collision)
    {
        int collidedObjectLayer = collision.gameObject.layer;

        if (collidedObjectLayer.Equals(Utils.BulletLayer))
        {
            collision.rigidbody.velocity = Vector3.zero;
            collision.transform.position = Vector3.up * -300;

            currentHP -= 1;

            if (currentHP <= 0)
            {   
                transform.position = Vector3.up * -150;
                if(Player.OnPlayerScoreChanged != null) Player.OnPlayerScoreChanged(scoreAdd);
            }
        }
        else if (collidedObjectLayer.Equals(Utils.PlayerLayer) ||
            collidedObjectLayer.Equals(Utils.KillVolumeLayer))
        {
            if(Player.OnPlayerHit != null) Player.OnPlayerHit();
            transform.position = Vector3.up * -150;
        }
    }
}