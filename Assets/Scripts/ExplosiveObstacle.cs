using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveObstacle : Obstacle
{
    [SerializeField] private GameObject explosiveParticle;

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        if (collision.gameObject.CompareTag("Player") && explosiveParticle != null)
            Instantiate(explosiveParticle, transform.position, Quaternion.identity);
    }
    protected override void HandlePlayerCollision(GameObject player)
    {
        TransformChanger transformChanger = player.GetComponent<TransformChanger>(); // Oyuncunun TransformChanger bileþenini al

        transformChanger.ChangeHeight(heightChangeAmount);
        transformChanger.ChangeThickness(thicknessChangeAmount);
    }
}
