using AdvancedMDDomain.DTOs.Requests;
using AdvancedMDDomain.DTOs.Responses;
using AdvancedMDService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PVAMCommon;
using System;
using System.Collections.Generic;

namespace AdvancedMDServiceTest
{
    [TestClass]
    public class UnitTestAdMDServices
    {
        private static Uri ApiUrl;
        private static string UserContext;

        [TestInitialize]
        public void Initialize()
        {

            var uri = new Uri("https://partnerlogin.advancedmd.com/practicemanager/xmlrpc/processrequest.aspx");
            var app = new LoginService();

            var response = app.ProcessLogin(uri, 1, "cmcus0228", "wer789gg", "133358", "CMCU", null).Result;
            ////var response = app.ProcessLogin(1, "cmcus0228", "rbw!@345", "132677", "MyApp", null);
            ////var response = app.ProcessLogin(1, "CMUC-FUL", "xcv900bb", "991347", "MyApp", null);
            ////var response = app.ProcessLogin(1, "SINAI0725", "hjp()567", "132637", "MyApp", null);
            ////var response =  app.ProcessLogin(1, "pv0809", "xcv789mm", "136989", "MyApp", null);
            ////var response = app.ProcessLogin(1, "PRA0925", "RLaJ4eH1", "137273", "MyApp", null);


            if (response.GetType() == typeof(PpmLoginResponse))
            {

                ApiUrl = new Uri(response.Results.Usercontext.Webserver + "/xmlrpc/processrequest.asp");
                UserContext = response.Results.Usercontext.Text;
            }

        }


        [TestMethod]
        public void CanLookupProviders()
        {
            var srv = new LookupService();
            var response = srv.LookupProviderByName(ApiUrl, UserContext, "b").Result;
            Assert.IsNotNull(response.Results.Profilelist);
        }



        [TestMethod]
        public void CanLookupProvidersAync()
        {
            var srv = new LookupService();
            var response = (PpmLookUpProviderResponse)srv.LookupProviderByNameAsync(ApiUrl, UserContext, "STYLES").Result;
            Assert.IsNotNull(response.Results.Profilelist);
        }

        [TestMethod]
        public void CanAddNewBatch()
        {
            var srv = new VisitService();
            var response = (PpmNewBatchResponse)srv.AddNewBatch(ApiUrl, UserContext, new PpmAddNewBatchRequest()).Result;
            Assert.IsNotNull(response.Results.Batchlist.Batch.Id);
        }

        [TestMethod]
        public void CanAddNewPatient()
        {
            var srv = new PatientService();


            var request = new PpmAddPatientRequest()
            {
                RequestPatientlist = new RequestPatientlist()
                {
                    Patient = new AdvancedMDDomain.DTOs.Requests.Patient()
                    {
                        Finclass = "fclass24",
                        Name = "Test12,DannyTest",
                        Address = new AdvancedMDDomain.DTOs.Requests.Address()
                        {
                            Address1 = "123",
                            Address2 = "Mantague",
                            CountryCode = "USA",
                            Zip = "29705",
                            City = "Hunt",
                            State = "WV"
                        },
                        Dob = "09/01/1977",
                        HipaaRelationship = "1",
                        Relationship = "SELF",
                        Respparty = "SELF",
                        Sex = "F",
                        Ssn = "123-456-789",
                        Contactinfo = new AdvancedMDDomain.DTOs.Requests.Contactinfo()
                        {
                            Homephone = "123-456-7890"
                        },
                        Profile = ""
                    }
                },
                Resppartylist = new AdvancedMDDomain.DTOs.Requests.Resppartylist()
                {
                    Respparty = new AdvancedMDDomain.DTOs.Requests.Respparty()
                    {
                        Name = "Test12,DannyTest",
                        Accttype = "accttype4"
                    }
                }
            };

            var response = (PpmAddPatientResponse)srv.AddPatient(ApiUrl, UserContext, request).Result;
            Assert.IsNotNull(response.Results.Patientlist);
        }

        [TestMethod]
        public void CanSavePatientNote()
        {
            var srv = new PatientService();

            var response = srv.SavePatientNote(ApiUrl, UserContext, "127085", "24", "TestNote");
        }


        [TestMethod]
        public void CanAddResparty()
        {
            var srv = new PatientService();

            var request = new PpmAddResPartyRequest
            {
                Respparty = new AddRespparty()
                {
                    Address = new AddAddress()
                    {
                        Address2 = "123",
                        Address1 = "noway",
                        City = "nowhere",
                        State = "WV",
                        Zip = "25704"

                    },
                    Name = "Danny, Test",

                    Dob = DateTime.MinValue.ToString(),
                    Ssn = "000-00-0000",

                }

            };

            var response = (PpmLookUpProviderResponse)srv.AddResparty(ApiUrl, UserContext, request).Result;

            Assert.IsNotNull(response.Results.Profilelist);
        }

        [TestMethod]
        public void CanLookupFinClass()
        {
            var srv = new LookupService();
            var response = (PpmLookUpFinClassResponse)srv.LookupFinClassById(ApiUrl, UserContext, "").Result;

            Assert.IsNotNull(response.Results.Finclasslist);
        }

