using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Dictionary<string, float> DamageDic;
    public Dictionary<string, float> HealthDic;
    public Dictionary<string, float> FireRateDic;
    public Dictionary<string, float> SpeedDic;
    public Dictionary<string, float> TurnSpeedDic;
    public Dictionary<string, int> RewardDic;

    public float money;

    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
        DamageDic = new Dictionary<string, float>();
        HealthDic = new Dictionary<string, float>();
        FireRateDic = new Dictionary<string, float>();
        SpeedDic = new Dictionary<string, float>();
        TurnSpeedDic = new Dictionary<string, float>();
        RewardDic = new Dictionary<string, int>();

       /* minX = -150;
        maxX = 150;
        minZ = -200;
        maxZ = 200;*/

        DamageDic.Add("Player", 10f);
        DamageDic.Add("boat", 5f);
        DamageDic.Add("ship", 10f);
        DamageDic.Add("Limit", 10f);

        HealthDic.Add("Player", 100f);
        HealthDic.Add("boat", 10f);
        HealthDic.Add("ship", 20f);

        FireRateDic.Add("Player", 2f);
        FireRateDic.Add("boat", 3f);
        FireRateDic.Add("ship", 2f);

        SpeedDic.Add("Player", 15f);
        SpeedDic.Add("boat", 9f);
        SpeedDic.Add("ship",7f);

        TurnSpeedDic.Add("Player", 2.5f);
        TurnSpeedDic.Add("boat", 1.5f);
        TurnSpeedDic.Add("ship", 1f);
        
        RewardDic.Add("boat", 100);
        RewardDic.Add("ship", 200);
    }

    public void addMoney(string tag)
    {
        money += RewardDic[tag];
    }

    public float calculateDamage(string type, float health)
    {
        return health - DamageDic[type];
    }

    public void updateDamage(string type, float amount)
    {
        DamageDic[type] = amount + DamageDic[type];
    }

    public void updateSteering(string type, float amount)
    {
        TurnSpeedDic[type] = amount + TurnSpeedDic[type];
    }

    public void updateSpeed(string type, float amount)
    {
        SpeedDic[type] = amount + SpeedDic[type];
    }

    public void updateMaxHeatlh(string type, float amount)
    {
        HealthDic[type] = amount + HealthDic[type];
    }

    public void resetGame()
    {
        PlayerController.instance.reset();
        EnemyFactory.instance.clearEnemies();
        CameraController.instance.changeToMenu();
        MenuManager.instance.StartMenu();
        SpawnManager.instance.maxEnemies = 3;

        DamageDic["boat"] = 5f;
        DamageDic["ship"] = 10f;
        SpeedDic["boat"] = 9f;
        SpeedDic["ship"] = 7f;
        TurnSpeedDic["boat"] = 1.5f;
        TurnSpeedDic["ship"] = 2f;
        HealthDic["boat"] = 10f;
        HealthDic["ship"] = 20f;
    }
    public void enableFrontCannon()
    {
        PlayerController.instance.frontCannon.SetActive(true);
        PlayerController.instance.frontPos.gameObject.SetActive(true);
    }


}
