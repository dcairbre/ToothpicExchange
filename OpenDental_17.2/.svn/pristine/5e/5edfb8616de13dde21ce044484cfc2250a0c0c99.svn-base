using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Etrans835Attaches{
	
		///<summary>Get all claim attachments for every 835 in the list.  Ran as a batch for efficiency purposes.</summary>
		public static List<Etrans835Attach> GetForClaimNums(params long[] listClaimNums) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Etrans835Attach>>(MethodBase.GetCurrentMethod(),listClaimNums);
			}
			if(listClaimNums.Length==0) {
				return new List<Etrans835Attach>();
			}
			string command="SELECT * FROM etrans835attach WHERE ClaimNum IN ("+String.Join(",",listClaimNums.Select(x => POut.Long(x)))+")";
			return Crud.Etrans835AttachCrud.SelectMany(command);
		}

		///<summary>Get all claim attachments for every 835 in the list.  Ran as a batch for efficiency purposes.</summary>
		public static List<Etrans835Attach> GetForEtrans(params long[] listEtrans835Nums){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Etrans835Attach>>(MethodBase.GetCurrentMethod(),listEtrans835Nums);
			}
			if(listEtrans835Nums.Length==0) {
				return new List<Etrans835Attach>();
			}
			string command="SELECT * FROM etrans835attach WHERE EtransNum IN ("+String.Join(",",listEtrans835Nums.Select(x => POut.Long(x)))+")";
			return Crud.Etrans835AttachCrud.SelectMany(command);
		}

		///<summary>Create a single attachment for a claim to an 835.</summary>
		public static long Insert(Etrans835Attach etrans835Attach){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				etrans835Attach.Etrans835AttachNum=Meth.GetLong(MethodBase.GetCurrentMethod(),etrans835Attach);
				return etrans835Attach.Etrans835AttachNum;
			}
			return Crud.Etrans835AttachCrud.Insert(etrans835Attach);
		}

		///<summary>Delete the attachemt for the claim currently attached to the 835 with the specified segment index.
		///Safe to run even if no claim is currently attached at the specified index.</summary>
		public static void Delete(long etransNum,int clpSegmentIndex) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),etransNum,clpSegmentIndex);
				return;
			}
			string command="DELETE FROM etrans835attach WHERE EtransNum="+POut.Long(etransNum)+" AND ClpSegmentIndex="+POut.Int(clpSegmentIndex);
			Db.NonQ(command);
		}

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.
		#region Get Methods
		///<summary></summary>
		public static List<Etrans835Attach> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Etrans835Attach>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM etrans835attach WHERE PatNum = "+POut.Long(patNum);
			return Crud.Etrans835AttachCrud.SelectMany(command);
		}
		
		///<summary>Gets one Etrans835Attach from the db.</summary>
		public static Etrans835Attach GetOne(long etrans835AttachNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<Etrans835Attach>(MethodBase.GetCurrentMethod(),etrans835AttachNum);
			}
			return Crud.Etrans835AttachCrud.SelectOne(etrans835AttachNum);
		}
		#endregion
		#region Modification Methods
			#region Update
			///<summary></summary>
		public static void Update(Etrans835Attach etrans835Attach){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),etrans835Attach);
				return;
			}
			Crud.Etrans835AttachCrud.Update(etrans835Attach);
		}
			#endregion
			#region Delete
		///<summary></summary>
		public static void Delete(long etrans835AttachNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),etrans835AttachNum);
				return;
			}
			Crud.Etrans835AttachCrud.Delete(etrans835AttachNum);
		}
			#endregion
		#endregion
		#region Misc Methods
		

		
		#endregion
		*/



	}
}