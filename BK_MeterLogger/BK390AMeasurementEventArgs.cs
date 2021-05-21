using System;

/// <summary>
/// Passes BK390A measurment event args from phy to event handler
/// </summary>
namespace BK_MeterLogger
{
    public class BK390AMeasurementEventArgs : EventArgs
    {
        #region Constants

        private enum Offset
        {
            Range = 0,
            Digit3,
            Digit2,
            Digit1,
            Digit0,
            Function,
            Status,
            Option1,
            Option2
        }

        private const int StatusJudge = 0x08;
        private const int StatusSign = 0x04;
        private const int StatusBatt = 0x02;
        private const int StatusOverflow = 0x01;

        private const int Option2DC = 0x08;
        private const int Option2AC = 0x04;
        private const int Option2Auto = 0x02;
        private const int Option2Apo = 0x01;

        //private static readonly decimal[] VoltageRangeDecimal =
        //{
        //   0.400M, 4.000M, 40.00M, 400.0M, 4000M, 0M, 0M, 0M
        //};

        private static readonly decimal[] VoltageRangeMultipliers =
        {
            0.1M, 0.001M, 0.01M, 0.1M, 1M, 1M, 1M, 1M
        };

        private static readonly string[] VoltageRangeUnits =
        {
            "mV", "V", "V", "V", "V", "", "", ""
        };

        private static readonly decimal[] mA_CurrentRangeDecimal =
        {
            0.0400M, 0.400M, 0M, 0M, 0M, 0M, 0M, 0M
        };

        private static readonly decimal[] mA_CurrentRangeMultipliers =
        {
            0.01M, 0.1M, 1M, 1M, 1M, 1M, 1M, 1M
        };

        private static readonly string[] mA_CurrentRangeUnits =
        {
            "mA", "mA", "", "", "", "", "", ""
        };

        private static readonly decimal[] uA_CurrentRangeDecimal =
        {
            0.000400M, 0.00400M, 0M, 0M, 0M, 0M, 0M, 0M
        };

        private static readonly decimal[] uA_CurrentRangeMultipliers =
        {
            0.1M, 1M, 1M, 1M, 1M, 1M, 1M, 1M
        };

        private static readonly string[] uA_CurrentRangeUnits =
        {
            "μA", "μA", "", "", "", "", "", ""
        };

        private static readonly decimal A_CurrentResolutionDecimal = 100M;

        private static readonly decimal A_CurrentMultiplier = 0.01M;

        private static readonly string[] A_CurrentRangeUnits =
        {
            "A", "", "", "", "", "", "", ""
        };

        private static readonly decimal DiodeRangeDecimal = 4000M;
        private static readonly decimal DiodeRangeMultiplier = 0.001M;
        private static readonly string DiodeUnits = "V";

        private static readonly decimal ContinuityRangeDecimal = 400M;
        private static readonly decimal ContinuityMultiplier = 0.1M;
        private static string ContinuityUnits = "Ω";

        private static readonly decimal TemperatureRangeDecimal = 400M;
        private static readonly decimal TemperatureMultiplier = 0.1M;

        private static readonly string TemperatureCUnits = "\u00B0C";
        private static readonly string TemperatureFUnits = "\u00B0F";

        private static readonly decimal[] OhmRangeDecimal =
        {
            400M, 4000M, 40000M, 400000M, 4000000M, 40000000M, 0M, 0M
        };

        private static readonly decimal[] OhmRangeMultipliers =
        {
            0.1M, 0.001M, 0.01M, 0.1M, 0.001M, 0.01M, 1M, 1M
        };

        private static readonly string[] OhmRangeUnits =
        {
            "Ω", "KΩ", "KΩ", "KΩ", "MΩ", "MΩ", "", ""
        };

        private static readonly decimal[] FrequencyRangeDecimal =
        {
            4000M, 40000M, 400000M, 4000000M, 40000000M, 400000000M, 0M, 0M
        };

        private static readonly decimal[] FrequencyRangeMultipliers =
        {
            0.001M, 0.01M, 0.1M, 0.001M, 0.01M, 0.1M, 1M, 1M
        };

        private static readonly string[] FrequencyRangeUnits =
        {
            "KHz", "KHz", "KHz", "MHz", "MHz", "MHz", "", ""
        };

        private static readonly decimal[] RpmRangeDecimal =
        {
            40000M, 400000M, 4000000M, 40000000M, 400000000M, 4000000000M, 0M, 0M
        };

        private static readonly decimal[] RpmRangeMultipliers =
        {
            0.01M, 0.1M, 0.001M, 0.01M, 0.1M, 1M, 1M, 1M
        };

        private static readonly string[] RpmRangeUnits =
        {
            "KRPM", "KRPM", "MRPM", "MRPM", "MRPM", "MRPM", "", ""
        };

        private static readonly decimal[] CapacitanceRangeDecimal =
        {
            0.00000004M, 0.000000040M, 0.000000400M, 0.000004000M, 0.000040000M, 0.0004000000M, 0.004000000M, 0.040000000M
        };

        private static readonly decimal[] CapacitanceRangeMultipliers =
        {
            0.001M, 0.01M, 0.1M, 0.001M, 0.01M, 0.1M, 0.001M, 0.01M
        };

        private static readonly string[] CapacitanceRangeUnits =
        {
            "nF", "nF", "nF", "μF", "μF", "μF", "mF", "mF"
        };

        #endregion

        private byte[] _frame;

        public enum ReadingType
        {
            Voltage = 0x3b,
            uA_Current = 0x3d,
            mA_Current = 0x39,
            A_Current = 0x3f,
            Ohm = 0x33,
            Continuity = 0x35,
            Diode = 0x31,
            FrequencyRpm = 0x32,
            Capacitance= 0x36,
            Temperature = 0x34
        };

