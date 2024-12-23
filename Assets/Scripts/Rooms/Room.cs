// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Room : MonoBehaviour
// {
//     [SerializeField] private GameObject[] enemies; //mảng lưu chữ all kẻ thủ
//     private Vector3[] initialPostion; //VT bđầu của all kẻ thù

//     private void Awake()
//     {
//         //Save the initial positions of the enemies
//         initialPostion = new Vector3[enemies.Length];
//         for (int i = 0; i < enemies.Length; i++)
//         {
//             if(enemies[i] != null)
//                 initialPostion[i] = enemies[i].transform.position; // lưu VT của kẻ thù
//         }
//     }

//     public void ActivateRoom(bool _status)// tấn công kẻ thù
//     {
//         //Activate/deactivate enemies
//         for (int i = 0; i < enemies.Length; i++)
//         {
//             if(enemies[i] != null)
//             {
//                 enemies[i].SetActive(_status);
//                 enemies[i].transform.position = initialPostion[i];
//             }
//                 initialPostion[i] = enemies[i].transform.position; // lưu VT của kẻ thù
//         }
//     }
// }
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    private Vector3[] initialPosition;

    private void Awake()
    {
        //Save the initial positions of the enemies
        initialPosition = new Vector3[enemies.Length];
        for (int i = 0; i < enemies.Length; i++)
        {
            if(enemies[i] != null)
                initialPosition[i] = enemies[i].transform.position;
        }
    }
    public void ActivateRoom(bool _status)
    {
        //Activate/deactivate enemies
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                enemies[i].SetActive(_status);
                enemies[i].transform.position = initialPosition[i];
            }
        }
    }
}