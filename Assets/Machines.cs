using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machines : MonoBehaviour
{

    public GameObject machineModel;
    public GameObject pusherModel;
    public GameObject windshieldModel;
    public GameObject shieldPrefab;
    public GameObject frozenPrefab;
    public Material machineMaterial;
    public Material windshieldMaterial;
    public Color[] machineColors;
    public AudioClip kickSound;
    public AudioClip freezeSound;
    private Machine[] machines = new Machine[2];

    private bool[] pushButtonPressed = new bool[] { false, false};
    public string[] verticalAxisInputNames;
    public string[] horizontalAxisInputNames;

    private static Court courtScript;
    private bool controlsEnabled = true;
    public float machineMass;

    public AudioClip[] powerUpSounds;
    public GameObject minePrefab;
    public GameObject missilePrefab;

    internal bool isShielded(int machineIndex)
    {
        return machines[machineIndex].isShielded();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject court = GameObject.Find("Court");
        courtScript = court.GetComponent<Court>();

        for (int i = 0; i < 2; i++)
        {
            machines[i] = new Machine(machineModel, machineMaterial, machineColors[i], pusherModel, i, kickSound, windshieldModel, windshieldMaterial, verticalAxisInputNames[i], horizontalAxisInputNames[i], machineMass, shieldPrefab, powerUpSounds, minePrefab, missilePrefab, freezeSound, frozenPrefab);
            
        }
    }

    public Machine[] getMachines() {
        return machines;
    }

    private void Update()
    {
        if(controlsEnabled) {
            pushButtonPressed[0] = Input.GetButton("Push");
            pushButtonPressed[1] = Input.GetButton("Push_p2");            
            if(Input.GetButtonUp("PowerUp")) {
                machines[0].UsePowerUp();
            }
            if(Input.GetButtonUp("PowerUp_p2")) {
                machines[1].UsePowerUp();
            }
        }

        foreach(Machine machine in machines) {
            machine.UpdatePowerUps();
        } 
        
    }

    internal void FreezeMachine(int machineIndex)
    {
        machines[machineIndex].Freeze();
    }

    private void FixedUpdate()
    {
        

        for (int machineIndex = 0; machineIndex<machines.Length;machineIndex++)
        {
            if (pushButtonPressed[machineIndex]) machines[machineIndex].push();
            machines[machineIndex].adjustPush();            
            machines[machineIndex].ManagePosition();
        }

    }

    public void reset()
    {
        for (int machineIndex = 0; machineIndex < machines.Length; machineIndex++) reset(machineIndex);
    }

    private void reset(int machineIndex)
    {
        machines[machineIndex].reset();
    }

    public void disableControls() {
        controlsEnabled = false;
        pushButtonPressed[0] = false;
        pushButtonPressed[1] = false;

        for (int machineIndex = 0; machineIndex<machines.Length;machineIndex++)
        {
            machines[machineIndex].DisableControls();
        }
        

    }
    public void addPowerUp(int machineIndex, Type powerUpType){
        machines[machineIndex].AddPowerUp(powerUpType);
    }

    public class Machine
    {
        private const int WHEEL_COUNT = 4;
        private const int DEFAULT_MOTOR_TORQUE = 12000;
        float pushTime = 0;
        float energy = 4;
        public GameObject machineGameObject, pusherGameObject;
        int machineIndex;
        private string verticalAxisInputName, horizontalAxisInputName;
        private AudioClip[] powerUpSounds;
        private static Color PUSHER_COLOR_LESS_ENERGY = new Color(0.35f, 0.3f, 0.3f);
        private static Color PUSHER_COLOR_DEFAULT = new Color(0.5f, 0.5f, 0.5f);
        private static float pusherMovementRange = 9.0f;

        private static Vector3 pusherOffset = new Vector3(17+ pusherMovementRange / 2.0f, 0, 0);
        private Quaternion[] startRotations = new Quaternion[]{
            Quaternion.Euler(new Vector3(0, Mathf.Rad2Deg * (Mathf.PI / 2.0f), 0)),
            Quaternion.Euler(new Vector3(0, Mathf.Rad2Deg * (-Mathf.PI / 2.0f), 0))
        };
        
        private ConfigurableJoint pusherJoint;
        private JointDrive jointDrive;

        Renderer pusherRenderer;

        private static float PUSHER_PULL_FORCE = 1000 *10;
        private static float PUSHER_PUSH_FORCE = 5000 * 10;

        private static Vector3 PUSHER_PULL_TARGET_POS = new Vector3(0, 0, 4.5f);
        private static Vector3 PUSHER_PULL_TARGET_VEL = new Vector3(0, 0, 100);

        private static Vector3 PUSHER_PUSH_TARGET_POS = new Vector3(0, 0, -4.5f);
        private static Vector3 PUSHER_PUSH_TARGET_VEL = new Vector3(0, 0, -100);

        private static float WHEEL_RADIUS = 5.0f;

        private bool pushing = true;
        private AudioClip kickSound;
        private AudioSource pusherSound;
        private AudioSource powerUpAudioSource;
        private AudioSource freezeSoundAudioSource;
        private Rigidbody machineRigidbody;

        GameObject[] wheels;
        private float remainingShieldTime = 0;
        private GameObject shieldGameObject;
        private GameObject frozenGameObject;
        private bool shieldIsActive = false;
        private Type? currentPowerUpType = null;
        private int powerUpCount;
        private float turboTime;
        private GameObject minePrefab;
        private GameObject missilePrefab;
        private AudioClip freezeSound;
        private GameObject frozenPrefab;
        private float freezeTime = 0;
        private bool frozen= false;

        public Machine(GameObject machineModel, Material machineMaterial, Color machineColor, GameObject pusherModel, int machineIndex, AudioClip kickSound, GameObject windshieldModel, Material windshieldMaterial, string verticalAxisInputName, string horizontalAxisInputName, float machineMass, GameObject shieldPrefab, AudioClip[] powerUpSounds, GameObject minePrefab, GameObject missilePrefab, AudioClip freezeSound, GameObject frozenPrefab)
        {
            this.kickSound = kickSound;
            this.machineIndex = machineIndex;
            this.verticalAxisInputName = verticalAxisInputName;
            this.horizontalAxisInputName = horizontalAxisInputName;
            this.powerUpSounds = powerUpSounds;
            this.minePrefab = minePrefab;    
            this.missilePrefab = missilePrefab;    
            this.freezeSound = freezeSound;    
            this.frozenPrefab = frozenPrefab;
            createMachine(machineModel, machineMaterial, machineColor, machineMass);
            createWindshield(windshieldModel, windshieldMaterial);
            createPusher(machineMaterial, machineColor, pusherModel);
            reset();
            createSliderJoint();
            shieldGameObject = createShell(shieldPrefab);
            frozenGameObject = createShell(frozenPrefab);
            addCarControllerScript();
        }

        private GameObject createShell(GameObject shellPrefab)
        {
            GameObject shellGameObject = Instantiate(shellPrefab);
            Transform shellTransform = shellGameObject.transform;
            shellTransform.parent = machineGameObject.transform;
            shellTransform.localPosition = new Vector3(0,0,-3);
            shellTransform.localRotation = Quaternion.Euler(0, 180, 0);
            shellTransform.localScale = new Vector3(0.7f, 0.6f, 0.6f);
            return shellGameObject;
        }

        private void addCarControllerScript()
        {
            SimpleCarController simpleCarController = machineGameObject.AddComponent<SimpleCarController>();            
            AxleInfo frontAxle = new AxleInfo();
            frontAxle.wheels = new WheelCollider[] { wheels[0].GetComponent<WheelCollider>(), wheels[1].GetComponent<WheelCollider>() };
            frontAxle.motor = true;
            frontAxle.steering = true;


            AxleInfo rearAxle = new AxleInfo();
            rearAxle.wheels = new WheelCollider[] { wheels[2].GetComponent<WheelCollider>(), wheels[3].GetComponent<WheelCollider>() };
            rearAxle.motor = true;
            rearAxle.steering = false;
            simpleCarController.axleInfos = new List<AxleInfo>() { frontAxle, rearAxle }; ;
            simpleCarController.maxMotorTorque = DEFAULT_MOTOR_TORQUE;
            simpleCarController.maxSteeringAngle = 50;
            simpleCarController.verticalAxisInputName = verticalAxisInputName;
            simpleCarController.horizontalAxisInputName = horizontalAxisInputName;
            
        }

        private void createSliderJoint()
        {
            ConfigurableJoint configurableJoint = pusherGameObject.AddComponent<ConfigurableJoint>();
            configurableJoint.connectedBody = machineGameObject.GetComponent<Rigidbody>();
            configurableJoint.xMotion = ConfigurableJointMotion.Locked;
            configurableJoint.yMotion = ConfigurableJointMotion.Locked; 
            configurableJoint.zMotion = ConfigurableJointMotion.Limited;
            configurableJoint.angularXMotion = ConfigurableJointMotion.Locked;
            configurableJoint.angularYMotion = ConfigurableJointMotion.Locked;
            configurableJoint.angularZMotion = ConfigurableJointMotion.Locked;
            SoftJointLimit softJointLimit = new SoftJointLimit();
            softJointLimit.limit = pusherMovementRange / 2.0f;
            configurableJoint.linearLimit = softJointLimit;

            this.pusherJoint = configurableJoint;

            this.jointDrive = new JointDrive();

            pullSlider();

        }

        private void pullSlider()
        {
            if (!pushing) return;
            pushing = false;
            pusherJoint.targetPosition = PUSHER_PULL_TARGET_POS;
            pusherJoint.targetVelocity = PUSHER_PULL_TARGET_VEL;
            jointDrive.positionSpring = PUSHER_PULL_FORCE;
            jointDrive.maximumForce = PUSHER_PULL_FORCE;

            pusherJoint.zDrive = jointDrive;
        }

        private void pushSlider()
        {
            if (pushing) return;
            pushing = true;
            pusherJoint.targetPosition = PUSHER_PUSH_TARGET_POS;
            pusherJoint.targetVelocity = PUSHER_PUSH_TARGET_VEL;
            jointDrive.positionSpring = PUSHER_PUSH_FORCE;
            jointDrive.maximumForce = PUSHER_PUSH_FORCE;

            pusherJoint.zDrive = jointDrive;
        }

        private void createMachine(GameObject machineModel, Material machineMaterial, Color machineColor, float machineMass)
        {
            // Create Machine
            machineGameObject = Instantiate(machineModel);
            machineGameObject.tag = "Machine";
            machineGameObject.name = "Machine "+machineIndex;

            Renderer renderer = machineGameObject.GetComponent<Renderer>();
            renderer.material = machineMaterial;
            renderer.material.color = machineColor;

            machineRigidbody = machineGameObject.AddComponent<Rigidbody>();
            machineRigidbody.SetDensity(1.0f);
            machineRigidbody.mass = machineMass;
            machineRigidbody.drag = 0.05f;
            machineRigidbody.angularDrag = 0.05f;
            machineRigidbody.centerOfMass = new Vector3(0, -2.0f, 0);

            ConstantForce constantForce = machineGameObject.AddComponent<ConstantForce>();
            constantForce.force = new Vector3(0, -10000, 0);

            MeshCollider meshCollider = machineGameObject.AddComponent<MeshCollider>();
            meshCollider.convex = true;   
            
            powerUpAudioSource = machineGameObject.AddComponent<AudioSource>();
            freezeSoundAudioSource = machineGameObject.AddComponent<AudioSource>();            
            freezeSoundAudioSource.clip = freezeSound;

            createWheels();
            createWheelJoints();
        }

        private void createWheelJoints()
        {
            int wheelIndex = 0;
            foreach(GameObject wheel in wheels)
            {
                WheelCollider wheelCollider = wheel.AddComponent<WheelCollider>();
                wheelCollider.radius = WHEEL_RADIUS;
                JointSpring jointSpring = new JointSpring();
                if(wheelIndex<=1)
                {
                    jointSpring.spring = 10000;
                    jointSpring.damper = 200;
                }
                else
                {

                    jointSpring.spring = 100000;
                    jointSpring.damper = 2000;
                }
                jointSpring.targetPosition = 0.5f;
                
                wheelCollider.suspensionDistance = 0.1f;;
                wheelCollider.suspensionSpring = jointSpring;
                WheelFrictionCurve wheelFrictionCurve = new WheelFrictionCurve();
                wheelFrictionCurve.stiffness = 4;
                wheelFrictionCurve.extremumSlip = 0.2f;
                wheelFrictionCurve.extremumValue = 1;
                wheelFrictionCurve.asymptoteSlip = 0.5f;
                wheelFrictionCurve.asymptoteValue = 0.75f;
                wheelCollider.forwardFriction = wheelFrictionCurve;
                wheelCollider.sidewaysFriction = wheelFrictionCurve;
                wheelIndex++;

            }
        }

        private void createWheels()
        {

            wheels = new GameObject[WHEEL_COUNT];
            
            for(int i=0; i< WHEEL_COUNT; i++)
            {
                wheels[i] = new GameObject();
                wheels[i].layer = 8;
            }


            wheels[0].transform.position = new Vector3(-6, -0.5f, -12);
            wheels[1].transform.position = new Vector3(6, -0.5f, -12);
            wheels[2].transform.position = new Vector3(-6, -0.5f, 7);
            wheels[3].transform.position = new Vector3(6,-0.5f,7);


            for (int i = 0; i < WHEEL_COUNT ; i++) wheels[i].transform.parent = machineGameObject.transform;



        }

        private void createWindshield(GameObject windshieldModel, Material windshieldMaterial)
        {
            GameObject windshieldGameObject = Instantiate(windshieldModel);
            windshieldGameObject.transform.parent = machineGameObject.transform;
            Renderer renderer = windshieldGameObject.GetComponent<Renderer>();
            renderer.material = windshieldMaterial;
            windshieldGameObject.layer = 9;

        }

        private void createPusher(Material machineMaterial, Color machineColor, GameObject pusherModel)
        {
            // Create Pusher
            pusherGameObject = Instantiate(pusherModel);
            pusherGameObject.transform.parent = machineGameObject.transform;
            pusherGameObject.layer = 9;

            pusherRenderer = pusherGameObject.GetComponent<Renderer>();
            pusherRenderer.material = machineMaterial;
            pusherRenderer.material.color = PUSHER_COLOR_DEFAULT;

            Rigidbody rigidbody = pusherGameObject.AddComponent<Rigidbody>();
            rigidbody.SetDensity(1.0f);
            rigidbody.mass = 0.3f * 40;

            MeshCollider meshCollider = pusherGameObject.AddComponent<MeshCollider>();
            meshCollider.convex = true;

            pusherSound = pusherGameObject.AddComponent<AudioSource>();
            pusherSound.clip = kickSound;
        }

        public void reset()
        {
            machineRigidbody.angularVelocity = Vector3.zero;
            machineRigidbody.velocity = Vector3.zero;
            if (machineIndex == 0)
            {
                machineGameObject.transform.SetPositionAndRotation(new Vector3(-courtScript.getLength() / 2.0f, 26.5f, 0), startRotations[machineIndex]);
                pusherGameObject.transform.SetPositionAndRotation(machineGameObject.transform.position + pusherOffset, startRotations[machineIndex]);
            }
            else
            {
                machineGameObject.transform.SetPositionAndRotation(new Vector3(courtScript.getLength() / 2.0f, 26.5f, 0), startRotations[machineIndex]);
                pusherGameObject.transform.SetPositionAndRotation(machineGameObject.transform.position - pusherOffset, startRotations[machineIndex]);
            }
            currentPowerUpType = null;
            powerUpCount = 0;

        }

        public void adjustPush()
        {

            pushTime -= Time.deltaTime;
            energy += Time.deltaTime;

            if (pushTime > 0)
            {
                pushSlider();
            }
            else pullSlider();

            if(energy>=4)
            {
                pusherRenderer.material.color = PUSHER_COLOR_DEFAULT;
            }
            else
            {
                pusherRenderer.material.color = PUSHER_COLOR_LESS_ENERGY;
            }

        }

        public void push()
        {

            if(energy >= 4)
            {
                pushTime = 0.5f;
                energy = 0.0f;
                pusherSound.Play();
            }

        }

        

        internal void AddPowerUp(Type powerUpType)
        {
            if(powerUpType == Type.SHIELD) {
                remainingShieldTime += 20;
                shieldIsActive = true;
                shieldGameObject.SetActive(true);
            } else {

                if(currentPowerUpType != powerUpType) {
                    currentPowerUpType = powerUpType;
                    powerUpCount = 1;
                } else powerUpCount++;

                if(powerUpType == Type.MINE) {
                    // Two mines per powerup
                    powerUpCount++;
                }
            }
        }

        internal void UpdatePowerUps()
        {
            if(shieldIsActive) {
                remainingShieldTime -= Time.deltaTime;    
                if(remainingShieldTime < 0) {
                    shieldGameObject.SetActive(false);
                    shieldIsActive = false;
                    remainingShieldTime = 0;
                }
            } 

            if(frozen) {                
                freezeTime -= Time.deltaTime;
                if(freezeTime < 0) {
                    frozenGameObject.SetActive(false);
                    EnableControls();
                    frozen = false;
                    freezeTime = 0;
                }                
            }
            


            if(turboTime > 0) {
                turboTime -= Time.deltaTime;
                machineRigidbody.AddRelativeForce(new Vector3(0,0,4000000f)*Time.deltaTime);
            }  
                        
        }

        internal bool isShielded()
        {
            return shieldIsActive;
        }

        internal void UsePowerUp()
        {
            if(powerUpCount > 0) {  
                powerUpCount--;     
                Debug.Log("type: "+currentPowerUpType);         
                switch(currentPowerUpType) {
                    case Type.TURBO:                  
                        powerUpAudioSource.clip = powerUpSounds[(int) currentPowerUpType];
                        powerUpAudioSource.Play();
                        turboTime = 3;
                        break;
                    case Type.MINE:                        
                        powerUpAudioSource.clip = powerUpSounds[(int) currentPowerUpType];
                        powerUpAudioSource.Play();
                        GameObject mineGameObject = Instantiate(minePrefab, machineGameObject.transform.position, Quaternion.Euler(-90, 0, 0));
                        mineGameObject.GetComponent<Mine>().SetOwner(machineGameObject);
                        break;
                    case Type.MISSILE:
                        UseMissile();
                        break;
                    case Type.EMP:
                        UseEMP();
                        break;
                }
            }
        }

        private void UseMissile()
        {
            powerUpAudioSource.clip = powerUpSounds[(int) currentPowerUpType];
            powerUpAudioSource.Play();
            GameObject missilePrefabGameObject = Instantiate(missilePrefab, pusherGameObject.transform.position, Quaternion.Euler(machineGameObject.transform.rotation.eulerAngles.x, machineGameObject.transform.rotation.eulerAngles.y + 90, machineGameObject.transform.rotation.eulerAngles.z));
            Missile missile = missilePrefabGameObject.GetComponent<Missile>();
            missile.SetType(MissileType.MISSILE);
            missile.SetOwner(machineGameObject);
        }

        private void UseEMP()
        {
            powerUpAudioSource.clip = powerUpSounds[(int) currentPowerUpType];
            powerUpAudioSource.Play();
            GameObject missilePrefabGameObject = Instantiate(missilePrefab, pusherGameObject.transform.position, Quaternion.Euler(machineGameObject.transform.rotation.eulerAngles.x, machineGameObject.transform.rotation.eulerAngles.y + 90, machineGameObject.transform.rotation.eulerAngles.z));
            Missile missile = missilePrefabGameObject.GetComponent<Missile>();
            missile.SetType(MissileType.EMP);
            missile.SetOwner(machineGameObject);
        }

        internal void ManagePosition()
        {
            if(machineGameObject.transform.position.y > 350) {
                machineRigidbody.angularVelocity = Vector3.zero; 
                machineRigidbody.position = new Vector3(machineRigidbody.position.x, 320, machineRigidbody.position.z);
                machineRigidbody.velocity = Vector3.zero;
                machineRigidbody.rotation = Quaternion.Euler(machineRigidbody.rotation.eulerAngles.x, machineRigidbody.rotation.eulerAngles.y, 0);
            } 
            if(machineGameObject.transform.position.y > 100) {
                machineRigidbody.angularVelocity = Vector3.zero; 
                machineRigidbody.velocity = machineRigidbody.velocity * 0.95f;
                machineRigidbody.AddForce(new Vector3(0, -10f*(machineGameObject.transform.position.y-100),0));
            }

            if(machineGameObject.transform.position.y > 24) {
                Vector3 position = machineGameObject.transform.position;
                if(position.x > courtScript.getLength()) {
                    position.x = courtScript.getLength()-40;
                }
                if(position.x < -courtScript.getLength()) {
                    position.x = -(courtScript.getLength()-40);
                }
                if(position.z > courtScript.getWidth()) {
                    position.z = courtScript.getWidth()-40;
                }
                if(position.z < -courtScript.getWidth()) {
                    position.z = -(courtScript.getWidth()-40);
                }
                machineGameObject.transform.position = position;
            }
            
        }

        internal void Freeze()
        {
            Debug.Log("Freeze");
            if(shieldIsActive) return;
            frozenGameObject.SetActive(true);
            frozen = true;
            freezeTime = 9;
            DisableControls();
            freezeSoundAudioSource.Play();

        }

        internal void DisableControls()
        {
            machineGameObject.GetComponent<SimpleCarController>().maxMotorTorque = 0;
        }

        internal void EnableControls()
        {
            machineGameObject.GetComponent<SimpleCarController>().maxMotorTorque = DEFAULT_MOTOR_TORQUE;
        }
    }

}
