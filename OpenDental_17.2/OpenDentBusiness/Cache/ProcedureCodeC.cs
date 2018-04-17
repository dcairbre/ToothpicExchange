using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace OpenDentBusiness {
	public class ProcedureCodeC {	
		private static List<ProcedureCode> _list;
		private static Hashtable _hList;
		private static object _lockObj=new object();

		///<summary>Returns a list of CodeNums for specific BW procedure codes.
		///There are several places in the program that need a BW group.  E.g. when computing limitations.</summary>
		public static List<long> ListBWCodeNums {
			get {
				bool isListNull=false;
				lock(_lockObj) {
					if(_list==null) {
						isListNull=true;
					}
				}
				if(isListNull) {
					GetListLong();//Fill _list for the first time but don't worry about saving a deep copy.
				}
				List<long> listBWs=new List<long>();
				lock(_lockObj) {
					List<string> listCodes=PrefC.GetString(PrefName.InsBenBWCodes).Split(',').ToList();
					foreach(string code in listCodes) {
						ProcedureCode procCode=_list.FirstOrDefault(x => x.ProcCode==code);
						if(procCode!=null) {
							listBWs.Add(procCode.CodeNum);
						}
					}
				}
				return listBWs;
			}
		}

		///<summary>Returns a list of CodeNums for specific Exam procedure codes.
		///There are several places in the program that need a Exam group.  E.g. when computing limitations.</summary>
		public static List<long> ListExamCodeNums {
			get {
				bool isListNull=false;
				lock(_lockObj) {
					if(_list==null) {
						isListNull=true;
					}
				}
				if(isListNull) {
					GetListLong();//Fill _list for the first time but don't worry about saving a deep copy.
				}
				List<long> listExams=new List<long>();
				lock(_lockObj) {//Always directly use _list for speed.
					List<string> listCodes=PrefC.GetString(PrefName.InsBenExamCodes).Split(',').ToList();
					foreach(string code in listCodes) {
						ProcedureCode procCode=_list.FirstOrDefault(x => x.ProcCode==code);
						if(procCode!=null) {
							listExams.Add(procCode.CodeNum);
						}
					}
				}
				return listExams;
			}
		}

		///<summary>Returns a list of CodeNums for specific PanoFMX procedure codes.
		///There are several places in the program that need a PanoFMX group.  E.g. when computing limitations.</summary>
		public static List<long> ListPanoFMXCodeNums {
			get {
				bool isListNull=false;
				lock(_lockObj) {
					if(_list==null) {
						isListNull=true;
					}
				}
				if(isListNull) {
					GetListLong();//Fill _list for the first time but don't worry about saving a deep copy.
				}
				List<long> listPanoFMXs=new List<long>();
				lock(_lockObj) {
					List<string> listCodes=PrefC.GetString(PrefName.InsBenPanoCodes).Split(',').ToList();
					foreach(string code in listCodes) {
						ProcedureCode procCode=_list.FirstOrDefault(x => x.ProcCode==code);
						if(procCode!=null) {
							listPanoFMXs.Add(procCode.CodeNum);
						}
					}
				}
				return listPanoFMXs;
			}
		}

		public static List<ProcedureCode> Listt {
			get {
				return GetListLong();
			}
			set {
				lock(_lockObj) {
					_list=value;
				}
			}
		}

		///<summary>key:ProcCode, value:ProcedureCode</summary>
		public static Hashtable HList {
			get {
				return GetHList();
			}
			set {
				lock(_lockObj) {
					_hList=value;
				}
			}
		}

		///<summary></summary>
		public static List<ProcedureCode> GetListLong() {
			bool isListNull=false;
			lock(_lockObj) {
				if(_list==null) {
					isListNull=true;
				}
			}
			if(isListNull) {
				ProcedureCodes.RefreshCache();
			}
			List<ProcedureCode> listProcCodes=new List<ProcedureCode>();
			lock(_lockObj) {
				for(int i=0;i<_list.Count;i++) {
					listProcCodes.Add(_list[i].Copy());
				}
			}
			return listProcCodes;
		}

		///<summary>key:ProcCode, value:ProcedureCode</summary>
		public static Hashtable GetHList() {
			bool isListNull=false;
			lock(_lockObj) {
				if(_hList==null) {
					isListNull=true;
				}
			}
			if(isListNull) {
				ProcedureCodes.RefreshCache();
			}
			Hashtable hashProcCodes=new Hashtable();
			lock(_lockObj) {
				//Jordan approved foreach loop for speed purposes.  Looping through a hashtable of 38,000 items using a for loop took ~22,840ms whereas a foreach loop takes ~10ms.
				foreach(DictionaryEntry entry in _hList) {
					hashProcCodes.Add(entry.Key,((ProcedureCode)entry.Value).Copy());
				}
			}
			return hashProcCodes;
		}

		
	}
}