        [TestMethod]
        public void LookupAcctType()
        {
            var srv = new LookupService();

            var response = (PpmLookUpAcctTypeResponse)srv.LookupAcctType(ApiUrl, UserContext, "").Result;

            Assert.IsNotNull(response.Results.Accttypelist);
        }


        [TestMethod]
        public void LookupCarrier()
        {
            var srv = new LookupService();



            var response = (PpmLookUpCarrierResponse)srv.LookUpCarrier(ApiUrl, UserContext, "N", "0").Result;

            Assert.IsNotNull(response.Results.Carrierlist);
        }


        [TestMethod]
        public void LookupCarrierByCode()
        {
            var srv = new LookupService();

            var response = (PpmLookUpCarrierResponse)srv.LookUpCarrierByCode(ApiUrl, UserContext, "UNI564").Result;
            Assert.IsNotNull(response.Results.Carrierlist);
        }


        [TestMethod]
        public void LookupDiagCode()
        {
            var srv = new LookupService();

            var response = (PpmLookupDiagCodeResponse)srv.LookUpDiagCode(ApiUrl, UserContext, "J10.1").Result;

            Assert.IsNotNull(response.Results.Diagcodelist.Diagcode.Id);
        }

        [TestMethod]
        public void CanLookUpProcCode()
        {
            var srv = new LookupService();

            var response = (PpmLookUpProcCodeResponse)srv.LookUpProcCode(ApiUrl, UserContext, "99051 ").Result;
            Assert.IsNotNull(response.Results.Proccodelist.Proccode.Id);
        }


        [TestMethod]
        public void CanSaveCharges()
        {
            var srv = new VisitService();

            var request = new PpmSaveChargesRequest
            {
                Patientid = "pat126636" /*"pat126815"*/,


                Visit = new AmdVisit
                {
                    Id = "vst101653" /*"vst101472"*/,
                    Chargelist = new Chargelist
                    {
                        Charge = new List<Charge>
                        {
                            new Charge
                            {
                                Codeset = "10",
                                Diagcodes = "dcode456552,dcode456555"/*"dcode90382,dcode456551"*/,
                                Proccode ="99214" /*"99204"*/,
                                Units = "1"
                            },
                            new Charge
                            {
                                Codeset = "10",
                                Diagcodes = "dcode456552,dcode456555"/*"dcode90382,dcode456551"*/,
                                Proccode = "pcode40928" /*"99214"*/,
                                Units = "2"
                            },

                        }
                    }
                }
            };
            var response = (PpmSaveChargesResponse)srv.SaveCharges(ApiUrl, UserContext, request).Result;
            Assert.IsNotNull(response.Results.Success);
        }


        [TestMethod]
        public void CanUpdateVisithWithCharges()
        {
            var srv = new VisitService();

            var request = new PpmUpdateVisitWithNewCharngeRequest()
            {
                Patientid = "pat126636" /*"pat126815"*/,
                Episodeid = "60212",
                Approval = "1",

                Visit = new UpdateVisit
                {
                    Id = "vst101653" /*"vst101472"*/,
                    Profile = "",
                    Refplan = "",
                    Force = "1",
                    Chargelist = new UpdateChargelist
                    {
                        Charges = new List<UpdateCharge> {
                            new UpdateCharge
                            {
                                Diagcodes = "dcode456552 dcode456555"/*"dcode90382,dcode456551"*/,
                                Proccode ="99214" /*"99204"*/,
                                Units = "1",
                                Begindate = "",
                                Enddate = "",
                                BatchNumber = "251"
                            }}
                    }
                }
            };
            var response = (PpmSaveChargesResponse)srv.UpdateVisitCharges(ApiUrl, UserContext, request).Result;
            Assert.IsNotNull(response.Results.Success);
        }

        [TestMethod]
        public void CanGetFees()
        {

            var srv = new VisitService();


            var request = new PpmGetFeesRequest
            {
                Dos = "01/30/2017",
                Proccode = new RequestProccode()
                {
                    Id = "pcode37588",
                    Units = "1.0",
                    //lookup value in api urgent care
                }
            };

            var response = (PpmGetFeesResponse)srv.GetFees(ApiUrl, UserContext, request).Result;
            Assert.IsNotNull(response.Results.Proccodelist.Proccode);
        }


