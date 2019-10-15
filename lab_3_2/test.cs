using System;

namespace lab_3_2 {
    class Test {
        public Test(string title, bool isPass) {
            Name   = title;
            IsPass = isPass;
        }

        public Test() {
            Name   = "Math";
            IsPass = true;
        }

        public override string ToString() {
            return Name + " " + IsPass;
        }

        public string Name;
        public bool   IsPass;
    };
};
