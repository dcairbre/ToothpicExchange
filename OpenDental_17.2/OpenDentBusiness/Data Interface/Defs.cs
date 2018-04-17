using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using CodeBase;

namespace OpenDentBusiness {
	public class Defs {
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

		///<summary>If using remoting, then the calling program is responsible for filling the arrays on the client since the automated part only happens on the server.  So there are TWO sets of arrays in a server situation, but only one in a small office that connects directly to the database.</summary>
		public static DataTable RefreshCache(){
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM definition ORDER BY Category,ItemOrder";
			DataConnection dcon=new DataConnection();
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="Def";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			List<Def> list=Crud.DefCrud.TableToList(table);
			Def[][] arrayLong=new Def[Enum.GetValues(typeof(DefCat)).Length][];
			for(int j=0;j<Enum.GetValues(typeof(DefCat)).Length;j++) {
				arrayLong[j]=GetForCategory(j,true,list);
			}
			Def[][] arrayShort=new Def[Enum.GetValues(typeof(DefCat)).Length][];
			for(int j=0;j<Enum.GetValues(typeof(DefCat)).Length;j++) {
				arrayShort[j]=GetForCategory(j,false,list);
			}
			DefC.Long=arrayLong;
			DefC.Short=arrayShort;
		}

		///<summary>Used by the refresh method above.</summary>
		private static Def[] GetForCategory(int catIndex,bool includeHidden,List<Def> list) {
			//No need to check RemotingRole; no call to db.
			List<Def> retVal=new List<Def>();
			for(int i=0;i<list.Count;i++) {
				if((int)list[i].Category!=catIndex){
					continue;
				}
				if(list[i].IsHidden && !includeHidden){
					continue;
				}
				retVal.Add(list[i]);
			}
			return retVal.ToArray();
		}

		///<summary>Only used in FormDefinitions</summary>
		public static Def[] GetCatList(int myCat){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Def[]>(MethodBase.GetCurrentMethod(),myCat);
			}
			string command=
				"SELECT * from definition"
				+" WHERE category = '"+myCat+"'"
				+" ORDER BY ItemOrder";
			return Crud.DefCrud.SelectMany(command).ToArray();
		}

