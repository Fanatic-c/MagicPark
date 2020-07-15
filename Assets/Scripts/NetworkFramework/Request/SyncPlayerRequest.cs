using System;
using System.Collections;
using System.Collections.Generic;
using ConnectBridge;
using ConnectBridge.Util;
using ExitGames.Client.Photon;
using LitJson;
using NetworkFramework.Request;
using UnityEngine;

namespace NetworkFramework.Request
{
    public class SyncPlayerRequest : RequestBase
    {
        private Player _player;

        private void Awake()
        {
            OpCode = OperationCode.SyncPlayer;
            _player = GetComponent<Player>();
        }

        public override void DefaultRequest()
        {
            PhotonEngine.Peer.OpCustom((byte)OpCode, null, true);
        }

        public override void OnOperationResponse(OperationResponse resp)
        {
            string usernameListJson = (string)DictUtil.GetValue(resp.Parameters, (byte)ParameterCode.UsernameList);
            List<string> usernameList = JsonMapper.ToObject<List<string>>(usernameListJson);
            _player.OnSyncPlayerResponse(usernameList);
        }
    }
}