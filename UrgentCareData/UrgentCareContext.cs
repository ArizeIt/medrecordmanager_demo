using Microsoft.EntityFrameworkCore;
using UrgentCareData.Models;

namespace UrgentCareData
{
    public partial class UrgentCareContext : DbContext
    {
       

        public UrgentCareContext(string connectionString) : base(GetOptions(connectionString))
        {

        }

        public UrgentCareContext(DbContextOptions<UrgentCareContext> options)
            : base(options)
        {
            
        }

        public virtual DbSet<AdvanceMdimportLog> AdvanceMdimportLog { get; set; }
        public virtual DbSet<AdvancedMdcolumnHeader> AdvancedMdcolumnHeader { get; set; }
        public virtual DbSet<AmdLoginSession> AmdLoginSession { get; set; }
        public virtual DbSet<Audit> Audit { get; set; }
        public virtual DbSet<BatchJob> BatchJob { get; set; }
        public virtual DbSet<BulkVisit> BulkVisit { get; set; }
        public virtual DbSet<BulkVisitProcCode> BulkVisitProcCode { get; set; }
        public virtual DbSet<BulkVisitICDCode> BulkVisitICDCode { get; set; }
        public virtual DbSet<Chart> Chart { get; set; }
        public virtual DbSet<ChartDocument> ChartDocument { get; set; }
        public virtual DbSet<ChartDocumentHistory> ChartDocumentHistory { get; set; }
        public virtual DbSet<ChartImportLog> ChartImportLog { get; set; }
        public virtual DbSet<ClinicProfile> ClinicProfile { get; set; }
        public virtual DbSet<CodeReviewQueue> CodeReviewQueue { get; set; }
        public virtual DbSet<CodeReviewRule> CodeReviewRule { get; set; }
        public virtual DbSet<CompanyClinic> CompanyClinic { get; set; }
        public virtual DbSet<CptCodeLookup> CptCodeLookup { get; set; }
        public virtual DbSet<FinClass> FinClass { get; set; }
        public virtual DbSet<GuarantorImportLog> GuarantorImportLog { get; set; }
        public virtual DbSet<GuarantorInformation> GuarantorInformation { get; set; }
        public virtual DbSet<InsuranceInformation> InsuranceInformation { get; set; }
        public virtual DbSet<Modifier> Modifier { get; set; }
        public virtual DbSet<PatientDocument> PatientDocument { get; set; }
        public virtual DbSet<PatientImportLog> PatientImportLog { get; set; }
        public virtual DbSet<PatientInformation> PatientInformation { get; set; }
        public virtual DbSet<PayerImportLog> PayerImportLog { get; set; }
        public virtual DbSet<PayerInformation> PayerInformation { get; set; }
        public virtual DbSet<Physician> Physician { get; set; }
        public virtual DbSet<ProgramConfig> ProgramConfig { get; set; }
        public virtual DbSet<Relationship> Relationship { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RoleClaim> RoleClaims { get; set; }
        public virtual DbSet<SourceProcessLog> SourceProcessLog { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserClaim> UserClaims { get; set; }
        public virtual DbSet<UserLogin> UserLogins { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<UserToken> UserTokens { get; set; }
        public virtual DbSet<Visit> Visit { get; set; }
        public virtual DbSet<VisitCodeHistory> VisitCodeHistory { get; set; }
        public virtual DbSet<VisitHistory> VisitHistory { get; set; }
        public virtual DbSet<VisitICDCode> VisitIcdcode { get; set; }
        public virtual DbSet<VisitImportLog> VisitImportLog { get; set; }
        public virtual DbSet<VisitProcCode> VisitProcCode { get; set; }
        public virtual DbSet<VisitRuleSet> VisitRuleSet { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdvanceMdimportLog>(entity =>
            {
                entity.ToTable("AdvanceMDImportLog");

                entity.Property(e => e.ImportedDate).HasColumnType("datetime");

                entity.Property(e => e.Status).HasMaxLength(50);
            });

            modelBuilder.Entity<AdvancedMdcolumnHeader>(entity =>
            {
                entity.HasKey(e => new { e.Clinic, e.OfficeKey });

                entity.ToTable("AdvancedMDColumnHeader");

                entity.Property(e => e.Clinic)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ColumnHeader)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FacilityId)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AmdLoginSession>(entity =>
            {
                entity.Property(e => e.ApiUrl)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Born).HasColumnType("datetime");

                entity.Property(e => e.Context)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<Audit>(entity =>
            {
                entity.Property(e => e.KeyValues)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ModifiedBy)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ModifiedTime).HasColumnType("datetime");

                entity.Property(e => e.NewValues).IsRequired();

                entity.Property(e => e.OldValues).IsRequired();

                entity.Property(e => e.TableName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<BatchJob>(entity =>
            {
                entity.HasKey(e => e.BatchJobId);

                entity.Property(e => e.RecordsProcessed).ValueGeneratedNever();

                entity.Property(e => e.BatchJobId).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.FinishedDateTime).HasColumnType("datetime");

                entity.Property(e => e.JobName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.JobStatus)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Paramters).IsRequired();
            });

            modelBuilder.Entity<BulkVisit>(entity =>
            {
                entity.HasKey(e => e.VisitId);

                entity.Property(e => e.VisitId).ValueGeneratedNever();

                entity.Property(e => e.ClinicId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CoPayAmount).HasColumnType("money");

                entity.Property(e => e.CopayNote).HasMaxLength(500);

                entity.Property(e => e.CopayType).HasMaxLength(50);

                entity.Property(e => e.CurrentPaymentAmount).HasColumnType("money");

                entity.Property(e => e.CurrentPaymentNote).HasMaxLength(500);

                entity.Property(e => e.CurrentPaymentType).HasMaxLength(50);

                entity.Property(e => e.DiagCodes).HasMaxLength(500);

                entity.Property(e => e.Emcode)
                    .HasColumnName("EMCode")
                    .HasMaxLength(50);

                entity.Property(e => e.Emmodifier)
                    .HasColumnName("EMModifier")
                    .HasMaxLength(50);

                entity.Property(e => e.Emquantity).HasColumnName("EMQuantity");

                entity.Property(e => e.FinClass).HasMaxLength(50);

                entity.Property(e => e.Icdcodes)
                    .HasColumnName("ICDCodes")
                    .HasMaxLength(500);

                entity.Property(e => e.LastUpdateTime).HasColumnType("datetime");

                entity.Property(e => e.LastUpdateUser)
                    //.IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Notes).HasMaxLength(500);

                entity.Property(e => e.PatientName).HasMaxLength(200);

                entity.Property(e => e.PreviousPaymentAmount).HasColumnType("money");

                entity.Property(e => e.PreviousPaymentNote).HasMaxLength(500);

                entity.Property(e => e.PreviousPaymentType).HasMaxLength(50);

                entity.Property(e => e.ProcCodes).HasMaxLength(500);

                entity.Property(e => e.PvlogNum).HasColumnName("PVLogNum");

                entity.Property(e => e.ServiceDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeIn).HasColumnType("datetime");

                entity.Property(e => e.TimeOut).HasColumnType("datetime");

                entity.Property(e => e.VisitType).HasMaxLength(50);
            });

            modelBuilder.Entity<BulkVisitICDCode>(entity =>
            {
                entity.ToTable("BulkVisitICDCode");
                entity.HasKey("BulkVisitICDCodeId");
                entity.Property(e => e.BulkVisitICDCodeId).HasColumnName("BulkVisitICDCodeId");

                entity.Property(e => e.ICDCode)
                    .IsRequired()
                    .HasColumnName("ICDCode")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.BulkVisit)
                    .WithMany(p => p.VisitICDCodes)
                    .HasForeignKey(d => d.VisitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BulkVisitICDCode_BulkVisit")
                    .OnDelete(DeleteBehavior.Cascade);
            });


            modelBuilder.Entity<BulkVisitProcCode>(entity =>
            {
                entity.Property(e => e.Modifier).HasMaxLength(50);
                entity.HasKey("BulkVisitProcCodeId");
                entity.Property(e => e.ProcCode).HasMaxLength(200);

                entity.HasOne(d => d.BulkVisit)
                    .WithMany(p => p.VisitProcCodes)
                    .HasForeignKey(d => d.VisitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BulkVisitProcCode_BulkVisit")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Chart>(entity =>
            {
                entity.Property(e => e.DischargedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DischargedDate).HasColumnType("date");

                entity.Property(e => e.SignOffSealedDate).HasColumnType("date");

                entity.Property(e => e.SignedOffSealedBy)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ChartDocument>(entity =>
            {
                entity.HasKey(e => e.ChartDocId);

                entity.Property(e => e.DocumentImage)
                    .IsRequired()
                    .HasColumnType("image");

                entity.Property(e => e.DocumentText).HasColumnType("text");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastUpdatedOn).HasColumnType("date");

                entity.HasOne(d => d.Chart)
                    .WithMany(p => p.ChartDocument)
                    .HasForeignKey(d => d.ChartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChartDocument_Chart");
            });

            modelBuilder.Entity<ChartDocumentHistory>(entity =>
            {
                entity.Property(e => e.ChartImage)
                    .IsRequired()
                    .HasColumnType("image");

                entity.Property(e => e.Filename)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.UploadedBy)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UploadedTime)
                    .HasColumnName("uploadedTime")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<ChartImportLog>(entity =>
            {
                entity.Property(e => e.AmdFileId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AmdimportId).HasColumnName("AMDImportId");

                entity.Property(e => e.ImportedDate).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Amdimport)
                    .WithMany(p => p.ChartImportLog)
                    .HasForeignKey(d => d.AmdimportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChartImportLog_AdvanceMDImportLog");
            });

            modelBuilder.Entity<ClinicProfile>(entity =>
            {
                entity.HasKey(e => e.ClinicId);

                entity.Property(e => e.ClinicId).HasMaxLength(50);

                entity.Property(e => e.AmdcodeName)
                    .HasColumnName("AMDCodeName")
                    .HasMaxLength(50);

                entity.Property(e => e.AmdcodePrefix)
                    .HasColumnName("AMDCodePrefix")
                    .HasMaxLength(50);

                entity.Property(e => e.ClinicFullName)
                    .HasMaxLength(150)
                    .IsUnicode(false);
                
                entity.Property(e => e.Enabled)
                    .HasColumnName("Enabled");

            });

            modelBuilder.Entity<CodeReviewQueue>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.ParentQueue)
                    .WithMany(p => p.ChildrenQueue)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_CodeReviewQueue_CodeReviewQueue");
            });

            modelBuilder.Entity<CodeReviewRule>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy)
                    .HasColumnName("lastModifiedBy")
                    .HasMaxLength(200);

                entity.Property(e => e.LastModifiedTime)
                    .HasColumnName("lastModifiedTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.RuleName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<CptCodeLookup>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.Property(e => e.Code).HasMaxLength(255);

                entity.Property(e => e.LongDescription).HasMaxLength(500);

                entity.Property(e => e.ShortDescription).HasMaxLength(255);
            });

            modelBuilder.Entity<FinClass>(entity =>
            {
                entity.Property(e => e.AmFinClassCode)
                    .HasColumnName("Am_FinClassCode")
                    .HasMaxLength(50);

                entity.Property(e => e.AmFinancialClass)
                    .HasColumnName("AM_FinancialClass")
                    .HasMaxLength(50);

                entity.Property(e => e.OfficeKey)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PvClassCode).HasColumnName("Pv_ClassCode");

                entity.Property(e => e.PvName).HasMaxLength(50);
            });

            modelBuilder.Entity<GuarantorImportLog>(entity =>
            {
                entity.Property(e => e.AmdResponsiblePartyId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AmdimportId).HasColumnName("AMDImportId");

                entity.Property(e => e.ImportedDate).HasColumnType("datetime");

                entity.Property(e => e.OfficeKey).HasMaxLength(50);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.PayerNumberNavigation)
                    .WithMany(p => p.GuarantorImportLog)
                    .HasForeignKey(d => d.PayerNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GuarantorImportLog_AdvanceMDImportLog");
            });

            modelBuilder.Entity<GuarantorInformation>(entity =>
            {
                entity.HasKey(e => e.PayerNum)
                    .HasName("PK_PayerInfo");

                entity.Property(e => e.PayerNum)
                    .HasColumnName("Payer_Num")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address1)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Address2)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RelationshipCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.State)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Zip).HasMaxLength(50);
            });

            modelBuilder.Entity<InsuranceInformation>(entity =>
            {
                entity.HasKey(e => e.InsuranceId);

                entity.Property(e => e.AmdCode).HasMaxLength(50);

                entity.Property(e => e.PrimaryAddress1)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.PrimaryAddress2)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.PrimaryCity)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PrimaryName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.PrimaryPhone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PrimaryState)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PrimaryZip)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Modifier>(entity =>
            {
                entity.HasKey(e => e.ModifierCode)
                    .HasName("PK_ModifierCode");

                entity.Property(e => e.ModifierCode).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(200);
            });

            modelBuilder.Entity<PatientDocument>(entity =>
            {
                entity.HasKey(e => e.PatDocId);

                entity.Property(e => e.AmdFileId).HasMaxLength(50);

                entity.Property(e => e.FileImage)
                    .IsRequired()
                    .HasColumnType("image");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.LastVerifedBy)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.LastVerifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Visit)
                    .WithMany(p => p.PatientDocument)
                    .HasForeignKey(d => d.VisitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientDocument_Visit");
            });

            modelBuilder.Entity<PatientImportLog>(entity =>
            {
                entity.Property(e => e.AmdimportId).HasColumnName("AMDImportId");

                entity.Property(e => e.AmdpatientId)
                    .IsRequired()
                    .HasColumnName("AMDPatientId")
                    .HasMaxLength(50);

                entity.Property(e => e.ImportedDate).HasColumnType("datetime");

                entity.Property(e => e.PvpatientId).HasColumnName("PVPatientId");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Amdimport)
                    .WithMany(p => p.PatientImportLog)
                    .HasForeignKey(d => d.AmdimportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientImportLog_AdvanceMDImportLog");
            });

            modelBuilder.Entity<PatientInformation>(entity =>
            {
                entity.HasKey(e => e.PatNum)
                    .HasName("PK_Patient_Information");

                entity.Property(e => e.PatNum)
                    .HasColumnName("Pat_Num")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address1)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Address2)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Birthday)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.CellPhone)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HomePhone)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sex)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Ssn)
                    .IsRequired()
                    .HasColumnName("SSN")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Zip)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PayerImportLog>(entity =>
            {
                entity.Property(e => e.AmdimportId).HasColumnName("AMDImportId");

                entity.Property(e => e.AmdpayerId)
                    .IsRequired()
                    .HasColumnName("AMDPayerId")
                    .HasMaxLength(50);

                entity.Property(e => e.ImportedDate).HasColumnType("datetime");

                entity.Property(e => e.OfficeKey).HasMaxLength(50);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Amdimport)
                    .WithMany(p => p.PayerImportLog)
                    .HasForeignKey(d => d.AmdimportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PayerImportLog_AdvanceMDImportLog");
            });

            modelBuilder.Entity<PayerInformation>(entity =>
            {
                entity.HasKey(e => e.PayerInfoId);

                entity.Property(e => e.GroupId).HasMaxLength(50);

                entity.Property(e => e.InsName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.MemberId).HasMaxLength(50);

                entity.Property(e => e.Type)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.HasOne(d => d.Insurance)
                    .WithMany(p => p.PayerInformation)
                    .HasForeignKey(d => d.InsuranceId)
                    .HasConstraintName("FK_PayerInformation_InsuranceInformation");

                entity.HasOne(d => d.Visit)
                    .WithMany(p => p.PayerInformation)
                    .HasForeignKey(d => d.VisitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PayerInformation_Visit");
            });

            modelBuilder.Entity<Physician>(entity =>
            {
                entity.HasKey(e => new { e.PvPhysicianId, e.OfficeKey })
                    .HasName("PK_Physican_1");

                entity.Property(e => e.PvPhysicianId).HasColumnName("PV_PhysicianId");

                entity.Property(e => e.AmProviderId)
                    .HasColumnName("AM_ProviderId")
                    .HasMaxLength(50);

                entity.Property(e => e.AmdCode).HasMaxLength(50);

                entity.Property(e => e.Clinic).HasMaxLength(50);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProgramConfig>(entity =>
            {
                entity.Property(e => e.AmdAppName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AmdofficeKey).HasColumnName("AMDOfficeKey");

                entity.Property(e => e.Apipassword)
                    .IsRequired()
                    .HasColumnName("APIPassword")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Apiuri)
                    .IsRequired()
                    .HasColumnName("APIUri")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ApiuserName)
                    .IsRequired()
                    .HasColumnName("APIUserName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CcemailAddress)
                    .HasColumnName("CCEmailAddress")
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.Environment)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FilePath)
                    .IsRequired()
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.FromEmailAddress)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.PvfilePrefix)
                    .HasColumnName("PVFilePrefix")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Pvfilename)
                    .HasColumnName("PVFilename")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RunAs)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Smtppassword)
                    .HasColumnName("SMTPPassword")
                    .HasMaxLength(50);

                entity.Property(e => e.Smtpport)
                    .HasColumnName("SMTPPort")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Smtpserver)
                    .HasColumnName("SMTPServer")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Smtpusername)
                    .HasColumnName("SMTPUsername")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TechEmailAddress)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.ToEmailAddress)
                    .HasMaxLength(1024)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Relationship>(entity =>
            {
                entity.HasKey(e => e.Hipaarelationship);

                entity.Property(e => e.Hipaarelationship)
                    .HasColumnName("HIPAARelationship")
                    .HasMaxLength(5);

                entity.Property(e => e.AmrelationshipCode).HasColumnName("AMRelationshipCode");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role", "Application");

                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<RoleClaim>(entity =>
            {
                entity.ToTable("RoleClaims", "Application");

                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<SourceProcessLog>(entity =>
            {
                entity.HasKey(e => e.ProcessId)
                    .HasName("pk_PorcessId");

                entity.Property(e => e.ImportedToAmd).HasColumnName("ImportedToAMD");

                entity.Property(e => e.ProcessResult)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ProcessedDate).HasColumnType("datetime");

                entity.Property(e => e.SourceFileName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "Application");

                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<UserClaim>(entity =>
            {
                entity.ToTable("UserClaims", "Application");

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserClaims)
                    .HasForeignKey(d => d.UserId);
            });

              

            modelBuilder.Entity<UserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.ToTable("UserLogins", "Application");

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserLogins)
                    .HasForeignKey(d => d.UserId);
            });

           
            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.ToTable("UserRoles", "Application");

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<UserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.ToTable("UserTokens", "Application");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Visit>(entity =>
            {
                entity.Property(e => e.ClinicId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CoPayAmount).HasColumnType("money");

                entity.Property(e => e.CopayNote).HasMaxLength(500);

                entity.Property(e => e.CopayType).HasMaxLength(50);

                entity.Property(e => e.CurrentPaymentAmount).HasColumnType("money");

                entity.Property(e => e.CurrentPaymentNote).HasMaxLength(500);

                entity.Property(e => e.CurrentPaymentType).HasMaxLength(50);

                entity.Property(e => e.DiagCodes).HasMaxLength(500);

                entity.Property(e => e.Emcode)
                    .HasColumnName("EMCode")
                    .HasMaxLength(50);

                entity.Property(e => e.EmModifier)
                    .HasColumnName("EMModifier")
                    .HasMaxLength(50);

                entity.Property(e => e.EmQuantity).HasColumnName("EMQuantity");

                entity.Property(e => e.Icdcodes)
                    .HasColumnName("ICDCodes")
                    .HasMaxLength(500);

                entity.Property(e => e.LastUpdateTime).HasColumnType("datetime");

                entity.Property(e => e.LastUpdateUser)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Notes).HasMaxLength(500);

                entity.Property(e => e.PreviousPaymentAmount).HasColumnType("money");

                entity.Property(e => e.PreviousPaymentNote).HasMaxLength(500);

                entity.Property(e => e.PreviousPaymentType).HasMaxLength(50);

                entity.Property(e => e.ProcCodes).HasMaxLength(500);

                entity.Property(e => e.PvlogNum).HasColumnName("PVLogNum");

                entity.Property(e => e.ServiceDate).HasColumnType("datetime");

                entity.Property(e => e.TimeIn).HasColumnType("datetime");

                entity.Property(e => e.TimeOut).HasColumnType("datetime");

                entity.Property(e => e.VisitType).HasMaxLength(50);

                entity.HasOne(d => d.Chart)
                    .WithMany(p => p.Visit)
                    .HasForeignKey(d => d.ChartId)
                    .HasConstraintName("FK_Visit_Chart");

                entity.HasOne(d => d.Clinic)
                    .WithMany(p => p.Visits)
                    .HasForeignKey(d => d.ClinicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Visit_ClinicProfile");

                entity.HasOne(d => d.GuarantorPayer)
                    .WithMany(p => p.Visit)
                    .HasForeignKey(d => d.GuarantorPayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Visit_GuarantorInformation");

                entity.HasOne(d => d.PvPatient)
                    .WithMany(p => p.Visit)
                    .HasForeignKey(d => d.PvPatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Visit_Patient_Information");

                entity.HasOne(d => d.Physician)
                    .WithMany(p => p.Visit)
                    .HasForeignKey(d => new { d.PhysicianId, d.OfficeKey })
                    .HasConstraintName("FK_Visit_Physician");
            });

            modelBuilder.Entity<VisitCodeHistory>(entity =>
            {
                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Code).HasMaxLength(200);

                entity.Property(e => e.CodeType)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ModifiedBy)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ModifiedTime).HasColumnType("datetime");

                entity.Property(e => e.Modifier).HasMaxLength(50);

                entity.Property(e => e.Modifier2).HasMaxLength(50);

                entity.HasOne(d => d.VisitHistory)
                    .WithMany(p => p.VisitCodeHistory)
                    .HasForeignKey(d => d.VisitHistoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VisitCodeHistory_VisitHistory");
            });

            modelBuilder.Entity<VisitHistory>(entity =>
            {
                entity.Property(e => e.CopayNote).HasMaxLength(500);

                entity.Property(e => e.DiagCodes).HasMaxLength(500);

                entity.Property(e => e.Emcode)
                    .HasColumnName("EMCode")
                    .HasMaxLength(50);

                entity.Property(e => e.FinalizedTime).HasColumnType("datetime");

                entity.Property(e => e.Icdcodes)
                    .HasColumnName("ICDCodes")
                    .HasMaxLength(500);

                entity.Property(e => e.ModifiedBy)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ModifiedTime).HasColumnType("datetime");

                entity.Property(e => e.ProcCodes).HasMaxLength(500);

                entity.Property(e => e.ServiceDate).HasColumnType("datetime");

                entity.Property(e => e.VisitId).HasColumnName("visitId");

                entity.HasOne(d => d.Visit)
                    .WithMany(p => p.VisitHistory)
                    .HasForeignKey(d => d.VisitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VisitHistory_Visit");
            });

            modelBuilder.Entity<VisitICDCode>(entity =>
            {
                entity.ToTable("VisitICDCode");

                entity.Property(e => e.VisitICDCodeId).HasColumnName("VisitICDCodeId");

                entity.Property(e => e.ICDCode)
                    .IsRequired()
                    .HasColumnName("ICDCode")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Visit)
                    .WithMany(p => p.VisitICDCode)
                    .HasForeignKey(d => d.VisitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VisitICDCode_Visit");
            });


            modelBuilder.Entity<VisitImportLog>(entity =>
            {
                entity.Property(e => e.AmdimportLogId).HasColumnName("AMDImportLogId");

                entity.Property(e => e.AmdvisitId)
                    .IsRequired()
                    .HasColumnName("AMDVisitId")
                    .HasMaxLength(50);

                entity.Property(e => e.ImportedDate).HasColumnType("datetime");

                entity.Property(e => e.OfficeKey)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.AmdimportLog)
                    .WithMany(p => p.VisitImportLog)
                    .HasForeignKey(d => d.AmdimportLogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VisitImportLog_AdvanceMDImportLog");

                entity.HasOne(d => d.Visit)
                    .WithMany(p => p.VisitImportLog)
                    .HasForeignKey(d => d.VisitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VisitImportLog_Visit");
            });

            modelBuilder.Entity<VisitProcCode>(entity =>
            {
                entity.Property(e => e.Modifier).HasMaxLength(50);

                entity.Property(e => e.ProcCode).HasMaxLength(200);

                entity.HasOne(d => d.Visit)
                    .WithMany(p => p.VisitProcCode)
                    .HasForeignKey(d => d.VisitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VisitProcCode_Visit");
            });

            modelBuilder.Entity<VisitRuleSet>(entity =>
            {
                entity.HasKey(e => e.VisitRuleId)
                    .HasName("PK_VisitRuleSets");

                entity.HasOne(d => d.CodeReviewRuleSet)
                    .WithMany(p => p.AppliedRules)
                    .HasForeignKey(d => d.RuleSetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VisitRuleSets_CodeReviewRule");

                entity.HasOne(d => d.Visit)
                    .WithMany(p => p.AppliedRules)
                    .HasForeignKey(d => d.VisitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VisitRuleSets_Visit");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }
    }
}

