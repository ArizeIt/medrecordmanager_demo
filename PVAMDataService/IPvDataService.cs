using AdvancedMDDomain.DTOs.Responses;
using PracticeVelocityDomain.DTOs;
using System;
using System.Collections.Generic;
using UrgentCareData.Models;

namespace PVAMDataService
{
    public interface IPvDataService
    {
        bool SavePvXmlRecord(Log_Record pvRecord, string filePath, int sourceProcessId, bool additionalCharge);

        //void SaveProcessLog(SourceProcessLog log);

        //bool IfFileProcessed(string filename);

        //int InitiateNewProcessLog(string fileName);

        //void UpdateProcessLogById(int logId, int goodRecCount, int totalRecCount);

        //void UpdatePatientLog(int pvPatientId, string amdPatianId, string officekey, int amdImportId);

        //void UpdateGuarantorLog(int guarantorPayerId, string resPartyId, int amdImportId, string officeKey);

        //void UpdatePatientDoc(string amdDocId, PatientDocument patDoc);

        //void UpdatePhysician(Physican physicain, Profile profileId);

        //void UpdateVisitImportLog(int visitImportid, bool imported);

        //IList<PatientImportLog> GetPatientImportLogByOfficeKey(string officeKey);

        //IList<Visit> GetVisitGraph(int processId);

        //IList<Visit> GetUnSyncedVisitGraph(DateTime startDate, DateTime endDate);

        //IList<ChartDocument> GetChartDocuments(int chartid);

        //IList<AdvancedMdcolumnHeader> GetColumHeaders(int officeKey);

        //IList<PatientDocument> GetPatientDocs(int visitId);

        //PatientImportLog GetPatientImportByPvId(int patNum, string officeKey);

        //PatientImportLog GetPatientImportByAmdId(string amdPatId);

        //GuarantorImportLog GetGuarantorImportLog(int payerNumber, string officeKey);

        //SourceProcessLog GetSourceProcessLog(string fileName);

        //AdvanceMdimportLog GetImportLogByProcess(int processId);

        //InsuranceInformation FindInsuranceInfo(int? insuranceid);

        //IList<Relationship> GetAllRelationships();

        //IList<FinClass> GetALlFinclasses(string officeKey);

        //IList<Physican> GetAllPhysicians();

        //IList<ProgramConfig> GetProgramConfigs();

        //Physican GetAmdProfileId(int pvPhysicanId, int officeKey, string clinic);
        //Physican GetMatchingProfileIdByPv(int pvPhysicanId, int officeKey);

        //Physican GetDefaultProfile(int officeKey, string clinic);

        //IList<ClinicProfile> GetClinicsByOfficekey(int officeKey);

        //VisitImpotLog FindVisitImpotLogByVisitId(int visitId);

        //PayerImportLog FindInsurancePayerLog(int payerInfoId);

        //ChartImportLog FindChartDocLog(int chartDocId);


        //AdvanceMdimportLog SaveAmdImportLog(int sourceProcessId);

        //void SavePatientImportLog(string amdPaitentId, int patnum, string officeKey, int amdLogId);

        //void SaveResPartyImportLog(int amdImportId, string amdRespartyId, int payerNum, string officeKey);

        //void SaveVisitImportLog(int amdImportId, int visitId, string officeKey, string amdVisitId);

        //void SavePayerImportLog(int amdImportId, string amdInsId, int payerInfoId, string OfficeKey);

        //void SaveChartImportLog(int amdImportLogId, int chartDocId, string amdFileId);

    }
}
