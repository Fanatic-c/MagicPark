using System;
using ConnectBridge;
using ConnectBridge.Util;
using ExitGames.Client.Photon;

namespace NetworkFramework.Event
{
    public class NewPlayerEvent : EventBase
    {
        private Player _player;
        private void Awake()
        {
            eventCode = EventCode.NewPlayer;
            _player = GetComponent<Player>();
        }

        public override void OnEvent(EventData eventData)
        {
            string username = (string)DictUtil.GetValue(eventData.Parameters, (byte)ParameterCode.Username);
            _player.OnNewPlayerEvent(username);
        }
    }
}
