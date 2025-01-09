using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReolder : MonoBehaviour
{
public void Reoled()
    {
        int getLevel = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(getLevel);

    }
}
