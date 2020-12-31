using MedRecordManager.Extension;
using MedRecordManager.Models;
using MedRecordManager.Models.DailyRecord;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PVAMCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrgentCareData;
using UrgentCareData.Models;

namespace MedRecordManager.Controllers
{

    [Authorize]
    public class BulkEditController : Controller
    {
        private readonly UrgentCareContext _urgentCareContext;
        public BulkEditController(UrgentCareContext urgentCareContext)
        {
            _urgentCareContext = urgentCareContext;
        }


        [HttpGet]
        public IActionResult BulkUpdate(int? page, int? limit)
        {
            var records = _urgentCareContext.Visit.Where(x => x.Flagged).ToList();
            var physicianIds = records.DistinctBy(x => x.PhysicanId).Select(x => x.PhysicanId);

            var vm = new FilterRecord
            {
                Clinics = records.DistinctBy(x => x.ClinicId).Select(y => new SelectListItem
                {
                    Selected = false,
                    Text = y.ClinicId,
                    Value = y.ClinicId,
                }).OrderBy(r => r.Text),
                Physicians = _urgentCareContext.Physican.Where(x => physicianIds.Contains(x.PvPhysicanId)).DistinctBy(x => x.PvPhysicanId).Select(y => new SelectListItem
                {
                    Selected = false,
                    Text = y.DisplayName,
                    Value = y.PvPhysicanId.ToString(),
                }).OrderBy(r => r.Text),

                FinClasses = _urgentCareContext.PayerInformation.Where(x => records.Select(y => y.VisitId).Contains(x.VisitId)).DistinctBy(x => x.Class).Select(y => new SelectListItem
                {
                    Selected = false,
                    Text = y.Class.ToString(),
                    Value = y.Class.ToString()
                }).OrderBy(r => int.Parse(r.Value)),


                Clinic = string.Empty,
                FinClass = string.Empty,
                Physician = string.Empty,
                FlaggedRule = string.Empty,
            };

            vm.AppliedRuleIds = _urgentCareContext.VisitRuleSet.Where(x => records.Select(y => y.VisitId).Contains(x.VisitId)).DistinctBy(x => x.RuleSetId).Select(x => x.RuleSetId).ToList();

            vm.FlaggedRules = _urgentCareContext.CodeReviewRule.Where(x => vm.AppliedRuleIds.Contains(x.Id)).Select(y => new SelectListItem
            {
                Selected = false,
                Text = y.RuleName,
                Value = y.Id.ToString()
            }).ToList();

            vm.StartDate = records.OrderBy(x => x.ServiceDate).FirstOrDefault() != null ? records.OrderBy(x => x.ServiceDate).FirstOrDefault().ServiceDate : DateTime.Today;
            vm.EndDate = records.OrderByDescending(x => x.ServiceDate).FirstOrDefault() != null ? records.OrderByDescending(x => x.ServiceDate).FirstOrDefault().ServiceDate : DateTime.Today;

                   
            return View("BulkUpdateView", vm);
        }


