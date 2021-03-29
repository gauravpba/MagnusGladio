using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    [Header("Touch Hit box")]
    public Vector2 knockBackSpeed;
    public Vector2 knockBackSpeedAttack1;
    public Vector2 knockBackSpeedAttack2;
    public Vector2 knockBackSpeedAttack3;
    public Vector2 touchDamageBotLeft;
    public Vector2 touchDamageTopRight;

    public float
        touchDamageWidth,
        touchDamageHeight;

    private float[] attackDetails;

    public Transform touchDamageCheck;

    public float laserDamage;

    public LayerMask whatIsPlayer;

    int directionToMove = 0;

    private void OnEnable()
    {
        attackDetails = new float[2];
        directionToMove = FindObjectOfType<GolemBoss>().currentDirection;

        if (directionToMove == -1)
        {
            transform.Rotate(0, 180, 0f, 0);
        }
        Invoke("CheckTouchDamage", 1f);
        
    }

    private void CheckTouchDamage()
    {
        
        touchDamageBotLeft.Set(touchDamageCheck.position.x - touchDamageWidth / 2, touchDamageCheck.position.y - touchDamageHeight / 2);
        touchDamageTopRight.Set(touchDamageCheck.position.x + touchDamageWidth / 2, touchDamageCheck.position.y + touchDamageHeight / 2);

        Collider2D hit = Physics2D.OverlapArea(touchDamageBotLeft, touchDamageTopRight, whatIsPlayer);

        if (hit != null)
        {
            hit.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            attackDetails[0] = laserDamage/2;
            attackDetails[1] = transform.position.x;
            hit.SendMessage("Damage", attackDetails);

        }
        
    }
    public void DestroyLaserAfterAnim()
    {
        Destroy(gameObject);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Vector2 botLeft = new Vector2(touchDamageCheck.position.x - touchDamageWidth / 2, touchDamageCheck.position.y - touchDamageHeight / 2);
        Vector2 botRight = new Vector2(touchDamageCheck.position.x + touchDamageWidth / 2, touchDamageCheck.position.y - touchDamageHeight / 2);
        Vector2 topLeft = new Vector2(touchDamageCheck.position.x - touchDamageWidth / 2, touchDamageCheck.position.y + touchDamageHeight / 2);
        Vector2 topRight = new Vector2(touchDamageCheck.position.x + touchDamageWidth / 2, touchDamageCheck.position.y + touchDamageHeight / 2);
        Gizmos.DrawLine(botLeft, botRight);
        Gizmos.DrawLine(botRight, topRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, botLeft);
    }
}