		///<summary></summary>
		public static void Update(Def def) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),def);
				return;
			}
			Crud.DefCrud.Update(def);
		}

		///<summary></summary>
		public static long Insert(Def def) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				def.DefNum=Meth.GetLong(MethodBase.GetCurrentMethod(),def);
				return def.DefNum;
			}
			return Crud.DefCrud.Insert(def);
		}

		///<summary>CAUTION.  This does not perform all validations.  It only properly validates for four def types right now:
		///SupplyCats, ClaimCustomTracking, InsurancePaymentType, and AutoNoteCats.</summary>
		public static void Delete(Def def) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),def);
				return;
			}
			List<string> listCommands=new List<string>();
			switch(def.Category) {
				case DefCat.ClaimCustomTracking:
					listCommands.Add("SELECT COUNT(*) FROM securitylog WHERE DefNum="+POut.Long(def.DefNum));
					listCommands.Add("SELECT COUNT(*) FROM claim WHERE CustomTracking="+POut.Long(def.DefNum));
					break;
				case DefCat.ClaimErrorCode:
					listCommands.Add("SELECT COUNT(*) FROM claimtracking WHERE TrackingErrorDefNum="+POut.Long(def.DefNum));
					break;
				case DefCat.InsurancePaymentType:
					listCommands.Add("SELECT COUNT(*) FROM claimpayment WHERE PayType="+POut.Long(def.DefNum));
					break;
				case DefCat.SupplyCats:
					listCommands.Add("SELECT COUNT(*) FROM supply WHERE Category="+POut.Long(def.DefNum));
					break;
				case DefCat.AccountQuickCharge:
					break;//Users can delete AcctProcQuickCharge entries.  Nothing has an FKey to a AcctProcQuickCharge Def so no need to check anything.
				case DefCat.AutoNoteCats:
					AutoNotes.RemoveFromCategory(def.DefNum);//set any autonotes assinged to this category to 0 (unassigned), user already warned about this
					listCommands.Add("SELECT COUNT(*) FROM autonote WHERE Category="+POut.Long(def.DefNum));//just in case update failed or concurrency issue
					break;
				case DefCat.WebSchedNewPatApptTypes:
					break;//Users can delete WebSchedNewPatApptTypes entries.  Nothing has an FKey to a WebSchedNewPatApptTypes Def so no need to check anything.
				default:
					throw new ApplicationException("NOT Allowed to delete this type of def.");
			}
			for(int i=0;i<listCommands.Count;i++) {
				if(Db.GetCount(listCommands[i])!="0") {
					throw new ApplicationException(Lans.g("Defs","Def is in use.  Not allowed to delete."));
				}
			}
			string command="DELETE FROM definition WHERE DefNum="+POut.Long(def.DefNum);
			Db.NonQ(command);
			command="UPDATE definition SET ItemOrder=ItemOrder-1 "
				+"WHERE Category="+POut.Long((int)def.Category)
				+" AND ItemOrder > "+POut.Long(def.ItemOrder);
			Db.NonQ(command);
		}

		///<summary></summary>
		public static void HideDef(Def def) {
			//No need to check RemotingRole; no call to db.
			def.IsHidden=true;
			Defs.Update(def);
		}

		///<summary>Returns true if this category has definitions that can be hidden and we want to disallow hiding all definitions.</summary>
		public static bool IsHidable(DefCat category) {
			if(category==DefCat.AdjTypes
				|| category==DefCat.ApptConfirmed
				|| category==DefCat.ApptProcsQuickAdd
				|| category==DefCat.BillingTypes
				|| category==DefCat.BlockoutTypes
				|| category==DefCat.ClaimPaymentTracking
				//|| category==DefCat.ClinicSpecialty //ClinicSpecialties can be hidden and we want to allow users to hide ALL specialties if they so desire.
				|| category==DefCat.CommLogTypes
				|| category==DefCat.ContactCategories
				|| category==DefCat.Diagnosis
				|| category==DefCat.ImageCats
				|| category==DefCat.LetterMergeCats
				|| category==DefCat.PaymentTypes
				|| category==DefCat.PaySplitUnearnedType
				|| category==DefCat.ProcButtonCats
				|| category==DefCat.ProcCodeCats
				|| category==DefCat.Prognosis
				|| category==DefCat.ProviderSpecialties
				|| category==DefCat.RecallUnschedStatus
				|| category==DefCat.TaskPriorities
				|| category==DefCat.TxPriorities) 
			{
				return true;
			}
			return false;
		}

		///<summary>Returns true if there are any entries in definition that do not have a Category named "General".  
		///Returning false means the user has ProcButtonCategory customizations.</summary>
		public static bool HasCustomCategories() {
			List<Def> listDefs=DefC.GetListLong(DefCat.ProcButtonCats);
			foreach(Def defCur in listDefs) {
				if(!defCur.ItemName.Equals("General")) { 
					return true;
				}
			}
			return false;
		}		

		///<summary>Returns true if this definition is in use within the program. Consider enhancing this method if you add a definition category.
		///Does not check patient billing type or provider specialty since those are handled in their S-class.</summary>
		public static bool IsDefinitionInUse(Def def) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetBool(MethodBase.GetCurrentMethod(),def);
			}
			List<string> listStrCommands=new List<string>();
			switch(def.Category) {
				case DefCat.AdjTypes:
					if(new[] {
							PrefName.BrokenAppointmentAdjustmentType,
							PrefName.TreatPlanDiscountAdjustmentType,
							PrefName.BillingChargeAdjustmentType,
							PrefName.FinanceChargeAdjustmentType,
							PrefName.SalesTaxAdjustmentType
						}.Any(x => PrefC.GetLong(x)==def.DefNum))
					{
						return true;
					}
					listStrCommands.Add("SELECT COUNT(*) FROM adjustment WHERE AdjType="+POut.Long(def.DefNum));
					break;
				case DefCat.ApptConfirmed:
					if(new[] { PrefName.AppointmentTimeArrivedTrigger,PrefName.AppointmentTimeSeatedTrigger,PrefName.AppointmentTimeDismissedTrigger }
						.Any(x => PrefC.GetLong(x)==def.DefNum))
					{
						return true;
					}
					if(new[] { PrefName.ApptEConfirmStatusSent,PrefName.ApptEConfirmStatusAccepted,PrefName.ApptEConfirmStatusDeclined,PrefName.ApptEConfirmStatusSendFailed }
						.Any(x => PrefC.GetLong(x)==def.DefNum))
					{
						return true;
					}
					listStrCommands.Add("SELECT COUNT(*) FROM appointment WHERE Confirmed="+POut.Long(def.DefNum));
					break;
				case DefCat.AutoNoteCats:
					listStrCommands.Add("SELECT COUNT(*) FROM autonote WHERE Category="+POut.Long(def.DefNum));
					break;
				case DefCat.BillingTypes:
					if(new[] {
							PrefName.PracticeDefaultBillType
						}.Any(x => PrefC.GetLong(x)==def.DefNum)) {
						return true;
					}
					break;
				case DefCat.ContactCategories:
					listStrCommands.Add("SELECT COUNT(*) FROM contact WHERE Category="+POut.Long(def.DefNum));
					break;
				case DefCat.Diagnosis:
					listStrCommands.Add("SELECT COUNT(*) FROM procedurelog WHERE Dx="+POut.Long(def.DefNum));
					break;
				case DefCat.ImageCats:
					listStrCommands.Add("SELECT COUNT(*) FROM document WHERE DocCategory="+POut.Long(def.DefNum));
					listStrCommands.Add("SELECT COUNT(*) FROM sheetfielddef WHERE FieldType="+POut.Int((int)SheetFieldType.PatImage)+" AND FieldName="+POut.Long(def.DefNum));
					break;
				case DefCat.PaymentTypes:
					listStrCommands.Add("SELECT COUNT(*) FROM payment WHERE PayType="+POut.Long(def.DefNum));
					break;
				case DefCat.PaySplitUnearnedType:
					listStrCommands.Add("SELECT COUNT(*) FROM paysplit WHERE UnearnedType="+POut.Long(def.DefNum));
					break;
				case DefCat.Prognosis:
					listStrCommands.Add("SELECT COUNT(*) FROM procedurelog WHERE Prognosis="+POut.Long(def.DefNum));
					break;
				case DefCat.RecallUnschedStatus:
					if(def.DefNum.In(
						PrefC.GetLong(PrefName.RecallStatusMailed),
						PrefC.GetLong(PrefName.RecallStatusTexted),
						PrefC.GetLong(PrefName.RecallStatusEmailed),
						PrefC.GetLong(PrefName.RecallStatusEmailedTexted))) 
					{
						return true;
					}
					listStrCommands.Add("SELECT COUNT(*) FROM appointment WHERE UnschedStatus="+POut.Long(def.DefNum));
					listStrCommands.Add("SELECT COUNT(*) FROM recall WHERE RecallStatus="+POut.Long(def.DefNum));
					break;
				case DefCat.TaskPriorities:
					listStrCommands.Add("SELECT COUNT(*) FROM task WHERE PriorityDefNum="+POut.Long(def.DefNum));
					break;
				case DefCat.TxPriorities:
					listStrCommands.Add("SELECT COUNT(*) FROM procedurelog WHERE Priority="+POut.Long(def.DefNum));
					break;
				case DefCat.CommLogTypes:
					listStrCommands.Add("SELECT COUNT(*) FROM commlog WHERE CommType="+POut.Long(def.DefNum));
					break;
				default:
					break;
			}
			return listStrCommands.Any(x => Db.GetCount(x)!="0");
		}

	}
}
