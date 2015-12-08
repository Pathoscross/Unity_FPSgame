using System;
using System.Collections;
using UnityEngine;

public class playerControl : MonoBehaviour
{ //取得角色位置
    public Transform charcaterTransform;

    //取得攝影機位置
    private Transform cameraTransform;

    //取得角色控制
    public CharacterController player;

    //移動速度
    public float movSpeed = 3.0f;

    //跑步加成
    public float speedBonus = 2.0f;

    //跳躍高度
    public float jumpHight = 3.0f;

    //角色重力
    public float charactGravity = 2.0f;

    //是否在地板上
    private bool isGround = true;

    //玩家攝影機
    public Camera playerCamera;

    //攝影機旋轉角度
    private Vector3 cameraRotation;

    //角色血量
    public int healthPoint = 100;

    //攝影機高度
    public float cameraHight = 0.0f;

    //槍口
    private GameObject muzzlepoint;

    //射線碰撞層
    public LayerMask shootedLayer;

    //噴出的粒子特效
    public Transform bulletFX;

    //射擊間隔
    private float shootTime = 0;

    //射擊音效
    //public AudioClip shootAudio;
    //音效資源
    //public AudioSource shoot;

    public GameObject bulletTest;

    //子彈perfab
    public GameObject bullet;

    private void Start()
    {
        charcaterTransform = this.GetComponent<Transform>();
        player = this.GetComponent<CharacterController>();

        //取得攝影機
        cameraTransform = Camera.main.transform;

        //設定攝影機初始位置
        Vector3 cameraPosition = cameraTransform.position;
        cameraPosition.y += cameraHight;
        cameraTransform.position = cameraPosition;
        cameraTransform.rotation = player.transform.rotation;

        //初始化滑鼠狀態
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //取得槍口位置
        muzzlepoint = GameObject.Find("muzzlepoint_temp");

        //取得音效組件
        //shoot.GetComponent<AudioSource>();
    }

    private void Update()
    {
        //生命值狀況
        if (healthPoint <= 0)
        {
            return;
        }
        else
        {
            if (Cursor.visible == false)
            {
                cameraControl();
                Control();
            }
        }

        //更新射擊時間
        shootTime -= Time.deltaTime;

        //滑鼠游標狀態
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            curosrState();
        }
    }

    private void Control()
    {
        float x = 0, y = 0, z = 0;
        float moveSpeed;

        //charactGravity
        y -= charactGravity * Time.deltaTime;
        // jump
        if (Input.GetKeyDown(KeyCode.Space) && player.isGrounded == true)
        {
            y += jumpHight * Time.deltaTime;
        }

        //running
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = movSpeed * speedBonus;
        }
        else
        {
            moveSpeed = movSpeed;
        }

        // up and down
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            z += moveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            z -= moveSpeed * Time.deltaTime;
        }
        // left and right
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            x -= moveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            x += moveSpeed * Time.deltaTime;
        }

        // Shoot
        if (Input.GetMouseButton(0) && shootTime <= 0.0f)
        {
            //Debug.Log("射擊");
            shootTime = 0.1f;

            //射擊音效
            //shoot.PlayOneShot(shootAudio, 1.0f);

            //減少彈藥，更新彈藥 UI

            //探測擊中結果
            RaycastHit info;

            //射出射線
            bool hit = Physics.Raycast(cameraTransform.position, cameraTransform.TransformDirection(Vector3.forward), out info, 100.0f, shootedLayer);

            if (hit)
            {
                if (info.transform.tag.CompareTo("enemy") == 0)
                {
                }
                //擊中點播放特效
                Instantiate(bulletFX, info.point, info.transform.rotation);
                Instantiate(bulletTest, info.point, info.transform.rotation);
            }

            //

            //生成一個子彈
            Instantiate(bullet, muzzlepoint.transform.position, muzzlepoint.transform.rotation);
            colliderDestory.CatchPoint(info.point);

            //Debug.Log("point:" + info.normal);
        }

        player.Move(charcaterTransform.TransformDirection(new Vector3(x, y, z)));
    }

    private void curosrState()
    {
        // 打開滑鼠游標
        if (Cursor.visible == false)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        // 隱藏滑鼠游標
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void cameraControl()
    {
        //取得滑鼠位置
        float cameraY = Input.GetAxis("Mouse X");
        float cameraX = Input.GetAxis("Mouse Y");

        // 旋轉太低停止繼續轉
        if (playerCamera.transform.rotation.x > 0.4f)
        {
            if (cameraX > 0)
            {
                playerCamera.transform.Rotate(-cameraX, 0.0f, 0.0f, Space.Self);
            }
        }
        // 旋轉太高停止繼續轉
        else if (playerCamera.transform.rotation.x < -0.4f)
        {
            if
             (cameraX < 0)
            {
                playerCamera.transform.Rotate(-cameraX, 0.0f, 0.0f, Space.Self);
            }
        }
        // 正常情況旋轉
        else
        {
            playerCamera.transform.Rotate(-cameraX, 0.0f, 0.0f, Space.Self);
        }
        // 左右旋轉不控制
        //playerCamera.transform.Rotate(0.0f, cameraY, 0.0f, Space.World);
        player.transform.Rotate(0.0f, cameraY, 0.0f, Space.World);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(this.transform.position, "Spawn.tif");
    }
}