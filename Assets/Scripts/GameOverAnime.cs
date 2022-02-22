using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverAnime : MonoBehaviour
{
    public void ActivateGOAnimation()
    {
        GetComponent<Animator>().SetTrigger("LoadGameOver");
    }
}
