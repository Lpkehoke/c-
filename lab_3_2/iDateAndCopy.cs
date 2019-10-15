using System;

namespace lab_3_2 {
    public interface IDateAndCopy {
        object DeepCopy();

        System.DateTime Date {
            get;
            set;
        }
    };

}