        [TestMethod]
        public void CanUpdatePatient()
        {
            var srv = new PatientService();
            var lookupsrv = new LookupService();

            // var response = (PpmLookUpPatientResponse)srv.LookUpPatientByChartNo("1963");
            var demographic = (PpmGetDemographicResponse)lookupsrv.LookUpDemographic(ApiUrl, UserContext, "pat10306").Result;
            var oldPatient = demographic.Results.Patientlist.Patient;

            var request = new PpmUpdatePatientRequest
            {
                Patientlist = new UpdatePatientlist
                {
                    Patient = new UpdatePatient()
                    {

                        //Id = oldPatient.Id,
                        ////O_ssn  = oldPatient.Ssn,
                        ////Ssn = "123-456-7890",
                        // O_profile= demographic.Results.Profilelist.Profile.Id,
                        //Profile= "pro39",
                        Id = oldPatient.Id,

                        O_insorder = demographic.Results.Patientlist.Patient.Insorder,
                        Insorder = "3"
                        //Address = new UpdatePatientAddress()
                        //{
                        //    Address1 = "123",
                        //    Address2 = "montague",
                        //    Zip ="25701",
                        //    City = "HUNTINGTON",
                        //    State = "WV",
                        //    O_address1 = oldPatient.Address.Address1,
                        //    O_address2 = oldPatient.Address.Address2,
                        //    O_city = oldPatient.Address.City,
                        //    O_state = oldPatient.Address.State,
                        //    O_zip = oldPatient.Address.Zip
                        //},
                        //Contactinfo =  new UpdateContactinfo()
                        //{
                        //    O_homephone = oldPatient.Contactinfo.Homephone,
                        //    Homephone = "123456789"
                        //}
                    }
                }

            };

            var updateResponse = (PpmUpdatePatientResponse)srv.UpdatePatient(ApiUrl, UserContext, request).Result;
            Assert.IsTrue(updateResponse.Results.Patientlist.Patient.Ssn == "123-456-7890");
        }

        [TestMethod]
        public void CanUpdateResparty()
        {
            var srv = new PatientService();


            var lsrv = new LookupService();


            var response = (PpmLookUpResPartyResponse)lsrv.LookUpResPartyByName(ApiUrl, UserContext, "a").Result;

            Assert.IsNotNull(response.Results.Resppartylist.Respparty);

            var updateRequest = new PpmUpdateRespPartyRequest
            {
                Respparty = new UpdateRespparty
                {
                    Id = response.Results.Resppartylist.Respparty.Id,
                    Name = "Danny, NewTest",
                    Contactinfo = new UpdateContactInfo
                    {
                        Homephone = "8035532301"
                    },
                    Address = new UpdateAddress
                    {
                        Address1 = "1816",
                        Address2 = "E Mantague Ave",
                        CountryCode = "USA",
                        Zip = "29405",
                        City = "North Charleston",
                        State = "SC"
                    }
                }

            };
            var updateResponse = (PpmUpdateRespPartyResponse)srv.UpdateRespParty(ApiUrl, UserContext, updateRequest).Result;
            Assert.IsNotNull(updateResponse.Results);
        }

        [TestMethod]
        public void CannLookupEpisodes()
        {
            var srv = new VisitService();


            var request = new PpmGetEpisodesRequest
            {
                Patientid = "pat126636"
            };
            var response = (PpmGetEpisodesResponse)srv.GetEpisodes(ApiUrl, UserContext, request).Result;
            Assert.IsNotNull(response.Results);
        }



        [TestMethod]
        public void CanAddVisit()
        {
            var srv = new VisitService();


            var response = (PpmAddVisitResponse)srv.AddVisit(ApiUrl, UserContext, "", "pat88", "prof17", "col16", "type", "4/10/2017", "11:00", "30").Result;
            Assert.IsNotNull(response.Results.Visit);
        }


        [TestMethod]
        public void CanAddPayement()
        {
            var srv = new VisitService();

            var request = new PpmAddPaymentRequest
            {
                Patient = new AddPaymentPatient
                {
                    Amount = "50.00",
                    Patientid = "pat88",
                    Paysource = "1",
                    Paycode = "pp",
                    Depositdate = DateTime.Now.AddHours(-24).ToString(),
                    Unappliedpaymentlist = new Unappliedpaymentlist
                    {
                        Payment = new Payment
                        {
                            Source = "1",
                            Profile = "prof17",
                            Amount = "50.00"
                        }
                    }
                }
            };
            var response = srv.AddPayment(ApiUrl, UserContext, request).Result;
        }

        [TestMethod]
        public void CanLookUpPatientbyChart()
        {
            var srv = new PatientService();

            var response = (PpmLookUpPatientResponse)srv.LookUpPatientByChartNo(ApiUrl, UserContext, "1963").Result;
            Assert.IsNotNull(response.Results.Patientlist);

        }

        [TestMethod]
        public void CanLookUpPatientbyName()
        {
            var srv = new PatientService();

            var response = (PpmLookUpPatientResponse)srv.LookupPatientByName(ApiUrl, UserContext, "A,J").Result;
            Assert.IsNotNull(response.Results.Patientlist);

        }



        [TestMethod]
        public void CanLookupResPartybyName()
        {
            var srv = new LookupService();

            var response = (PpmLookUpResPartyResponse)srv.LookUpResPartyByName(ApiUrl, UserContext, "CAGLE,ANDREW").Result;
            Assert.IsNotNull(response.Results.Resppartylist.Respparty);


        }

        //[TestMethod]
        //public void CanSendMail()
        //{
        //    var testmail = new Email("smtp.gmail.com", 587, "sinai_pf_integration@summitemed.com ", "Sinaip&f9554");

