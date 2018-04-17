using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace OpenDentBusiness{
	///<summary></summary>
	public class ClaimForms {
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

		private static ClaimForm[] listLong;
		private static ClaimForm[] listShort;

		///<summary>List of all claim forms.</summary>
		public static ClaimForm[] ListLong {
			//No need to check RemotingRole; no call to db.
			get {
				if(listLong==null) {
					RefreshCache();
				}
				return listLong;
			}
			set {
				listLong=value;
			}
		}

		///<summary>List of all claim forms except those marked as hidden.</summary>
		public static ClaimForm[] ListShort {
			//No need to check RemotingRole; no call to db.
			get {
				if(listShort==null) {
					RefreshCache();
				}
				return listShort;
			}
			set {
				listShort=value;
			}
		}

		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM claimform";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="ClaimForm";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			listLong=Crud.ClaimFormCrud.TableToList(table).ToArray();
			List<ClaimForm> ls=new List<ClaimForm>();
			for(int i=0;i<listLong.Length;i++) {
				ListLong[i].Items=ClaimFormItems.GetListForForm(ListLong[i].ClaimFormNum);
				if(!listLong[i].IsHidden){
					ls.Add(ListLong[i]);
				}
			}
			listShort=ls.ToArray();
		}

		///<summary>Inserts this claimform into database and retrieves the new primary key.
		///Assigns all claimformitems to the claimform and inserts them if the bool is true.</summary>
		public static long Insert(ClaimForm cf, bool includeClaimFormItems) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetLong(MethodBase.GetCurrentMethod(),cf, includeClaimFormItems);
			}
			long retVal = Crud.ClaimFormCrud.Insert(cf);
			if(includeClaimFormItems) {
				foreach(ClaimFormItem claimFormItemCur in cf.Items) {
					claimFormItemCur.ClaimFormNum=cf.ClaimFormNum;//so even though the ClaimFormNum is wrong, this line fixes it.
					ClaimFormItems.Insert(claimFormItemCur);
				}
			}
			return retVal;
		}

		///<summary>Can be called externally as part of the conversion sequence.  Surround with try catch.
		///Returns the claimform object from the xml file or xml data passed in that can then be inserted if needed.
		///If xmlData is provided then path will be ignored.  If xmlData is not provided, a valid path is required.</summary>
		public static ClaimForm DeserializeClaimForm(string path,string xmlData) {
			//No need to check RemotingRole; no call to db.
			ClaimForm tempClaimForm = new ClaimForm();
			XmlSerializer serializer = new XmlSerializer(typeof(ClaimForm));
			if(xmlData=="") {//use path
				if(!File.Exists(path)) {
					throw new ApplicationException(Lans.g("FormClaimForm","File does not exist."));
				}
				try {
					using(TextReader reader = new StreamReader(path)) {
						tempClaimForm=(ClaimForm)serializer.Deserialize(reader);
					}
				}
				catch {
					throw new ApplicationException(Lans.g("FormClaimForm","Invalid file format"));
				}
			}
			else {//use xmlData
				try {
					using(TextReader reader = new StringReader(xmlData)) {
						tempClaimForm=(ClaimForm)serializer.Deserialize(reader);
					}
				}
				catch {
					throw new ApplicationException(Lans.g("FormClaimForm","Invalid file format"));
				}
			}
			return tempClaimForm;
		}

		///<summary></summary>
		public static void Update(ClaimForm cf){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),cf);
				return;
			}
			//Synch the claim form items associated to this claim form first.
			ClaimFormItems.DeleteAllForClaimForm(cf.ClaimFormNum);
			foreach(ClaimFormItem item in cf.Items) {
				ClaimFormItems.Insert(item);
			}
			//Now we can update any information specific to the claim form itself.
			Crud.ClaimFormCrud.Update(cf);
		}

		///<summary> Called when cancelling out of creating a new claimform, and from the claimform window when clicking delete. Returns true if successful or false if dependencies found.</summary>
		public static bool Delete(ClaimForm cf){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetBool(MethodBase.GetCurrentMethod(),cf);
			}
			//first, do dependency testing
			string command="SELECT * FROM insplan WHERE claimformnum = '"
				+cf.ClaimFormNum.ToString()+"' ";
			command+=DbHelper.LimitAnd(1);
 			DataTable table=Db.GetTable(command);
			if(table.Rows.Count==1){
				return false;
			}
			//Then, delete the claimform
			command="DELETE FROM claimform "
				+"WHERE ClaimFormNum = '"+POut.Long(cf.ClaimFormNum)+"'";
			Db.NonQ(command);
			command="DELETE FROM claimformitem "
				+"WHERE ClaimFormNum = '"+POut.Long(cf.ClaimFormNum)+"'";
			Db.NonQ(command);
			return true;
		}
		
		///<summary>Returns the claim form specified by the given claimFormNum</summary>
		public static ClaimForm GetClaimForm(long claimFormNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<ListLong.Length;i++){
				if(ListLong[i].ClaimFormNum==claimFormNum){
					return ListLong[i];
				}
			}
			return null;
		}

		///<summary>Returns a list of all internal claims within the OpenDentBusiness resources.  Throws exceptions.</summary>
		public static List<ClaimForm> GetInternalClaims() {
			//No need to check RemotingRole; no call to db.
			List<ClaimForm> listInternalClaimForms = new List<ClaimForm>();
			ResourceSet resources=OpenDentBusiness.Properties.Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture,true,true);
			foreach(DictionaryEntry item in resources) {
				if(!item.Key.ToString().StartsWith("ClaimForm")) {
					continue;
				}
				//Resources that start with ClaimForm are serialized ClaimForm objects in XML.
				ClaimForm cfCur = ClaimForms.DeserializeClaimForm("",item.Value.ToString());
				cfCur.IsInternal=true;
				listInternalClaimForms.Add(cfCur);
			}
			return listInternalClaimForms;
		}

		///<summary>Returns number of insplans affected.</summary>
		public static long Reassign(long oldClaimFormNum,long newClaimFormNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetLong(MethodBase.GetCurrentMethod(),oldClaimFormNum,newClaimFormNum);
			}
			string command="UPDATE insplan SET ClaimFormNum="+POut.Long(newClaimFormNum)
				+" WHERE ClaimFormNum="+POut.Long(oldClaimFormNum);
			return Db.NonQ(command);
		}



	}

	



}








