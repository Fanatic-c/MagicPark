using System;
using System.Collections;
using System.Collections.Generic;
using ConnectBridge;
using ExitGames.Client.Photon;
using NetworkFramework.Request;
using UnityEngine;

namespace NetworkFramework.Request
{
    public class SyncPosRequest : RequestBase
    {
        [HideInInspector] public Vector3 position;

        private void Awake()
        {
            OpCode = OperationCode.SyncPos;
        }

        public override void DefaultRequest()
        {
            Dictionary<byte, object> data = new Dictionary<byte, object>();
            data.Add((byte)ParameterCode.PositionX, position.x);
            data.Add((byte)ParameterCode.PositionY, position.y);
            data.Add((byte)ParameterCode.PositionZ, position.z);
            PhotonEngine.Peer.OpCustom((byte)OperationCode.SyncPos, data, true);
        }

        public override void OnOperationResponse(OperationResponse resp)
        {
            throw new NotImplementedException();
        }
    }
}