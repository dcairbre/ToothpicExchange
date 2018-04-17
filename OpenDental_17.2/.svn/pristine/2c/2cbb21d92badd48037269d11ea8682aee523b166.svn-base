using CodeBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Linq;

namespace OpenDentBusiness {
	///<summary></summary>
	public class InsEditLogs {
		///<summary>Automatic log entry. Fills in table and column names based on items passed in. 
		///Compares whole table excluding CrudColumnSpecialTypes of DateEntry, DateTEntry, ExcludeFromUpdate, and TimeStamp.
		///Pass in null for ItemOld if the item was just inserted. Pass in null for ItemCur if the item was just deleted.
		///Both itemCur and itemOld cannot be null.</summary>
		public static void MakeLogEntry<T>(T itemCur,T itemOld, InsEditLogType logType, long userNumCur) {
			//No need to check RemotingRole; no call to db.
			T priKeyItem = itemCur==null ? itemOld : itemCur;
			FieldInfo priKeyField = priKeyItem.GetType().GetFields().Where(x => x.IsDefined(typeof(CrudColumnAttribute))
			 && ((CrudColumnAttribute)x.GetCustomAttribute(typeof(CrudColumnAttribute))).IsPriKey).First();
			long priKey = (long) priKeyField.GetValue(priKeyItem);
			string priKeyColName = priKeyField.Name;
			long parentKey = priKeyItem.GetType() == typeof(Benefit) ? ((Benefit)(object)priKeyItem).PlanNum : 0; //parentKey only filled for Benefits.
			if(itemOld == null) { //new, just inserted. Show PriKey Column only.
				InsEditLog logCur = new InsEditLog() {
					FieldName = priKeyColName,
					UserNum = userNumCur,
					OldValue = "NEW",
					NewValue = priKey.ToString(),
					LogType = logType,
					FKey = priKey,
					ParentKey = parentKey,
				};
				Insert(logCur);
				return;
			}
			foreach(FieldInfo prop in priKeyItem.GetType().GetFields()) {
				if(prop.IsDefined(typeof(CrudColumnAttribute))
				&& (((CrudColumnAttribute)prop.GetCustomAttribute(typeof(CrudColumnAttribute))).SpecialType == CrudSpecialColType.DateEntry
				 || ((CrudColumnAttribute)prop.GetCustomAttribute(typeof(CrudColumnAttribute))).SpecialType == CrudSpecialColType.DateTEntry
				 || ((CrudColumnAttribute)prop.GetCustomAttribute(typeof(CrudColumnAttribute))).SpecialType == CrudSpecialColType.ExcludeFromUpdate
				 || ((CrudColumnAttribute)prop.GetCustomAttribute(typeof(CrudColumnAttribute))).SpecialType == CrudSpecialColType.TimeStamp)) 
				{
					continue; //skip logs that are not user editable.
				}
				object valOld = prop.GetValue(itemOld)??"";
				if(itemCur==null) { //deleted, show all deleted columns
					InsEditLog logCur = new InsEditLog() {
						FieldName = prop.Name,
						UserNum = userNumCur,
						OldValue = valOld.ToString(),
						NewValue = "DELETED",
						LogType = logType,
						FKey = priKey,
						ParentKey = parentKey,
					};
					Insert(logCur);
				}
				else { //updated, just show changes.
					object valCur = prop.GetValue(itemCur)??"";
					if(valCur.ToString() == valOld.ToString()) {
						continue;
					}
					InsEditLog logCur = new InsEditLog() {
						FieldName = prop.Name,
						UserNum = userNumCur,
						OldValue = valOld.ToString(),
						NewValue = valCur.ToString(),
						LogType = logType,
						FKey = priKey,
						ParentKey = parentKey,
					};
					Insert(logCur);
				}
			}
		}

		private static long Insert(InsEditLog logCur) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				logCur.InsEditLogNum=Meth.GetLong(MethodBase.GetCurrentMethod(),logCur);
				return logCur.InsEditLogNum;
			}
			return Crud.InsEditLogCrud.Insert(logCur);
		}

		///<summary>Manual log entry. Creates a new InsEditLog based on information passed in. PKey should be 0 unless LogType = Benefit. 
		///Use the automatic MakeLogEntry overload if possible. This only be used when manual UPDATE/INSERT/DELETE queries are run on the logged tables.</summary>
		public static void MakeLogEntry(string fieldName, long userNum, string oldVal, string newVal, InsEditLogType logType, long fKey, long pKey) {
			InsEditLog logCur = new InsEditLog() {
				FieldName = fieldName,
				UserNum = userNum,
				OldValue = oldVal,
				NewValue = newVal,
				LogType = logType,
				FKey = fKey,
				ParentKey = pKey,
			};
			Insert(logCur);
		}

		public static void DeletePreInsertedLogsForPlanNum(long planNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),planNum);
				return;
			}
			string command = "DELETE FROM inseditlog "
				+"WHERE (LogType = " + POut.Int((int)InsEditLogType.Benefit)+" "
				+"AND ParentKey = " + POut.Long(planNum) +") "
				+"OR (LogType = " + POut.Int((int)InsEditLogType.InsPlan)+" "
				+"AND FKey = " + POut.Long(planNum) +")";
			Db.NonQ(command);
		}


	}
}