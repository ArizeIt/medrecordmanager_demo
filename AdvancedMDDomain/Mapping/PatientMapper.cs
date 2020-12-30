using AdvancedMDDomain.DTOs.Requests;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UrgentCareData.Models;

namespace AdvancedMDDomain.Mapping
{
    public static class PatientMapper
    {
        public static PpmAddPatientRequest MapPatient(Visit rec, IList<Relationship> relationships, IList<FinClass> finClasses)
        {
            var request = new PpmAddPatientRequest
            {
                Resppartylist = new Resppartylist(),
                RequestPatientlist = new RequestPatientlist()
            };

            var respParty = new Respparty();

            var newPatient = new Patient()
            {
                Address = new Address()
            };

            //Source contains only address line 1 no address line 2, parse the address1 
            if (rec.PvPatient.Address1.Length > 0 && string.IsNullOrEmpty(rec.PvPatient.Address2))
            {
                newPatient.Address.Address2 = rec.PvPatient.Address1;
            }
            else
            {
                newPatient.Address.Address1 = rec.PvPatient.Address2;
                newPatient.Address.Address2 = rec.PvPatient.Address1;
            }

            newPatient.Address.City = rec.PvPatient.City;
            newPatient.Address.State = rec.PvPatient.State;
            newPatient.Address.Zip = rec.PvPatient.Zip;
            newPatient.Address.CountryCode = "USA";

            newPatient.Dob = rec.PvPatient.Birthday;
            newPatient.Sex = rec.PvPatient.Sex;
            newPatient.Ssn = rec.PvPatient.Ssn;

            newPatient.Contactinfo = new Contactinfo()
            {
                Homephone = rec.PvPatient.HomePhone,
                Email = rec.PvPatient.Email,
                Otherphone = rec.PvPatient.CellPhone,
                Othertype = "Cell"
            };

            if (string.IsNullOrEmpty(rec.PvPatient.MiddleName))
            {
                newPatient.Name = rec.PvPatient.LastName + "," + rec.PvPatient.FirstName;
            }
            else
            {
                newPatient.Name = rec.PvPatient.LastName + "," + rec.PvPatient.FirstName + " " + (rec.PvPatient.MiddleName);
            }

            newPatient.Maritalstatus = "6"; //unknow form the source


            var payerInformation = rec.PayerInformation.FirstOrDefault();
            if (payerInformation != null)
            {

                var pvType = payerInformation.Type;
                var matchFin = finClasses.FirstOrDefault(x => x.AmFinClassCode == pvType.Trim());
                if (pvType.Trim() == "PPO" || pvType.Trim() == "BCBS")
                {
                    matchFin = finClasses.FirstOrDefault(x => x.AmFinClassCode == "CC");
                }

                if (matchFin == null)
                {
                    matchFin = finClasses.FirstOrDefault(x => x.PvClassCode == payerInformation.Class);
                }
                newPatient.Finclass = matchFin != null ? matchFin.AmFinancialClass : "fclass16";

            }
            else
            {

                newPatient.Finclass = "fclass12";
            }
            newPatient.Chart = "AUTO";

            if (rec.GuarantorPayer != null)
            {
                if (rec.GuarantorPayer.RelationshipCode == "18" || rec.GuarantorPayer.RelationshipCode == "01")
                {
                    newPatient.Respparty = "SELF";
                    respParty.Name = newPatient.Name;
                }

                var relationshipCode = relationships.FirstOrDefault(x => x.Hipaarelationship == rec.GuarantorPayer.RelationshipCode);
                if (relationshipCode != null)
                {
                    if (rec.GuarantorPayer.RelationshipCode == "01" || rec.GuarantorPayer.RelationshipCode == "18")
                    {
                        newPatient.Relationship = "1";
                        newPatient.HipaaRelationship = "18";
                    }
                    else
                    {
                        newPatient.Relationship = relationshipCode.AmrelationshipCode.ToString();
                        newPatient.HipaaRelationship = rec.GuarantorPayer.RelationshipCode;
                    }
                }
                else
                {
                    newPatient.Relationship = "4";
                    newPatient.HipaaRelationship = "G8";
                }


                var gurantorName = string.Empty;
                if (string.IsNullOrEmpty(rec.GuarantorPayer.MiddleName))
                {
                    gurantorName = rec.GuarantorPayer.LastName + "," + rec.GuarantorPayer.FirstName;
                }
                else
                {
                    gurantorName = rec.GuarantorPayer.LastName + "," + rec.GuarantorPayer.FirstName + " " + (rec.GuarantorPayer.MiddleName);
                }


                if ((rec.PayerInformation != null & rec.PayerInformation.Any()) && !(rec.GuarantorPayer.RelationshipCode == "01" || rec.GuarantorPayer.RelationshipCode == "18"))
                {
                    foreach (var recpayer in rec.PayerInformation)
                    {
                        recpayer.InsName = Regex.Replace(recpayer.InsName, @"\s+", "");
                        recpayer.InsName = Regex.Replace(gurantorName, @"\s+", "");
                        if (recpayer.InsName.Trim() != gurantorName.Trim())
                        {
                            respParty.Name = recpayer.InsName;
                            break;
                        }
                        else
                        {
                            respParty.Name = gurantorName;
                        }
                    }
                }

                respParty.City = rec.GuarantorPayer.City;
                respParty.State = rec.GuarantorPayer.State;
                respParty.Zip = rec.GuarantorPayer.Zip;

                //address is not longer than 30 chars
                if (rec.GuarantorPayer.Address1.Length > 0 &&
                    string.IsNullOrEmpty(rec.GuarantorPayer.Address2))
                {
                    respParty.Address2 = rec.GuarantorPayer.Address1;
                }
                else
                {
                    newPatient.Address.Address1 = rec.GuarantorPayer.Address2;
                    newPatient.Address.Address2 = rec.GuarantorPayer.Address1;
                }
            }
            else
            {
                newPatient.Respparty = "";
            }
            respParty.Accttype = "accttype4";

            request.Resppartylist.Respparty = respParty;
            request.RequestPatientlist.Patient = newPatient;
            return request;
        }


        public static PpmUpdateRespPartyRequest MapUpdateResParty(Respparty respParty, string respPartyId)
        {
            var updateRespPartyRequest = new PpmUpdateRespPartyRequest()
            {
                Respparty = new UpdateRespparty
                {
                    Id = respPartyId,
                    Sex = "",
                    Title = "",
                    Address = new UpdateAddress
                    {
                        Address1 = respParty.Address1,
                        Address2 = respParty.Address2,
                        City = respParty.City,
                        State = respParty.State,
                        Zip = respParty.Zip
                    },
                    Contactinfo = new UpdateContactInfo
                    {
                        Homephone = respParty.Homephone
                    }
                }
            };
            return updateRespPartyRequest;
        }
    }
}
