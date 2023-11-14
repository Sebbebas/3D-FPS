using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Sceneloader : MonoBehaviour
{
    //Next Scene transition
    [Header("Transition")]
    [SerializeField] Animation[] animations;

    //Reload scene when collide
    [Header("Death")]
    [SerializeField] Collider[] colliders;
    [SerializeField] LayerMask killLayers;

    //Quit
    [Header("QuitGame")]
    [SerializeField] Quiting[] quitLife;

    [Header("OLD")]
    [SerializeField] float transitionTime = 1.5f;
    [SerializeField] int quitTime = 1;

    //Loads a certian Scene
    public void LoadSceneNumber(int sceneNumber)
    {
        StartCoroutine(ChangeSceneRoutine(sceneNumber));
    }

    IEnumerator ChangeSceneRoutine(int scene)
    {
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(scene);
    }

    //Reload Scene
    public void ReloadScene()
    {
        StartCoroutine(ReloaadSceneRoutine());
    }

    IEnumerator ReloaadSceneRoutine()
    {
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //Quits the game
    public void Quit()
    {
        StartCoroutine(QuitGameRoutine());
    }

    IEnumerator QuitGameRoutine()
    {
        yield return new WaitForSeconds(quitTime);
        Application.Quit();
        Debug.Log("Quit");
    }

    [System.Serializable] public struct Animation
    {
        [Tooltip("Animator")]public Animator animator;
        [Tooltip("bool name")]public string name;
        public float transitionTime;
    }
    [System.Serializable] public struct Quiting
    {
        
    }
}