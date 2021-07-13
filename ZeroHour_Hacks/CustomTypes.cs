using System;

namespace CustomTypes
{
    public static class m_Types
    {
        public class trackedBool
        {
            public bool currentValue;
            public bool wasValue;
            public trackedBool(bool v)
            {
                currentValue = v;
                wasValue = v;
            }

            public void setPrevious()
            {
                wasValue = currentValue;
            }

            public bool changed()
            {
                return (currentValue == wasValue ? false : true);
            }
        }
        public class trackedFloat
        {
            public float currentValue;
            public float wasValue;
            public trackedFloat(float v)
            {
                currentValue = v;
                wasValue = v;
            }

            public void setPrevious()
            {
                wasValue = currentValue;
            }

            public bool changed()
            {
                return (currentValue == wasValue ? false : true);
            }
        }


        public static void trackedBoolExecution(m_Types.trackedBool db, Action enabled, Action disabled, bool optionalCondition = false)
        {
            if (db.changed() || optionalCondition)
            {
                if (db.currentValue)
                {
                    enabled();
                }
                else
                {
                    disabled();
                }
            }
            db.setPrevious();
        }
        public static void trackedBoolExecutionWithFloat(m_Types.trackedBool db, m_Types.trackedFloat val, Action enabled, Action disabled, bool optionalCondition = false)
        {
            if (val.changed() || db.changed() || optionalCondition)
            {
                if (db.currentValue)
                {
                    enabled();
                }
                else
                {
                    disabled();
                }
            }
            db.setPrevious();
            val.setPrevious();
        }

    }
}
