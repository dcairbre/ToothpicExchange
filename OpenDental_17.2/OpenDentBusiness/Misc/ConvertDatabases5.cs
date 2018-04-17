using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeBase;
using System.Text.RegularExpressions;
using System.Globalization;

namespace OpenDentBusiness {
	public partial class ConvertDatabases {

		///<summary>This is the start of our new ConvertDatabases pattern where engineers do not need to worry about versioning info.
		///From this point on, the only version that engineers need to know about is the version within the method name.</summary>
		private static void To17_1_1() {
			string command;
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO displayreport(InternalName,ItemOrder,Description,Category,IsHidden) VALUES('ODDentalSealantMeasure',0,'FQHC Dental Sealant Measure',6,0)";
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO displayreport(DisplayReportNum,InternalName,ItemOrder,Description,Category,IsHidden) VALUES((SELECT MAX(DisplayReportNum)+1 FROM displayreport),'ODDentalSealantMeasure',0,'FQHC Dental Sealant Measure',6,0)";
				Db.NonQ(command);
			}
			command="SELECT MAX(displayreport.ItemOrder) FROM displayreport WHERE displayreport.Category = 2"; //monthly
			long itemorder = Db.GetLong(command)+1; //get the next available ItemOrder for the Monthly Category to put this new report last.
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO displayreport(InternalName,ItemOrder,Description,Category,IsHidden) VALUES('ODInsurancePayPlansPastDue',"+POut.Long(itemorder)+",'Ins Pay Plans Past Due',2,0)";
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO displayreport(DisplayReportNum,InternalName,ItemOrder,Description,Category,IsHidden) VALUES((SELECT MAX(DisplayReportNum)+1 FROM displayreport),'ODInsurancePayPlansPastDue',"+POut.Long(itemorder)+",'Ins Pay Plans Past Due',2,0)";
				Db.NonQ(command);
			}
			//Add ReportDaily permission to groups with existing ReportProdInc permission------------------------------------------------------
			command="SELECT DISTINCT UserGroupNum FROM grouppermission WHERE PermType=39";  //ReportProdInc
			DataTable table = Db.GetTable(command);
			long groupNum;
			if(DataConnection.DBtype==DatabaseType.MySql) {
				foreach(DataRow row in table.Rows) {
					groupNum=PIn.Long(row["UserGroupNum"].ToString());
					command="INSERT INTO grouppermission (UserGroupNum,PermType) "
							+"VALUES("+POut.Long(groupNum)+",133)";  //ReportDaily
					Db.NonQ(command);
				}
			}
			else {//oracle
				foreach(DataRow row in table.Rows) {
					groupNum=PIn.Long(row["UserGroupNum"].ToString());
					command="INSERT INTO grouppermission (GroupPermNum,NewerDays,UserGroupNum,PermType) "
							+"VALUES((SELECT MAX(GroupPermNum)+1 FROM grouppermission),0,"+POut.Long(groupNum)+",133)";  //ReportDaily
					Db.NonQ(command);
				}
			}
			//Add ReportProdIncAllProviders permission to groups with existing ReportProdInc permission------------------------------------------------------
			command="SELECT DISTINCT UserGroupNum FROM grouppermission WHERE PermType=39";  //ReportProdInc
			DataTable tableAllProv = Db.GetTable(command);
			long groupNumAllProv;
			if(DataConnection.DBtype==DatabaseType.MySql) {
				foreach(DataRow row in tableAllProv.Rows) {
					groupNumAllProv=PIn.Long(row["UserGroupNum"].ToString());
					command="INSERT INTO grouppermission (UserGroupNum,PermType) "
							+"VALUES("+POut.Long(groupNumAllProv)+",132)";  //ReportProdIncAllProviders
					Db.NonQ(command);
				}
			}
			else {//oracle
				foreach(DataRow row in tableAllProv.Rows) {
					groupNumAllProv=PIn.Long(row["UserGroupNum"].ToString());
					command="INSERT INTO grouppermission (GroupPermNum,NewerDays,UserGroupNum,PermType) "
							+"VALUES((SELECT MAX(GroupPermNum)+1 FROM grouppermission),0,"+POut.Long(groupNumAllProv)+",132)";  //ReportProdIncAllProviders
					Db.NonQ(command);
				}
			}
			//Add ReportDailyAllProviders permission to groups with existing ReportProdInc permission------------------------------------------------------
			command="SELECT DISTINCT UserGroupNum FROM grouppermission WHERE PermType=39";  //ReportProdInc
			DataTable tableAllDaily = Db.GetTable(command);
			long groupNumAllDaily;
			if(DataConnection.DBtype==DatabaseType.MySql) {
				foreach(DataRow row in tableAllDaily.Rows) {
					groupNumAllDaily=PIn.Long(row["UserGroupNum"].ToString());
					command="INSERT INTO grouppermission (UserGroupNum,PermType) "
							+"VALUES("+POut.Long(groupNumAllDaily)+",134)";  //ReportDailyAllProviders
					Db.NonQ(command);
				}
			}
			else {//oracle
				foreach(DataRow row in tableAllDaily.Rows) {
					groupNumAllDaily=PIn.Long(row["UserGroupNum"].ToString());
					command="INSERT INTO grouppermission (GroupPermNum,NewerDays,UserGroupNum,PermType) "
							+"VALUES((SELECT MAX(GroupPermNum)+1 FROM grouppermission),0,"+POut.Long(groupNumAllDaily)+",134)";  //ReportDailyAllProviders
					Db.NonQ(command);
				}
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO preference (PrefName,ValueString) VALUES('PasswordsWeakChangeToStrong','0')";
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO preference (PrefNum,PrefName,ValueString) VALUES((SELECT COALESCE(MAX(PrefNum),0)+1 FROM preference),'PasswordsWeakChangeToStrong','0')";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO preference(PrefName,ValueString) VALUES('ClaimPaymentNoShowZeroDate',CURDATE())";
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO preference(PrefNum,PrefName,ValueString) VALUES((SELECT MAX(PrefNum)+1 FROM preference),'ClaimPaymentNoShowZeroDate',SYSDATE)";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="DROP TABLE IF EXISTS patientlink";
				Db.NonQ(command);
				command=@"CREATE TABLE patientlink (
					PatientLinkNum bigint NOT NULL auto_increment PRIMARY KEY,
					PatNumFrom bigint NOT NULL,
					PatNumTo bigint NOT NULL,
					LinkType tinyint NOT NULL,
					DateTimeLink datetime NOT NULL DEFAULT '0001-01-01 00:00:00',
					INDEX(PatNumFrom),
					INDEX(PatNumTo)
					) DEFAULT CHARSET=utf8";
				Db.NonQ(command);
			}
			else {//oracle
				command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE patientlink'; EXCEPTION WHEN OTHERS THEN NULL; END;";
				Db.NonQ(command);
				command=@"CREATE TABLE patientlink (
					PatientLinkNum number(20) NOT NULL,
					PatNumFrom number(20) NOT NULL,
					PatNumTo number(20) NOT NULL,
					LinkType number(3) NOT NULL,
					DateTimeLink date DEFAULT TO_DATE('0001-01-01','YYYY-MM-DD') NOT NULL,
					CONSTRAINT patientlink_PatientLinkNum PRIMARY KEY (PatientLinkNum)
					)";
				Db.NonQ(command);
				command=@"CREATE INDEX patientlink_PatNumFrom ON patientlink (PatNumFrom)";
				Db.NonQ(command);
				command=@"CREATE INDEX patientlink_PatNumTo ON patientlink (PatNumTo)";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO preference(PrefName,ValueString) VALUES('QuickBooksClassRefs','')";
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO preference(PrefNum,PrefName,ValueString) VALUES((SELECT MAX(PrefNum)+1 FROM preference),'QuickBooksClassRefs','')";
				Db.NonQ(command);
			}
			command="UPDATE program SET ProgDesc='Carestream Ortho/OMS from www.carestreamdental.com' WHERE ProgName='Carestream'";//Renaming Carestream TO Carestream Ortho/OMS
			Db.NonQ(command);
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="ALTER TABLE clinic ADD ExternalID bigint NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE clinic ADD INDEX (ExternalID)";
				Db.NonQ(command);
			}
			else {//oracle
				command="ALTER TABLE clinic ADD ExternalID number(20)";
				Db.NonQ(command);
				command="UPDATE clinic SET ExternalID = 0 WHERE ExternalID IS NULL";
				Db.NonQ(command);
				command="ALTER TABLE clinic MODIFY ExternalID NOT NULL";
				Db.NonQ(command);
				command=@"CREATE INDEX clinic_ExternalID ON clinic (ExternalID)";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO preference (PrefName,ValueString) VALUES('CommLogAutoSave','0')";
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO preference (PrefNum,PrefName,ValueString) VALUES ((SELECT MAX(PrefNum)+1 FROM preference),'CommLogAutoSave','0')";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="ALTER TABLE displayfield ADD DescriptionOverride varchar(255) NOT NULL";
				Db.NonQ(command);
			}
			else {//oracle
				command="ALTER TABLE displayfield ADD DescriptionOverride varchar2(255)";
				Db.NonQ(command);
			}
			//Get the 1500_02_12 form.
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="SELECT ClaimFormNum FROM claimform WHERE UniqueID='OD12' LIMIT 1";
			}
			else {//oracle doesn't have LIMIT
				command="SELECT * FROM (SELECT ClaimFormNum FROM claimform WHERE UniqueID='OD12') WHERE RowNum<=1";
			}
			long claimFormNum = PIn.Long(Db.GetScalar(command));
			command="INSERT INTO claimformitem (ClaimFormItemNum,ClaimFormNum,ImageFileName,FieldName,FormatString,XPos,YPos,Width,Height) "
				+"VALUES ("+GetClaimFormItemNum()+","+POut.Long(claimFormNum)+",'','AcceptAssignmentY','','400','951','0','0')";
			Db.NonQ(command);
			command="INSERT INTO claimformitem (ClaimFormItemNum,ClaimFormNum,ImageFileName,FieldName,FormatString,XPos,YPos,Width,Height) "
				+"VALUES ("+GetClaimFormItemNum()+","+POut.Long(claimFormNum)+",'','AcceptAssignmentN','','450','951','0','0')";
			Db.NonQ(command);
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="ALTER TABLE userod ADD DateTFail datetime NOT NULL DEFAULT '0001-01-01 00:00:00'";
				Db.NonQ(command);
			}
			else {//oracle
				command="ALTER TABLE userod ADD DateTFail date";
				Db.NonQ(command);
				command="UPDATE userod SET DateTFail = TO_DATE('0001-01-01','YYYY-MM-DD') WHERE DateTFail IS NULL";
				Db.NonQ(command);
				command="ALTER TABLE userod MODIFY DateTFail NOT NULL";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="ALTER TABLE userod ADD FailedAttempts tinyint unsigned NOT NULL";
				Db.NonQ(command);
			}
			else {//oracle
				command="ALTER TABLE userod ADD FailedAttempts number(3)";
				Db.NonQ(command);
				command="UPDATE userod SET FailedAttempts = 0 WHERE FailedAttempts IS NULL";
				Db.NonQ(command);
				command="ALTER TABLE userod MODIFY FailedAttempts NOT NULL";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="ALTER TABLE claim ADD DateSentOrig date NOT NULL DEFAULT '0001-01-01'";
				Db.NonQ(command);
			}
			else {//oracle
				command="ALTER TABLE claim ADD DateSentOrig date";
				Db.NonQ(command);
				command="UPDATE claim SET DateSentOrig = TO_DATE('0001-01-01','YYYY-MM-DD') WHERE DateSentOrig IS NULL";
				Db.NonQ(command);
				command="ALTER TABLE claim MODIFY DateSentOrig NOT NULL";
				Db.NonQ(command);
			}
			//SheetDelete permission - Added to anybody that has SheetEdit
			command="SELECT DISTINCT UserGroupNum FROM grouppermission WHERE PermType=42";//42 - SheetEdit
			table=Db.GetTable(command);
			if(DataConnection.DBtype==DatabaseType.MySql) {
				foreach(DataRow row in table.Rows) {
					groupNum=PIn.Long(row["UserGroupNum"].ToString());
					command="INSERT INTO grouppermission (UserGroupNum,PermType) VALUES("+POut.Long(groupNum)+",136)";//136 - SheetDelete
					Db.NonQ(command);
				}
			}
			else {//oracle
				foreach(DataRow row in table.Rows) {
					groupNum=PIn.Long(row["UserGroupNum"].ToString());
					command="INSERT INTO grouppermission (GroupPermNum,NewerDays,UserGroupNum,PermType) "
						 +"VALUES((SELECT MAX(GroupPermNum)+1 FROM grouppermission),0,"+POut.Long(groupNum)+",136)";//136 - SheetDelete
					Db.NonQ(command);
				}
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO preference (PrefName,ValueString) VALUES('ClaimPaymentBatchOnly','0')";
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO preference (PrefNum,PrefName,ValueString) VALUES((SELECT MAX(PrefNum)+1 FROM preference),'ClaimPaymentBatchOnly','0')";
				Db.NonQ(command);
			}
			//UpdateCustomTracking permission - Added to anybody that has ClaimSentEdit
			command="SELECT DISTINCT UserGroupNum FROM grouppermission WHERE PermType=14";//14 - ClaimSentEdit
			table=Db.GetTable(command);
			if(DataConnection.DBtype==DatabaseType.MySql) {
				foreach(DataRow row in table.Rows) {
					groupNum=PIn.Long(row["UserGroupNum"].ToString());
					command="INSERT INTO grouppermission (UserGroupNum,PermType) VALUES("+POut.Long(groupNum)+",137)";//137 - UpdateCustomTracking
					Db.NonQ(command);
				}
			}
			else {//oracle
				foreach(DataRow row in table.Rows) {
					groupNum=PIn.Long(row["UserGroupNum"].ToString());
					command="INSERT INTO grouppermission (GroupPermNum,NewerDays,UserGroupNum,PermType) "
						 +"VALUES((SELECT MAX(GroupPermNum)+1 FROM grouppermission),0,"+POut.Long(groupNum)+",137)";//137 - UpdateCustomTracking
					Db.NonQ(command);
				}
			}
			//Add GraphicsEdit permission to everyone------------------------------------------------------
			command="SELECT DISTINCT UserGroupNum FROM grouppermission";
			table=Db.GetTable(command);
			if(DataConnection.DBtype==DatabaseType.MySql) {
				foreach(DataRow row in table.Rows) {
					groupNum=PIn.Long(row["UserGroupNum"].ToString());
					command="INSERT INTO grouppermission (UserGroupNum,PermType) "
						 +"VALUES("+POut.Long(groupNum)+",138)";  //138 - GraphicsEdit
					Db.NonQ(command);
				}
			}
			else {//oracle
				foreach(DataRow row in table.Rows) {
					groupNum=PIn.Long(row["UserGroupNum"].ToString());
					command="INSERT INTO grouppermission (GroupPermNum,NewerDays,UserGroupNum,PermType) "
						 +"VALUES((SELECT MAX(GroupPermNum)+1 FROM grouppermission),0,"+POut.Long(groupNum)+",138)";  //138 - GraphicsEdit
					Db.NonQ(command);
				}
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO preference (PrefName,ValueString) VALUES('WebSchedRecallIgnoreBlockoutTypes','')";
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO preference (PrefNum,PrefName,ValueString) VALUES "
					+"((SELECT MAX(PrefNum)+1 FROM preference),'WebSchedRecallIgnoreBlockoutTypes','')";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO preference (PrefName,ValueString) VALUES('WebSchedNewPatApptIgnoreBlockoutTypes','')";
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO preference (PrefNum,PrefName,ValueString) VALUES "
					+"((SELECT MAX(PrefNum)+1 FROM preference),'WebSchedNewPatApptIgnoreBlockoutTypes','')";
				Db.NonQ(command);
			}
			string defaultApptMessage = "Your first dental appointment with us will include a comprehensive exam, xrays and a consultation with the dentist.  "
				+"The dentist will provide treatment options, recommend care and address any remaining questions.";
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO preference (PrefName,ValueString) VALUES('WebSchedNewPatApptMessage','"+defaultApptMessage+"')";
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO preference (PrefNum,PrefName,ValueString) VALUES "
					+"((SELECT MAX(PrefNum)+1 FROM preference),'WebSchedNewPatApptMessage','"+defaultApptMessage+"')";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO preference (PrefName,ValueString) VALUES('AgingServiceTimeDue','')";
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO preference (PrefNum,PrefName,ValueString) VALUES ((SELECT MAX(PrefNum)+1 FROM preference),'AgingServiceTimeDue','')";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="ALTER TABLE refattach CHANGE IsFrom RefType tinyint NOT NULL";
				Db.NonQ(command);
			}
			else {//oracle
				command="ALTER TABLE refattach RENAME COLUMN IsFrom TO RefType";
				Db.NonQ(command);
			}
			//RAMQ clearinghouse.
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command=@"INSERT INTO clearinghouse (Description,ExportPath,Payors,Eformat,ISA05,SenderTin,ISA07,ISA08,ISA15,Password,ResponsePath,
					CommBridge,ClientProgram,LastBatchNumber,ModemPort,LoginID,SenderName,SenderTelephone,GS03,ISA02,ISA04,ISA16,SeparatorData,SeparatorSegment)
					VALUES ('Ramq','"+POut.String(@"C:\Ramq\")+"','','7','','','','','','','','18','',0,0,'','','','','','','','','')";
				Db.NonQ(command);
			}
			else {//oracle
				command=@"INSERT INTO clearinghouse (ClearinghouseNum,Description,ExportPath,Payors,Eformat,ISA05,SenderTin,ISA07,ISA08,ISA15,Password,
					ResponsePath,CommBridge,ClientProgram,LastBatchNumber,ModemPort,LoginID,SenderName,SenderTelephone,GS03,ISA02,ISA04,ISA16,SeparatorData,
					SeparatorSegment) VALUES ((SELECT MAX(ClearinghouseNum+1) FROM clearinghouse),
					'Ramq','"+POut.String(@"C:\Ramq\")+"','','7','','','','','','','','18','',0,0,'','','','','','','','','')";
				Db.NonQ(command);
			}
			command="UPDATE clearinghouse SET HqClearinghouseNum=ClearinghouseNum WHERE HqClearinghouseNum=0";
			Db.NonQ(command);
			//REPORTING SERVER PREFERENCES
			//serverName
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO preference (PrefName,ValueString) VALUES('ReportingServerCompName','')";
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO preference (PrefNum,PrefName,ValueString) VALUES "
					+"((SELECT MAX(PrefNum)+1 FROM preference),'ReportingServerCompName','')";
				Db.NonQ(command);
			}
			//dbName
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO preference (PrefName,ValueString) VALUES('ReportingServerDbName','')";
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO preference (PrefNum,PrefName,ValueString) VALUES "
					+"((SELECT MAX(PrefNum)+1 FROM preference),'ReportingServerDbName','')";
				Db.NonQ(command);
			}
			//mysqlUser
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO preference (PrefName,ValueString) VALUES('ReportingServerMySqlUser','')";
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO preference (PrefNum,PrefName,ValueString) VALUES "
					+"((SELECT MAX(PrefNum)+1 FROM preference),'ReportingServerMySqlUser','')";
				Db.NonQ(command);
			}
			//mysqlPassHash
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO preference (PrefName,ValueString) VALUES('ReportingServerMySqlPassHash','')";
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO preference (PrefNum,PrefName,ValueString) VALUES "
					+"((SELECT MAX(PrefNum)+1 FROM preference),'ReportingServerMySqlPassHash','')";
				Db.NonQ(command);
			}
			//ReportingServer URI for middle tier
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO preference (PrefName,ValueString) VALUES('ReportingServerURI','')";
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO preference (PrefNum,PrefName,ValueString) VALUES "
					+"((SELECT MAX(PrefNum)+1 FROM preference),'ReportingServerURI','')";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO preference (PrefName,ValueString) VALUES('WebSchedAutomaticSendTextSetting','0')";//Do not send by default
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO preference (PrefNum,PrefName,ValueString) VALUES ((SELECT MAX(PrefNum)+1 FROM preference),"
					+"'WebSchedAutomaticSendTextSetting','0')";//Do not send by default
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO preference (PrefName,ValueString) VALUES('RecallStatusTexted','0')";//Blank by default
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO preference (PrefNum,PrefName,ValueString) VALUES ((SELECT MAX(PrefNum)+1 FROM preference),"
					+"'RecallStatusTexted','0')";//Do not send by default
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO preference (PrefName,ValueString) VALUES('RecallStatusEmailedTexted','0')";//Blank by default
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO preference (PrefNum,PrefName,ValueString) VALUES ((SELECT MAX(PrefNum)+1 FROM preference),"
					+"'RecallStatusEmailedTexted','0')";//Do not send by default
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="DROP TABLE IF EXISTS webschedrecall";
				Db.NonQ(command);
				command=@"CREATE TABLE webschedrecall (
						WebSchedRecallNum bigint NOT NULL auto_increment PRIMARY KEY,
						ClinicNum bigint NOT NULL,
						PatNum bigint NOT NULL,
						RecallNum bigint NOT NULL,
						DateTimeEntry datetime NOT NULL DEFAULT '0001-01-01 00:00:00',
						DateDue date NOT NULL DEFAULT '0001-01-01',
						ReminderCount int NOT NULL,
						PreferRecallMethod tinyint NOT NULL,
						DateTimeReminderSent datetime NOT NULL DEFAULT '0001-01-01 00:00:00',
						DateTimeSendFailed datetime NOT NULL DEFAULT '0001-01-01 00:00:00',
						EmailSendStatus tinyint NOT NULL,
						SmsSendStatus tinyint NOT NULL,
						PhonePat varchar(255) NOT NULL,
						EmailPat varchar(255) NOT NULL,
						MsgTextToMobileTemplate text NOT NULL,
						MsgTextToMobile text NOT NULL,
						EmailSubjTemplate text NOT NULL,
						EmailSubj text NOT NULL,
						EmailTextTemplate text NOT NULL,
						EmailText text NOT NULL,
						GuidMessageToMobile text NOT NULL,
						ShortGUIDSms varchar(255) NOT NULL,
						ShortGUIDEmail varchar(255) NOT NULL,
						ResponseDescript text NOT NULL,
						Source tinyint NOT NULL,
						INDEX(ClinicNum),
						INDEX(PatNum),
						INDEX(RecallNum),
						INDEX(DateTimeReminderSent)
						) DEFAULT CHARSET=utf8";
				Db.NonQ(command);
			}
			else {//oracle
				command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE webschedrecall'; EXCEPTION WHEN OTHERS THEN NULL; END;";
				Db.NonQ(command);
				command=@"CREATE TABLE webschedrecall (
						WebSchedRecallNum number(20) NOT NULL,
						ClinicNum number(20) NOT NULL,
						PatNum number(20) NOT NULL,
						RecallNum number(20) NOT NULL,
						DateTimeEntry date DEFAULT TO_DATE('0001-01-01','YYYY-MM-DD') NOT NULL,
						DateDue date DEFAULT TO_DATE('0001-01-01','YYYY-MM-DD') NOT NULL,
						ReminderCount number(11) NOT NULL,
						PreferRecallMethod number(3) NOT NULL,
						DateTimeReminderSent date DEFAULT TO_DATE('0001-01-01','YYYY-MM-DD') NOT NULL,
						DateTimeSendFailed date DEFAULT TO_DATE('0001-01-01','YYYY-MM-DD') NOT NULL,
						EmailSendStatus number(3) NOT NULL,
						SmsSendStatus number(3) NOT NULL,
						PhonePat varchar2(255),
						EmailPat varchar2(255),
						MsgTextToMobileTemplate clob,
						MsgTextToMobile clob,
						EmailSubjTemplate clob,
						EmailSubj clob,
						EmailTextTemplate clob,
						EmailText clob,
						GuidMessageToMobile clob,
						ShortGUIDSms varchar2(255),
						ShortGUIDEmail varchar2(255),
						ResponseDescript clob,
						Source number(3) NOT NULL,
						CONSTRAINT webschedrecall_WebSchedRecallN PRIMARY KEY (WebSchedRecallNum)
						)";
				Db.NonQ(command);
				command=@"CREATE INDEX webschedrecall_ClinicNum ON webschedrecall (ClinicNum)";
				Db.NonQ(command);
				command=@"CREATE INDEX webschedrecall_PatNum ON webschedrecall (PatNum)";
				Db.NonQ(command);
				command=@"CREATE INDEX webschedrecall_RecallNum ON webschedrecall (RecallNum)";
				Db.NonQ(command);
				command=@"CREATE INDEX webschedrecall_DateTimeRemind ON webschedrecall (DateTimeReminderSent)";
				Db.NonQ(command);
			}
			//ProcCode Override Preference
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO preference (PrefName,ValueString) VALUES('ClaimPrintProcChartedDesc','0')";
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO preference (PrefNum,PrefName,ValueString) VALUES "
					+"((SELECT MAX(PrefNum)+1 FROM preference),'ClaimPrintProcChartedDesc','0')";
				Db.NonQ(command);
			}
			command="SELECT DISTINCT UserGroupNum FROM grouppermission WHERE PermType=110";//InsPlanEdit
			table=Db.GetTable(command);
			if(DataConnection.DBtype==DatabaseType.MySql) {
				foreach(DataRow row in table.Rows) {
					groupNum=PIn.Long(row["UserGroupNum"].ToString());
					command="INSERT INTO grouppermission (UserGroupNum,PermType) "
						+"VALUES("+POut.Long(groupNum)+",139)";//InsPlanOrthoEdit
					Db.NonQ(command);
				}
			}
			else {//oracle
				foreach(DataRow row in table.Rows) {
					groupNum=PIn.Long(row["UserGroupNum"].ToString());
					command="INSERT INTO grouppermission (GroupPermNum,NewerDays,UserGroupNum,PermType) "
						+"VALUES((SELECT MAX(GroupPermNum)+1 FROM grouppermission),0,"+POut.Long(groupNum)+",139)";//InsPlanOrthoEdit
					Db.NonQ(command);
				}
			}
			//RecurringChargesUseTransDate 
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO preference (PrefName,ValueString) VALUES ('RecurringChargesUseTransDate','0')";
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO preference (PrefNum,PrefName,ValueString) VALUES "
					+"((SELECT MAX(PrefNum)+1 FROM preference),'RecurringChargesUseTransDate','0')";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO preference (PrefName,ValueString) VALUES('ProcFeeUpdatePrompt','0')";
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO preference (PrefNum,PrefName,ValueString) VALUES ((SELECT MAX(PrefNum)+1 FROM preference),'ProcFeeUpdatePrompt','0')";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="DROP TABLE IF EXISTS claimtracking";
				Db.NonQ(command);
				command=@"CREATE TABLE claimtracking (
					ClaimTrackingNum bigint NOT NULL auto_increment PRIMARY KEY,
					ClaimNum bigint NOT NULL,
					TrackingType varchar(255) NOT NULL,
					UserNum bigint NOT NULL,
					DateTimeEntry timestamp,
					Note text NOT NULL,
					TrackingDefNum bigint NOT NULL,
					INDEX(ClaimNum),
					INDEX(UserNum),
					INDEX(TrackingDefNum)
					) DEFAULT CHARSET=utf8";
				Db.NonQ(command);
			}
			else {//oracle
				command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE claimtracking'; EXCEPTION WHEN OTHERS THEN NULL; END;";
				Db.NonQ(command);
				command=@"CREATE TABLE claimtracking (
					ClaimTrackingNum number(20) NOT NULL,
					ClaimNum number(20) NOT NULL,
					TrackingType varchar2(255),
					UserNum number(20) NOT NULL,
					DateTimeEntry timestamp,
					Note clob,
					TrackingDefNum number(20) NOT NULL,
					CONSTRAINT claimtracking_ClaimTrackingNum PRIMARY KEY (ClaimTrackingNum)
					)";
				Db.NonQ(command);
				command=@"CREATE INDEX claimtracking_ClaimNum ON claimtracking (ClaimNum)";
				Db.NonQ(command);
				command=@"CREATE INDEX claimtracking_UserNum ON claimtracking (UserNum)";
				Db.NonQ(command);
				command=@"CREATE INDEX claimtracking_TrackingDefNum ON claimtracking (TrackingDefNum)";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO preference (PrefName,ValueString) VALUES('ClaimTrackingRequiresError','0')";
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO preference (PrefNum,PrefName,ValueString) VALUES "
					+"((SELECT MAX(PrefNum)+1 FROM preference),'ClaimTrackingRequiresError','0')";
				Db.NonQ(command);
			}
			ODEvent.Fire(new ODEventArgs("ConvertDatabases","Upgrading database to version: 17.1.1 - securitylog | DefNumError column"));//No translation in convert script.
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="ALTER TABLE securitylog ADD DefNumError bigint NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE securitylog ADD INDEX (DefNumError)";
				Db.NonQ(command);
			}
			else {//oracle
				command="ALTER TABLE securitylog ADD DefNumError number(20)";
				Db.NonQ(command);
				command="UPDATE securitylog SET DefNumError = 0 WHERE DefNumError IS NULL";
				Db.NonQ(command);
				command="ALTER TABLE securitylog MODIFY DefNumError NOT NULL";
				Db.NonQ(command);
				command=@"CREATE INDEX securitylog_DefNumError ON securitylog (DefNumError)";
				Db.NonQ(command);
			}
			ODEvent.Fire(new ODEventArgs("ConvertDatabases","Upgrading database to version: 17.1.1"));//No translation in convert script.
			#region Web Sched Automated Texting
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO preference (PrefName,ValueString) VALUES('WebSchedAggregatedTextMessage',"
					+"'Dental checkups due: [FamilyListURLs].\r\nVisit links to schedule appointments or call [OfficePhone].')";
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO preference (PrefNum,PrefName,ValueString) VALUES ((SELECT MAX(PrefNum)+1 FROM preference),"
					+"'WebSchedAggregatedTextMessage','Dental checkups due: [FamilyListURLs].\r\nVisit links to schedule appointments or call [OfficePhone].')";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO preference (PrefName,ValueString) VALUES('WebSchedAggregatedEmailBody',"
					+"'These family members are due for a dental checkup: \r\n[FamilyListURLs]\r\nPlease visit the links above or "
					+"call our office today at [OfficePhone] to schedule your appointment.')";
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO preference (PrefNum,PrefName,ValueString) VALUES ((SELECT MAX(PrefNum)+1 FROM preference),"
					+"'WebSchedAggregatedEmailBody','These family members are due for a dental checkup: \r\n[FamilyListURLs]\r\nPlease visit the links above "
					+"or call our office today at [OfficePhone] to schedule your appointment.')";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO preference (PrefName,ValueString) VALUES('WebSchedAggregatedEmailSubject','Dental Care Reminder')";
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO preference (PrefNum,PrefName,ValueString) VALUES ((SELECT MAX(PrefNum)+1 FROM preference),"
					+"'WebSchedAggregatedEmailSubject','Dental Care Reminder')";
				Db.NonQ(command);
			}
			string textMessageTemplate="Dental checkup due for [NameF].\r\nVisit [URL] to schedule appointments or call [PracticePhone].";
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO preference (PrefName,ValueString) VALUES('WebSchedMessageText','"+textMessageTemplate+"')";
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO preference (PrefNum,PrefName,ValueString) VALUES ((SELECT MAX(PrefNum)+1 FROM preference),"
					+"'WebSchedMessageText','"+textMessageTemplate+".')";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO preference (PrefName,ValueString) VALUES('WebSchedMessageText2','"+textMessageTemplate+"')";
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO preference (PrefNum,PrefName,ValueString) VALUES ((SELECT MAX(PrefNum)+1 FROM preference),"
					+"'WebSchedMessageText2','"+textMessageTemplate+"')";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO preference (PrefName,ValueString) VALUES('WebSchedMessageText3','"+textMessageTemplate+"')";
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO preference (PrefNum,PrefName,ValueString) VALUES ((SELECT MAX(PrefNum)+1 FROM preference),"
					+"'WebSchedMessageText3','"+textMessageTemplate+"')";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="ALTER TABLE alertitem ADD FormToOpen tinyint NOT NULL";
				Db.NonQ(command);
			}
			else {//oracle
				command="ALTER TABLE alertitem ADD FormToOpen number(3)";
				Db.NonQ(command);
				command="UPDATE alertitem SET FormToOpen = 0 WHERE FormToOpen IS NULL";
				Db.NonQ(command);
				command="ALTER TABLE alertitem MODIFY FormToOpen NOT NULL";
				Db.NonQ(command);
			}
			command="UPDATE alertitem SET FormToOpen = 2";//The only possible alert type that might exist in the database is for pending payments.
			Db.NonQ(command);
			//Create an alert for offices that are using web sched telling them that they can use texting for automated recall.
			bool isUsingWebSched=false;
			command="SELECT ValueString FROM preference WHERE PrefName='WebSchedService'";
			table=Db.GetTable(command);
			if(table.Rows.Count > 0 && PIn.Bool(table.Rows[0][0].ToString())) {
				isUsingWebSched=true;
			}
			if(isUsingWebSched) {
				string alertDescript=@"Web Sched can now automatically send text messages to remind patients to schedule their recall appointments online.
Go to eServices -> Web Sched to activate.";
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="INSERT INTO alertitem (Type,Actions,Severity,ClinicNum,Description,FormToOpen) VALUES("
						+"0,"//AlertType.Generic
						+"7,"//ActionType.Delete|ActionType.MarkAsRead|ActionType.OpenForm
						+"1,"//SeverityType.Low
						+"0,"//ClinicNum
						+"'"+alertDescript+"',"
						+"1)";//FormType.FormEServicesWebSchedRecall
					Db.NonQ(command);
				}
				else {//oracle
					command="INSERT INTO alertitem (AlertItemNum,Type,Actions,Severity,ClinicNum,Description,FormToOpen) VALUES("+
						"(SELECT MAX(AlertItemNum)+1 FROM alertitem),"
						+"0,"//AlertType.Generic
						+"7,"//ActionType.Delete|ActionType.MarkAsRead|ActionType.OpenForm
						+"1,"//SeverityType.Low
						+"0,"//ClinicNum
						+"'"+alertDescript+"',"
						+"1)";//FormType.FormEServicesWebSchedRecall
					Db.NonQ(command);
				}
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO preference (PrefName,ValueString) VALUES('WebSchedSendThreadFrequency','7')";
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO preference (PrefNum,PrefName,ValueString) VALUES ((SELECT MAX(PrefNum)+1 FROM preference),"
					+"'WebSchedSendThreadFrequency','7')";
				Db.NonQ(command);
			}
			if(isUsingWebSched) {
				command=@"SELECT MAX(ItemOrder) MaxItemOrder
					FROM displayfield
					WHERE Category=4";//DisplayFieldCategory.RecallList
				int itemOrder=Db.GetInt(command);
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="INSERT INTO displayfield (InternalName,Description,ItemOrder,ColumnWidth,Category) VALUES("
						+"'WebSched',"
						+"'Web Sched',"
						+POut.Int(itemOrder+1)+","
						+"100,"
						+"4)";//DisplayFieldCategory.RecallList
					Db.NonQ(command);
				}
				else {//oracle
					command="INSERT INTO displayfield (DisplayFieldNum,InternalName,Description,ItemOrder,ColumnWidth,Category) VALUES("+
						"(SELECT MAX(DisplayFieldNum)+1 FROM displayfield),"
						+"'WebSched',"
						+"'Web Sched',"
						+POut.Int(itemOrder+1)+","
						+"100,"
						+"4)";//DisplayFieldCategory.RecallList
					Db.NonQ(command);
				}
			}
			#endregion Web Sched Automated Texting
			ODEvent.Fire(new ODEventArgs("ConvertDatabases","Upgrading database to version: 17.1.1 - Aging List | statement Index"));//No translation in convert script.
			try {
				if(DataConnection.DBtype==DatabaseType.MySql) {
					if(!IndexExists("statement","SuperFamily,Mode_,DateSent")) {//add composite index if it doesn't already exist
						command="ALTER TABLE statement ADD INDEX SuperFamModeDateSent (SuperFamily,Mode_,DateSent)";
						Db.NonQ(command);
						if(IndexExists("statement","SuperFamily")) {//drop redundant index once composite index is successfully added and only if it exists
							command="ALTER TABLE statement DROP INDEX SuperFamily";
							Db.NonQ(command);
						}
					}
				}
				else {//oracle
					command="CREATE INDEX statement_SFamModeDateSent ON statement (SuperFamily,Mode_,DateSent)";//add composite index
					Db.NonQ(command);
					command="DROP INDEX statement_SuperFamily";//drop redundant index once composite index is successfully added
					Db.NonQ(command);
				}
			}
			catch(Exception) { }//Only an index.
			ODEvent.Fire(new ODEventArgs("ConvertDatabases","Upgrading database to version: 17.1.1"));//No translation in convert script.
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="ALTER TABLE alertitem ADD FKey bigint NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE alertitem ADD INDEX (FKey)";
				Db.NonQ(command);
			}
			else {//oracle
				command="ALTER TABLE alertitem ADD FKey number(20)";
				Db.NonQ(command);
				command="UPDATE alertitem SET FKey = 0 WHERE FKey IS NULL";
				Db.NonQ(command);
				command="ALTER TABLE alertitem MODIFY FKey NOT NULL";
				Db.NonQ(command);
				command=@"CREATE INDEX alertitem_FKey ON alertitem (FKey)";
				Db.NonQ(command);
			}
			//Force all users to TP Use Sheets.  For now 17.1 we're just updating pref, for 17.2 we'll remove the pref from code.
			command="UPDATE preference SET ValueString='1' WHERE PrefName='TreatPlanUseSheets'";
			Db.NonQ(command);
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="ALTER TABLE clearinghouse ADD IsEraDownloadAllowed tinyint NOT NULL DEFAULT 1";
				Db.NonQ(command);
			}
			else {//oracle
				command="ALTER TABLE clearinghouse ADD IsEraDownloadAllowed number(3) DEFAULT 1";
				Db.NonQ(command);
				command="UPDATE clearinghouse SET IsEraDownloadAllowed = 1 WHERE IsEraDownloadAllowed IS NULL";
				Db.NonQ(command);
				command="ALTER TABLE clearinghouse MODIFY IsEraDownloadAllowed NOT NULL";
				Db.NonQ(command);
			}
			string alertDescriptStr=@"eServices can now be activated online.  Go to eServices -> Signup to activate.";
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO alertitem (Type,Actions,Severity,ClinicNum,Description,FormToOpen) VALUES("
					+"0,"//AlertType.Generic
					+"7,"//ActionType.Delete|ActionType.MarkAsRead|ActionType.OpenForm
					+"1,"//SeverityType.Low
					+"0,"//ClinicNum
					+"'"+alertDescriptStr+"',"
					+"4)";//4 - FormType.FormEServicesSetup.
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO alertitem (AlertItemNum,Type,Actions,Severity,ClinicNum,Description,FormToOpen) VALUES("+
					"(SELECT COALESCE(MAX(AlertItemNum),0)+1 FROM alertitem),"
					+"0,"//AlertType.Generic
					+"7,"//ActionType.Delete|ActionType.MarkAsRead|ActionType.OpenForm
					+"1,"//SeverityType.Low
					+"0,"//ClinicNum
					+"'"+alertDescriptStr+"',"
					+"4)";//4 - FormType.FormEServicesSetup.
				Db.NonQ(command);
			}
		}

		private static void To17_1_3() {
			string command;
			//Add ProcProvChangesClaimProcWithClaim pref if doesn't exist.  Default to on.
			//This pref was backported to 16.4 so it might exist in DB already.
			command="SELECT COUNT(PrefName) FROM preference WHERE PrefName='ProcProvChangesClaimProcWithClaim'";
			if(PIn.Int(Db.GetCount(command))==0) {
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="INSERT INTO preference(PrefName,ValueString) VALUES('ProcProvChangesClaimProcWithClaim','1')";
					Db.NonQ(command);
				}
				else {//oracle
					command="INSERT INTO preference(PrefNum,PrefName,ValueString) VALUES((SELECT COALESCE(MAX(PrefNum),0)+1 FROM preference),"
						+"'ProcProvChangesClaimProcWithClaim','1')";
					Db.NonQ(command);
				}
			}
		}

		private static void To17_1_7() {
			string command;
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO preference(PrefName,ValueString) VALUES('ApptConfirmExcludeERemind','')";
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO preference(PrefNum,PrefName,ValueString) VALUES((SELECT COALESCE(MAX(PrefNum),0)+1 FROM preference),"
					+"'ApptConfirmExcludeERemind','')";
				Db.NonQ(command);
			}
		}

		private static void To17_1_8() {
			string command;
			ODEvent.Fire(new ODEventArgs("ConvertDatabases","Upgrading database to version: 17.1.8 - Recall Sync | procedurelog composite index"));
			try {
				if(DataConnection.DBtype==DatabaseType.MySql) {
					if(!IndexExists("procedurelog","PatNum,ProcStatus,CodeNum,ProcDate")) {//add composite index if it doesn't already exist
						command="ALTER TABLE procedurelog ADD INDEX PatStatusCodeDate (PatNum,ProcStatus,CodeNum,ProcDate)";
						Db.NonQ(command);
						if(IndexExists("procedurelog","PatNum")) {//drop redundant index once composite index is successfully added and only if it exists
							command="ALTER TABLE procedurelog DROP INDEX indexPatNum";
							Db.NonQ(command);
						}
					}
				}
				else {//oracle
					command="CREATE INDEX procedurelog_PatStatusCodeDate ON procedurelog (PatNum,ProcStatus,CodeNum,ProcDate)";//add composite index
					Db.NonQ(command);
					command="DROP INDEX procedurelog_indexPatNum";//drop redundant index once composite index is successfully added
					Db.NonQ(command);
				}
			}
			catch(Exception) { }//Only an index.
			ODEvent.Fire(new ODEventArgs("ConvertDatabases","Upgrading database to version: 17.1.8 - Recall Sync | appointment composite index"));
			try {
				if(DataConnection.DBtype==DatabaseType.MySql) {
					if(!IndexExists("appointment","AptStatus,AptDateTime")) {//add composite index if it doesn't already exist
						command="ALTER TABLE appointment ADD INDEX StatusDate (AptStatus,AptDateTime)";
						Db.NonQ(command);
						if(IndexExists("appointment","AptStatus")) {//drop redundant index once composite index is successfully added and only if it exists
							command="ALTER TABLE appointment DROP INDEX AptStatus";
							Db.NonQ(command);
						}
					}
				}
				else {//oracle
					command="CREATE INDEX appointment_StatusDate ON appointment (AptStatus,AptDateTime)";//add composite index
					Db.NonQ(command);
					command="DROP INDEX appointment_AptStatus";//drop redundant index once composite index is successfully added
					Db.NonQ(command);
				}
			}
			catch(Exception) { }//Only an index.
			ODEvent.Fire(new ODEventArgs("ConvertDatabases","Upgrading database to version: 17.1.8"));//No translation in convert script.
		}

		private static void To17_1_10() {
			string command="SELECT ValueString FROM preference WHERE PrefName='RecallStatusTexted'";
			if(Db.GetScalar(command)=="0") {//They have not set the status yet.
				long defNum=0;
				//Check to see if they already have a 'Texted' status. If they do, use that definition, otherwise insert a new definition.
				command="SELECT DefNum FROM definition WHERE Category=13 "//DefCat.RecallUnschedStatus
					+"AND LOWER(ItemName)='texted' AND IsHidden=0";
				DataTable table=Db.GetTable(command);
				if(table.Rows.Count > 0) {
					defNum=PIn.Long(table.Rows[0][0].ToString());
				}
				if(defNum==0) {//We didn't find a definition named 'Texted'.
					//Insert new status for 'Texted'
					command="SELECT MAX(ItemOrder)+1 FROM definition WHERE Category=13";
					string maxOrder=Db.GetScalar(command);
					if(maxOrder=="") {
						maxOrder="0";
					}
					if(DataConnection.DBtype==DatabaseType.MySql) {
						command="INSERT INTO definition (Category, ItemOrder, ItemName) VALUES (13,"+maxOrder+",'Texted')";
						defNum=Db.NonQ(command,true);
					}
					else {//oracle
						command="INSERT INTO definition (DefNum,Category, ItemOrder, ItemName) VALUES ((SELECT MAX(DefNum)+1 FROM definition),13,"+maxOrder
							+",'Texted')";
						defNum=Db.NonQ(command,true);
					}
				}
				command="UPDATE preference SET ValueString='"+POut.Long(defNum)+"' WHERE PrefName='RecallStatusTexted'";
				Db.NonQ(command);
			}
			command="SELECT ValueString FROM preference WHERE PrefName='RecallStatusEmailedTexted'";
			if(Db.GetScalar(command)=="0") {//They have not set the status yet.
				//Insert new status for 'Texted/Emailed'
				long defNum;
				command="SELECT MAX(ItemOrder)+1 FROM definition WHERE Category=13";
				string maxOrder=Db.GetScalar(command);
				if(maxOrder=="") {
					maxOrder="0";
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="INSERT INTO definition (Category, ItemOrder, ItemName) VALUES (13,"+maxOrder+",'Texted/Emailed')";
					defNum=Db.NonQ(command,true);
				}
				else {//oracle
					command="INSERT INTO definition (DefNum,Category, ItemOrder, ItemName) VALUES ((SELECT MAX(DefNum)+1 FROM definition),13,"+maxOrder
						+",'Texted/Emailed')";
					defNum=Db.NonQ(command,true);
				}
				command="UPDATE preference SET ValueString='"+POut.Long(defNum)+"' WHERE PrefName='RecallStatusEmailedTexted'";
				Db.NonQ(command);
			}
		}

		private static void To17_1_16() {
			string command;
			ODEvent.Fire(new ODEventArgs("ConvertDatabases","Upgrading database to version: 17.1.16 - Global Update Fees | patient FeeSched index"));
			try {
				if(DataConnection.DBtype==DatabaseType.MySql) {
					if(!IndexExists("patient","FeeSched")) {//add index if it doesn't already exist
						command="ALTER TABLE patient ADD INDEX FeeSched (FeeSched)";
						Db.NonQ(command);
					}
				}
				else {//oracle
					command="CREATE INDEX patient_FeeSched ON patient (FeeSched)";
					Db.NonQ(command);
				}
			}
			catch(Exception) { }//Only an index.
		}

		private static void To17_1_17() {
			string command;
			ODEvent.Fire(new ODEventArgs("ConvertDatabases","Upgrading database to version: 17.1.17 - Updating payment table"));
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="ALTER TABLE payment ADD RecurringChargeDate date NOT NULL DEFAULT '0001-01-01'";
				Db.NonQ(command);
			}
			else {//oracle
				command="ALTER TABLE payment ADD RecurringChargeDate date";
				Db.NonQ(command);
				command="UPDATE payment SET RecurringChargeDate = TO_DATE('0001-01-01','YYYY-MM-DD') WHERE RecurringChargeDate IS NULL";
				Db.NonQ(command);
				command="ALTER TABLE payment MODIFY RecurringChargeDate NOT NULL";
				Db.NonQ(command);
			}
		}

		private static void To17_1_18() {
			string command;
			//Insert PracticeByNumbers bridge-----------------------------------------------------------------
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO program (ProgName,ProgDesc,Enabled,Path,CommandLine,Note"
					+") VALUES("
					+"'PracticeByNumbers', "
					+"'Practice by Numbers from www.practicenumbers.com', "
					+"'0', "
					+"'', "
					+"'', "
					+"'')";
				long programNum=Db.NonQ(command,true);
				command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
					+") VALUES("
					+"'"+POut.Long(programNum)+"', "
					+"'Disable Advertising', "
					+"'0')";
				Db.NonQ(command);
			}
			else {//oracle	
				command="INSERT INTO program (ProgramNum,ProgName,ProgDesc,Enabled,Path,CommandLine,Note"
					+") VALUES("
					+"(SELECT COALESCE(MAX(ProgramNum),0)+1 FROM program),"
					+"'PracticeByNumbers', "
					+"'Practice by Numbers from www.practicenumbers.com', "
					+"'0', "
					+"'', "
					+"'', "
					+"'')";
				long programNum=Db.NonQ(command,true);
				command="INSERT INTO programproperty (ProgramPropertyNum,ProgramNum,PropertyDesc,PropertyValue,ClinicNum"
					+") VALUES("
					+"(SELECT COALESCE(MAX(ProgramPropertyNum),0)+1 FROM programproperty),"
					+"'"+POut.Long(programNum)+"', "
					+"'Disable Advertising', "
					+"'0', "
					+"'0')";
				Db.NonQ(command);
			}//end PracticeByNumbers bridge
		}

		private static void To17_1_19() {
			string command;
			command="UPDATE claim SET DateSentOrig=DateSent WHERE DateSentOrig='0001-01-01' AND DateSent!='0001-01-01' AND ClaimStatus IN ('S','R')";
			Db.NonQ(command);
		}

		private static void To17_1_22() {
			string command;
			//Insert NewTom bridge-----------------------------------------------------------------
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO program (ProgName,ProgDesc,Enabled,Path,CommandLine,Note"
					 +") VALUES("
					 +"'NewTomNNT', "
					 +"'NewTom NNT from www.newtom.it', "
					 +"'0', "
					 +"'"+POut.String(@"C:\NNT\NNTBridge.exe")+"', "
					 +"'"+POut.String(@"")+"', "//leave blank if none
					 +"'')";
				long programNum = Db.NonQ(command,true);
				command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
					 +") VALUES("
					 +"'"+POut.Long(programNum)+"', "
					 +"'Enter 0 to use PatientNum, or 1 to use ChartNum', "
					 +"'0')";
				Db.NonQ(command);
				command="INSERT INTO toolbutitem (ProgramNum,ToolBar,ButtonText) "
					 +"VALUES ("
					 +"'"+POut.Long(programNum)+"', "
					 +"'2', "//ToolBarsAvail.ChartModule
					 +"'NewTomNNT')";
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO program (ProgramNum,ProgName,ProgDesc,Enabled,Path,CommandLine,Note"
					 +") VALUES("
					 +"(SELECT COALESCE(MAX(ProgramNum),0)+1 FROM program),"
					 +"'NewTomNNT', "
					 +"'NewTom NNT from www.newtom.it', "
					 +"'0', "
					 +"'"+POut.String(@"C:\NNT\NNTBridge.exe")+"', "
					 +"'"+POut.String(@"")+"', "//leave blank if none
					 +"'')";
				long programNum = Db.NonQ(command,true);
				command="INSERT INTO programproperty (ProgramPropertyNum,ProgramNum,PropertyDesc,PropertyValue,ClinicNum"
					 +") VALUES("
					 +"(SELECT COALESCE(MAX(ProgramPropertyNum),0)+1 FROM programproperty),"
					 +"'"+POut.Long(programNum)+"', "
					 +"'Enter 0 to use PatientNum, or 1 to use ChartNum', "
					 +"'0', "
					 +"'0')";
				Db.NonQ(command);
				command="INSERT INTO toolbutitem (ToolButItemNum,ProgramNum,ToolBar,ButtonText) "
					 +"VALUES ("
					 +"(SELECT COALESCE(MAX(ToolButItemNum),0)+1 FROM toolbutitem),"
					 +"'"+POut.Long(programNum)+"', "
					 +"'2', "//ToolBarsAvail.ChartModule
					 +"'NewTomNNT')";
				Db.NonQ(command);
			}//end NewTom bridge
			//Insert i-Dixel bridge-----------------------------------------------------------------
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO program (ProgName,ProgDesc,Enabled,Path,CommandLine,Note"
					 +") VALUES("
					 +"'iDixel', "
					 +"'i-Dixel from http://www.jmoritaeurope.de/en/products/diagnostic-and-imaging-equipment/imaging-software/i-dixel/', "
					 +"'0', "
					 +"'"+POut.String(@"C:\Program Files\JMorita\ToIView\ToiViewLauncher.bat")+"', "
					 +"'"+POut.String(@"")+"', "//leave blank if none
					 +"'')";
				long programNum = Db.NonQ(command,true);
				command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
					 +") VALUES("
					 +"'"+POut.Long(programNum)+"', "
					 +"'Enter 0 to use PatientNum, or 1 to use ChartNum', "
					 +"'0')";
				Db.NonQ(command);
				command="INSERT INTO toolbutitem (ProgramNum,ToolBar,ButtonText) "
					 +"VALUES ("
					 +"'"+POut.Long(programNum)+"', "
					 +"'2', "//ToolBarsAvail.ChartModule
					 +"'i-Dixel')";
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO program (ProgramNum,ProgName,ProgDesc,Enabled,Path,CommandLine,Note"
					 +") VALUES("
					 +"(SELECT COALESCE(MAX(ProgramNum),0)+1 FROM program),"
					 +"'iDixel', "
					 +"'i-Dixel from http://www.jmoritaeurope.de/en/products/diagnostic-and-imaging-equipment/imaging-software/i-dixel/', "
					 +"'0', "
					 +"'"+POut.String(@"C:\Program Files\JMorita\ToIView\ToiViewLauncher.bat")+"', "
					 +"'"+POut.String(@"")+"', "//leave blank if none
					 +"'')";
				long programNum = Db.NonQ(command,true);
				command="INSERT INTO programproperty (ProgramPropertyNum,ProgramNum,PropertyDesc,PropertyValue,ClinicNum"
					 +") VALUES("
					 +"(SELECT COALESCE(MAX(ProgramPropertyNum),0)+1 FROM programproperty),"
					 +"'"+POut.Long(programNum)+"', "
					 +"'Enter 0 to use PatientNum, or 1 to use ChartNum', "
					 +"'0', "
					 +"'0')";
				Db.NonQ(command);
				command="INSERT INTO toolbutitem (ToolButItemNum,ProgramNum,ToolBar,ButtonText) "
					 +"VALUES ("
					 +"(SELECT COALESCE(MAX(ToolButItemNum),0)+1 FROM toolbutitem),"
					 +"'"+POut.Long(programNum)+"', "
					 +"'2', "//ToolBarsAvail.ChartModule
					 +"'i-Dixel')";
				Db.NonQ(command);
			}//end iDixel bridge
			//ADSTRA Imaging bridge-----------------------------------------------------------------
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO program (ProgName,ProgDesc,Enabled,Path,CommandLine,Note"
					 +") VALUES("
					 +"'Adstra', "
					 +"'ADSTRA Imaging from http://adstra.com/adstra-dental-software/', "
					 +"'0', "
					 +"'"+POut.String(@"C:/Program Files/ADSTRA/adstradde.exe")+"', "
					 +"'"+POut.String(@"")+"', "//leave blank if none
					 +"'')";
				long programNum = Db.NonQ(command,true);
				command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
					+") VALUES("
					+"'"+POut.Long(programNum)+"', "
					+"'Enter 0 to use PatientNum, or 1 to use ChartNum', "
					+"'0')";
				Db.NonQ(command);
				command="INSERT INTO toolbutitem (ProgramNum,ToolBar,ButtonText) "
					 +"VALUES ("
					 +"'"+POut.Long(programNum)+"', "
					 +"'2', "//ToolBarsAvail.ChartModule
					 +"'ADSTRA')";
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO program (ProgramNum,ProgName,ProgDesc,Enabled,Path,CommandLine,Note"
					 +") VALUES("
					 +"(SELECT COALESCE(MAX(ProgramNum),0)+1 FROM program),"
					 +"'Adstra', "
					 +"'ADSTRA Imaging from http://adstra.com/adstra-dental-software/', "
					 +"'0', "
					 +"'"+POut.String(@"C:/Program Files/ADSTRA/adstradde.exe")+"', "
					 +"'"+POut.String(@"")+"', "//leave blank if none
					 +"'')";
				long programNum = Db.NonQ(command,true);
				command="INSERT INTO programproperty (ProgramPropertyNum,ProgramNum,PropertyDesc,PropertyValue,ClinicNum"
					+") VALUES("
					+"(SELECT COALESCE(MAX(ProgramPropertyNum),0)+1 FROM programproperty),"
					+"'"+POut.Long(programNum)+"', "
					+"'Enter 0 to use PatientNum, or 1 to use ChartNum', "
					+"'0', "
					+"'0')";
				Db.NonQ(command);
				command="INSERT INTO toolbutitem (ToolButItemNum,ProgramNum,ToolBar,ButtonText) "
					 +"VALUES ("
					 +"(SELECT COALESCE(MAX(ToolButItemNum),0)+1 FROM toolbutitem),"
					 +"'"+POut.Long(programNum)+"', "
					 +"'2', "//ToolBarsAvail.ChartModule
					 +"'ADSTRA')";
				Db.NonQ(command);
			}//end ADSTRA Imaging bridge
			//Remove the . characters from the WebSchedAggregatedTextMessage preference. Clicking a link with a period will not get recognized. 
			command="UPDATE preference SET ValueString=REPLACE(ValueString,'[FamilyListURLs].','[FamilyListURLs]') WHERE PrefName='WebSchedAggregatedTextMessage'";
			Db.NonQ(command);
		}

		private static void To17_2_1() {
			string command;
			//For the next 3 database columns added, type is "text", because text is dynamically resized.
			//This means that if the column is empty then it will not take any extra space, other than header data.
			//Since these 3 database columns are HQ only, we do not want to bloat the table for our clients.
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="ALTER TABLE repeatcharge ADD Npi text NOT NULL";
				Db.NonQ(command);
			}
			else {//oracle
				command="ALTER TABLE repeatcharge ADD Npi clob";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="ALTER TABLE repeatcharge ADD ErxAccountId text NOT NULL";
				Db.NonQ(command);
			}
			else {//oracle
				command="ALTER TABLE repeatcharge ADD ErxAccountId clob";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="ALTER TABLE repeatcharge ADD ProviderName text NOT NULL";
				Db.NonQ(command);
			}
			else {//oracle
				command="ALTER TABLE repeatcharge ADD ProviderName clob";
				Db.NonQ(command);
			}
			DataTable table;
			command="SELECT ValueString FROM preference WHERE preference.PrefName = 'DistributorKey'";
			if(PIn.Bool(Db.GetScalar(command))) {
				command="SELECT * FROM repeatcharge WHERE repeatcharge.Note LIKE '%NPI%' OR Note LIKE '%ErxAccountId%'";
				table=Db.GetTable(command);
				foreach(DataRow row in table.Rows) {
					string strNoteOld=row["Note"].ToString();
					string strNPI="";
					string strAccountID="";
					string strNoteNew="";
					Match m=Regex.Match(strNoteOld,"^NPI=([0-9]{10})(  ErxAccountId=([0-9]+\\-[a-zA-Z0-9]{5}))?(\r\n)?",RegexOptions.IgnoreCase);
					if(m.Success) {
						strNPI=m.Result("$1");
						strAccountID=m.Result("$3");
						strNoteNew=strNoteOld.Substring(m.Length);//We use m.length so we can additionally skip the newline if present.
						command="UPDATE repeatcharge SET repeatcharge.Npi='"+POut.String(strNPI)+"',repeatcharge.ErxAccountId='"+POut.String(strAccountID)
							+"',repeatCharge.Note='"+POut.String(strNoteNew)+"' WHERE repeatcharge.RepeatChargeNum="+row["RepeatChargeNum"];
						Db.NonQ(command);
					}
				}
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="ALTER TABLE providererx ADD IsEpcs tinyint NOT NULL";
				Db.NonQ(command);
			}
			else {//oracle
				command="ALTER TABLE providererx ADD IsEpcs number(3)";
				Db.NonQ(command);
				command="UPDATE providererx SET IsEpcs = 0 WHERE IsEpcs IS NULL";
				Db.NonQ(command);
				command="ALTER TABLE providererx MODIFY IsEpcs NOT NULL";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="ALTER TABLE tasksubscription ADD TaskNum bigint NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE tasksubscription ADD INDEX (TaskNum)";
				Db.NonQ(command);
			}
			else {//oracle
				command="ALTER TABLE tasksubscription ADD TaskNum number(20)";
				Db.NonQ(command);
				command="UPDATE tasksubscription SET TaskNum = 0 WHERE TaskNum IS NULL";
				Db.NonQ(command);
				command="ALTER TABLE tasksubscription MODIFY TaskNum NOT NULL";
				Db.NonQ(command);
				command=@"CREATE INDEX tasksubscription_TaskNum ON tasksubscription (TaskNum)";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="ALTER TABLE claimtracking ADD TrackingErrorDefNum bigint NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE claimtracking ADD INDEX (TrackingErrorDefNum)";
				Db.NonQ(command);
			}
			else {//oracle
				command="ALTER TABLE claimtracking ADD TrackingErrorDefNum number(20)";
				Db.NonQ(command);
				command="UPDATE claimtracking SET TrackingErrorDefNum = 0 WHERE TrackingErrorDefNum IS NULL";
				Db.NonQ(command);
				command="ALTER TABLE claimtracking MODIFY TrackingErrorDefNum NOT NULL";
				Db.NonQ(command);
				command=@"CREATE INDEX claimtracking_TrackingErrorNum ON claimtracking (TrackingErrorDefNum)";
				Db.NonQ(command);
			}
			ODEvent.Fire(new ODEventArgs("ConvertDatabases","Upgrading database to version: 17.2.1 - Claim Tracking"));
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="INSERT INTO claimtracking (ClaimNum,TrackingType,UserNum,DateTimeEntry,Note,TrackingDefNum,TrackingErrorDefNum) "
					+"SELECT FKey,'StatusHistory',UserNum,LogDateTime,LogText,DefNum,DefNumError "
					+"FROM securitylog "
					+"WHERE PermType=95";//PermType 95 is ClaimHistoryEdit
					Db.NonQ(command);
				}
				else {//oracle
				command="SELECT * FROM securitylog WHERE PermType=95;";//PermType 95 is ClaimHistoryEdit
				DataTable tableCustomTrackingLogs=Db.GetTable(command);
				foreach(DataRow row in tableCustomTrackingLogs.Rows) {
					command="INSERT INTO claimtracking(ClaimTrackingNum,ClaimNum,TrackingType,UserNum,DateTimeEntry,Note,TrackingDefNum,TrackingErrorDefNum) "
						+"VALUES("
							+"(SELECT COALESCE(MAX(ClaimTrackingNum),0)+1 FROM claimtracking)"
							+","+POut.Long(PIn.Long(row["FKey"].ToString()))//ClaimNum
							+",'StatusHistory'"//TrackingType
							+","+POut.Long(PIn.Long(row["UserNum"].ToString()))//UserNum
							+","+POut.DateT(PIn.DateT(row["LogDateTime"].ToString()))//DateTimeEntry
							+",'"+POut.String(row["LogText"].ToString())+"'"//Note
							+","+POut.Long(PIn.Long(row["DefNum"].ToString()))//TrackingDefNum
							+","+POut.Long(PIn.Long(row["DefNumError"].ToString()))//TrackingErrorDefNum
						+")";
					Db.NonQ(command);
				}
			}
			ODEvent.Fire(new ODEventArgs("ConvertDatabases","Upgrading database to version: 17.2.1"));//No translation in convert script.
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="ALTER TABLE grouppermission ADD FKey bigint NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE grouppermission ADD INDEX (FKey)";
				Db.NonQ(command);
			}
			else {//oracle
				command="ALTER TABLE grouppermission ADD FKey number(20)";
				Db.NonQ(command);
				command="UPDATE grouppermission SET FKey = 0 WHERE FKey IS NULL";
				Db.NonQ(command);
				command="ALTER TABLE grouppermission MODIFY FKey NOT NULL";
				Db.NonQ(command);
				command=@"CREATE INDEX grouppermission_FKey ON grouppermission (FKey)";
				Db.NonQ(command);
			}
			DataTable userGroupTable=Db.GetTable("SELECT * FROM usergroup");
			DataTable reportTable=Db.GetTable("SELECT * FROM displayreport");
			foreach(DataRow row in userGroupTable.Rows) {
				//See if this UserGroup has ProdInc or DailyReport permissions
				string permissionReports=Db.GetCount("SELECT COUNT(*) FROM grouppermission WHERE PermType=22 AND UserGroupNum="+row["UserGroupNum"].ToString());
				if(permissionReports=="0") {
					continue;//This group doesn't have Reports permission.  Don't add any individual report permissions.
				}
				string permissionProdInc=Db.GetCount("SELECT COUNT(*) FROM grouppermission WHERE PermType=39 AND UserGroupNum="+row["UserGroupNum"].ToString());
				string permissionDailyReport=Db.GetCount("SELECT COUNT(*) FROM grouppermission WHERE PermType=133 AND UserGroupNum="+row["UserGroupNum"].ToString());
				foreach(DataRow reportRow in reportTable.Rows) {
					if(reportRow["IsHidden"].ToString()=="1") {
						continue;
					}
					if(permissionProdInc=="0" && (reportRow["InternalName"].ToString()=="ODToday" //They don't have ProdInc permission.  Don't add perms for those reports.
						|| reportRow["InternalName"].ToString()=="ODYesterday"
						|| reportRow["InternalName"].ToString()=="ODThisMonth"
						|| reportRow["InternalName"].ToString()=="ODLastMonth"
						|| reportRow["InternalName"].ToString()=="ODThisYear"
						|| reportRow["InternalName"].ToString()=="ODMoreOptions"
						|| reportRow["InternalName"].ToString()=="ODProviderPayrollSummary"
						|| reportRow["InternalName"].ToString()=="ODProviderPayrollDetailed"))
					{
						continue;
					}
					if(permissionDailyReport=="0" && (reportRow["InternalName"].ToString()=="ODAdjustments" //They don't have DailyReport permission.  Don't add perms for those reports.
						|| reportRow["InternalName"].ToString()=="ODPayments"
						|| reportRow["InternalName"].ToString()=="ODProcedures"
						|| reportRow["InternalName"].ToString()=="ODWriteoffs"))
					{
						continue;
					}
					//The report isn't hidden and it isn't restricted based on ProdInc or DailyReport perms.  Add a GroupPermission for it.
					if(DataConnection.DBtype==DatabaseType.MySql) {
						command="INSERT INTO grouppermission (NewerDate,NewerDays,UserGroupNum,PermType,FKey) " //Insert Reports permission (22) for the particular report.
							+"VALUES('0001-01-01',0,"+row["UserGroupNum"].ToString()+",22,"+reportRow["DisplayReportNum"].ToString()+")";
						Db.NonQ(command);
					}
					else {//oracle
						command="INSERT INTO grouppermission (GroupPermissionNum,NewerDate,NewerDays,UserGroupNum,PermType,FKey) " //Insert Reports permission (22) for the particular report.
							+"VALUES((SELECT MAX(GroupPermissionNum)+1 FROM grouppermission),'0001-01-01',0,"+row["UserGroupNum"].ToString()+",22,"+reportRow["DisplayReportNum"].ToString()+")";
						Db.NonQ(command);
					}
				}
			}
			//Insert OrthoCAD bridge-----------------------------------------------------------------
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO program (ProgName,ProgDesc,Enabled,Path,CommandLine,Note"
					+") VALUES("
					+"'OrthoCAD', "
					+"'OrthoCAD from www.itero.com/', "
					+"'0', "
					+"'"+POut.String(@"C:\Program Files\Cadent\OrthoCAD\OrthoCAD.exe")+"', "
					+"'', "//leave blank if none
					+"'')";
				long programNum=Db.NonQ(command,true);
				command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
					+") VALUES("
					+"'"+POut.Long(programNum)+"', "
					+"'Enter 0 to use PatientNum, or 1 to use ChartNum', "
					+"'0')";
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO program (ProgramNum,ProgName,ProgDesc,Enabled,Path,CommandLine,Note"
					+") VALUES("
					+"(SELECT MAX(ProgramNum)+1 FROM program),"
					+"'OrthoCAD', "
					+"'OrthoCAD from www.itero.com/', "
					+"'0', "
					+"'"+POut.String(@"C:\Program Files\Cadent\OrthoCAD\OrthoCAD.exe")+"', "
					+"'', "//leave blank if none
					+"'')";
				long programNum=Db.NonQ(command,true);
				command="INSERT INTO programproperty (ProgramPropertyNum,ProgramNum,PropertyDesc,PropertyValue,ClinicNum"
					+") VALUES("
					+"(SELECT MAX(ProgramPropertyNum+1) FROM programproperty),"
					+"'"+POut.Long(programNum)+"', "
					+"'Enter 0 to use PatientNum, or 1 to use ChartNum', "
					+"'0', "
					+"'0')";
				Db.NonQ(command);
			}//end OrthoCAD bridge
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="ALTER TABLE signalod ADD RemoteRole tinyint NOT NULL";
				Db.NonQ(command);
			}
			else {//oracle
				command="ALTER TABLE signalod ADD RemoteRole number(3)";
				Db.NonQ(command);
				command="UPDATE signalod SET RemoteRole = 0 WHERE RemoteRole IS NULL";
				Db.NonQ(command);
				command="ALTER TABLE signalod MODIFY RemoteRole NOT NULL";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO preference(PrefName,ValueString) VALUES('PatInitBillingTypeFromPriInsPlan','0')";
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO preference(PrefNum,PrefName,ValueString) VALUES((SELECT COALESCE(MAX(PrefNum),0)+1 FROM preference),"
					+"'PatInitBillingTypeFromPriInsPlan','0')";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="ALTER TABLE insplan ADD BillingType bigint NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE insplan ADD INDEX (BillingType)";
				Db.NonQ(command);
			}
			else {//oracle
				command="ALTER TABLE insplan ADD BillingType number(20)";
				Db.NonQ(command);
				command="UPDATE insplan SET BillingType = 0 WHERE BillingType IS NULL";
				Db.NonQ(command);
				command="ALTER TABLE insplan MODIFY BillingType NOT NULL";
				Db.NonQ(command);
				command=@"CREATE INDEX insplan_BillingType ON insplan (BillingType)";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO preference(PrefName,ValueString) VALUES('FormClickDelay','0.2')";
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO preference(PrefNum,PrefName,ValueString) VALUES((SELECT COALESCE(MAX(PrefNum),0)+1 FROM preference),'FormClickDelay','0.2')";
				Db.NonQ(command);
			}
			//Add InsPlanMerge permission for users that have InsPlanEdit permission------------------------------------------------------
			command="SELECT DISTINCT UserGroupNum FROM grouppermission WHERE PermType=110 GROUP BY UserGroupNum";
			DataTable groupPermTable=Db.GetTable(command);
			long groupNum;
			if(DataConnection.DBtype==DatabaseType.MySql) {
				foreach(DataRow row in groupPermTable.Rows) {
					groupNum=PIn.Long(row["UserGroupNum"].ToString());
					command="INSERT INTO grouppermission (UserGroupNum,PermType) "
						+"VALUES("+POut.Long(groupNum)+",141)";//InsPlanMerge
					Db.NonQ(command);
				}
			}
			else {//oracle
				foreach(DataRow row in groupPermTable.Rows) {
					groupNum=PIn.Long(row["UserGroupNum"].ToString());
					command="INSERT INTO grouppermission (GroupPermNum,NewerDays,UserGroupNum,PermType) "
						+"VALUES((SELECT MAX(GroupPermNum)+1 FROM grouppermission),0,"+POut.Long(groupNum)+",141)";//InsPlanMerge
					Db.NonQ(command);
				}
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="ALTER TABLE carrier ADD CarrierGroupName varchar(255) NOT NULL";
				Db.NonQ(command);
			}
			else {//oracle
				command="ALTER TABLE carrier ADD CarrierGroupName varchar2(255)";
				Db.NonQ(command);
			}		
			//Invoice Payments Grid Show WriteOffs
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO preference(PrefName,ValueString) VALUES('InvoicePaymentsGridShowNetProd','1')";
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO preference(PrefNum,PrefName,ValueString) VALUES((SELECT COALESCE(MAX(PrefNum),0)+1 FROM preference),"
					+"'InvoicePaymentsGridShowNetProd','1')";
				Db.NonQ(command);
			}
			//Add InsCarrierCombine permission to everyone------------------------------------------------------
			command="SELECT UserGroupNum FROM grouppermission GROUP BY UserGroupNum";
			groupPermTable=Db.GetTable(command);
			if(DataConnection.DBtype==DatabaseType.MySql) {
				foreach(DataRow row in groupPermTable.Rows) {
					groupNum=PIn.Long(row["UserGroupNum"].ToString());
					command="INSERT INTO grouppermission (UserGroupNum,PermType) "
						+"VALUES("+POut.Long(groupNum)+",142)";//InsCarrierCombine
					Db.NonQ(command);
				}
			}
			else {//oracle
				foreach(DataRow row in groupPermTable.Rows) {
					groupNum=PIn.Long(row["UserGroupNum"].ToString());
					command="INSERT INTO grouppermission (GroupPermNum,NewerDays,UserGroupNum,PermType) "
						+"VALUES((SELECT MAX(GroupPermNum)+1 FROM grouppermission),0,"+POut.Long(groupNum)+",142)";//InsCarrierCombine
					Db.NonQ(command);
				}
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="ALTER TABLE erxlog ADD UserNum bigint NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE erxlog ADD INDEX (UserNum)";
				Db.NonQ(command);
			}
			else {//oracle
				command="ALTER TABLE erxlog ADD UserNum number(20)";
				Db.NonQ(command);
				command="UPDATE erxlog SET UserNum = 0 WHERE UserNum IS NULL";
				Db.NonQ(command);
				command="ALTER TABLE erxlog MODIFY UserNum NOT NULL";
				Db.NonQ(command);
				command=@"CREATE INDEX erxlog_UserNum ON erxlog (UserNum)";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="ALTER TABLE labcase ADD DateTStamp timestamp";
				Db.NonQ(command);
				command="UPDATE labcase SET DateTStamp = NOW()";
				Db.NonQ(command);
			}
			else {//oracle
				command="ALTER TABLE labcase ADD DateTStamp timestamp";
				Db.NonQ(command);
				command="UPDATE labcase SET DateTStamp = SYSTIMESTAMP";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="ALTER TABLE etrans ADD UserNum bigint NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE etrans ADD INDEX (UserNum)";
				Db.NonQ(command);
			}
			else {//oracle
				command="ALTER TABLE etrans ADD UserNum number(20)";
				Db.NonQ(command);
				command="UPDATE etrans SET UserNum = 0 WHERE UserNum IS NULL";
				Db.NonQ(command);
				command="ALTER TABLE etrans MODIFY UserNum NOT NULL";
				Db.NonQ(command);
				command=@"CREATE INDEX etrans_UserNum ON etrans (UserNum)";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="ALTER TABLE proccodenote ADD ProcStatus tinyint NOT NULL";
				Db.NonQ(command);
				command="UPDATE proccodenote SET ProcStatus = 2";// ProcStat.Complete
				Db.NonQ(command);
			}
			else {//oracle
				command="ALTER TABLE proccodenote ADD ProcStatus number(3)";
				Db.NonQ(command);
				command="UPDATE proccodenote SET ProcStatus = 2 WHERE ProcStatus IS NULL";// ProcStat.Complete
				Db.NonQ(command);
				command="ALTER TABLE proccodenote MODIFY ProcStatus NOT NULL";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="ALTER TABLE procedurecode ADD DefaultTPNote text NOT NULL";
				Db.NonQ(command);
			}
			else {//oracle
				command="ALTER TABLE procedurecode ADD DefaultTPNote varchar2(4000)";
				Db.NonQ(command);
			}
			command="SELECT DISTINCT UserGroupNum FROM grouppermission";
			table=Db.GetTable(command);
			if(DataConnection.DBtype==DatabaseType.MySql) {
				foreach(DataRow row in table.Rows) {
					groupNum=PIn.Long(row["UserGroupNum"].ToString());
					command="INSERT INTO grouppermission (UserGroupNum,PermType) "
						 +"VALUES("+POut.Long(groupNum)+",143)"; //PopupEdit
					Db.NonQ(command);
				}
			}
			else {//oracle
				foreach(DataRow row in table.Rows) {
					groupNum=PIn.Long(row["UserGroupNum"].ToString());
					command="INSERT INTO grouppermission (GroupPermNum,NewerDays,UserGroupNum,PermType) "
						 +"VALUES((SELECT MAX(GroupPermNum)+1 FROM grouppermission),0,"+POut.Long(groupNum)+",143)"; //PopupEdit
					Db.NonQ(command);
				}
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO preference(PrefName,ValueString) VALUES('ClaimReportReceivedByService','0')";
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO preference(PrefNum,PrefName,ValueString) VALUES((SELECT COALESCE(MAX(PrefNum),0)+1 FROM preference),"
					+"'ClaimReportReceivedByService','0')";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO preference(PrefName,ValueString) VALUES('TitleBarClinicUseAbbr','0')";
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO preference(PrefNum,PrefName,ValueString) VALUES((SELECT COALESCE(MAX(PrefNum),0)+1 FROM preference),'TitleBarClinicUseAbbr','0')";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="DROP TABLE IF EXISTS histappointment";
				Db.NonQ(command);
				command=@"CREATE TABLE histappointment (
						HistApptNum bigint NOT NULL auto_increment PRIMARY KEY,
						HistUserNum bigint NOT NULL,
						HistDateTStamp datetime NOT NULL DEFAULT '0001-01-01 00:00:00',
						HistApptAction tinyint NOT NULL,
						ApptSource tinyint NOT NULL,
						AptNum bigint NOT NULL,
						PatNum bigint NOT NULL,
						AptStatus tinyint NOT NULL,
						Pattern varchar(255) NOT NULL,
						Confirmed bigint NOT NULL,
						TimeLocked tinyint NOT NULL,
						Op bigint NOT NULL,
						Note varchar(255) NOT NULL,
						ProvNum bigint NOT NULL,
						ProvHyg bigint NOT NULL,
						AptDateTime datetime NOT NULL DEFAULT '0001-01-01 00:00:00',
						NextAptNum bigint NOT NULL,
						UnschedStatus bigint NOT NULL,
						IsNewPatient tinyint NOT NULL,
						ProcDescript varchar(255) NOT NULL,
						Assistant bigint NOT NULL,
						ClinicNum bigint NOT NULL,
						IsHygiene tinyint NOT NULL,
						DateTStamp timestamp,
						DateTimeArrived datetime NOT NULL DEFAULT '0001-01-01 00:00:00',
						DateTimeSeated datetime NOT NULL DEFAULT '0001-01-01 00:00:00',
						DateTimeDismissed datetime NOT NULL DEFAULT '0001-01-01 00:00:00',
						InsPlan1 bigint NOT NULL,
						InsPlan2 bigint NOT NULL,
						DateTimeAskedToArrive datetime NOT NULL DEFAULT '0001-01-01 00:00:00',
						ProcsColored varchar(255) NOT NULL,
						ColorOverride int NOT NULL,
						AppointmentTypeNum bigint NOT NULL,
						SecUserNumEntry bigint NOT NULL,
						SecDateEntry date NOT NULL DEFAULT '0001-01-01',
						INDEX(HistUserNum),
						INDEX(AptNum),
						INDEX(PatNum),
						INDEX(Confirmed),
						INDEX(Op),
						INDEX(ProvNum),
						INDEX(ProvHyg),
						INDEX(NextAptNum),
						INDEX(UnschedStatus),
						INDEX(Assistant),
						INDEX(ClinicNum),
						INDEX(InsPlan1),
						INDEX(InsPlan2),
						INDEX(AppointmentTypeNum),
						INDEX(SecUserNumEntry)
						) DEFAULT CHARSET=utf8";
				Db.NonQ(command);
			}
			else {//oracle
				command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE histappointment'; EXCEPTION WHEN OTHERS THEN NULL; END;";
				Db.NonQ(command);
				command=@"CREATE TABLE histappointment (
						HistApptNum number(20) NOT NULL,
						HistUserNum number(20) NOT NULL,
						HistDateTStamp date DEFAULT TO_DATE('0001-01-01','YYYY-MM-DD') NOT NULL,
						HistApptAction number(3) NOT NULL,
						ApptSource number(3) NOT NULL,
						AptNum number(20) NOT NULL,
						PatNum number(20) NOT NULL,
						AptStatus number(3) NOT NULL,
						Pattern varchar2(255),
						Confirmed number(20) NOT NULL,
						TimeLocked number(3) NOT NULL,
						Op number(20) NOT NULL,
						Note varchar2(255),
						ProvNum number(20) NOT NULL,
						ProvHyg number(20) NOT NULL,
						AptDateTime date DEFAULT TO_DATE('0001-01-01','YYYY-MM-DD') NOT NULL,
						NextAptNum number(20) NOT NULL,
						UnschedStatus number(20) NOT NULL,
						IsNewPatient number(3) NOT NULL,
						ProcDescript varchar2(255),
						Assistant number(20) NOT NULL,
						ClinicNum number(20) NOT NULL,
						IsHygiene number(3) NOT NULL,
						DateTStamp timestamp,
						DateTimeArrived date DEFAULT TO_DATE('0001-01-01','YYYY-MM-DD') NOT NULL,
						DateTimeSeated date DEFAULT TO_DATE('0001-01-01','YYYY-MM-DD') NOT NULL,
						DateTimeDismissed date DEFAULT TO_DATE('0001-01-01','YYYY-MM-DD') NOT NULL,
						InsPlan1 number(20) NOT NULL,
						InsPlan2 number(20) NOT NULL,
						DateTimeAskedToArrive date DEFAULT TO_DATE('0001-01-01','YYYY-MM-DD') NOT NULL,
						ProcsColored varchar2(255),
						ColorOverride number(11) NOT NULL,
						AppointmentTypeNum number(20) NOT NULL,
						SecUserNumEntry number(20) NOT NULL,
						SecDateEntry date DEFAULT TO_DATE('0001-01-01','YYYY-MM-DD') NOT NULL,
						CONSTRAINT histappointment_HistApptNum PRIMARY KEY (HistApptNum)
						)";
				Db.NonQ(command);
				command=@"CREATE INDEX histappointment_HistUserNum ON histappointment (HistUserNum)";
				Db.NonQ(command);
				command=@"CREATE INDEX histappointment_AptNum ON histappointment (AptNum)";
				Db.NonQ(command);
				command=@"CREATE INDEX histappointment_PatNum ON histappointment (PatNum)";
				Db.NonQ(command);
				command=@"CREATE INDEX histappointment_Confirmed ON histappointment (Confirmed)";
				Db.NonQ(command);
				command=@"CREATE INDEX histappointment_Op ON histappointment (Op)";
				Db.NonQ(command);
				command=@"CREATE INDEX histappointment_ProvNum ON histappointment (ProvNum)";
				Db.NonQ(command);
				command=@"CREATE INDEX histappointment_ProvHyg ON histappointment (ProvHyg)";
				Db.NonQ(command);
				command=@"CREATE INDEX histappointment_NextAptNum ON histappointment (NextAptNum)";
				Db.NonQ(command);
				command=@"CREATE INDEX histappointment_UnschedStatus ON histappointment (UnschedStatus)";
				Db.NonQ(command);
				command=@"CREATE INDEX histappointment_Assistant ON histappointment (Assistant)";
				Db.NonQ(command);
				command=@"CREATE INDEX histappointment_ClinicNum ON histappointment (ClinicNum)";
				Db.NonQ(command);
				command=@"CREATE INDEX histappointment_InsPlan1 ON histappointment (InsPlan1)";
				Db.NonQ(command);
				command=@"CREATE INDEX histappointment_InsPlan2 ON histappointment (InsPlan2)";
				Db.NonQ(command);
				command=@"CREATE INDEX histappointment_AppointmentTyp ON histappointment (AppointmentTypeNum)";
				Db.NonQ(command);
				command=@"CREATE INDEX histappointment_SecUserNumEntr ON histappointment (SecUserNumEntry)";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="DROP TABLE IF EXISTS substitutionlink";
				Db.NonQ(command);
				command=@"CREATE TABLE substitutionlink (
					SubstitutionLinkNum bigint NOT NULL auto_increment PRIMARY KEY,
					PlanNum bigint NOT NULL,
					CodeNum bigint NOT NULL,
					INDEX(PlanNum),
					INDEX(CodeNum)
					) DEFAULT CHARSET=utf8";
				Db.NonQ(command);
			}
			else {//oracle
				command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE substitutionlink'; EXCEPTION WHEN OTHERS THEN NULL; END;";
				Db.NonQ(command);
				command=@"CREATE TABLE substitutionlink (
					SubstitutionLinkNum number(20) NOT NULL,
					PlanNum number(20) NOT NULL,
					CodeNum number(20) NOT NULL,
					CONSTRAINT substitutionlink_SubstitutionL PRIMARY KEY (SubstitutionLinkNum)
					)";
				Db.NonQ(command);
				command=@"CREATE INDEX substitutionlink_PlanNum ON substitutionlink (PlanNum)";
				Db.NonQ(command);
				command=@"CREATE INDEX substitutionlink_CodeNum ON substitutionlink (CodeNum)";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="ALTER TABLE treatplan ADD TPType tinyint NOT NULL";
				Db.NonQ(command);
			}
			else {//oracle
				command="ALTER TABLE treatplan ADD TPType number(3)";
				Db.NonQ(command);
				command="UPDATE treatplan SET TPType = 0 WHERE TPType IS NULL";
				Db.NonQ(command);
				command="ALTER TABLE treatplan MODIFY TPType NOT NULL";
				Db.NonQ(command);
			}
			command="UPDATE treatplan INNER JOIN patient ON treatplan.PatNum=patient.PatNum SET TPType=1 WHERE patient.DiscountPlanNum!=0";
			Db.NonQ(command);
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="DROP TABLE IF EXISTS inseditlog";
				Db.NonQ(command);
				command=@"CREATE TABLE inseditlog (
						InsEditLogNum bigint NOT NULL auto_increment PRIMARY KEY,
						FKey bigint NOT NULL,
						LogType tinyint NOT NULL,
						FieldName varchar(255) NOT NULL,
						OldValue varchar(255) NOT NULL,
						NewValue varchar(255) NOT NULL,
						UserNum bigint NOT NULL,
						DateTStamp timestamp,
						ParentKey bigint NOT NULL,
						INDEX FKeyType (LogType,FKey),
						INDEX(UserNum),
						INDEX(ParentKey)
						) DEFAULT CHARSET=utf8";
				Db.NonQ(command);
			}
			else {//oracle
				command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE inseditlog'; EXCEPTION WHEN OTHERS THEN NULL; END;";
				Db.NonQ(command);
				command=@"CREATE TABLE inseditlog (
						InsEditLogNum number(20) NOT NULL,
						FKey number(20) NOT NULL,
						LogType number(3) NOT NULL,
						FieldName varchar2(255),
						OldValue varchar2(255),
						NewValue varchar2(255),
						UserNum number(20) NOT NULL,
						DateTStamp timestamp,
						ParentKey number(20) NOT NULL,
						CONSTRAINT inseditlog_InsEditLogNum PRIMARY KEY (InsEditLogNum)
						)";
				Db.NonQ(command);
				command=@"CREATE INDEX inseditlog_FKeyType ON inseditlog (LogType,FKey)";
				Db.NonQ(command);
				command=@"CREATE INDEX inseditlog_UserNum ON inseditlog (UserNum)";
				Db.NonQ(command);
				command=@"CREATE INDEX inseditlog_ParentKey ON inseditlog (ParentKey)";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="DROP TABLE IF EXISTS stmtlink";
				Db.NonQ(command);
				command=@"CREATE TABLE stmtlink (
						StmtLinkNum bigint NOT NULL auto_increment PRIMARY KEY,
						StatementNum bigint NOT NULL,
						StmtLinkType tinyint NOT NULL,
						FKey bigint NOT NULL,
						INDEX(StatementNum),
						INDEX FKeyAndType (StmtLinkType,FKey)
						) DEFAULT CHARSET=utf8";
				Db.NonQ(command);
			}
			else {//oracle
				command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE stmtlink'; EXCEPTION WHEN OTHERS THEN NULL; END;";
				Db.NonQ(command);
				command=@"CREATE TABLE stmtlink (
						StmtLinkNum number(20) NOT NULL,
						StatementNum number(20) NOT NULL,
						StmtLinkType number(3) NOT NULL,
						FKey number(20) NOT NULL,
						CONSTRAINT stmtlink_StmtLinkNum PRIMARY KEY (StmtLinkNum)
						)";
				Db.NonQ(command);
				command=@"CREATE INDEX stmtlink_StatementNum ON stmtlink (StatementNum)";
				Db.NonQ(command);
				command=@"CREATE INDEX stmtlink_FKey ON stmtlink (FKey)";
				Db.NonQ(command);
			}
			//Convert StmtProcAttach rows to stmtlink table
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO stmtlink (StatementNum,StmtLinkType,FKey) "
						+"SELECT StatementNum,1,ProcNum "
						+"FROM stmtprocattach ";
				Db.NonQ(command);
			}
			else {//oracle
				command="SELECT StatementNum,ProcNum FROM stmtprocattach";
				DataTable tableStmtLink=Db.GetTable(command);
				foreach(DataRow row in tableStmtLink.Rows) {
					command="INSERT INTO stmtink(StmtLinkNum,StatementNum,StmtLinkType,FKey) "
							+"VALUES("
									+"(SELECT COALESCE(MAX(StmtLinkNum),0)+1 FROM stmtlink)"
									+","+POut.Long(PIn.Long(row["StatementNum"].ToString()))
									+",1"//Type
									+","+POut.Long(PIn.Long(row["ProcNum"].ToString()))//AdjNum
							+")";
					Db.NonQ(command);
				}
			}
			//Convert StmtPaySplitAttach rows to stmtlink table
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO stmtlink (StatementNum,StmtLinkType,FKey) "
						+"SELECT StatementNum,2,PaySplitNum "
						+"FROM stmtpaysplitattach ";
				Db.NonQ(command);
			}
			else {//oracle
				command="SELECT StatementNum,PaySplitNum FROM stmtpaysplitattach";
				DataTable tableStmtLink = Db.GetTable(command);
				foreach(DataRow row in tableStmtLink.Rows) {
					command="INSERT INTO stmtink(StmtLinkNum,StatementNum,StmtLinkType,FKey) "
							+"VALUES("
									+"(SELECT COALESCE(MAX(StmtLinkNum),0)+1 FROM stmtlink)"
									+","+POut.Long(PIn.Long(row["StatementNum"].ToString()))
									+",2"//Type
									+","+POut.Long(PIn.Long(row["PaySplitNum"].ToString()))//PaySplitNum
							+")";
					Db.NonQ(command);
				}
			}
			//Convert StmtAdjAttach rows to stmtlink table
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO stmtlink (StatementNum,StmtLinkType,FKey) "
						+"SELECT StatementNum,3,AdjNum "
						+"FROM stmtadjattach ";
				Db.NonQ(command);
			}
			else {//oracle
				command="SELECT StatementNum,AdjNum FROM stmtadjattach";
				DataTable tableStmtLink = Db.GetTable(command);
				foreach(DataRow row in tableStmtLink.Rows) {
					command="INSERT INTO stmtink(StmtLinkNum,StatementNum,StmtLinkType,FKey) "
							+"VALUES("
									+"(SELECT COALESCE(MAX(StmtLinkNum),0)+1 FROM stmtlink)"
									+","+POut.Long(PIn.Long(row["StatementNum"].ToString()))
									+",3"//Type
									+","+POut.Long(PIn.Long(row["AdjNum"].ToString()))//AdjNum
							+")";
					Db.NonQ(command);
				}
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="DROP TABLE IF EXISTS stmtprocattach";
				Db.NonQ(command);
			}
			else {//oracle
				command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE stmtprocattach'; EXCEPTION WHEN OTHERS THEN NULL; END;";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="DROP TABLE IF EXISTS stmtpaysplitattach";
				Db.NonQ(command);
			}
			else {//oracle
				command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE stmtpaysplitattach'; EXCEPTION WHEN OTHERS THEN NULL; END;";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="DROP TABLE IF EXISTS stmtadjattach";
				Db.NonQ(command);
			}
			else {//oracle
				command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE stmtadjattach'; EXCEPTION WHEN OTHERS THEN NULL; END;";
				Db.NonQ(command);
			}
			//We shouldn't have ADA hardcoded into our program.  Changing the description to Proc Code but preserving ADA Code for current USA customers.
			//The display fields Categories of 0 and 6 are None (default progress note grid) and ProcedureGroupNote
			//The following queries are Oracle compatible. 
			if(CultureInfo.CurrentCulture.Name=="en-US") {
				command="UPDATE displayfield SET Description='ADA Code' WHERE Description='' AND Category IN (0,6) AND InternalName = 'ADA Code'";
				Db.NonQ(command);
			}
			command="UPDATE displayfield SET InternalName='Proc Code' WHERE InternalName='ADA Code' AND Category IN (0,6)";
			Db.NonQ(command);
			//Add InsPlanPickListExisting permission to groups with existing permission InsPlanEdit------------------------------------------------------
			command="SELECT DISTINCT UserGroupNum FROM grouppermission WHERE PermType=110";  //110 is InsPlanEdit
			table=Db.GetTable(command);
			if(DataConnection.DBtype==DatabaseType.MySql) {
				foreach(DataRow row in table.Rows) {
					groupNum=PIn.Long(row["UserGroupNum"].ToString());
					command="INSERT INTO grouppermission (UserGroupNum,PermType) "
						 +"VALUES("+POut.Long(groupNum)+",144)"; //InsPlanPickListExisting
					Db.NonQ(command);
				}
			}
			else {//oracle
				foreach(DataRow row in table.Rows) {
					groupNum=PIn.Long(row["UserGroupNum"].ToString());
					command="INSERT INTO grouppermission (GroupPermNum,NewerDays,UserGroupNum,PermType) "
						 +"VALUES((SELECT COALESCE(MAX(GroupPermNum),0)+1 FROM grouppermission),0,"+POut.Long(groupNum)+",144)"; //InsPlanPickListExisting
					Db.NonQ(command);
				}
			}
			if(DataConnection.DBtype==DatabaseType.MySql) { //allow future debits, default to current behavior. 0 by default
				command="INSERT INTO preference(PrefName,ValueString) VALUES('AccountAllowFutureDebits',0)";
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO preference(PrefNum,PrefName,ValueString) VALUES((SELECT COALESCE(MAX(PrefNum),0)+1 FROM preference),'AccountAllowFutureDebits',0)";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="ALTER TABLE procedurecode ADD BypassGlobalLock tinyint NOT NULL";//Defaults to NeverBypass
				Db.NonQ(command);
			}
			else {//oracle
				command="ALTER TABLE procedurecode ADD BypassGlobalLock number(3)";
				Db.NonQ(command);
				command="UPDATE procedurecode SET BypassGlobalLock = 0 WHERE BypassGlobalLock IS NULL";
				Db.NonQ(command);
				command="ALTER TABLE procedurecode MODIFY BypassGlobalLock NOT NULL";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="ALTER TABLE clinic ADD Specialty bigint NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE clinic ADD INDEX (Specialty)";
				Db.NonQ(command);
			}
			else {//oracle
				command="ALTER TABLE clinic ADD Specialty number(20)";
				Db.NonQ(command);
				command="UPDATE clinic SET Specialty = 0 WHERE Specialty IS NULL";
				Db.NonQ(command);
				command="ALTER TABLE clinic MODIFY Specialty NOT NULL";
				Db.NonQ(command);
				command=@"CREATE INDEX clinic_Specialty ON clinic (Specialty)";
				Db.NonQ(command);
			}
			//Create a new clone preference that will allow clones to be created into their own family and tied to the master via a super family.
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO preference (PrefName,ValueString) VALUES('CloneCreateSuperFamily','0')";
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO preference (PrefNum,PrefName,ValueString) VALUES((SELECT COALESCE(MAX(PrefNum),0)+1 FROM preference),'CloneCreateSuperFamily','0')";
				Db.NonQ(command);
			}
			command="SELECT ValueString FROM preference WHERE PrefName='ShowFeaturePatientClone'";
			table=Db.GetTable(command);
			if(DataConnection.DBtype==DatabaseType.MySql && table.Rows.Count > 0 && PIn.Bool(table.Rows[0]["ValueString"].ToString())) {
				//Get every single potential clone in the database.
				ODEvent.Fire(new ODEventArgs("ConvertDatabases","Upgrading database to version: 17.2.1 - Clones | Finding Potentials"));
				//Our current clone system requires the following fields to be exactly the same: Guarantor,LName,FName, and Birthdate.
				command=@"SELECT Guarantor,LName,FName,Birthdate
					FROM patient
					WHERE PatStatus=0  -- PatientStatus.Patient
					AND YEAR(Birthdate) > 1880
					GROUP BY Guarantor,LName,FName,Birthdate
					HAVING COUNT(*) > 1";
				table=Db.GetTable(command);
				//Now that we have all potential clones from the database, go try and find the corresponding master.
				for(int i=0;i<table.Rows.Count;i++) {
					ODEvent.Fire(new ODEventArgs("ConvertDatabases","Upgrading database to version: 17.2.1 - Clones | Considering "+(i+1)+" / "+table.Rows.Count));
					command="SELECT PatNum,LName,FName,Birthdate "
						+"FROM patient "
						+"WHERE PatStatus=0 "//PatientStatus.Patient
						+"AND LName='"+POut.String(table.Rows[i]["LName"].ToString())+"' "
						+"AND FName='"+POut.String(table.Rows[i]["FName"].ToString())+"' "
						+"AND Birthdate="+POut.Date(PIn.Date(table.Rows[i]["Birthdate"].ToString()));
					DataTable tableClones=Db.GetTable(command);
					//There needs to be at least two patients matching the exact name and birthdate in order to even consider them clones of each other.
					if(tableClones.Rows.Count < 2) {
						continue;
					}
					//At this point we know we have at least 2 patients, we need to find the master or original patient and then link the rest to them.
					//The master patient will have at least one lower case character within the last and first name fields.  Clones will have all caps.
					DataRow rowMaster=tableClones.Select().FirstOrDefault(x => x["LName"].ToString().Any(y => char.IsLower(y))
						&& x["FName"].ToString().Any(y => char.IsLower(y)));
					List<DataRow> listCloneRows=tableClones.Select().ToList().FindAll(x => x["LName"].ToString().All(y => char.IsUpper(y)
						&& x["FName"].ToString().All(z => char.IsUpper(z))));
					if(rowMaster==null || listCloneRows==null || listCloneRows.Count==0) {
						continue;//Either no master was found or no clones were found (this will happen for true duplicate patients).
					}
					//Now we can make patientlink entries that will associate the master or original patient with the corresponding clones.
					long patNumMaster=PIn.Long(rowMaster["PatNum"].ToString());
					foreach(DataRow rowClone in listCloneRows) {
						long patNumClone=PIn.Long(rowClone["PatNum"].ToString());
						if(patNumMaster==patNumClone) {
							continue;//Do not create a link between the master and themself.
						}
						command="INSERT INTO patientlink (PatNumFrom,PatNumTo,LinkType,DateTimeLink) "
							+"VALUES("+POut.Long(patNumMaster)+","
							+POut.Long(patNumClone)+","
							+"2,"//PatientLinkType.Clone
							+"NOW())";
						Db.NonQ(command);
					}
				}
				//Set the progress bar back to the way it was.
				ODEvent.Fire(new ODEventArgs("ConvertDatabases","Upgrading database to version: 17.2.1"));
			}
			//Conceal clearinghouse passwords.
			command="SELECT ClearinghouseNum,Password FROM clearinghouse WHERE Password !='' AND Password IS NOT NULL";//Null check for Oracle.
			table=Db.GetTable(command);
			foreach(DataRow row in table.Rows) {
				string concealPass="";
				CDT.Class1.ConcealClearinghouse(PIn.String(row["Password"].ToString()),out concealPass);
				command="UPDATE clearinghouse SET Password='"+POut.String(concealPass)+"' "
					+"WHERE ClearinghouseNum="+PIn.Long(row["ClearinghouseNum"].ToString());
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command=@"INSERT INTO histappointment (HistUserNum, HistApptAction, ApptSource, AptNum, PatNum, AptStatus, Pattern, Confirmed, TimeLocked, 
					Op, Note, ProvNum, ProvHyg, AptDateTime, NextAptNum, UnschedStatus, IsNewPatient, ProcDescript, Assistant, ClinicNum, IsHygiene, 
					DateTStamp, DateTimeArrived, DateTimeSeated, DateTimeDismissed, InsPlan1, InsPlan2, DateTimeAskedToArrive, ProcsColored, ColorOverride, 
					AppointmentTypeNum, SecUserNumEntry, SecDateEntry)
					SELECT 0,4/*Deleted*/,0/*No eService Type*/, AptNum, PatNum, AptStatus, Pattern, Confirmed, TimeLocked, Op, Note, ProvNum, 
					ProvHyg, AptDateTime, NextAptNum, UnschedStatus, IsNewPatient, ProcDescript, Assistant, ClinicNum, IsHygiene, DateTStamp, DateTimeArrived, 
					DateTimeSeated, DateTimeDismissed, InsPlan1, InsPlan2, DateTimeAskedToArrive, ProcsColored, ColorOverride, AppointmentTypeNum, 
					SecUserNumEntry, SecDateEntry
					FROM appointmentdeleted";
				Db.NonQ(command);
				command="DROP TABLE IF EXISTS appointmentdeleted";
				Db.NonQ(command);
			}
			else {//oracle
				command=@"INSERT INTO histappointment (HistAppointmentNum, HistUserNum, HistApptAction, ApptSource, AptNum, PatNum, AptStatus, Pattern, 
					Confirmed, TimeLocked, Op, Note, ProvNum, ProvHyg, AptDateTime, NextAptNum, UnschedStatus, IsNewPatient, ProcDescript, Assistant, 
					ClinicNum, IsHygiene, DateTStamp, DateTimeArrived, DateTimeSeated, DateTimeDismissed, InsPlan1, InsPlan2, DateTimeAskedToArrive, 
					ProcsColored, ColorOverride, AppointmentTypeNum, SecUserNumEntry, SecDateEntry)
					SELECT (SELECT COALESCE(MAX(HistAppointmentNum),0)+1 FROM histappointment), 0,4/*Deleted*/,0/*No eService Type*/, AptNum, 
					PatNum, AptStatus, Pattern, Confirmed, TimeLocked, Op, Note, ProvNum, ProvHyg, AptDateTime, NextAptNum, UnschedStatus, IsNewPatient, 
					ProcDescript, Assistant, ClinicNum, IsHygiene, DateTStamp, DateTimeArrived, DateTimeSeated, DateTimeDismissed, InsPlan1, InsPlan2, 
					DateTimeAskedToArrive, ProcsColored, ColorOverride, AppointmentTypeNum, SecUserNumEntry, SecDateEntry
					FROM appointmentdeleted";
				Db.NonQ(command);
				command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE appointmentdeleted'; EXCEPTION WHEN OTHERS THEN NULL; END;";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="ALTER TABLE sheet ADD SheetDefNum bigint NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE sheet ADD INDEX (SheetDefNum)";
				Db.NonQ(command);
			}
			else {//oracle
				command="ALTER TABLE sheet ADD SheetDefNum number(20)";
				Db.NonQ(command);
				command="UPDATE sheet SET SheetDefNum = 0 WHERE SheetDefNum IS NULL";
				Db.NonQ(command);
				command="ALTER TABLE sheet MODIFY SheetDefNum NOT NULL";
				Db.NonQ(command);
				command=@"CREATE INDEX sheet_SheetDefNum ON sheet (SheetDefNum)";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="ALTER TABLE sheetdef ADD BypassGlobalLock tinyint NOT NULL";//Defaults to NeverBypass
				Db.NonQ(command);
			}
			else {//oracle
				command="ALTER TABLE sheetdef ADD BypassGlobalLock number(3)";
				Db.NonQ(command);
				command="UPDATE sheetdef SET BypassGlobalLock = 0 WHERE BypassGlobalLock IS NULL";
				Db.NonQ(command);
				command="ALTER TABLE sheetdef MODIFY BypassGlobalLock NOT NULL";
				Db.NonQ(command);
			}
			//Update Payment clinic settings
			command="UPDATE preference SET PrefName='PaymentClinicSetting' WHERE PrefName='PaymentsUsePatientClinic'";
			Db.NonQ(command);
			//Adding domainuserlogin
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="ALTER TABLE userod ADD DomainUser varchar(255) NOT NULL";
				Db.NonQ(command);
			}
			else {//oracle
				command="ALTER TABLE userod ADD DomainUser varchar2(255)";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO preference(PrefName,ValueString) VALUES('DomainLoginEnabled','0')";
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO preference(PrefNum,PrefName,ValueString) VALUES((SELECT COALESCE(MAX(PrefNum),0)+1 FROM preference),'DomainLoginEnabled','0')";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO preference(PrefName,ValueString) VALUES('DomainLoginPath','')";
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO preference(PrefNum,PrefName,ValueString) VALUES((SELECT COALESCE(MAX(PrefNum),0)+1 FROM preference),'DomainLoginPath','')";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="DROP TABLE IF EXISTS smsblockphone";
				Db.NonQ(command);
				command=@"CREATE TABLE smsblockphone (
						SmsBlockPhoneNum bigint NOT NULL auto_increment PRIMARY KEY,
						BlockWirelessNumber varchar(255) NOT NULL
						) DEFAULT CHARSET=utf8";
				Db.NonQ(command);
			}
			else {//oracle
				command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE smsblockphone'; EXCEPTION WHEN OTHERS THEN NULL; END;";
				Db.NonQ(command);
				command=@"CREATE TABLE smsblockphone (
						SmsBlockPhoneNum number(20) NOT NULL,
						BlockWirelessNumber varchar2(255),
						CONSTRAINT smsblockphone_SmsBlockPhoneNum PRIMARY KEY (SmsBlockPhoneNum)
						)";
				Db.NonQ(command);
			}
			//Insert Tigerview bridge program property-----------------------------------------------------------------
			command="SELECT ProgramNum FROM program WHERE ProgName = 'TigerView'";
			long tigerViewProgNum=PIn.Long(Db.GetScalar(command));
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
					+") VALUES("
					+"'"+POut.Long(tigerViewProgNum)+"', "
					+"'Birthdate format (default MM/dd/yy)', "
					+"'MM/dd/yy')";
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO programproperty (ProgramPropertyNum,ProgramNum,PropertyDesc,PropertyValue,ClinicNum"
					+") VALUES("
					+"(SELECT COALESCE(MAX(ProgramPropertyNum),0)+1 FROM programproperty),"
					+"'"+POut.Long(tigerViewProgNum)+"', "
					+"'Birthdate format (default MM/dd/yy)', "
					+"'MM/dd/yy', "
					+"'0')";
				Db.NonQ(command);
			}//End Tigerview
			//Giving OrthoChartEditUser permission to everybody that has OrthoChartEditFull permission -----------------------------------------------------
			command="SELECT DISTINCT UserGroupNum FROM grouppermission WHERE PermType=79";//OrthoChartEditFull permission
			table=Db.GetTable(command);
			if(DataConnection.DBtype==DatabaseType.MySql) {
				foreach(DataRow row in table.Rows) {
					groupNum=PIn.Long(row["UserGroupNum"].ToString());
					command="INSERT INTO grouppermission (UserGroupNum,PermType) "
						 +"VALUES("+POut.Long(groupNum)+",145)"; //OrthoChartEditUser permission
					Db.NonQ(command);
				}
			}
			else {//oracle
				foreach(DataRow row in table.Rows) {
					groupNum=PIn.Long(row["UserGroupNum"].ToString());
					command="INSERT INTO grouppermission (GroupPermNum,NewerDays,UserGroupNum,PermType) "
						 +"VALUES((SELECT MAX(GroupPermNum)+1 FROM grouppermission),0,"+POut.Long(groupNum)+",145)"; //OrthoChartEditUser permission
					Db.NonQ(command);
				}
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="ALTER TABLE orthochart ADD UserNum bigint NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE orthochart ADD INDEX (UserNum)";
				Db.NonQ(command);
			}
			else {//oracle
				command="ALTER TABLE orthochart ADD UserNum number(20)";
				Db.NonQ(command);
				command="UPDATE orthochart SET UserNum = 0 WHERE UserNum IS NULL";
				Db.NonQ(command);
				command="ALTER TABLE orthochart MODIFY UserNum NOT NULL";
				Db.NonQ(command);
				command=@"CREATE INDEX orthochart_UserNum ON orthochart (UserNum)";
				Db.NonQ(command);
			}
			//Giving ProcedureNoteUser permission to everybody that has ProcedureNoteFull permission -------------------------------------------------------
			command="SELECT DISTINCT UserGroupNum FROM grouppermission WHERE PermType=53";//ProcedureNoteFull permission
			table=Db.GetTable(command);
			if(DataConnection.DBtype==DatabaseType.MySql) {
				foreach(DataRow row in table.Rows) {
					groupNum=PIn.Long(row["UserGroupNum"].ToString());
					command="INSERT INTO grouppermission (UserGroupNum,PermType) "
						 +"VALUES("+POut.Long(groupNum)+",146)"; //ProcedureNoteUser permission
					Db.NonQ(command);
				}
			}
			else {//oracle
				foreach(DataRow row in table.Rows) {
					groupNum=PIn.Long(row["UserGroupNum"].ToString());
					command="INSERT INTO grouppermission (GroupPermNum,NewerDays,UserGroupNum,PermType) "
						 +"VALUES((SELECT MAX(GroupPermNum)+1 FROM grouppermission),0,"+POut.Long(groupNum)+",146)"; //ProcedureNoteUser permission
					Db.NonQ(command);
				}
			}
			//Giving GroupNoteEditSigned permission to everybody -------------------------------------------------------------------------------------------------
			command="SELECT DISTINCT UserGroupNum FROM grouppermission";
			table=Db.GetTable(command);
			if(DataConnection.DBtype==DatabaseType.MySql) {
				foreach(DataRow row in table.Rows) {
					groupNum=PIn.Long(row["UserGroupNum"].ToString());
					command="INSERT INTO grouppermission (UserGroupNum,PermType) "
						 +"VALUES("+POut.Long(groupNum)+",147)"; //GroupNoteEditSigned permission
					Db.NonQ(command);
				}
			}
			else {//oracle
				foreach(DataRow row in table.Rows) {
					groupNum=PIn.Long(row["UserGroupNum"].ToString());
					command="INSERT INTO grouppermission (GroupPermNum,NewerDays,UserGroupNum,PermType) "
						 +"VALUES((SELECT MAX(GroupPermNum)+1 FROM grouppermission),0,"+POut.Long(groupNum)+",147)"; //GroupNoteEditSigned permission
					Db.NonQ(command);
				}
			}
			#region Transworld Systems bridge
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO program (ProgName,ProgDesc,Enabled,Path,CommandLine,Note) "
					+"VALUES ("
					+"'Transworld',"
					+"'Transworld Systems Inc (TSI) from www.tsico.com',"
					+"'0',"
					+"'',"
					+"'',"//leave blank if none
					+"'No program path or arguments. Usernames, passwords, client IDs, and SFTP connection details are supplied by TSI.')";
				long programNum=Db.NonQ(command,true);
				command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue,ComputerName,ClinicNum) "
					+"VALUES ("
					+POut.Long(programNum)+","
					+"'SftpServerAddress',"
					+"'',"
					+"'',"
					+"0)";
				Db.NonQ(command);
				command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue,ComputerName,ClinicNum) "
					+"VALUES ("
					+POut.Long(programNum)+","
					+"'SftpServerPort',"
					+"'',"
					+"'',"
					+"0)";
				Db.NonQ(command);
				command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue,ComputerName,ClinicNum) "
					+"VALUES ("
					+POut.Long(programNum)+","
					+"'SftpUsername',"
					+"'',"
					+"'',"
					+"0)";
				Db.NonQ(command);
				command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue,ComputerName,ClinicNum) "
					+"VALUES ("
					+POut.Long(programNum)+","
					+"'SftpPassword',"
					+"'',"
					+"'',"
					+"0)";
				Db.NonQ(command);
				command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue,ComputerName,ClinicNum) "
					+"VALUES ("
					+POut.Long(programNum)+","
					+"'ClientIdAccelerator',"
					+"'',"
					+"'',"
					+"0)";
				Db.NonQ(command);
				command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue,ComputerName,ClinicNum) "
					+"VALUES ("
					+POut.Long(programNum)+","
					+"'ClientIdCollection',"
					+"'',"
					+"'',"
					+"0)";
				Db.NonQ(command);
				command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue,ComputerName,ClinicNum) "
					+"VALUES ("
					+POut.Long(programNum)+","
					+"'IsThankYouLetterEnabled',"
					+"'0',"
					+"'',"
					+"0)";
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO program (ProgramNum,ProgName,ProgDesc,Enabled,Path,CommandLine,Note) "
					+"VALUES ("
					+"(SELECT COALESCE(MAX(ProgramNum),0)+1 FROM program),"
					+"'Transworld',"
					+"'Transworld Systems Inc (TSI) from www.tsico.com',"
					+"'0',"
					+"'',"
					+"'',"
					+"'No program path or arguments. Usernames, passwords, client IDs, and SFTP connection details are supplied by TSI.')";
				long programNum=Db.NonQ(command,true);
				command="INSERT INTO programproperty (ProgramPropertyNum,ProgramNum,PropertyDesc,PropertyValue,ComputerName,ClinicNum) "
					+"VALUES ("
					+"(SELECT COALESCE(MAX(ProgramPropertyNum),0)+1 FROM programproperty),"
					+POut.Long(programNum)+","
					+"'SftpServerAddress',"
					+"'',"
					+"'',"
					+"0)";
				Db.NonQ(command);
				command="INSERT INTO programproperty (ProgramPropertyNum,ProgramNum,PropertyDesc,PropertyValue,ComputerName,ClinicNum) "
					+"VALUES ("
					+"(SELECT COALESCE(MAX(ProgramPropertyNum),0)+1 FROM programproperty),"
					+POut.Long(programNum)+","
					+"'SftpServerPort',"
					+"'',"
					+"'',"
					+"0)";
				Db.NonQ(command);
				command="INSERT INTO programproperty (ProgramPropertyNum,ProgramNum,PropertyDesc,PropertyValue,ComputerName,ClinicNum) "
					+"VALUES ("
					+"(SELECT COALESCE(MAX(ProgramPropertyNum),0)+1 FROM programproperty),"
					+POut.Long(programNum)+","
					+"'SftpUsername',"
					+"'',"
					+"'',"
					+"0)";
				Db.NonQ(command);
				command="INSERT INTO programproperty (ProgramPropertyNum,ProgramNum,PropertyDesc,PropertyValue,ComputerName,ClinicNum) "
					+"VALUES ("
					+"(SELECT COALESCE(MAX(ProgramPropertyNum),0)+1 FROM programproperty),"
					+POut.Long(programNum)+","
					+"'SftpPassword',"
					+"'',"
					+"'',"
					+"0)";
				Db.NonQ(command);
				command="INSERT INTO programproperty (ProgramPropertyNum,ProgramNum,PropertyDesc,PropertyValue,ComputerName,ClinicNum) "
					+"VALUES ("
					+"(SELECT COALESCE(MAX(ProgramPropertyNum),0)+1 FROM programproperty),"
					+POut.Long(programNum)+","
					+"'ClientIdAccelerator',"
					+"'',"
					+"'',"
					+"0)";
				Db.NonQ(command);
				command="INSERT INTO programproperty (ProgramPropertyNum,ProgramNum,PropertyDesc,PropertyValue,ComputerName,ClinicNum) "
					+"VALUES ("
					+"(SELECT COALESCE(MAX(ProgramPropertyNum),0)+1 FROM programproperty),"
					+POut.Long(programNum)+","
					+"'ClientIdCollection',"
					+"'',"
					+"'',"
					+"0)";
				Db.NonQ(command);
				command="INSERT INTO programproperty (ProgramPropertyNum,ProgramNum,PropertyDesc,PropertyValue,ComputerName,ClinicNum) "
					+"VALUES ("
					+"(SELECT COALESCE(MAX(ProgramPropertyNum),0)+1 FROM programproperty),"
					+POut.Long(programNum)+","
					+"'IsThankYouLetterEnabled',"
					+"'0',"
					+"'',"
					+"0)";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="DROP TABLE IF EXISTS tsitranslog";
				Db.NonQ(command);
				command=@"CREATE TABLE tsitranslog (
					TsiTransLogNum bigint NOT NULL auto_increment PRIMARY KEY,
					PatNum bigint NOT NULL,
					UserNum bigint NOT NULL,
					TransType tinyint NOT NULL,
					TransDateTime datetime NOT NULL DEFAULT '0001-01-01 00:00:00',
					DemandType tinyint NOT NULL,
					ServiceCode tinyint NOT NULL,
					TransAmt double NOT NULL,
					AccountBalance double NOT NULL,
					FKeyType tinyint NOT NULL,
					FKey bigint NOT NULL,
					RawMsgText varchar(1000) NOT NULL,
					INDEX(PatNum),
					INDEX(UserNum),
					INDEX FKeyAndType (FKey,FKeyType)
					) DEFAULT CHARSET=utf8";
				Db.NonQ(command);
			}
			else {//oracle
				command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE tsitranslog'; EXCEPTION WHEN OTHERS THEN NULL; END;";
				Db.NonQ(command);
				command=@"CREATE TABLE tsitranslog (
					TsiTransLogNum number(20) NOT NULL,
					PatNum number(20) NOT NULL,
					UserNum number(20) NOT NULL,
					TransType number(3) NOT NULL,
					TransDateTime date DEFAULT TO_DATE('0001-01-01','YYYY-MM-DD') NOT NULL,
					DemandType number(3) NOT NULL,
					ServiceCode number(3) NOT NULL,
					TransAmt number(38,8) NOT NULL,
					AccountBalance number(38,8) NOT NULL,
					FKeyType number(3) NOT NULL,
					FKey number(20) NOT NULL,
					RawMsgText varchar2(1000),
					CONSTRAINT tsitranslog_TsiTransLogNum PRIMARY KEY (TsiTransLogNum)
					)";
				Db.NonQ(command);
				command=@"CREATE INDEX tsitranslog_PatNum ON tsitranslog (PatNum)";
				Db.NonQ(command);
				command=@"CREATE INDEX tsitranslog_UserNum ON tsitranslog (UserNum)";
				Db.NonQ(command);
				command=@"CREATE INDEX tsitranslog_FKeyAndType ON tsitranslog (FKey,FKeyType)";
				Db.NonQ(command);
			}
			#endregion Transworld Systems bridge
		}//end function

		private static void To17_2_2() {
			string command;
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="ALTER TABLE refattach ADD DateTStamp timestamp";
				Db.NonQ(command);
				command="UPDATE refattach SET DateTStamp = NOW()";
				Db.NonQ(command);
			}
			else {//oracle
				command="ALTER TABLE refattach ADD DateTStamp timestamp";
				Db.NonQ(command);
				command="UPDATE refattach SET DateTStamp = SYSTIMESTAMP";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="ALTER TABLE referral ADD DateTStamp timestamp";
				Db.NonQ(command);
				command="UPDATE referral SET DateTStamp = NOW()";
				Db.NonQ(command);
			}
			else {//oracle
				command="ALTER TABLE referral ADD DateTStamp timestamp";
				Db.NonQ(command);
				command="UPDATE referral SET DateTStamp = SYSTIMESTAMP";
				Db.NonQ(command);
			}
		}

		private static void To17_2_4() {
			string command;
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO preference(PrefName,ValueString) VALUES('WebSchedTextsPerBatch','25')";
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO preference(PrefNum,PrefName,ValueString) VALUES((SELECT COALESCE(MAX(PrefNum),0)+1 FROM preference),'WebSchedTextsPerBatch','25')";
				Db.NonQ(command);
			}
		}

		private static void To17_2_5() {
			string command;
			//Tigerview bridge program property-----------------------------------------------------------------
			command="SELECT ProgramNum FROM program WHERE ProgName = 'TigerView'";
			long tigerViewProgNum=PIn.Long(Db.GetScalar(command));
			//There was a bug with an old 17.2.1 script where a new program property for TigerView would not associate to TigerView correctly.
			//Even though it isn't necessary, we should try and clean up after ourselves by deleting all properties with a -1 program num.
			command="DELETE FROM programproperty WHERE ProgramNum=-1";
			//The 17.2.1 script was corrected in 17.2.5 so we now want to see if the property exists first.
			command="SELECT COUNT(*) "+
				"FROM programproperty "+
				"WHERE ProgramNum="+POut.Long(tigerViewProgNum)+" "+
				"AND PropertyDesc='Birthdate format (default MM/dd/yy)' "+
				"AND PropertyValue='MM/dd/yy'";
			if(Db.GetScalar(command)=="0") {
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
						+") VALUES("
						+"'"+POut.Long(tigerViewProgNum)+"', "
						+"'Birthdate format (default MM/dd/yy)', "
						+"'MM/dd/yy')";
					Db.NonQ(command);
				}
				else {//oracle
					command="INSERT INTO programproperty (ProgramPropertyNum,ProgramNum,PropertyDesc,PropertyValue,ClinicNum"
						+") VALUES("
						+"(SELECT COALESCE(MAX(ProgramPropertyNum),0)+1 FROM programproperty),"
						+"'"+POut.Long(tigerViewProgNum)+"', "
						+"'Birthdate format (default MM/dd/yy)', "
						+"'MM/dd/yy', "
						+"'0')";
					Db.NonQ(command);
				}
			}//End Tigerview
		}

		private static void To17_2_6() {
			string command;
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO preference (PrefName,ValueString) VALUES('WebSchedNewPatAllowChildren','0')";
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO preference (PrefNum,PrefName,ValueString) VALUES((SELECT COALESCE(MAX(PrefNum),0)+1 FROM preference),'WebSchedNewPatAllowChildren','0')";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO preference (PrefName,ValueString) VALUES('UnschedDaysFuture','0')";
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO preference (PrefNum,PrefName,ValueString) VALUES((SELECT COALESCE(MAX(PrefNum),0)+1 FROM preference),'UnschedDaysFuture','0')";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO preference (PrefName,ValueString) VALUES('UnschedDaysPast','365')";//Default ot 1 year.
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO preference (PrefNum,PrefName,ValueString) VALUES((SELECT COALESCE(MAX(PrefNum),0)+1 FROM preference),'UnschedDaysPast','365')";//Default ot 1 year.
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="ALTER TABLE clearinghouse ADD IsClaimExportAllowed tinyint NOT NULL DEFAULT 1";
				Db.NonQ(command);
			}
			else {//oracle
				command="ALTER TABLE clearinghouse ADD IsClaimExportAllowed number(3) DEFAULT 1";
				Db.NonQ(command);
				command="UPDATE clearinghouse SET IsClaimExportAllowed = 0 WHERE IsClaimExportAllowed IS NULL";
				Db.NonQ(command);
				command="ALTER TABLE clearinghouse MODIFY IsClaimExportAllowed NOT NULL";
				Db.NonQ(command);
			}
		}

		private static void To17_2_8() {
			string command;
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="DROP TABLE IF EXISTS etrans835attach";
				Db.NonQ(command);
				command=@"CREATE TABLE etrans835attach (
						Etrans835AttachNum bigint NOT NULL auto_increment PRIMARY KEY,
						EtransNum bigint NOT NULL,
						ClaimNum bigint NOT NULL,
						ClpSegmentIndex int NOT NULL,
						INDEX(EtransNum),
						INDEX(ClaimNum)
						) DEFAULT CHARSET=utf8";
				Db.NonQ(command);
			}
			else {//oracle
				command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE etrans835attach'; EXCEPTION WHEN OTHERS THEN NULL; END;";
				Db.NonQ(command);
				command=@"CREATE TABLE etrans835attach (
						Etrans835AttachNum number(20) NOT NULL,
						EtransNum number(20) NOT NULL,
						ClaimNum number(20) NOT NULL,
						ClpSegmentIndex number(11) NOT NULL,
						CONSTRAINT etrans835attach_Etrans835Attac PRIMARY KEY (Etrans835AttachNum)
						)";
				Db.NonQ(command);
				command=@"CREATE INDEX etrans835attach_EtransNum ON etrans835attach (EtransNum)";
				Db.NonQ(command);
				command=@"CREATE INDEX etrans835attach_ClaimNum ON etrans835attach (ClaimNum)";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO preference (PrefName,ValueString) VALUES('PlannedApptDaysFuture','0')";
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO preference (PrefNum,PrefName,ValueString) VALUES((SELECT COALESCE(MAX(PrefNum),0)+1 FROM preference),'PlannedApptDaysFuture','0')";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="INSERT INTO preference (PrefName,ValueString) VALUES('PlannedApptDaysPast','365')";//Default ot 1 year.
				Db.NonQ(command);
			}
			else {//oracle
				command="INSERT INTO preference (PrefNum,PrefName,ValueString) VALUES((SELECT COALESCE(MAX(PrefNum),0)+1 FROM preference),'PlannedApptDaysPast','365')";//Default ot 1 year.
				Db.NonQ(command);
			}
		}

		private static void To17_2_13() {
			string command;
			command="SELECT COUNT(*) FROM preference WHERE PrefName='PlannedApptDaysFuture'";
			if(Db.GetScalar(command)=="0") {//Was suppose to be in 17.2.12 but was put in 17.2.8, so fixing in 17.2.13
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="INSERT INTO preference (PrefName,ValueString) VALUES('PlannedApptDaysFuture','0')";
					Db.NonQ(command);
				}
				else {//oracle
					command="INSERT INTO preference (PrefNum,PrefName,ValueString) VALUES((SELECT COALESCE(MAX(PrefNum),0)+1 FROM preference),'PlannedApptDaysFuture','0')";
					Db.NonQ(command);
				}
			}
			command="SELECT COUNT(*) FROM preference WHERE PrefName='PlannedApptDaysPast'";
			if(Db.GetScalar(command)=="0") {//Was suppose to be in 17.2.12 but was put in 17.2.8, so fixing in 17.2.13
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="INSERT INTO preference (PrefName,ValueString) VALUES('PlannedApptDaysPast','365')";//Default ot 1 year.
					Db.NonQ(command);
				}
				else {//oracle
					command="INSERT INTO preference (PrefNum,PrefName,ValueString) VALUES((SELECT COALESCE(MAX(PrefNum),0)+1 FROM preference),'PlannedApptDaysPast','365')";//Default ot 1 year.
					Db.NonQ(command);
				}
			}
		}

		private static void To17_2_23() {
			string command="";
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="ALTER TABLE task ADD DateTimeOriginal datetime NOT NULL DEFAULT '0001-01-01 00:00:00'";
				Db.NonQ(command);
			}
			else {//oracle
				command="ALTER TABLE task ADD DateTimeOriginal date";
				Db.NonQ(command);
				command="UPDATE task SET DateTimeOriginal = TO_DATE('0001-01-01','YYYY-MM-DD') WHERE DateTimeOriginal IS NULL";
				Db.NonQ(command);
				command="ALTER TABLE task MODIFY DateTimeOriginal NOT NULL";
				Db.NonQ(command);
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="ALTER TABLE taskhist ADD DateTimeOriginal datetime NOT NULL DEFAULT '0001-01-01 00:00:00'";
				Db.NonQ(command);
			}
			else {//oracle
				command="ALTER TABLE taskhist ADD DateTimeOriginal date";
				Db.NonQ(command);
				command="UPDATE taskhist SET DateTimeOriginal = TO_DATE('0001-01-01','YYYY-MM-DD') WHERE DateTimeOriginal IS NULL";
				Db.NonQ(command);
				command="ALTER TABLE taskhist MODIFY DateTimeOriginal NOT NULL";
				Db.NonQ(command);
			}
		}

		private static void To17_2_32() {
			string command="SELECT ValueString FROM preference WHERE PrefName='WebSchedAutomaticSendTextSetting'";
			bool isAutoRecallTextingEnabled=(Db.GetScalar(command)=="4");//4 is WebSchedAutomaticSend.SendToText
			command="SELECT ValueString FROM preference WHERE PrefName='WebSchedTextsPerBatch'";
			int textsPerBatch=PIn.Int(Db.GetScalar(command));
			if(!isAutoRecallTextingEnabled && textsPerBatch==25) {
				//We only want to change the texts per batch if it is not currently enabled and they have the default number of 25.
				//25 texts every 10 minutes was causing offices to reach their limit of 675 texts per 24 hours.
				//2 was chosen so that we do not buy them another phone number. If we send 2 texts every 10 minutes, we would send 180 texts in 15 hours
				//which is the default automatic send window.
				command="UPDATE preference SET ValueString='2' WHERE PrefName='WebSchedTextsPerBatch'";
				Db.NonQ(command);
			}
			//Moving codes to the Obsolete category that were deleted in CDT 2018.
			if(CultureInfo.CurrentCulture.Name.EndsWith("US")) {//United States
				//Move deprecated codes to the Obsolete procedure code category.
				//Make sure the procedure code category exists before moving the procedure codes.
				string procCatDescript="Obsolete";
				long defNum=0;
				command="SELECT DefNum FROM definition WHERE Category=11 AND ItemName='"+POut.String(procCatDescript)+"'";//11 is DefCat.ProcCodeCats
				DataTable dtDef=Db.GetTable(command);
				if(dtDef.Rows.Count==0) { //The procedure code category does not exist, add it
					command="SELECT COUNT(*) FROM definition WHERE Category=11";//11 is DefCat.ProcCodeCats
					int countCats=PIn.Int(Db.GetCount(command));
					if(DataConnection.DBtype==DatabaseType.MySql) {
						command="INSERT INTO definition (Category,ItemName,ItemOrder) "
								+"VALUES (11"+",'"+POut.String(procCatDescript)+"',"+POut.Int(countCats)+")";//11 is DefCat.ProcCodeCats
					}
					else {//oracle
						command="INSERT INTO definition (DefNum,Category,ItemName,ItemOrder) "
								+"VALUES ((SELECT MAX(DefNum)+1 FROM definition),11,'"+POut.String(procCatDescript)+"',"+POut.Int(countCats)+")";//11 is DefCat.ProcCodeCats
					}
					defNum=Db.NonQ(command,true);
				}
				else { //The procedure code category already exists, get the existing defnum
					defNum=PIn.Long(dtDef.Rows[0]["DefNum"].ToString());
				}
				string[] cdtCodesDeleted=new string[] {
					"D5510","D5610","D5620"
				};
				//Change the procedure codes' category to Obsolete.
				command="UPDATE procedurecode SET ProcCat="+POut.Long(defNum)
					+" WHERE ProcCode IN('"+string.Join("','",cdtCodesDeleted.Select(x => POut.String(x)))+"') ";
				Db.NonQ(command);
			}//end United States CDT codes update
		}

		private static void To17_2_41() {
			string command="SELECT ValueString FROM preference WHERE PrefName='ShowFeaturePatientClone'";
			DataTable table=Db.GetTable(command);
			if(DataConnection.DBtype==DatabaseType.MySql && table.Rows.Count > 0 && PIn.Bool(table.Rows[0]["ValueString"].ToString())) {
				//Get every single potential clone in the database.
				ODEvent.Fire(new ODEventArgs("ConvertDatabases","Upgrading database to version: 17.2.41 - Clones | Finding Potentials"));
				//Our current clone system requires the following fields to be exactly the same: Guarantor,LName,FName, and Birthdate.
				//We have to join on the patientlink table because users could have manually associated patients to their clones / added new clones entirely.
				//The previous time that we tried to convert old clones into the new system did not account for non-alphabetic characters (symbols) in names.
				command=@"SELECT Guarantor,LName,FName,Birthdate
					FROM patient
					LEFT JOIN patientlink patientlinkfrom ON patient.PatNum=patientlinkfrom.PatNumFrom AND patientlinkfrom.LinkType=2
					LEFT JOIN patientlink patientlinkto ON patient.PatNum=patientlinkto.PatNumTo AND patientlinkto.LinkType=2
					WHERE PatStatus=0  -- PatientStatus.Patient
					AND YEAR(Birthdate) > 1880
					AND patientlinkfrom.PatNumFrom IS NULL
					AND patientlinkto.PatNumTo IS NULL
					GROUP BY Guarantor,LName,FName,Birthdate
					HAVING COUNT(*) > 1";
				table=Db.GetTable(command);
				//Now that we have all potential clones from the database, go try and find the corresponding master.
				for(int i=0;i<table.Rows.Count;i++) {
					ODEvent.Fire(new ODEventArgs("ConvertDatabases","Upgrading database to version: 17.2.41 - Clones | Considering "+(i+1)+" / "+table.Rows.Count));
					command="SELECT PatNum,LName,FName,Birthdate "
						+"FROM patient "
						+"WHERE PatStatus=0 "//PatientStatus.Patient
						+"AND LName='"+POut.String(table.Rows[i]["LName"].ToString())+"' "
						+"AND FName='"+POut.String(table.Rows[i]["FName"].ToString())+"' "
						+"AND Birthdate="+POut.Date(PIn.Date(table.Rows[i]["Birthdate"].ToString()));
					DataTable tableClones=Db.GetTable(command);
					//There needs to be at least two patients matching the exact name and birthdate in order to even consider them clones of each other.
					if(tableClones.Rows.Count < 2) {
						continue;
					}
					//At this point we know we have at least 2 patients, we need to find the master or original patient and then link the rest to them.
					//The master patient will have at least one lower case character within the last and first name fields.  Clones will have all caps.
					//We have to ignore any non-alphabetic characters (like symbols and spaces).
					DataRow rowMaster=tableClones.Select().FirstOrDefault(x => x["LName"].ToString().Any(y => char.IsLower(y))
						&& x["FName"].ToString().Any(y => char.IsLower(y)));
					List<DataRow> listCloneRows=tableClones.Select().ToList().FindAll(x => x["LName"].ToString().Where(Char.IsLetter).All(y => char.IsUpper(y)
						&& x["FName"].ToString().Where(Char.IsLetter).All(z => char.IsUpper(z))));
					if(rowMaster==null || listCloneRows==null || listCloneRows.Count==0) {
						continue;//Either no master was found or no clones were found (this will happen for true duplicate patients).
					}
					//Now we can make patientlink entries that will associate the master or original patient with the corresponding clones.
					long patNumMaster=PIn.Long(rowMaster["PatNum"].ToString());
					foreach(DataRow rowClone in listCloneRows) {
						long patNumClone=PIn.Long(rowClone["PatNum"].ToString());
						if(patNumMaster==patNumClone) {
							continue;//Do not create a link between the master and themself.
						}
						command="INSERT INTO patientlink (PatNumFrom,PatNumTo,LinkType,DateTimeLink) "
							+"VALUES("+POut.Long(patNumMaster)+","
							+POut.Long(patNumClone)+","
							+"2,"//PatientLinkType.Clone
							+"NOW())";
						Db.NonQ(command);
					}
				}
				//Set the progress bar back to the way it was.
				ODEvent.Fire(new ODEventArgs("ConvertDatabases","Upgrading database to version: 17.2.41"));
			}
		}

	}
}