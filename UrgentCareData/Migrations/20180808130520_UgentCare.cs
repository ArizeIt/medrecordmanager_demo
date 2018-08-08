using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UrgentCareData.Migrations
{
    public partial class UrgentCareMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdvancedMDColumnHeader",
                columns: table => new
                {
                    Clinic = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    OfficeKey = table.Column<int>(nullable: false),
                    ColumnHeader = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    FacilityId = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvancedMDColumnHeader", x => new { x.Clinic, x.OfficeKey });
                });

            migrationBuilder.CreateTable(
                name: "AdvancedMDProgram",
                columns: table => new
                {
                    AMDOfficeKey = table.Column<string>(maxLength: 50, nullable: false),
                    AMDUserName = table.Column<string>(maxLength: 50, nullable: false),
                    AMDPassword = table.Column<string>(maxLength: 50, nullable: false),
                    AMDUrl = table.Column<string>(maxLength: 250, nullable: false),
                    Environment = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvancedMDProgram", x => x.AMDOfficeKey);
                });

            migrationBuilder.CreateTable(
                name: "AdvanceMDImportLog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SourceProcessId = table.Column<int>(nullable: true),
                    ImportedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvanceMDImportLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Chart",
                columns: table => new
                {
                    DischargedDate = table.Column<DateTime>(type: "date", nullable: false),
                    DischargedBy = table.Column<string>(maxLength: 50, nullable: false),
                    SignOffSealedDate = table.Column<DateTime>(type: "date", nullable: false),
                    SignedOffSealedBy = table.Column<string>(maxLength: 50, nullable: false),
                    ChartId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chart", x => x.ChartId);
                });

            migrationBuilder.CreateTable(
                name: "ClinicProfile",
                columns: table => new
                {
                    ClinicFullName = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    ClinicId = table.Column<string>(maxLength: 50, nullable: false),
                    AMDCodeName = table.Column<string>(maxLength: 50, nullable: true),
                    AMDCodePrefix = table.Column<string>(maxLength: 50, nullable: true),
                    OfficeKey = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicProfile", x => x.ClinicId);
                });

            migrationBuilder.CreateTable(
                name: "GuarantorInformation",
                columns: table => new
                {
                    Payer_Num = table.Column<int>(nullable: false),
                    RelationshipCode = table.Column<string>(maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    MiddleName = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    LastName = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Address1 = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    Address2 = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    City = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    State = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Zip = table.Column<string>(maxLength: 50, nullable: true),
                    Phone = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    PvPatientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuarantorInformation", x => x.Payer_Num);
                });

            migrationBuilder.CreateTable(
                name: "InsuranceInformation",
                columns: table => new
                {
                    PrimaryName = table.Column<string>(unicode: false, maxLength: 150, nullable: false),
                    PrimaryPhone = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    PrimaryAddress1 = table.Column<string>(unicode: false, maxLength: 150, nullable: false),
                    PrimaryAddress2 = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    PrimaryCity = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    PrimaryState = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    PrimaryZip = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    InsuranceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AmdCode = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceInformation", x => x.InsuranceId);
                });

            migrationBuilder.CreateTable(
                name: "PatientDocument",
                columns: table => new
                {
                    FileName = table.Column<string>(unicode: false, maxLength: 500, nullable: false),
                    FileType = table.Column<int>(nullable: false),
                    NumofPages = table.Column<int>(nullable: false),
                    LastVerifedBy = table.Column<string>(unicode: false, maxLength: 150, nullable: false),
                    LastVerifiedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    FileImage = table.Column<byte[]>(type: "image", nullable: false),
                    VisitId = table.Column<int>(nullable: false),
                    PatDocId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AmdFileId = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientDocument", x => x.PatDocId);
                });

            migrationBuilder.CreateTable(
                name: "PatientInformation",
                columns: table => new
                {
                    Pat_Num = table.Column<int>(nullable: false),
                    SSN = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    FirstName = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    LastName = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    MiddleName = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Address1 = table.Column<string>(unicode: false, maxLength: 200, nullable: false),
                    Address2 = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    City = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    State = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Zip = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Birthday = table.Column<string>(unicode: false, maxLength: 25, nullable: false),
                    Sex = table.Column<string>(unicode: false, maxLength: 1, nullable: true),
                    PatPhone = table.Column<string>(unicode: false, maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientInformation", x => x.Pat_Num);
                });

            migrationBuilder.CreateTable(
                name: "Physican",
                columns: table => new
                {
                    PV_PhysicanId = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    LastName = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    MiddleName = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Clinic = table.Column<string>(maxLength: 50, nullable: false),
                    AM_ProviderId = table.Column<string>(maxLength: 50, nullable: true),
                    DisplayName = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    OfficeKey = table.Column<string>(maxLength: 50, nullable: true),
                    AmdCode = table.Column<string>(maxLength: 50, nullable: true),
                    IsDefault = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Physican", x => new { x.PV_PhysicanId, x.Clinic });
                });

            migrationBuilder.CreateTable(
                name: "ProgramConfig",
                columns: table => new
                {
                    AMDOfficeKey = table.Column<int>(nullable: false),
                    APIUserName = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    APIPassword = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    APIUri = table.Column<string>(unicode: false, maxLength: 500, nullable: false),
                    Environment = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RunAs = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Force = table.Column<bool>(nullable: false),
                    AmdSync = table.Column<bool>(nullable: false),
                    FilePath = table.Column<string>(unicode: false, maxLength: 1024, nullable: false),
                    FromEmailAddress = table.Column<string>(unicode: false, maxLength: 1024, nullable: true),
                    TechEmailAddress = table.Column<string>(unicode: false, maxLength: 1024, nullable: true),
                    ToEmailAddress = table.Column<string>(unicode: false, maxLength: 1024, nullable: true),
                    CCEmailAddress = table.Column<string>(unicode: false, maxLength: 1024, nullable: true),
                    SMTPServer = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    SMTPPort = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    SMTPUsername = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    PVFilePrefix = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    OffSetDays = table.Column<int>(nullable: false),
                    AmdAppName = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Enabled = table.Column<bool>(nullable: false),
                    ProcessSource = table.Column<bool>(nullable: false),
                    SyncOnly = table.Column<bool>(nullable: true),
                    PVFilename = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    AdditionalCharge = table.Column<bool>(nullable: false),
                    SMTPPassword = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramConfig", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Relationship",
                columns: table => new
                {
                    Description = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    HIPAARelationship = table.Column<string>(maxLength: 5, nullable: false),
                    AMRelationshipCode = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relationship", x => x.HIPAARelationship);
                });

            migrationBuilder.CreateTable(
                name: "SourceProcessLog",
                columns: table => new
                {
                    ProcessId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SourceFileName = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    FileSize = table.Column<long>(nullable: true),
                    ProcessedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    SuccessFlag = table.Column<bool>(nullable: true),
                    ProcessResult = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    MarkAsProcessed = table.Column<bool>(nullable: true),
                    MarkDelete = table.Column<bool>(nullable: true),
                    ImportedToAMD = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SourceProcessLog", x => x.ProcessId);
                });

            migrationBuilder.CreateTable(
                name: "ChartImportLog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PvChartDocId = table.Column<int>(nullable: false),
                    Status = table.Column<string>(maxLength: 50, nullable: false),
                    ImportedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    AMDImportId = table.Column<int>(nullable: false),
                    AmdFileId = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChartImportLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChartImportLog_AdvanceMDImportLog",
                        column: x => x.AMDImportId,
                        principalTable: "AdvanceMDImportLog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PatientImportLog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PVPatientId = table.Column<int>(nullable: false),
                    AMDPatientId = table.Column<string>(maxLength: 50, nullable: false),
                    AMDImportId = table.Column<int>(nullable: false),
                    Status = table.Column<string>(maxLength: 50, nullable: false),
                    ImportedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    OfficeKey = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientImportLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientImportLog_AdvanceMDImportLog",
                        column: x => x.AMDImportId,
                        principalTable: "AdvanceMDImportLog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PayerImportLog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PayerInfoId = table.Column<int>(nullable: false),
                    Status = table.Column<string>(maxLength: 50, nullable: false),
                    ImportedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    AMDImportId = table.Column<int>(nullable: false),
                    AMDPayerId = table.Column<string>(maxLength: 50, nullable: false),
                    OfficeKey = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayerImportLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayerImportLog_AdvanceMDImportLog",
                        column: x => x.AMDImportId,
                        principalTable: "AdvanceMDImportLog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VisitImpotLog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Status = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    OfficeKey = table.Column<string>(maxLength: 10, nullable: false),
                    ImportedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    AMDImportLogId = table.Column<int>(nullable: false),
                    VisitId = table.Column<int>(nullable: false),
                    AMDVisitId = table.Column<string>(maxLength: 50, nullable: false),
                    ChargeImported = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitImpotLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VisitImpotLog_AdvanceMDImportLog",
                        column: x => x.AMDImportLogId,
                        principalTable: "AdvanceMDImportLog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChartDocument",
                columns: table => new
                {
                    FileName = table.Column<string>(unicode: false, maxLength: 200, nullable: false),
                    FileType = table.Column<short>(nullable: false),
                    NumberOfPages = table.Column<short>(nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(type: "date", nullable: false),
                    LastUpdatedBy = table.Column<string>(maxLength: 50, nullable: false),
                    PatNum = table.Column<int>(nullable: false),
                    ChartDocId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ChartId = table.Column<int>(nullable: false),
                    DocumentImage = table.Column<byte[]>(type: "image", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChartDocument", x => x.ChartDocId);
                    table.ForeignKey(
                        name: "FK_ChartDocument_Chart",
                        column: x => x.ChartId,
                        principalTable: "Chart",
                        principalColumn: "ChartId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GuarantorImportLog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PayerNumber = table.Column<int>(nullable: false),
                    Status = table.Column<string>(maxLength: 50, nullable: false),
                    ImportedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    AMDImportId = table.Column<int>(nullable: false),
                    AmdResponsiblePartyId = table.Column<string>(maxLength: 50, nullable: false),
                    OfficeKey = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuarantorImportLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuarantorImportLog_AdvanceMDImportLog",
                        column: x => x.PayerNumber,
                        principalTable: "GuarantorInformation",
                        principalColumn: "Payer_Num",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Visit",
                columns: table => new
                {
                    ServiceDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    VisitId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClinicId = table.Column<string>(maxLength: 50, nullable: false),
                    PVLogNum = table.Column<int>(nullable: false),
                    LastUpdateUser = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    DiagCodes = table.Column<string>(maxLength: 500, nullable: true),
                    ICDCodes = table.Column<string>(maxLength: 500, nullable: true),
                    VisitType = table.Column<string>(maxLength: 50, nullable: true),
                    EMCode = table.Column<string>(maxLength: 10, nullable: true),
                    PhysicanId = table.Column<int>(nullable: false),
                    CoPayAmount = table.Column<decimal>(type: "money", nullable: true),
                    CopayType = table.Column<string>(maxLength: 50, nullable: true),
                    CopayNote = table.Column<string>(maxLength: 500, nullable: true),
                    PreviousPaymentAmount = table.Column<decimal>(type: "money", nullable: true),
                    PreviousPaymentType = table.Column<string>(maxLength: 50, nullable: true),
                    PreviousPaymentNote = table.Column<string>(maxLength: 500, nullable: true),
                    CurrentPaymentAmount = table.Column<decimal>(type: "money", nullable: true),
                    CurrentPaymentType = table.Column<string>(maxLength: 50, nullable: true),
                    CurrentPaymentNote = table.Column<string>(maxLength: 500, nullable: true),
                    TimeIn = table.Column<DateTime>(type: "datetime", nullable: false),
                    TimeOut = table.Column<DateTime>(type: "datetime", nullable: false),
                    Notes = table.Column<string>(maxLength: 500, nullable: true),
                    PvPaitentId = table.Column<int>(nullable: false),
                    ProcCodes = table.Column<string>(maxLength: 500, nullable: true),
                    ProcQty = table.Column<int>(nullable: true),
                    ChartId = table.Column<int>(nullable: true),
                    GuarantorPayerId = table.Column<int>(nullable: false),
                    SourceProcessId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visit", x => x.VisitId);
                    table.ForeignKey(
                        name: "FK_Visit_Chart",
                        column: x => x.ChartId,
                        principalTable: "Chart",
                        principalColumn: "ChartId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Visit_ClinicProfile_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "ClinicProfile",
                        principalColumn: "ClinicId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visit_GuarantorInformation",
                        column: x => x.GuarantorPayerId,
                        principalTable: "GuarantorInformation",
                        principalColumn: "Payer_Num",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Visit_Patient_Information",
                        column: x => x.PvPaitentId,
                        principalTable: "PatientInformation",
                        principalColumn: "Pat_Num",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Visit_Physican",
                        columns: x => new { x.PhysicanId, x.ClinicId },
                        principalTable: "Physican",
                        principalColumns: new[] { "PV_PhysicanId", "Clinic" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PayerInformation",
                columns: table => new
                {
                    PayerNum = table.Column<int>(nullable: true),
                    GroupId = table.Column<string>(maxLength: 50, nullable: true),
                    MemberId = table.Column<string>(maxLength: 50, nullable: true),
                    Class = table.Column<int>(nullable: true),
                    Type = table.Column<string>(maxLength: 10, nullable: true),
                    Priority = table.Column<int>(nullable: true),
                    PayerInfoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InsuranceId = table.Column<int>(nullable: true),
                    VisitId = table.Column<int>(nullable: false),
                    InsName = table.Column<string>(unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayerInformation", x => x.PayerInfoId);
                    table.ForeignKey(
                        name: "FK_PayerInformation_InsuranceInformation",
                        column: x => x.InsuranceId,
                        principalTable: "InsuranceInformation",
                        principalColumn: "InsuranceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayerInformation_Visit",
                        column: x => x.VisitId,
                        principalTable: "Visit",
                        principalColumn: "VisitId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VisitProcCode",
                columns: table => new
                {
                    ProcCode = table.Column<string>(maxLength: 200, nullable: true),
                    Quantity = table.Column<int>(nullable: true),
                    VisitId = table.Column<int>(nullable: false),
                    VisitProcCodeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitProcCode", x => x.VisitProcCodeId);
                    table.ForeignKey(
                        name: "FK_VisitProcCode_Visit",
                        column: x => x.VisitId,
                        principalTable: "Visit",
                        principalColumn: "VisitId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChartDocument_ChartId",
                table: "ChartDocument",
                column: "ChartId");

            migrationBuilder.CreateIndex(
                name: "IX_ChartImportLog_AMDImportId",
                table: "ChartImportLog",
                column: "AMDImportId");

            migrationBuilder.CreateIndex(
                name: "IX_GuarantorImportLog_PayerNumber",
                table: "GuarantorImportLog",
                column: "PayerNumber");

            migrationBuilder.CreateIndex(
                name: "IX_PatientImportLog_AMDImportId",
                table: "PatientImportLog",
                column: "AMDImportId");

            migrationBuilder.CreateIndex(
                name: "IX_PayerImportLog_AMDImportId",
                table: "PayerImportLog",
                column: "AMDImportId");

            migrationBuilder.CreateIndex(
                name: "IX_PayerInformation_InsuranceId",
                table: "PayerInformation",
                column: "InsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_PayerInformation_VisitId",
                table: "PayerInformation",
                column: "VisitId");

            migrationBuilder.CreateIndex(
                name: "IX_Visit_ChartId",
                table: "Visit",
                column: "ChartId");

            migrationBuilder.CreateIndex(
                name: "IX_Visit_ClinicId",
                table: "Visit",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_Visit_GuarantorPayerId",
                table: "Visit",
                column: "GuarantorPayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Visit_PvPaitentId",
                table: "Visit",
                column: "PvPaitentId");

            migrationBuilder.CreateIndex(
                name: "IX_Visit_PhysicanId_ClinicId",
                table: "Visit",
                columns: new[] { "PhysicanId", "ClinicId" });

            migrationBuilder.CreateIndex(
                name: "IX_VisitImpotLog_AMDImportLogId",
                table: "VisitImpotLog",
                column: "AMDImportLogId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitProcCode_VisitId",
                table: "VisitProcCode",
                column: "VisitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdvancedMDColumnHeader");

            migrationBuilder.DropTable(
                name: "AdvancedMDProgram");

            migrationBuilder.DropTable(
                name: "ChartDocument");

            migrationBuilder.DropTable(
                name: "ChartImportLog");

            migrationBuilder.DropTable(
                name: "GuarantorImportLog");

            migrationBuilder.DropTable(
                name: "PatientDocument");

            migrationBuilder.DropTable(
                name: "PatientImportLog");

            migrationBuilder.DropTable(
                name: "PayerImportLog");

            migrationBuilder.DropTable(
                name: "PayerInformation");

            migrationBuilder.DropTable(
                name: "ProgramConfig");

            migrationBuilder.DropTable(
                name: "Relationship");

            migrationBuilder.DropTable(
                name: "SourceProcessLog");

            migrationBuilder.DropTable(
                name: "VisitImpotLog");

            migrationBuilder.DropTable(
                name: "VisitProcCode");

            migrationBuilder.DropTable(
                name: "InsuranceInformation");

            migrationBuilder.DropTable(
                name: "AdvanceMDImportLog");

            migrationBuilder.DropTable(
                name: "Visit");

            migrationBuilder.DropTable(
                name: "Chart");

            migrationBuilder.DropTable(
                name: "ClinicProfile");

            migrationBuilder.DropTable(
                name: "GuarantorInformation");

            migrationBuilder.DropTable(
                name: "PatientInformation");

            migrationBuilder.DropTable(
                name: "Physican");
        }
    }
}
