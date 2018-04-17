using System;
using System.Collections;
using System.Data;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace OpenDentBusiness{
	///<summary></summary>
	public class ClaimFormItems {
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


		///<summary>Gets all claimformitems for all claimforms.  Items for individual claimforms can later be extracted as needed.</summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command=
				"SELECT * FROM claimformitem ORDER BY imagefilename desc";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="ClaimFormItem";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			ClaimFormItemC.Listt=Crud.ClaimFormItemCrud.TableToList(table).ToArray();
		}

		///<summary></summary>
		public static long Insert(ClaimFormItem item) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				item.ClaimFormItemNum=Meth.GetLong(MethodBase.GetCurrentMethod(),item);
				return item.ClaimFormItemNum;
			}
			return Crud.ClaimFormItemCrud.Insert(item);
		}

		///<summary></summary>
		public static void Update(ClaimFormItem item){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),item);
				return;
			}
			Crud.ClaimFormItemCrud.Update(item);
		}

		///<summary></summary>
		public static void Delete(ClaimFormItem item){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),item);
				return;
			}
			string command = "DELETE FROM claimformitem "
				+"WHERE ClaimFormItemNum = '"+POut.Long(item.ClaimFormItemNum)+"'";
 			Db.NonQ(command);
		}

		///<summary></summary>
		public static void DeleteAllForClaimForm(long claimFormNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),claimFormNum);
				return;
			}
			string command="DELETE FROM claimformitem WHERE ClaimFormNum = '"+POut.Long(claimFormNum)+"'";
			Db.NonQ(command);
		}

		///<summary>Gets all claimformitems for the specified claimform from the preloaded List.</summary>
		public static List<ClaimFormItem> GetListForForm(long claimFormNum) {
			//No need to check RemotingRole; no call to db.
			return ClaimFormItemC.Listt.Where(x => x.ClaimFormNum == claimFormNum).ToList();
		}


	}

	

	

}









