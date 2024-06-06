using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerPlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] SuperManagerTimer superManagerTimer;
    private Animator anim;
    private PlayerControllerTimer playerControllerTimer;
    private float cooldownTimer = Mathf.Infinity;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerControllerTimer = GetComponent<PlayerControllerTimer>();
        GetButtonComponentFromDictionary(superManagerTimer.timerUIElementDictionary, "btnAttack").onClick.AddListener(appleAttack);
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;
    }
    private void appleAttack()
    {
        if (cooldownTimer > attackCooldown && playerControllerTimer.canAttack())
        {
            Attack();
        }
    }
    private void Attack()
    {
        superManagerTimer.AppleAttack.Play();
        anim.SetTrigger("Attack");
        cooldownTimer = 0;

        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<TimerProjectTile>().SetDirection(Mathf.Sign(transform.localScale.x));
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