        [HttpGet]
        public IActionResult GetBulkVisit(int? page, int? limit, string clinic, string physician, string rule, string finclass, DateTime startDate, DateTime endDate)
        {
            try
            {
                var records = new List<VisitRecordVm>();
                var vRecords = _urgentCareContext.Visit.Include(x=>x.PvPatient).Include(x=>x.PayerInformation).Where(x => x.Flagged)
               .Select(y => new VisitRecordVm
               {
                   VisitId = y.VisitId,
                   PatientId = y.PvPatientId,
                   ClinicName = y.ClinicId,
                   DiagCode = y.DiagCodes.Replace("|", "<br/>"),
                   PvRecordId = y.PvlogNum,
                   VisitTime = y.ServiceDate.Date.ToString(),
                   PatientName = y.PvPatient.FirstName + y.PvPatient.LastName,
                   OfficeKey = y.OfficeKey.GetValueOrDefault(),
                   PVFinClass = y.PayerInformation.FirstOrDefault() != null? y.PayerInformation.FirstOrDefault().Class.ToString() : string.Empty,
                   IcdCodes = y.Icdcodes.Replace("|", "<br/>"),
                   Payment = y.CoPayAmount.GetValueOrDefault(),
                   ProcCodes = y.ProcCodes.Replace(",|", "<br/>").Replace("|", "<br/>"),
                   IsFlagged = y.Flagged,
                   PhysicanId = y.PhysicanId,
                   PhysicianName = _urgentCareContext.Physican.FirstOrDefault(x => x.PvPhysicanId == y.PhysicanId).DisplayName,
                   ServiceDate = y.ServiceDate.Date,
                   Selected = y.Selected

               }).OrderBy(x=>x.VisitTime);



                if (!string.IsNullOrEmpty(clinic))
                {
                    var clinicids = clinic.Split(',').ToList();
                    vRecords = vRecords.Where(x => clinicids.Contains(x.ClinicName)).OrderBy(x => x.VisitTime);
                }

                if (!string.IsNullOrEmpty(physician))
                {
                    var physicians = physician.Split(',').Select(int.Parse).ToList();
                    vRecords = vRecords.Where(x => physicians.Contains(x.PhysicanId)).OrderBy(x => x.VisitTime);

                }

                if (!string.IsNullOrEmpty(finclass))
                {
                    var finclasses = finclass.Split(',').ToList();
                    vRecords = vRecords.Where(x => finclasses.Contains(x.PVFinClass)).OrderBy(x => x.VisitTime);
                }

                if (!string.IsNullOrEmpty(rule))
                {
                    var rules = rule.Split(',').Select(int.Parse).ToList();
                    var affecedVisits = _urgentCareContext.VisitRuleSet.Where(x => rules.Contains(x.RuleSetId)).Select(y => y.VisitId).ToList();

                    vRecords = vRecords.Where(x => affecedVisits.Contains(x.VisitId)).OrderBy(x => x.VisitTime);
                }

                if (startDate != DateTime.MinValue)
                {
                    vRecords = vRecords.Where(x => x.ServiceDate >= startDate).OrderBy(x => x.VisitTime);
                }

                if (endDate != DateTime.MinValue)
                {
                    vRecords = vRecords.Where(x => x.ServiceDate <= endDate).OrderBy(x => x.VisitTime);
                }

                var total = vRecords.Count();

                if (page.HasValue && limit.HasValue)
                {
                    var start = (page.Value - 1) * limit.Value;
                    records = vRecords.Skip(start).Take(limit.Value).OrderBy(x=>x.VisitTime).ToList();
                    
                    foreach (var record in records)
                    {
                        var visitRules = _urgentCareContext.VisitRuleSet.Include(x => x.CodeReviewRuleSet).Where(x => x.VisitId == record.VisitId).Select(x => x.CodeReviewRuleSet.RuleName).ToList();
                        record.AppliedRules = string.Join("<br/>", visitRules);

                        if(_urgentCareContext.BulkVisit.Any(x=>x.VisitId == record.VisitId))
                        {
                            var match = _urgentCareContext.BulkVisit.FirstOrDefault(x => x.VisitId == record.VisitId);                        
                            record.PhysicanId = match.PhysicanId;
                            record.ClinicName = match.ClinicId;
                            record.OfficeKey = match.OfficeKey.GetValueOrDefault();
                            record.ProcCodes = match.ProcCodes.Replace(",|", "<br/>").Replace("|", "<br/>");                         
                            record.IcdCodes = match.Icdcodes.Replace("|", "<br/>");                         
                            record.IsFlagged = match.Flagged;                        
                            record.PatientId = match.PvPatientId;                        
                        }
                    }
                }

                var allSelcted = total == _urgentCareContext.BulkVisit.Where(x => x.Selected).Count();
                return Json(new { records, total, allSelcted });
            }
            catch (Exception ex)
            {
                return Json(new { succcess = false, message = ex.Message.ToString() });
            }
        }


