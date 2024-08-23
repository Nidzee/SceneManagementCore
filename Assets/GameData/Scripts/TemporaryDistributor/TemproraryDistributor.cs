using System.Collections.Generic;
using System.Collections;
using UnityEngine;






public class TemproraryDistributor : MonoBehaviour
{
    public static TemproraryDistributor Instance;


    public EnemyBuffData_Poison EnemyBuffData_Poison;









    public void Awake()
    {
        Instance = this;
    }




}