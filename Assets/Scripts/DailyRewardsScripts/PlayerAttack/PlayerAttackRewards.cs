using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerAttackRewards : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] SuperManagerDailyRewards superManager;
    private Animator anim;
    private PlayerControllerDailyRewards playerController;
    private float cooldownTimer = Mathf.Infinity;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerController = GetComponent<PlayerControllerDailyRewards>();
        GetButtonComponentFromDictionary(superManager.RewardsUIElementDictionary, "btnAttack").onClick.AddListener(appleAttack);
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;
    }
    private void appleAttack()
    {
        if (cooldownTimer > attackCooldown && playerController.canAttack())
        {
            Attack();
        }
    }
    private void Attack()
    {
        superManager.AppleAttack.Play();
        anim.SetTrigger("Attack");
        cooldownTimer = 0;

        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<ProjectTileRewards>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
    private Button GetButtonComponentFromDictionary(Dictionary<string, GameObject> dictionary, string key)
    {
        Button button = dictionary[key].GetComponent<Button>();
        return button;
    }
}