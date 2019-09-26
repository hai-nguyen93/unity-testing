using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttack : MonoBehaviour
{
    private Controller player;
    private Animator anim;

    private float attack1time, attack2time;
    private float attackTimer, comboTimer;
    public float comboDelay = 0.3f;

    public bool isAttacking = false;
    private bool canCombo = false;
    private int nextAttack = 1; // attack kế tiếp trong combo

    void Start()
    {
        player = GetComponent<Controller>();
        anim = GetComponent<Animator>();
        isAttacking = false;

        // lấy độ dài của mấy cái animation attack
        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
        foreach(AnimationClip c in clips)
        {
            if (c.name == "Knight_attack") attack1time = c.length;
            if (c.name == "Knight_attack2") attack2time = c.length;
        }
    }

    
    void Update()
    {
        // Đang ko đánh thì nhấn wánh dc
        if (!isAttacking)
        {
            if (Input.GetKeyDown(KeyCode.J) && player.isOnGround()) // nếu đang đứng trên dưới đất thì wánh dc
            {
                if (nextAttack == 1) // attack thứ 1 trong combo
                {
                    anim.Play("Knight_attack"); // chuyển sang animation wánh, chưa làm hitbox j hết nên chỉ cho đẹp thôi wwww
                    isAttacking = true; // để chạy cái timer cho animation attack
                    canCombo = false; // khi cái animation attack chạy xong thì bật timer cho khoảng thời gian có thể combo
                    attackTimer = attack1time;
                    ++nextAttack; // chuyển sang attack thứ 2 trong combo nếu bấm combo
                }
                if (canCombo) 
                {
                    if(nextAttack == 2) // attack thứ 2 trong combo
                    {
                        anim.Play("Knight_attack2"); 
                        isAttacking = true;
                        canCombo = false;
                        attackTimer = attack2time;
                        nextAttack = 1; // mới làm combo có 2 cái thôi nên wánh cái attack thứ 2 thì reset lại 1, combo dài hơn thì cứ ++ lên tới attack cuối thôi wwww
                    }
                }
            }
        }

        // chờ animation attack chạy, chạy hết animation wánh thì mới bắt đầu mở timer cho phép combo
        if (isAttacking)
        {
            if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }
            else
            {
                isAttacking = false;
                canCombo = true;
                comboTimer = comboDelay;
            }
        }

        // Nếu attack kế tiếp ko phải là attack đầu tiên trong combo thì mới cần tính timer cho combo
        if (nextAttack > 1)
        {
            // khoảng thời gian có thể combo, hết combo timer mà ko combo thì reset
            if (canCombo)
            {
                if (comboTimer > 0)
                {
                    comboTimer -= Time.deltaTime;
                }
                else
                {
                    canCombo = false;
                    nextAttack = 1;
                }
            }
        }
    }

}