        [HttpPost]

        public async Task<IActionResult> BulkSave()
        {
            var bulkVisits = _urgentCareContext.BulkVisit.Where(x => x.Selected).ToList();

            foreach (var bulkVisit in bulkVisits)
            {
                var visit = await _urgentCareContext.Visit.Include(x => x.VisitICDCode).Include(x => x.VisitProcCode).FirstOrDefaultAsync(x => x.VisitId == bulkVisit.VisitId);
                _urgentCareContext.Visit.Attach(visit);
                visit.ClinicId = bulkVisit.ClinicId;
                visit.PhysicanId = bulkVisit.PhysicanId;
                visit.Icdcodes = bulkVisit.Icdcodes;
                visit.ProcCodes = bulkVisit.ProcCodes;
                visit.Flagged = bulkVisit.Flagged;

                foreach (var icdCode in visit.VisitICDCode)
                {
                    _urgentCareContext.VisitIcdcode.Remove(icdCode);
                }

                foreach (var procCode in visit.VisitProcCode)
                {
                    _urgentCareContext.VisitProcCode.Remove(procCode);
                }

                foreach (var bulkIcd in bulkVisit.VisitICDCodes)
                {
                    _urgentCareContext.VisitIcdcode.Add(new VisitICDCode
                    {
                        ICDCode = bulkIcd.ICDCode,
                        VisitId = bulkIcd.VisitId,

                    });
                }

                foreach (var bulkProc in bulkVisit.VisitProcCodes)
                {
                    _urgentCareContext.VisitProcCode.Add(new VisitProcCode
                    {
                        Modifier = bulkProc.Modifier,
                        VisitId = bulkProc.VisitId,
                        ProcCode = bulkProc.ProcCode,
                        Quantity = bulkProc.Quantity
                    });
                }
            }

            try
            {
                await _urgentCareContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message.ToString() });
            }