        public ReadingType Reading { get; private set; }

        private int _range;

        private decimal _digits;

        public bool OverFlow { get; private set; }

        public bool LowBattery { get; private set; }

        public bool Negative { get; private set; }

        public decimal Value { get; private set; }

        public bool DC { get; private set; }

        public bool AC { get; private set; }

        public bool Centigrade { get; private set; }

        public bool Farenheit { get; private set; }

        public bool Hertz { get; private set; }

        public bool RPM { get; private set; }

        public bool Auto { get; private set; }

        public bool APO { get; private set; }

        public string Units
        {
            get
            {
                string value = "";

                switch (Reading)
                {
                    case ReadingType.Voltage:
                        value = VoltageRangeUnits[_range];
                        if (AC) value += "AC";
                        if (DC) value += "DC";
                        break;

                    case ReadingType.uA_Current:
                        value = uA_CurrentRangeUnits[_range];
                        break;

                    case ReadingType.mA_Current:
                        value = mA_CurrentRangeUnits[_range];
                        break;

                    case ReadingType.A_Current:
                        value = A_CurrentRangeUnits[_range];
                        break;

                    case ReadingType.Ohm:
                        value = OhmRangeUnits[_range];
                        break;

                    case ReadingType.Continuity:
                        value = ContinuityUnits;
                        break;

                    case ReadingType.Diode:
                        value = DiodeUnits;
                        break;

                    case ReadingType.FrequencyRpm:
                        if (Hertz)
                        {
                            value = FrequencyRangeUnits[_range];
                        }

                        if (RPM)
                        {
                            value = RpmRangeUnits[_range];
                        }
                        break;

                    case ReadingType.Capacitance:
                        value = CapacitanceRangeUnits[_range];
                        break;

                    case ReadingType.Temperature:
                        if (Farenheit)
                        {
                            value = TemperatureFUnits;
                        }
                        else
                        {
                            value = TemperatureCUnits;
                        }
                        break;

                    default:
                        break;
                }

                return value;
            }
        }
        
        public BK390AMeasurementEventArgs(byte[] frame)
        {
            _frame = frame;

            _range = frame[(int) Offset.Range] - 0x30;

            _digits = Conversions.FourAsciiDecimalBytesToDecimal(frame[(int) Offset.Digit3], frame[(int) Offset.Digit2],
                frame[(int) Offset.Digit1], frame[(int) Offset.Digit0]);

            Reading = (ReadingType) frame[(int)Offset.Function];

            OverFlow = (frame[(int)Offset.Status] & StatusOverflow) != 0;

            LowBattery = (frame[(int)Offset.Status] & StatusBatt) != 0;

            Negative = (frame[(int) Offset.Status] & StatusSign) != 0;

            Auto = (frame[(int) Offset.Option2] & Option2Auto) != 0;

            APO = (frame[(int) Offset.Option2] & Option2Apo) != 0;

            DC = false;
            AC = false;
            Centigrade = false;
            Farenheit = false;
            Hertz = false;
            RPM = false;

            switch (Reading)
            {
                case ReadingType.Voltage:
                    AC = (frame[(int) Offset.Option2] & Option2AC) != 0;
                    DC = (frame[(int)Offset.Option2] & Option2DC) != 0;

                    Value = _digits * VoltageRangeMultipliers[_range];

                    if (Negative)
                    {
                        Value = Value * -1M;
                    }
                    break;

                case ReadingType.uA_Current:
                    AC = (frame[(int)Offset.Option2] & Option2AC) != 0;
                    DC = (frame[(int)Offset.Option2] & Option2DC) != 0;

                    Value = _digits * uA_CurrentRangeMultipliers[_range];

                    if (Negative)
                    {
                        Value = Value * -1M;
                    }
                    break;

                case ReadingType.mA_Current:
                    AC = (frame[(int)Offset.Option2] & Option2AC) != 0;
                    DC = (frame[(int)Offset.Option2] & Option2DC) != 0;

                    Value = _digits * mA_CurrentRangeMultipliers[_range];

                    if (Negative)
                    {
                        Value = Value * -1M;
                    }
                    break;

                case ReadingType.A_Current:
                    AC = (frame[(int)Offset.Option2] & Option2AC) != 0;
                    DC = (frame[(int)Offset.Option2] & Option2DC) != 0;

                    Value = _digits * A_CurrentMultiplier;

                    if (Negative)
                    {
                        Value = Value * -1M;
                    }
                    break;

                case ReadingType.Ohm:
                    Value = _digits * OhmRangeMultipliers[_range];
                    break;

                case ReadingType.FrequencyRpm:
                    Hertz = (frame[(int) Offset.Status] & StatusJudge) == 0;
                    RPM = (frame[(int)Offset.Status] & StatusJudge) != 0;

                    if (RPM)
                    {
                        Value = _digits * RpmRangeMultipliers[_range];
                    }
                    else
                    {
                        Value = _digits * FrequencyRangeMultipliers[_range];
                    }
                    break;

                case ReadingType.Capacitance:
                    Value = _digits * CapacitanceRangeMultipliers[_range];
                    break;

                case ReadingType.Temperature:
                    Centigrade = (frame[(int) Offset.Status] & StatusJudge) != 0;
                    Farenheit = (frame[(int)Offset.Status] & StatusJudge) == 0;

                    Value = _digits;

                    if (Value < TemperatureRangeDecimal)
                    {
                        Value = Value * TemperatureMultiplier;
                    }
                    break;

                case ReadingType.Continuity:
                    Value = _digits * ContinuityMultiplier;
                    break;

                case ReadingType.Diode:
                    Value = _digits * DiodeRangeMultiplier;
                    break;
            }
        }
    }
}
