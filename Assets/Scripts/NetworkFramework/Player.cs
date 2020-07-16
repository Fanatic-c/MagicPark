using System;
using System.Collections;
using System.Collections.Generic;
using ConnectBridge;
using ConnectBridge.Util;
using UnityEngine;
using NetworkFramework.Request;

namespace NetworkFramework
{
    public class Player : MonoBehaviour
    {
        public bool isLocalPlayer = true;
        [HideInInspector] public string username;
        private SyncPosRequest _syncPosRequest;
        private SyncPlayerRequest _syncPlayerRequest;
        private Vector3 lastPosition = Vector3.zero;
        private Vector3 lastEulerAngles = Vector3.zero;
        private float moveOffset = 0f;
        public GameObject playerPrefab;
        public GameObject player;

        private Dictionary<string, GameObject> playerDict = new Dictionary<string, GameObject>();

        private void Awake()
        {
            username = PhotonEngine.Username;
        }

        private void Start()
        {
            _syncPosRequest = GetComponent<SyncPosRequest>();
            _syncPlayerRequest = GetComponent<SyncPlayerRequest>();
            _syncPlayerRequest.DefaultRequest();
            InvokeRepeating(nameof(SyncPosition), 3f, 0.05f);
        }

        void SyncPosition()
        {
            if (Vector3.Distance(transform.position, lastPosition) > moveOffset)
            {
                lastPosition = player.transform.position;
                lastEulerAngles = player.transform.eulerAngles;
                _syncPosRequest.position = player.transform.position;
                _syncPosRequest.eulerAngles = player.transform.eulerAngles;
                _syncPosRequest.DefaultRequest();
            }
        }

        //创建其他客户端player
        public void OnSyncPlayerResponse(List<string> usernameList)
        {
            foreach (string u in usernameList)
            {
                OnNewPlayerEvent(u);
            }
        }

        public void OnNewPlayerEvent(string u)
        {
            GameObject otherplayer = Instantiate(playerPrefab) as GameObject;
            playerDict.Add(u, otherplayer);
        }

        public void OnSyncPositionEvent(List<PlayerPositionData> posData)
        {
            foreach (PlayerPositionData data in posData)
            {
                if (!username.Equals(data.Username))
                {
                    GameObject player = DictUtil.GetValue(playerDict, data.Username);
                    Debug.Log(data.Username);
                    player.transform.position = new Vector3((float)data.Pos.X, (float)data.Pos.Y, (float)data.Pos.Z);
                    player.transform.eulerAngles = new Vector3((float)data.Angle.X, (float)data.Angle.Y, (float)data.Angle.Z);
                }
            }
        }
    }
}