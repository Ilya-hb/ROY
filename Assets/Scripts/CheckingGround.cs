using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckingGround : Entity { 
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
            gameObject.GetComponentInParent<Hero>().isGrounded = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
            gameObject.GetComponentInParent<Hero>().isGrounded =true;
    }
}
