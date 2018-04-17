using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;

namespace OpenDentBusiness {
	public class AutoNotes {
		private static List<AutoNote> _listt;
		private static object _lockObj=new object();
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


		#region Cache
		///<summary>Cached list of all Auto Notes.</summary>
		public static List<AutoNote> Listt {
			get {
				return GetListt();
			}
			set {
				lock(_lockObj) {
					_listt=value;
				}
			}
		}

		private static List<AutoNote> GetListt() {
			bool isListNull=false;
			lock(_lockObj) {
				if(_listt==null) {
					isListNull=true;
				}
			}
			if(isListNull) {
				RefreshCache();
			}
			List<AutoNote> listAutoNotes=new List<AutoNote>();
			lock(_lockObj) {
				_listt.ForEach(x => listAutoNotes.Add(x.Copy()));
			}
			return listAutoNotes;
		}

		///<summary></summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM autonote ORDER BY AutoNoteName";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="AutoNote";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			Listt=Crud.AutoNoteCrud.TableToList(table);
		}
		#endregion Cache

		///<summary></summary>
		public static long Insert(AutoNote autonote) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				autonote.AutoNoteNum=Meth.GetLong(MethodBase.GetCurrentMethod(),autonote);
				return autonote.AutoNoteNum;
			}
			return Crud.AutoNoteCrud.Insert(autonote);
		}

		///<summary></summary>
		public static void Update(AutoNote autonote) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),autonote);
				return;
			}
			Crud.AutoNoteCrud.Update(autonote);
		}

		///<summary></summary>
		public static void Delete(long autoNoteNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),autoNoteNum);
				return;
			}
			string command="DELETE FROM autonote "
				+"WHERE AutoNoteNum = "+POut.Long(autoNoteNum);
			Db.NonQ(command);
		}

		public static string GetByTitle(string autoNoteTitle) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),autoNoteTitle);
			}
			foreach(AutoNote autoNote in Listt) {
				if(autoNote.AutoNoteName==autoNoteTitle) {
					return autoNote.MainText;
				}
			}
			return "";//Couldn't find AutoNote
		}

		///<summary>Sets the autonote.Category=0 for the autonote category DefNum provided.  Returns the number of rows updated.</summary>
		public static long RemoveFromCategory(long autoNoteCatDefNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetLong(MethodBase.GetCurrentMethod(),autoNoteCatDefNum);
			}
			string command="UPDATE autonote SET Category=0 WHERE Category="+POut.Long(autoNoteCatDefNum);
			return Db.NonQ(command);
		}
	
	}
}
