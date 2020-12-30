namespace PracticeVelocityDomain.LookUps
{
    public class FinacialClass
    {
        private FinacialClass(string value) { Value = value; }

        public string Value { get; set; }

        public static FinacialClass Commercial { get { return new FinacialClass("5"); } }

        public static FinacialClass Medicare { get { return new FinacialClass("3"); } }

        public static FinacialClass Medicaid { get { return new FinacialClass("4"); } }

        public static FinacialClass SelfPay { get { return new FinacialClass("1"); } }

        public static FinacialClass WorkersComp { get { return new FinacialClass("20"); } }

    }
}
