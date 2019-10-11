using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{

    private int frequency;
    private float timeTillNextSpawn;
    private int[] frequencies = new int[]{3,10,30};
    private float turboFrequency;
    private float shieldFrequency;
    private float mineFrequency;
    private float missileFrequency;
    private float empFrequency;

    private float turboMaxRand;
    private float shieldMaxRand;
    private float mineMaxRand;
    private float missileMaxRand;
    private float empMaxRand;

    public GameObject powerUpPrefab;
    private Court court;
    public Machines machines;

    // Start is called before the first frame update
    void Start()
    {
        if(!GameControl.gameControl.matchSettings.powerupsEnabled) {
            Destroy(gameObject);
            return;
        } 
        frequency = frequencies[GameControl.gameControl.matchSettings.powerupFrequeny];
        calculateProbabilityValues();
        timeTillNextSpawn = frequency;
        court = GameObject.Find("Court").GetComponent<Court>();
    }

    void calculateProbabilityValues() {
        
        turboFrequency = GameControl.gameControl.matchSettings.powerupTurboEnabled ? 1 : 0;
        shieldFrequency = GameControl.gameControl.matchSettings.powerupShieldEnabled ? 1 : 0;
        mineFrequency = GameControl.gameControl.matchSettings.powerupMineEnabled ? 1 : 0;
        missileFrequency = GameControl.gameControl.matchSettings.powerupMissileEnabled ? 1 : 0;
        empFrequency = GameControl.gameControl.matchSettings.powerupEMPEnabled ? 1 : 0;

        float sum = turboFrequency+shieldFrequency+mineFrequency+missileFrequency+empFrequency;

        turboMaxRand = turboFrequency / sum;
        shieldMaxRand = shieldFrequency / sum     + turboMaxRand;
        mineMaxRand = mineFrequency / sum         + shieldMaxRand;
        missileMaxRand = missileFrequency / sum   + mineMaxRand;
        empMaxRand = 1.0f;

    }

    // Update is called once per frame
    void Update()
    {
        timeTillNextSpawn -= Time.deltaTime;
        if(timeTillNextSpawn < 0.0f)
        {
            if(GameObject.FindGameObjectsWithTag("PowerUp").Length<5){
                spawnPowerUp();
            }
            timeTillNextSpawn = frequency;
        } 
    }

    

    private void spawnPowerUp()
    {


        float x = UnityEngine.Random.Range(-court.getLength()*0.9f, court.getLength()*0.9f);
        float y = 15;
        float z = UnityEngine.Random.Range(-court.getWidth()*0.9f, court.getWidth()*0.9f);
        
        GameObject powerUpGameObject = Instantiate(powerUpPrefab, new Vector3(x,y,z), Quaternion.identity);
        PowerUpTrigger powerUpTrigger = powerUpGameObject.transform.GetChild(0).GetComponent<PowerUpTrigger>();
        powerUpTrigger.machines = machines; 
        
        

        float randomNumber = UnityEngine.Random.Range(0.0f,1.0f);
        if(randomNumber <= turboMaxRand) {
            powerUpTrigger.SetType(Type.TURBO);
        } else if(randomNumber <= shieldMaxRand) {
            powerUpTrigger.SetType(Type.SHIELD);
        } else if(randomNumber <= mineMaxRand) {
            powerUpTrigger.SetType(Type.MINE);
        } else if(randomNumber <= missileMaxRand) {
            powerUpTrigger.SetType(Type.MISSILE);
        } else {
            powerUpTrigger.SetType(Type.EMP);
        }

    }
}
