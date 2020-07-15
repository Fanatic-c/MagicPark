namespace PlayerFramework.AnimationFramework.Base {
    public abstract class FsmBase {
        public abstract void OnEnter();

        public abstract void OnLeave();

        public abstract void Update();
    }

    public class FsmManager {
        FsmBase[] allState;
        sbyte stateIndex;
        public sbyte curIndex;

        public FsmManager(byte stateCount) {
            allState = new FsmBase[stateCount];
            stateIndex = -1;
            curIndex = -1;
        }

        public void AddState(FsmBase state) {
            if (stateIndex < allState.Length) {
                stateIndex++;
                allState[stateIndex] = state;
            }
        }

        public void ChangeState(sbyte stateNumber) {
            stateNumber = (sbyte) (stateNumber % allState.Length);
            if (curIndex != -1) {
                allState[curIndex].OnLeave();
                curIndex = stateNumber;
                allState[curIndex].OnEnter();
            }
            else {
                curIndex = stateNumber;
                allState[curIndex].OnEnter();
            }
        }

        public void Update() {
            if (curIndex != -1) {
                allState[curIndex].Update();
            }
        }
    }
}