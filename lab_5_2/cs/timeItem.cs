using System;


namespace lab_5_2 {
    [Serializable()]
    class TimeItem {
        public TimeItem() {
            Order   = 0;
            Repeat  = 0;
            TimeCpp = 0;
            TimeCs  = 0;
            Factor  = 0.0;
        }

        public TimeItem(TimeItem a) {
            Order   = a.Order;
            Repeat  = a.Repeat;
            TimeCpp = a.TimeCpp;
            TimeCs  = a.TimeCs;
            Factor  = a.Factor;
        }

        public override string ToString() {
            return Order + "\t" + Repeat + "\t" + TimeCpp + "\t" + TimeCs + "\t" + Factor;
        }

        public int Order;
        public int Repeat;
        public int TimeCpp;
        public int TimeCs;
        public double Factor;
    };
}
