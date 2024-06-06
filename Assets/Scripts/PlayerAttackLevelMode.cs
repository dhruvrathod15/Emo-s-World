using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerAttackLevelMode : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] SuperManagerLevels superManagerLevels;
    private Animator anim;
    private PlayerControllerForLevels playerControllerForLevels;
    private float cooldownTimer = Mathf.Infinity;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerControllerForLevels = GetComponent<PlayerControllerForLevels>();
        GetButtonComponentFromDictionary(superManagerLevels.levelUIElementDictionary, "btnAttack").onClick.AddListener(appleAttack);
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;
    }
    private void appleAttack()
    {
        if (cooldownTimer > attackCooldown && playerControllerForLevels.canAttack())
        {
            Attack();
        }
    }
    private void Attack()
    {
        superManagerLevels.AppleAttack.Play();
        anim.SetTrigger("Attack");
        cooldownTimer = 0;

        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<ProjectTileLevels>().SetDirection(Mathf.Sign(transform.localScale.x));
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
