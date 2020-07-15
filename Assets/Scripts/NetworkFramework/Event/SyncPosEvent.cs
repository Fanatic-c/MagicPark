using ConnectBridge;
using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;
using ConnectBridge.Util;
using LitJson;
using UnityEngine;

namespace NetworkFramework.Event {
    public class SyncPosEvent : EventBase {

        private Player _player;

        private void Awake() {
            _player = GetComponent<Player>();
            eventCode = EventCode.SyncPosition;
        }

        public override void OnEvent(EventData eventData) {
            string playerDataList = (string)DictUtil.GetValue(eventData.Parameters, (byte)ParameterCode.PlayerDataList);
            List<PlayerPositionData> posData = JsonMapper.ToObject<List<PlayerPositionData>>(playerDataList);
            _player.OnSyncPositionEvent(posData);
        }
    }
}