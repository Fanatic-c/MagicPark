using UnityEngine;

namespace InputFramework {
    public class NormalInputAxis {
        private KeyCode _positive; //正向按键
        private KeyCode _negative; //反向按键

        public KeyCode Positive {
            get => _positive;
            set => _positive = value;
        }

        public KeyCode Negative {
            get => _negative;
            set => _negative = value;
        }

        /// <summary>
        /// 轴向构造器
        /// </summary>
        /// <param name="positive">正向按键</param>
        /// <param name="negative">反向按键</param>
        public NormalInputAxis(KeyCode positive, KeyCode negative) {
            Positive = positive;
            Negative = negative;
        }
    }
}