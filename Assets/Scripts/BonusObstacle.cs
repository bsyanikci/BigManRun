using UnityEngine;

public class BonusObstacle : Obstacle
{
    private BonusArea bonusArea;
    protected override void Awake()
    {
        base.Awake();
        bonusArea = FindObjectOfType<BonusArea>();
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        //Bonus engeline player �arp�nda kazan�lan bonusu katla
        if (collision.gameObject.CompareTag("Player"))
        {
            bonusArea.UpdateBonusMultiplier();
        }
    }

}