        //    testmail.SendMail("sinai_pf_integration@summitemed.com ", new List<string> { "danny.x.li@gmail.com" }, new List<string> { "danny.x.li@gmail.com" }, "test", "Test, test, test", null);

        //}

        [TestMethod]
        public void TestFormatTime()
        {
            var now = DateTime.Now;
            var month = now.ToString("MM");
            var day = now.ToString("dd");
            var year = now.ToString("yy");
            var filename = $"newExportSINAI_{month}_{day}_{year}.xml";
            var filename1 = string.Format("newExportSINAI_{0:MM}_{0:dd}_{0:yy}.xml", DateTime.Now);
            Assert.AreEqual(filename, filename1);
        }

        [TestMethod]
        public void CanGetDemographic()
        {
            var srv = new LookupService();

            var response = (PpmGetDemographicResponse)srv.LookUpDemographic(ApiUrl, UserContext, "pat16527").Result;
            Assert.IsNotNull(response.Results);
        }




        [TestMethod]
        public void CanLookupDemo()
        {
            var srv = new LookupService();

            //var response = srv.LookUpDemographic("pat1399013");
            var response = srv.LookUpDemographic(ApiUrl, UserContext, "").Result;
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void CanDeserializePpmDemoResult()
        {
            var xmlstring = "&lt;?xml version=&apos;1.0&apos; encoding=&apos;ISO-8859-1&apos;?&gt;&lt;PPMDResults s=&quot;CB-API02&quot; lst=&quot;8/3/2017 9:20:33 AM&quot; n=&quot;1&quot;&gt;&lt;Results&gt;&lt;patientlist&gt;&lt;patient id=&quot;pat1398964&quot; importid=&quot;&quot; dob=&quot;06/11/1968&quot; deceased=&quot;&quot; inactive=&quot;&quot; inactivestatus=&quot;&quot; inactivetitle=&quot;&quot; inactivedescription=&quot;&quot; name=&quot;ELLIS,TERRY&quot; respparty=&quot;resp1817881&quot; profile=&quot;prof133&quot; insorder=&quot;&quot; sex=&quot;M&quot; chart=&quot;111&quot; maritalstatus=&quot;6&quot; employerid=&quot;&quot; employer=&quot;&quot; ssn=&quot;XXX-XX-4928&quot; finclasscode=&quot;03&quot; title=&quot;&quot; relationship=&quot;1&quot; hipaarelationship=&quot;18&quot; languageid=&quot;&quot; ethnicityid=&quot;&quot; language=&quot;&quot; ethnicity=&quot;&quot; races=&quot;&quot; recalcbuckets=&quot;0&quot; additionalmrn=&quot;&quot;&gt;&lt;address zip=&quot;76001&quot; city=&quot;ARLINGTON&quot; state=&quot;TX&quot; address1=&quot;&quot; address2=&quot;6916 GOLF GREEN DR&quot; areacode=&quot;817&quot; countrycode=&quot;USA&quot; /&gt;&lt;contactinfo name=&quot;&quot; homephone=&quot;(817) 946-3721&quot; officephone=&quot;&quot; officeext=&quot;&quot; otherphone=&quot;&quot; othertype=&quot;&quot; email=&quot;&quot; emailverificationstatus=&quot;0&quot; preferredcommunicationfid=&quot;&quot; confidentialcommunicationfid=&quot;&quot; communicationnote=&quot;&quot; /&gt;&lt;arbucketlist&gt;&lt;arbucket buckettype=&quot;p&quot; current=&quot;0.00&quot; past30=&quot;0.00&quot; past60=&quot;0.00&quot; past90=&quot;0.00&quot; past120=&quot;0.00&quot; unapplied=&quot;0.00&quot; total=&quot;0.00&quot; /&gt;&lt;arbucket buckettype=&quot;pi&quot; current=&quot;245.00&quot; past30=&quot;0.00&quot; past60=&quot;0.00&quot; past90=&quot;0.00&quot; past120=&quot;0.00&quot; unapplied=&quot;0.00&quot; total=&quot;245.00&quot; /&gt;&lt;/arbucketlist&gt;&lt;insplanlist /&gt;&lt;refplanlist&gt;&lt;refplan id=&quot;rplan2492462&quot; refprov=&quot;rprov246636&quot; type=&quot;I&quot; refplanstatus=&quot;1&quot; reason=&quot;SELF-REFERRAL&quot; preauthreq=&quot;0&quot; preauth=&quot;&quot; refnum=&quot;&quot; facility=&quot;fac1&quot; proccode=&quot;&quot; diagcodes=&quot;&quot; begindate=&quot;07/31/2017&quot; enddate=&quot;&quot; maxvisits=&quot;&quot; usedvisits=&quot;&quot; maxamount=&quot;&quot; usedamount=&quot;&quot; status=&quot;status1&quot; sequence=&quot;1&quot; notes=&quot;&quot; ismain=&quot;1&quot; /&gt;&lt;/refplanlist&gt;&lt;memolist /&gt;&lt;filelist /&gt;&lt;visitlist /&gt;&lt;/patient&gt;&lt;/patientlist&gt;&lt;resppartylist&gt;&lt;respparty id=&quot;resp1817881&quot; name=&quot;ELLIS,TERRY&quot; accttype=&quot;ac_type4&quot; acctnum=&quot;1817881&quot; dob=&quot;06/11/1968&quot; sex=&quot;M&quot; title=&quot;&quot; employer=&quot;&quot; employstatus=&quot;1&quot; ssn=&quot;XXX-XX-4928&quot; sendstmt=&quot;1&quot; stmtrestart=&quot;&quot; holdreasonfid=&quot;0&quot; holdreasondesc=&quot;&quot; holdreasoncode=&quot;&quot; billcycle=&quot;25&quot; fincharge=&quot;0&quot; stmtformat=&quot;&quot; balancefwd=&quot;1&quot; lastpmtdate=&quot;&quot; lastpmtamount=&quot;&quot; laststmtdate=&quot;&quot; laststmtamount=&quot;&quot; lastbillingcaption=&quot;&quot; lastbillingtip=&quot;&quot; paymentplanbalance=&quot;&quot;&gt;&lt;address zip=&quot;76001&quot; city=&quot;ARLINGTON&quot; state=&quot;TX&quot; address1=&quot;&quot; address2=&quot;6916 GOLF GREEN DR&quot; areacode=&quot;817&quot; countrycode=&quot;USA&quot; /&gt;&lt;contactinfo name=&quot;&quot; homephone=&quot;(817) 946-3721&quot; officephone=&quot;&quot; officeext=&quot;&quot; otherphone=&quot;&quot; othertype=&quot;&quot; email=&quot;&quot; preferredcommunicationfid=&quot;&quot; communicationnote=&quot;&quot; /&gt;&lt;creditcardonfile /&gt;&lt;arbucketlist&gt;&lt;arbucket buckettype=&quot;r&quot; current=&quot;0.00&quot; past30=&quot;0.00&quot; past60=&quot;0.00&quot; past90=&quot;0.00&quot; past120=&quot;0.00&quot; unapplied=&quot;0.00&quot; total=&quot;0.00&quot; /&gt;&lt;arbucket buckettype=&quot;ri&quot; current=&quot;245.00&quot; past30=&quot;0.00&quot; past60=&quot;0.00&quot; past90=&quot;0.00&quot; past120=&quot;0.00&quot; unapplied=&quot;0.00&quot; total=&quot;245.00&quot; /&gt;&lt;/arbucketlist&gt;&lt;familymemberlist&gt;&lt;familymember id=&quot;family1398964&quot; name=&quot;ELLIS,TERRY&quot; chart=&quot;111&quot; isrp=&quot;1&quot;&gt;&lt;contactinfo homephone=&quot;(817) 946-3721&quot; /&gt;&lt;address zip=&quot;76001&quot; city=&quot;ARLINGTON&quot; state=&quot;TX&quot; address1=&quot;&quot; address2=&quot;6916 GOLF GREEN DR&quot; countrycode=&quot;USA&quot; /&gt;&lt;/familymember&gt;&lt;/familymemberlist&gt;&lt;/respparty&gt;&lt;/resppartylist&gt;&lt;accttypelist&gt;&lt;accttype id=&quot;ac_type4&quot; name=&quot;TYPICAL ACCOUNT&quot; code=&quot;TA&quot; /&gt;&lt;/accttypelist&gt;&lt;profilelist&gt;&lt;profile id=&quot;prof133&quot; name=&quot;THURSTON,DANE&quot; fullname=&quot;THURSTON,DANE&quot; code=&quot;THURAL&quot; chargefeesched=&quot;feesch21092&quot; fullinactive=&quot;No&quot; status=&quot;ACTIVE&quot;&gt;&lt;finclasslist&gt;&lt;finclass id=&quot;fclass1&quot; name=&quot;&quot; code=&quot;&quot; billins=&quot;0&quot; billlab=&quot;1&quot; ca=&quot;0&quot; responsible=&quot;0&quot; acceptassign=&quot;0&quot; allowablewocode=&quot;&quot; autowo=&quot;0&quot; autowocode=&quot;&quot; riskwocode=&quot;&quot; riskpercent=&quot;0&quot; /&gt;&lt;finclass id=&quot;fclass2&quot; name=&quot;DOUBLE INSURANCE COVERAGE&quot; code=&quot;DC&quot; billins=&quot;1&quot; billlab=&quot;1&quot; ca=&quot;0&quot; responsible=&quot;1&quot; accttype=&quot;ac_type4&quot; acceptassign=&quot;1&quot; allowablewocode=&quot;WOINS&quot; autowo=&quot;0&quot; autowocode=&quot;None&quot; riskwocode=&quot;None&quot; riskpercent=&quot;0&quot; /&gt;&lt;finclass id=&quot;fclass4&quot; name=&quot;FULL FEE SCHEDULE&quot; code=&quot;FF&quot; billins=&quot;1&quot; billlab=&quot;1&quot; ca=&quot;0&quot; responsible=&quot;1&quot; accttype=&quot;ac_type4&quot; acceptassign=&quot;1&quot; allowablewocode=&quot;WOINS&quot; autowo=&quot;0&quot; autowocode=&quot;None&quot; riskwocode=&quot;None&quot; riskpercent=&quot;0&quot; /&gt;&lt;finclass id=&quot;fclass6&quot; name=&quot;TAKE WHAT INSURANCE PAYS&quot; code=&quot;IP&quot; billins=&quot;1&quot; billlab=&quot;1&quot; ca=&quot;0&quot; responsible=&quot;1&quot; accttype=&quot;ac_type5&quot; acceptassign=&quot;1&quot; allowablewocode=&quot;WOINS&quot; autowo=&quot;1&quot; autowocode=&quot;WOCOUR&quot; riskwocode=&quot;None&quot; riskpercent=&quot;0&quot; /&gt;&lt;finclass id=&quot;fclass8&quot; name=&quot;MEDICARE&quot; code=&quot;03&quot; billins=&quot;1&quot; billlab=&quot;1&quot; ca=&quot;0&quot; responsible=&quot;1&quot; accttype=&quot;ac_type4&quot; acceptassign=&quot;1&quot; allowfeesched=&quot;feesch21103&quot; allowablewocode=&quot;WOINS&quot; autowo=&quot;0&quot; autowocode=&quot;None&quot; riskwocode=&quot;None&quot; riskpercent=&quot;0&quot; /&gt;&lt;finclass id=&quot;fclass10&quot; name=&quot;MEDICAID&quot; code=&quot;MD&quot; billins=&quot;1&quot; billlab=&quot;1&quot; ca=&quot;0&quot; responsible=&quot;1&quot; accttype=&quot;ac_type5&quot; acceptassign=&quot;1&quot; allowablewocode=&quot;WOINS&quot; autowo=&quot;1&quot; autowocode=&quot;WOINS&quot; riskwocode=&quot;None&quot; riskpercent=&quot;0&quot; /&gt;&lt;finclass id=&quot;fclass12&quot; name=&quot;SELF PAY&quot; code=&quot;01&quot; billins=&quot;0&quot; billlab=&quot;1&quot; ca=&quot;0&quot; responsible=&quot;0&quot; accttype=&quot;ac_type4&quot; acceptassign=&quot;1&quot; allowfeesched=&quot;feesch21094&quot; allowablewocode=&quot;None&quot; autowo=&quot;0&quot; autowocode=&quot;None&quot; riskwocode=&quot;None&quot; riskpercent=&quot;0&quot; /&gt;&lt;finclass id=&quot;fclass14&quot; name=&quot;WORKERS COMP&quot; code=&quot;20&quot; billins=&quot;1&quot; billlab=&quot;1&quot; ca=&quot;0&quot; responsible=&quot;1&quot; accttype=&quot;ac_type4&quot; acceptassign=&quot;1&quot; allowablewocode=&quot;WOINS&quot; autowo=&quot;0&quot; autowocode=&quot;None&quot; riskwocode=&quot;None&quot; riskpercent=&quot;0&quot; /&gt;&lt;finclass id=&quot;fclass16&quot; name=&quot;CHANGE THIS!!!!&quot; code=&quot;XX&quot; billins=&quot;1&quot; billlab=&quot;1&quot; ca=&quot;0&quot; responsible=&quot;1&quot; accttype=&quot;ac_type4&quot; acceptassign=&quot;1&quot; allowfeesched=&quot;feesch21093&quot; allowablewocode=&quot;WOINS&quot; autowo=&quot;0&quot; autowocode=&quot;None&quot; riskwocode=&quot;None&quot; riskpercent=&quot;0&quot; /&gt;&lt;finclass id=&quot;fclass18&quot; name=&quot;NO PAY&quot; code=&quot;NP&quot; billins=&quot;0&quot; billlab=&quot;1&quot; ca=&quot;0&quot; responsible=&quot;0&quot; acceptassign=&quot;0&quot; allowfeesched=&quot;feesch21093&quot; allowablewocode=&quot;WOCOUR&quot; autowo=&quot;0&quot; autowocode=&quot;WOCOUR&quot; riskwocode=&quot;&quot; riskpercent=&quot;0&quot; /&gt;&lt;finclass id=&quot;fclass19&quot; name=&quot;TEST&quot; code=&quot;TE&quot; billins=&quot;1&quot; billlab=&quot;1&quot; ca=&quot;0&quot; responsible=&quot;1&quot; accttype=&quot;ac_type4&quot; acceptassign=&quot;1&quot; allowablewocode=&quot;None&quot; autowo=&quot;0&quot; autowocode=&quot;&quot; riskwocode=&quot;&quot; riskpercent=&quot;0&quot; /&gt;&lt;finclass id=&quot;fclass20&quot; name=&quot;COMMERCIAL&quot; code=&quot;05&quot; billins=&quot;1&quot; billlab=&quot;1&quot; ca=&quot;0&quot; responsible=&quot;1&quot; accttype=&quot;ac_type4&quot; acceptassign=&quot;1&quot; allowablewocode=&quot;WOINS&quot; autowo=&quot;0&quot; autowocode=&quot;None&quot; riskwocode=&quot;None&quot; riskpercent=&quot;0&quot; /&gt;&lt;finclass id=&quot;fclass1295&quot; name=&quot;EPS&quot; code=&quot;09&quot; billins=&quot;1&quot; billlab=&quot;1&quot; ca=&quot;0&quot; responsible=&quot;1&quot; accttype=&quot;ac_type4&quot; acceptassign=&quot;1&quot; allowablewocode=&quot;WOINS&quot; autowo=&quot;0&quot; autowocode=&quot;None&quot; riskwocode=&quot;&quot; riskpercent=&quot;0&quot; /&gt;&lt;finclass id=&quot;fclass1296&quot; name=&quot;PPO&quot; code=&quot;07&quot; billins=&quot;1&quot; billlab=&quot;1&quot; ca=&quot;0&quot; responsible=&quot;1&quot; accttype=&quot;ac_type4&quot; acceptassign=&quot;1&quot; allowablewocode=&quot;WOINS&quot; autowo=&quot;0&quot; autowocode=&quot;None&quot; riskwocode=&quot;&quot; riskpercent=&quot;0&quot; /&gt;&lt;finclass id=&quot;fclass1297&quot; name=&quot;BCBS&quot; code=&quot;02&quot; billins=&quot;1&quot; billlab=&quot;1&quot; ca=&quot;0&quot; responsible=&quot;1&quot; accttype=&quot;ac_type4&quot; acceptassign=&quot;1&quot; allowablewocode=&quot;WOINS&quot; autowo=&quot;0&quot; autowocode=&quot;None&quot; riskwocode=&quot;&quot; riskpercent=&quot;0&quot; /&gt;&lt;finclass id=&quot;fclass1298&quot; name=&quot;CIGNA&quot; code=&quot;18&quot; billins=&quot;1&quot; billlab=&quot;1&quot; ca=&quot;0&quot; responsible=&quot;1&quot; accttype=&quot;ac_type4&quot; acceptassign=&quot;1&quot; allowablewocode=&quot;WOINS&quot; autowo=&quot;0&quot; autowocode=&quot;None&quot; riskwocode=&quot;&quot; riskpercent=&quot;0&quot; /&gt;&lt;finclass id=&quot;fclass1299&quot; name=&quot;AETNA&quot; code=&quot;17&quot; billins=&quot;1&quot; billlab=&quot;1&quot; ca=&quot;0&quot; responsible=&quot;1&quot; accttype=&quot;ac_type4&quot; acceptassign=&quot;1&quot; allowablewocode=&quot;WOINS&quot; autowo=&quot;0&quot; autowocode=&quot;None&quot; riskwocode=&quot;&quot; riskpercent=&quot;0&quot; /&gt;&lt;finclass id=&quot;fclass1300&quot; name=&quot;UNITED HEALTHCARE&quot; code=&quot;15&quot; billins=&quot;1&quot; billlab=&quot;1&quot; ca=&quot;0&quot; responsible=&quot;1&quot; accttype=&quot;ac_type4&quot; acceptassign=&quot;1&quot; allowablewocode=&quot;WOINS&quot; autowo=&quot;0&quot; autowocode=&quot;None&quot; riskwocode=&quot;&quot; riskpercent=&quot;0&quot; /&gt;&lt;finclass id=&quot;fclass1301&quot; name=&quot;COVENTRY&quot; code=&quot;14&quot; billins=&quot;1&quot; billlab=&quot;1&quot; ca=&quot;0&quot; responsible=&quot;1&quot; accttype=&quot;ac_type4&quot; acceptassign=&quot;1&quot; allowablewocode=&quot;WOINS&quot; autowo=&quot;0&quot; autowocode=&quot;None&quot; riskwocode=&quot;&quot; riskpercent=&quot;0&quot; /&gt;&lt;finclass id=&quot;fclass1302&quot; name=&quot;TRICARE&quot; code=&quot;13&quot; billins=&quot;1&quot; billlab=&quot;1&quot; ca=&quot;0&quot; responsible=&quot;1&quot; accttype=&quot;ac_type4&quot; acceptassign=&quot;1&quot; allowablewocode=&quot;WOINS&quot; autowo=&quot;0&quot; autowocode=&quot;None&quot; riskwocode=&quot;&quot; riskpercent=&quot;0&quot; /&gt;&lt;finclass id=&quot;fclass1303&quot; name=&quot;HUMANA&quot; code=&quot;12&quot; billins=&quot;1&quot; billlab=&quot;1&quot; ca=&quot;0&quot; responsible=&quot;1&quot; accttype=&quot;ac_type4&quot; acceptassign=&quot;1&quot; allowablewocode=&quot;WOINS&quot; autowo=&quot;0&quot; autowocode=&quot;None&quot; riskwocode=&quot;&quot; riskpercent=&quot;0&quot; /&gt;&lt;/finclasslist&gt;&lt;/profile&gt;&lt;/profilelist&gt;&lt;carrierlist /&gt;&lt;proccodelist /&gt;&lt;modcodelist /&gt;&lt;diagcodelist /&gt;&lt;facilitylist /&gt;&lt;statuslist /&gt;&lt;refproviderlist&gt;&lt;refprovider id=&quot;rprov246636&quot; name=&quot;THURSTON,DANE&quot; code=&quot;THURST&quot; specialty=&quot;&quot; practicename=&quot;&quot;&gt;&lt;address address1=&quot;615&quot; address2=&quot;16775 ADDISON RD&quot; city=&quot;ADDISON&quot; state=&quot;TX&quot; zip=&quot;75001-5630&quot; areacode=&quot;972&quot; /&gt;&lt;contactinfo name=&quot;&quot; homephone=&quot;&quot; officephone=&quot;(972) 464-1611&quot; fax=&quot;(469) 519-2499&quot; officeext=&quot;&quot; otherphone=&quot;&quot; othertype=&quot;C&quot; email=&quot;&quot; /&gt;&lt;/refprovider&gt;&lt;/refproviderlist&gt;&lt;notelist /&gt;&lt;/Results&gt;&lt;Error /&gt;&lt;/PPMDResults&gt;";

            var response = xmlstring.Deserialize<PpmGetDemographicResponse>();

            Assert.IsNull(response);

        }

        //[TestMethod]
        //public void MapProfiles()
        //{
        //    var srv = new LookupService();


        //    var pysicians = datasrv.GetAllPhysicians().Where(x => x.OfficeKey == "132677" && !string.IsNullOrEmpty(x.AmdCode));
        //    var clinics = datasrv.GetClinicsByOfficekey(132677);
        //    foreach (var physician in pysicians)
        //    {
        //        var response = new PpmLookUpProviderResponse();
        //        if (!string.IsNullOrEmpty(physician.DisplayName) && string.IsNullOrEmpty(physician.AmdCode))
        //        {
        //            response = (PpmLookUpProviderResponse)srv.LookupProviderByName(physician.DisplayName.Split(',')[0].ToString());
        //        }
        //        else
        //        {
        //            response = (PpmLookUpProviderResponse)srv.LookupProviderByCode(physician.AmdCode);
        //        }

        //        if (response?.Results?.Profilelist != null)
        //        {
        //            var amdcode = clinics.FirstOrDefault(x => x.ClinicId == physician.Clinic).AmdCodePrefix;
        //            foreach (var profile in response.Results.Profilelist.Profile)
        //            {
        //                var codeNme = profile.Code.Substring(profile.Code.Length - 2);
        //                if (amdcode != null && codeNme == amdcode && physician.AmProviderId != profile.Id)
        //                {
        //                    datasrv.UpdatePhysician(physician, profile);
        //                    break;
        //                }
        //                else if (amdcode != null && codeNme == amdcode && physician.AmProviderId == profile.Id && string.IsNullOrEmpty(physician.AmdCode))
        //                {
        //                    datasrv.UpdatePhysician(physician, profile);
        //                    break;
        //                }
        //            }

        //            //Assert.IsNotNull(response.Results.Profilelist);
        //        }

        //    }
        //}




        //[TestMethod]
        //public void ReMapProfiles()
        //{
        //    var srv = new LookupService(new Uri(ApiUrl), UserContext);

        //    var pysicians = datasrv.GetAllPhysicians().Where(x => x.OfficeKey == "136989" && !string.IsNullOrEmpty(x.AmdCode));
        //    var clinics = datasrv.GetClinicsByOfficekey(136989);
        //    foreach (var physician in pysicians)
        //    {
        //        var response = new PpmLookUpProviderResponse();
        //        if (!string.IsNullOrEmpty(physician.DisplayName) && string.IsNullOrEmpty(physician.AmdCode))
        //        {
        //            response = (PpmLookUpProviderResponse)srv.LookupProviderByName(physician.DisplayName.Split(',')[0].ToString());
        //        }
        //        else
        //        {
        //            response = (PpmLookUpProviderResponse)srv.LookupProviderByCode(physician.AmdCode);
        //        }

        //        if (response?.Results?.Profilelist != null)
        //        {
        //            var amdcode = clinics.FirstOrDefault(x => x.ClinicId == physician.Clinic).AmdCodePrefix;
        //            foreach (var profile in response.Results.Profilelist.Profile)
        //            {
        //                datasrv.UpdatePhysician(physician, profile);

        //            }

        //            //Assert.IsNotNull(response.Results.Profilelist);
        //        }

        //    }
        //}


        //[TestMethod]
        //public void findBadMapping()
        //{
        //    var srv = new LookupService();


        //    var patientImportRecs = datasrv.GetPatientImportLogByOfficeKey("132677");
        //    var list = new List<string>();
        //    foreach (var patient in patientImportRecs)
        //    {
        //        var response = srv.LookUpDemographic(patient.AmdPatientId);
        //        if (response == null)
        //        {
        //            list.Add(patient.Id + ",");
        //        }
        //    }
        //}

        [TestMethod]
        public void TimeofTheDay()
        {
            var currenttime = DateTime.Now.AddHours(17);
            var hours = currenttime.TimeOfDay.Hours;
            Assert.IsTrue(hours >= 17);
        }
    }
}
