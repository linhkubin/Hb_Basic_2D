using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Link.HouseStack
{
    public enum HouseType
    {
        House_1, House_2, House_3, House_4, House_5, House_6, House_7,
    }

    public class Control : MonoBehaviour
    {
        private static Control instance;    
        public static Control Instance
        {
            get {
                if (instance == null){
                    instance = FindObjectOfType<Control>();
                }
                return instance; 
            }
        }

        public enum State { Building, Droping, Stoping }
        private State state = State.Building;    

        [SerializeField] House housePrefab;
        [SerializeField] House houseLast;
        [SerializeField] House housePreLast;
        [SerializeField] Transform crane;
        [SerializeField] Transform housePoint;
        [SerializeField] float moveSpeed = 1;
        [SerializeField] float space = 3.5f;

        private void Start()
        {
            StartBuilding();
        }

        private void Update()
        {
            if (state == State.Building)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    DropBuilding();
                }
                else
                {
                    houseLast.SetPoisition(housePoint.position);
                }
            }
            if(state == State.Stoping && housePreLast != null && transform.position.y < housePreLast.transform.position.y + space) 
            {
                transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
            }
        }

        private int[] craneAngle = new[] { -30, -20, -10, 10, 20, 30 };

        private void StartBuilding()
        {
            state = State.Building;
            SpawnHouse(housePoint.position, Quaternion.identity);
            crane.eulerAngles = Vector3.forward * craneAngle[Random.Range(0, craneAngle.Length)];
        }
        private void DropBuilding()
        {
            state = State.Droping;
            houseLast.OnDrop();
            Invoke(nameof(StopBuilding), 1f);
        }
        private void StopBuilding()
        {
            state = State.Stoping;
            Invoke(nameof(StartBuilding), 1.5f);
        }

        private void SpawnHouse(Vector2 pos, Quaternion rot)
        {
            housePreLast = houseLast;
            houseLast = Instantiate(housePrefab, pos, rot);
            houseLast.OnInit();
        }

        public void CheckHouseBuilding(House house)
        {
            if (house == houseLast || house == housePreLast)
            {
                Debug.Log("Lose");
            }
        }
    }
}
