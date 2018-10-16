using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeVelocityDomain.LookUps
{
   public class PVRelationShip
    {
        private PVRelationShip(string value) { Value = value; }
        public string Value { get; set; }

        public static PVRelationShip Self { get { return new PVRelationShip("01"); } }
        public static PVRelationShip Spouse { get { return new PVRelationShip("02"); } }

        public static PVRelationShip ChildAdopted { get { return new PVRelationShip("09"); } }
        public static PVRelationShip ChildFostered { get { return new PVRelationShip("10"); } }
        public static PVRelationShip StepParent { get { return new PVRelationShip("17"); } }
        public static PVRelationShip ChildNature { get { return new PVRelationShip("19"); } }
        public static PVRelationShip Employee { get { return new PVRelationShip("20"); } }
        public static PVRelationShip Unknown { get { return new PVRelationShip("21"); } }
        public static PVRelationShip Other { get { return new PVRelationShip("25"); } }
        public static PVRelationShip Mother { get { return new PVRelationShip("32"); } }
        public static PVRelationShip Father { get { return new PVRelationShip("33"); } }
        public static PVRelationShip LifePartner { get { return new PVRelationShip("53"); } }
        public static PVRelationShip G8 { get { return new PVRelationShip("G8"); } }

    }
}