            return Json(new { success = true, url = Url.Action("BulkUpdate") });
        }

        [HttpPost]
        public IActionResult ExecuteAct(List<BulkAction> actions)
        {
            var records = _urgentCareContext.Visit.Where(x => x.Selected);
            var bulkVisitIds = _urgentCareContext.Visit.Where(x => x.Selected).Select(x => x.VisitId).ToList();
            if (actions.Any() && bulkVisitIds.Any())
            {

                foreach (var record in records)
                {
                    var patient = _urgentCareContext.PatientInformation.FirstOrDefault(x => x.PatNum == record.PvPatientId);
                    var payer = _urgentCareContext.PayerInformation.FirstOrDefault(x => x.VisitId == record.VisitId);
                    var exisitingBulk = _urgentCareContext.BulkVisit.FirstOrDefault(x => x.VisitId == record.VisitId);

                    record.VisitProcCode = _urgentCareContext.VisitProcCode.Where(x => x.VisitId == record.VisitId).ToList();
                    record.VisitICDCode = _urgentCareContext.VisitIcdcode.Where(x => x.VisitId == record.VisitId).ToList();

                    if (exisitingBulk == null)
                    {
                        _urgentCareContext.BulkVisit.Add(new BulkVisit
                        {
                            VisitId = record.VisitId,
                            TimeIn = record.TimeIn,
                            TimeOut = record.TimeOut,
                            LastUpdateTime = record.LastUpdateTime,
                            ServiceDate = record.ServiceDate,
                            PhysicanId = record.PhysicanId,
                            ClinicId = record.ClinicId,
                            OfficeKey = record.OfficeKey,
                            ProcCodes = record.ProcCodes,
                            Emcode = record.Emcode,
                            Icdcodes = record.Icdcodes,
                            LastUpdateUser = record.LastUpdateUser,
                            Flagged = record.Flagged,
                            GuarantorPayerId = record.GuarantorPayerId,
                            PvlogNum = record.PvlogNum,
                            PvPatientId = record.PvPatientId,
                            FinClass = payer != null ? payer.Class.ToString() : "0",
                            PatientName = patient.FirstName + " " + patient.LastName,
                            CoPayAmount = record.CoPayAmount,
                            DiagCodes = record.DiagCodes,
                            Emmodifier = record.EmModifier,
                            Emquantity = record.EmQuantity,
                            ProcQty = record.ProcQty,
                            VisitType = record.VisitType,
                            SourceProcessId = record.SourceProcessId,
                            Selected = record.Selected

                        });
                    }

                    if (!record.VisitICDCode.Any())
                    {
                        if (!string.IsNullOrEmpty(record.Icdcodes))
                        {
                            var icdList = record.Icdcodes.ParseToList('|');
                            if (icdList.Any())
                            {
                                foreach (var icd in icdList)
                                {
                                    if (!string.IsNullOrEmpty(icd))
                                    {
                                       
                                        record.VisitICDCode.Add(new VisitICDCode
                                        {
                                            VisitId = record.VisitId,
                                            ICDCode = icd
                                        });
                                    }
                                }
                            }
                        }                      
                    }

                    if (!_urgentCareContext.BulkVisitICDCode.Any(x => x.VisitId == record.VisitId))
                    {
                        foreach (var icd in record.VisitICDCode)
                        {

                            _urgentCareContext.BulkVisitICDCode.Add(new BulkVisitICDCode
                            {
                                ICDCode = icd.ICDCode,
                                VisitId = record.VisitId
                            });
                        }
                    }


                    if (!record.VisitProcCode.Any())
                    {
                        var procList = record.ProcCodes.ParseToList('|');
                        if (procList.Any())
                        {
                            foreach (var proc in procList)
                            {
                                if (!string.IsNullOrEmpty(proc))
                                {
                                    var fullcode = string.Empty;
                                    var modifider = string.Empty;
                                    var procCode = string.Empty;

                                    var procInfo = proc.Split(new[] { ',' }, 2);
                                    if (procInfo[1].EndsWith(","))
                                    {
                                        fullcode = procInfo[1].TrimEnd(new char[] { ',' });
                                    }
                                    else
                                    {
                                        fullcode = procInfo[1];
                                    }

                                    if (fullcode.Contains(","))
                                    {
                                        var result = fullcode.Split(new[] { ',' }, 2);
                                        procCode = result[0];
                                        modifider = result[1];
                                    }
                                    else
                                    {
                                        procCode = fullcode;
                                    }
                                   
                                    record.VisitProcCode.Add(new VisitProcCode
                                    {
                                        VisitId = record.VisitId,
                                        Quantity = int.Parse(procInfo[0]),
                                        ProcCode = procCode,
                                        Modifier = modifider
                                    });
                                }
                            }
                        }
                    }


                    if (!_urgentCareContext.BulkVisitProcCode.Any(x => x.VisitId == record.VisitId))
                    {
                        foreach (var proc in record.VisitProcCode)
                        {

                            if (!_urgentCareContext.BulkVisitProcCode.Any(x => x.ProcCode == proc.ProcCode))
                            {
                                _urgentCareContext.BulkVisitProcCode.Add(new BulkVisitProcCode
                                {
                                    Modifier = proc.Modifier,
                                    ProcCode = proc.ProcCode,
                                    Quantity = proc.Quantity,
                                    VisitId = record.VisitId
                                });
                            }
                        }
                    }

                }
                try
                {
                    _urgentCareContext.SaveChanges();
                }

                catch(Exception ex)
                {
                    ex.Message.ToString();
                }

                /// apply action
                foreach (var action in actions)
                {        
                    var modifier = action.ActionSteps !=null? action.ActionSteps.FirstOrDefault(x => x.Key == "Mofifier").Value: string.Empty;
                    var cptCode = action.ActionSteps != null ? action.ActionSteps.FirstOrDefault(x => x.Key == "CPT").Value : string.Empty;
                    var IcdCode = action.ActionSteps != null ? action.ActionSteps.FirstOrDefault(x => x.Key == "ICD").Value : string.Empty;
                    var physicianId = action.ActionSteps != null ? action.ActionSteps.FirstOrDefault(x => x.Key == "UpdatePhysician").Value : string.Empty;
                    var clinicId = action.ActionSteps != null ? action.ActionSteps.FirstOrDefault(x => x.Key == "UpdateClinic").Value : string.Empty;
                    
                    switch (action.ActionName)
                    {
                        case "addCpt":

                            if (!string.IsNullOrEmpty(cptCode))
                            {
                                AddCpt(bulkVisitIds, cptCode);
                            }
                            break;

                        case "delCpt":
                            if (!string.IsNullOrEmpty(cptCode))
                                RemoveCpt(bulkVisitIds, cptCode);
                            break;

                        case "addIcd":
                            if (!string.IsNullOrEmpty(IcdCode))
                            {
                                AddIcd(bulkVisitIds, IcdCode);
                            }
                            break;

                        case "delIcd":
                            if (!string.IsNullOrEmpty(IcdCode))
                            {
                                RemoveIcd(bulkVisitIds, IcdCode);
                            }
                            break;

                        case "addMod":

                            AddModifier(bulkVisitIds, cptCode, modifier);
                            break;

                        case "delMod":
                            if (!string.IsNullOrEmpty(modifier))
                            {
                                RemoveModifier(bulkVisitIds, cptCode, modifier);
                            }
                            break;

                        case "updPhys":
                            if (!string.IsNullOrEmpty(physicianId))
                            {
                                UpdatePhysican(bulkVisitIds, int.Parse(physicianId));
                            }
                            break;

                        case "updClinic":
                            if (!string.IsNullOrEmpty(clinicId))
                            {
                                UpdateClinic(bulkVisitIds, clinicId);
                            }
                            break;
                        case "unFlag":
                            Unflag(bulkVisitIds);
                            break;
                    }

                }
            }

            return Json(new { success = true, message = "All actions have been applied to the selected records." });
        }

        [HttpPost]
        public async Task<IActionResult> SelectRecord(int visitId, bool isSelected)
        {
            var match = _urgentCareContext.Visit.FirstOrDefault(x => x.VisitId == visitId);
            _urgentCareContext.Visit.Attach(match);
            match.Selected = isSelected;
            await _urgentCareContext.SaveChangesAsync();
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult CancelChange()
        {
            var bulkVisits = _urgentCareContext.BulkVisit.Where(x => x.Selected).ToList();
            var bulkProc = _urgentCareContext.BulkVisitProcCode.Where(x => bulkVisits.Select(y => y.VisitId).Contains(x.VisitId)).ToList();
            var bulkIcd = _urgentCareContext.BulkVisitICDCode.Where(x => bulkVisits.Select(y => y.VisitId).Contains(x.VisitId)).ToList();

            _urgentCareContext.BulkVisitProcCode.RemoveRange(bulkProc);
            _urgentCareContext.BulkVisitICDCode.RemoveRange(bulkIcd);
            _urgentCareContext.BulkVisit.RemoveRange(bulkVisits);
            try
            {
                _urgentCareContext.SaveChanges();

            }
            catch (Exception ex)
            {
                return Json(new { successs = false, message = ex.ToString() });
            }

            return Json(new { success = true, url = Url.Action("BulkUpdate") });
        }

        [HttpPost]
        public async Task<IActionResult> ResetFilter()
        {
            var bulkVisits = _urgentCareContext.BulkVisit.Where(x => x.Selected);
            var visits = _urgentCareContext.Visit.Where(x => x.Selected);

            await visits.ForEachAsync(x => x.Selected = false);
            await bulkVisits.ForEachAsync(x => x.Selected = false);
            try
            {
                await _urgentCareContext.SaveChangesAsync();
            }
            catch
            {
                return Json(new { success = false });
            }

            return Json(new { success = true });
        }

        private void AddCpt(IList<int> bulkVisitIds, string cptCode)
        {
            var bulkVisits = _urgentCareContext.BulkVisit.Include(x => x.VisitProcCodes).Where(x => bulkVisitIds.Contains(x.VisitId));
            foreach (var visit in bulkVisits)
            {

                if (visit.VisitProcCodes.Any(x => x.ProcCode == cptCode))
                {
                    var upd = visit.VisitProcCodes.FirstOrDefault(x => x.ProcCode == cptCode);
                    _urgentCareContext.Attach(upd);
                    upd.Quantity += 1;
                }
                else
                {
                    visit.VisitProcCodes.Add(new BulkVisitProcCode()
                    {
                        ProcCode = cptCode,
                        Quantity = 1,
                        VisitId = visit.VisitId
                    });
                }

                visit.ProcCodes = ConverProcToString(visit.VisitProcCodes.ToList());
            }
            _urgentCareContext.SaveChanges();
        }

        private void Unflag(List<int> bulkVisitIds)
        {
            var bulkVisits = _urgentCareContext.BulkVisit.Where(x => bulkVisitIds.Contains(x.VisitId));
            _urgentCareContext.BulkVisit.AttachRange(bulkVisits);
            foreach (var bulkVisit in bulkVisits)
            {
                bulkVisit.Flagged = false;
            }
            _urgentCareContext.SaveChanges();
        }
        private void RemoveCpt(List<int> bulkVisitIds, string cptCode)
        {
            var bulkVisits = _urgentCareContext.BulkVisit.Include(x => x.VisitProcCodes).Where(x => bulkVisitIds.Contains(x.VisitId));
            foreach (var visit in bulkVisits)
            {

                var matches = visit.VisitProcCodes.Where(x => x.ProcCode == cptCode).ToList();
                foreach (var match in matches)
                {
                    visit.VisitProcCodes.Remove(match);
                    _urgentCareContext.BulkVisitProcCode.Remove(match);
                }


                visit.ProcCodes = ConverProcToString(visit.VisitProcCodes.ToList());
            }
            _urgentCareContext.SaveChanges();
        }
        private void AddIcd(List<int> bulkVisitIds, string icdCode)
        {
            var bulkVisits = _urgentCareContext.BulkVisit.Include(x => x.VisitICDCodes).Where(x => bulkVisitIds.Contains(x.VisitId));
            foreach (var visit in bulkVisits)
            {

                if (!visit.VisitProcCodes.Any(x => x.ProcCode == icdCode))
                {
                    visit.VisitICDCodes.Add(new BulkVisitICDCode()
                    {
                        ICDCode = icdCode,
                        VisitId = visit.VisitId
                    });

                }


                visit.Icdcodes = string.Join("|", visit.VisitICDCodes.Select(x => x.ICDCode));
            }
            _urgentCareContext.SaveChanges();
        }


        private void RemoveIcd(List<int> bulkVisitIds, string icd)
        {
            var bulkVisits = _urgentCareContext.BulkVisit.Include(x => x.VisitICDCodes).Where(x => bulkVisitIds.Contains(x.VisitId));
            try
            {

                foreach (var visit in bulkVisits)
                {


                    var matched = visit.VisitICDCodes.Where(x => x.ICDCode == icd).ToList();

                    if (matched.Any())
                    {
                        foreach (var match in matched)
                        {

                            _urgentCareContext.BulkVisitICDCode.Remove(match);
                            visit.VisitICDCodes.Remove(match);
                        }
                    }

                    visit.Icdcodes = string.Join("|", visit.VisitICDCodes.Select(x => x.ICDCode));
                }
                _urgentCareContext.SaveChanges();
            }


            catch (Exception ex)
            {

            }


        }
        private void AddModifier(List<int> bulkVisitIds, string cptCode, string modifier)
        {
            if(!string.IsNullOrEmpty(cptCode) && !string.IsNullOrEmpty(modifier))
            {
                var bulkVisits = _urgentCareContext.BulkVisit.Include(x => x.VisitProcCodes).Where(x => bulkVisitIds.Contains(x.VisitId));
                foreach (var bvisit in bulkVisits)
                {

                    if (bvisit.VisitProcCodes.Any(x => x.ProcCode == cptCode))
                    {
                        var upd = bvisit.VisitProcCodes.FirstOrDefault(x => x.ProcCode == cptCode);
                        _urgentCareContext.Attach(upd);
                        if (string.IsNullOrEmpty(upd.Modifier))
                        {
                            upd.Modifier = modifier;
                        }
                        else
                        {
                            upd.Modifier += "," + modifier;
                        }
                    }
                    bvisit.ProcCodes = ConverProcToString(bvisit.VisitProcCodes.ToList());
                }
                _urgentCareContext.SaveChanges();
            }


           
        }

        private void RemoveModifier(List<int> bulkVisitIds, string cptCode, string modifier)
        {

            var bulkVisits = _urgentCareContext.BulkVisit.Include(x => x.VisitProcCodes).Where(x => bulkVisitIds.Contains(x.VisitId));


            foreach (var visit in bulkVisits)
            {

                if (visit.VisitProcCodes.Any(x => x.ProcCode == cptCode))
                {
                    var match = visit.VisitProcCodes.FirstOrDefault(x => x.ProcCode == cptCode);

                    var modfilers = match.Modifier.Split(",").ToList().ConvertAll(x => x.ToUpperInvariant());
                    modfilers.Remove(modifier.ToUpperInvariant());
                    match.Modifier = string.Join(",", modfilers);
                }

                visit.ProcCodes = ConverProcToString(visit.VisitProcCodes.ToList());
            }

            _urgentCareContext.SaveChanges();


        }
        private void UpdateClinic(IList<int> bulkVisitIds, string clinicId)
        {
            var bulkVisits = _urgentCareContext.BulkVisit.Where(x => bulkVisitIds.Contains(x.VisitId)).ToList();
            _urgentCareContext.BulkVisit.AttachRange(bulkVisits);
            bulkVisits.ForEach(x => x.ClinicId = clinicId);

            _urgentCareContext.SaveChanges();
        }


        private void UpdatePhysican(List<int> bulkVisitIds, int physicanId)
        {
            var bulkVisits = _urgentCareContext.BulkVisit.Where(x => bulkVisitIds.Contains(x.VisitId)).ToList();
            _urgentCareContext.BulkVisit.AttachRange(bulkVisits);
            bulkVisits.ForEach(x => x.PhysicanId = physicanId);
            _urgentCareContext.SaveChanges();
        }

        private FullProcCode ParseProcCode(string proc)
        {
            var code = new FullProcCode();

            if (!string.IsNullOrEmpty(proc))
            {


                var procInfo = proc.Split(new[] { ',' }, 2);
                if (procInfo[1].EndsWith(","))
                {
                    code.FullCode = procInfo[1].TrimEnd(new char[] { ',' });
                }
                else
                {
                    code.FullCode = procInfo[1];
                }

                if (code.FullCode.Contains(","))
                {
                    var result = code.FullCode.Split(new[] { ',' }, 2);
                    code.ProcCode = result[0];
                    code.ModifierCode = result[1];
                }
                else
                {
                    code.ProcCode = code.FullCode;
                }

            }

            return code;
        }

        private string ConverProcToString(IList<BulkVisitProcCode> procCodes)
        {
            var codes = new List<string>();
            foreach (var cpt in procCodes)
            {
                codes.Add(cpt.Quantity + "," + cpt.ProcCode + "," + cpt.Modifier);
            }

            return string.Join("|", codes);
        }


        public class FullProcCode
        {
            public string FullCode { get; set; }
            public string ProcCode { get; set; }
            public string ModifierCode { get; set; }
        }
    }
}