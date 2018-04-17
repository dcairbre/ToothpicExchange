using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class InsFilingCodes{
		#region Get Methods
		#endregion

		#region Modification Methods
		
		#region Insert
		#endregion

		#region Update
		#endregion

		#region Delete
		#endregion

		#endregion

		#region Misc Methods
		#endregion


		///<summary></summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string c="SELECT * FROM insfilingcode ORDER BY ItemOrder";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),c);
			table.TableName="InsFilingCode";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			InsFilingCodeC.Listt=Crud.InsFilingCodeCrud.TableToList(table);
		}

		public static string GetEclaimCode(long insFilingCodeNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<InsFilingCodeC.Listt.Count;i++) {
				if(InsFilingCodeC.Listt[i].InsFilingCodeNum != insFilingCodeNum) {
					continue;
				}
				return InsFilingCodeC.Listt[i].EclaimCode;
			}
			return "CI";
		}

		///<summary>Gets the InsFilingCode for the specified eclaimCode, or creates one if the eclaimCodes does not exist.</summary>
		public static InsFilingCode GetOrInsertForEclaimCode(string descript,string eclaimCode) {
			//No need to check RemotingRole; no call to db.
			int itemOrderMax=0;
			for(int i=0;i<InsFilingCodeC.Listt.Count;i++) {
				if(InsFilingCodeC.Listt[i].ItemOrder > itemOrderMax) {
					itemOrderMax=InsFilingCodeC.Listt[i].ItemOrder;
				}
				if(InsFilingCodeC.Listt[i].EclaimCode != eclaimCode) {
					continue;
				}
				return InsFilingCodeC.Listt[i];
			}
			InsFilingCode insFilingCode=new InsFilingCode();
			insFilingCode.Descript=descript;
			insFilingCode.EclaimCode=eclaimCode;
			insFilingCode.ItemOrder=(itemOrderMax+1);
			Insert(insFilingCode);
			return insFilingCode;
		}

		public static List<InsFilingCode> GetAll() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<InsFilingCode>>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT * FROM insfilingcode ORDER BY ItemOrder";
			return Crud.InsFilingCodeCrud.SelectMany(command);
		}

		///<summary></summary>
		public static long Insert(InsFilingCode insFilingCode) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				insFilingCode.InsFilingCodeNum=Meth.GetLong(MethodBase.GetCurrentMethod(),insFilingCode);
				return insFilingCode.InsFilingCodeNum;
			}
			return Crud.InsFilingCodeCrud.Insert(insFilingCode);
		}

		///<summary></summary>
		public static void Update(InsFilingCode insFilingCode) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),insFilingCode);
				return;
			}
			Crud.InsFilingCodeCrud.Update(insFilingCode);
		}

		///<summary>Surround with try/catch</summary>
		public static void Delete(long insFilingCodeNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),insFilingCodeNum);
				return;
			}
			string command="SELECT COUNT(*) FROM insplan WHERE FilingCode="+POut.Long(insFilingCodeNum);
			if(Db.GetScalar(command) != "0") {
				throw new ApplicationException(Lans.g("InsFilingCode","Already in use by insplans."));
			}
			Crud.InsFilingCodeCrud.Delete(insFilingCodeNum);
		}


	}
}