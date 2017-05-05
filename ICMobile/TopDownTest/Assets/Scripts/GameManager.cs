using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Dictionary<string, float> DamageDicc;
    public Dictionary<string, float> HealthDicc;
    public Dictionary<string, float> FireRateDicc;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
        DamageDicc = new Dictionary<string, float>();
        HealthDicc = new Dictionary<string, float>();
        FireRateDicc = new Dictionary<string, float>();

        DamageDicc.Add("Player", 10f);
        DamageDicc.Add("boat", 5f);
        DamageDicc.Add("ship", 10f);

        HealthDicc.Add("Player", 100f);
        HealthDicc.Add("boat", 30f);
        HealthDicc.Add("ship", 80f);

        FireRateDicc.Add("Player", 2f);
        FireRateDicc.Add("boat", 3f);
        FireRateDicc.Add("ship", 5f);
    }


    public float calculateDamage(string type, float health)
    {
        return health -DamageDicc[type];
    }

    public void updateDamage(string type, float newDamage)
    {
        DamageDicc[type] = newDamage;
    }
    
}
