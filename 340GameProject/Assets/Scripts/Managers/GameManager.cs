using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject playerPref;
    [HideInInspector] public GameObject player;
    [SerializeField] protected GameObject UI;

    public void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        Respawn(Vector3.zero);
    }

    void Update()
    {
        
    }

    public void Respawn(Vector3 pos)
    {
        var temp = Instantiate(playerPref, pos, Quaternion.identity);
        player = temp;
        player.GetComponent<PlayerController>().playerCam = Camera.main;
        player.GetComponent<Health>().healthBar = UI.GetComponentInChildren<PlayerHealthBar>().gameObject;
    }
}