<Query Kind="Program">
  <Connection>
    <ID>464b7a4f-e1ba-4423-afa8-174cd4c9b311</ID>
    <Persist>true</Persist>
    <Server>SBK-WIN81</Server>
    <Database>professional_pro2011Test</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

// -- configuration --------------
// Hier den Statuswert eintragen, den ein neuer Artikel am Anfang bekommt
// Enter the status value that a new document receives at the beginning here
string wipState = "00100";
// Hier die Revision eintragen, die ein neuer Artikel bekommt. Im PSP Standard wird keine Revision gesetzt
// Enter the revision that a new document gets here. In the PSP standard no revision is set
string initialRevision = "";
// Hier mit Komma getrennt die entsprechenden Statuswerte eintragen (z.B. {"00003", "00013"};)
// Enter the corresponding status values here separated by commas (e.g. {"00003", "00013"};)
HashSet<string> releasedStates = new HashSet<string> {"00103"};
HashSet<string> obsoleteStates = new HashSet<string> {"00105"};
HashSet<string> quickFixStates = new HashSet<string>();
// Die folgenden Schalter werden benötigt um detaillierte Ausgaben zu bekommen.
// Am besten Schalter so eingestellt lassen.  Wenn man einzelne Dokument prüft ist es sinnvoll die auf „true“ zu setzen
// The following switches are needed to get detailed output.
// Best to leave the switch set like this. If you check single documents it is useful to set the "debug" to "true"
bool debug = false;
bool warnings = false;
// -- end configuration --------------

// counters
long countAllConflicts = 0;
Dictionary<string, long> mapConflictCounters = new Dictionary<string, long>();
// end counters

string currentIdent = null;
string currentRevision = null;
string currentState = null;
string currentDateField = null;
bool currentHasWIPState = false;
bool currentHasReleasedState = false;
DateTime currentDate = DateTime.MinValue;
	
void Main()
{
	var items = (from e in VIEW_ALL_PART //where e.IDENT == "JK-ENG-000072"
		orderby e.REVISION
		orderby e.IDENT
		select new { e.IDENT, e.REVISION, e.AIMKEY, e.STATUSKEY, e.CREATE_DATE, e.CHANGE_DATE, e.CHECKED_1_DATE }); //.Take(100);
	
	// initialize progress bar
	var pb = new Util.ProgressBar("Analyzing ...");
	pb.Dump();
	long i = 0;
	long count = items.Count();
	
	foreach (var d in items)
	{
		pb.Percent = (int)((double)++i/(double)count * 100) + 1;
		
		var historyData = from h in VIEW_HISTORY_PART where h.AIMKEY == d.AIMKEY select new { h.IDENT, h.REVISION, h.CHECK_DATE, h.CHANGE_DATE };
		if (debug) String.Format("{0} - {1}: State: {2},  History: {3}", d.IDENT, d.REVISION, d.STATUSKEY, historyData.Count()).Dump();
		if (currentIdent != d.IDENT)
		{
			// new document, reset variables
			currentHasWIPState = false;
			currentHasReleasedState = false;
			currentIdent = d.IDENT;
			currentDate = DateTime.MinValue;
			//String.Format("Analyzing document {0} ...", currentIdent).Dump();
			pb.Caption = String.Format("Analyzing '{0}' ...", currentIdent);
			AddInitialHistoryItem(d.STATUSKEY, d.REVISION, d.CREATE_DATE, d.CHECKED_1_DATE, (historyData.Count() > 0));
		}
		
		if (historyData.Count() == 0)
		{
			if (currentDate == DateTime.MinValue)
				AddAndCheckDate(d.CREATE_DATE, d.REVISION, d.STATUSKEY, "CREATE_DATE"); // first entry for new ident
			else
			{
				if (quickFixStates.Contains(d.STATUSKEY))
					AddAndCheckDate(currentDate, d.REVISION, d.STATUSKEY, "predecessor date"); // in case of quickFix item use date of predecessor
				else if (releasedStates.Contains(d.STATUSKEY))
					AddAndCheckDate(d.CHECKED_1_DATE, d.REVISION, d.STATUSKEY, "CHECKED_1_DATE");
				else
					AddAndCheckDate(d.CHANGE_DATE, d.REVISION, d.STATUSKEY, "CHANGE_DATE");
			}
		}
		else
		{
			int c = historyData.Count();
			foreach (var h in historyData)
			{
				//h.Dump();	
				if (--c != 0)
				{
					// all entries but the last one are considered released if checked date is set
					if (h.CHECK_DATE != null)
					{
						AddAndCheckDate(h.CHECK_DATE, h.REVISION, releasedStates.First(), "H:CHECK_DATE");
						currentHasReleasedState = true;
					}
					else
						AddAndCheckDate(h.CHANGE_DATE, h.REVISION, d.STATUSKEY, "H:CHANGE_DATE");
				}
				else
				{
					// last history entry
					if (obsoleteStates.Contains(d.STATUSKEY))
					{
						if (h.CHECK_DATE != null)
						{
							if (!currentHasReleasedState)
							{
								// if we don't have a released state yet, create one with this release date
								AddAndCheckDate(h.CHECK_DATE, h.REVISION, releasedStates.First(), "H:CHECK_DATE");
								currentHasReleasedState = true;
							}														
							AddAndCheckDate(h.CHECK_DATE, h.REVISION, d.STATUSKEY, "H:CHECK_DATE"); // use CHECK_DATE of history if it is obsolete and CHECK_DATE is set
						}
						else
							AddAndCheckDate(h.CHANGE_DATE, h.REVISION, d.STATUSKEY, "H:CHANGE_DATE"); // use CHANGE_DATE of history if it is obsolete and CHECK_DATE is not set
					}
					else if (releasedStates.Contains(d.STATUSKEY))
					{
						AddAndCheckDate(h.CHECK_DATE, h.REVISION, d.STATUSKEY, "H:CHECK_DATE");
						currentHasReleasedState = true;
					}
					else
						AddAndCheckDate(h.CHANGE_DATE, h.REVISION, d.STATUSKEY, "H:CHANGE_DATE");
				}
			}
		}
	}
	pb.Caption = "Done!";
	
	String.Format("Number date conflicts: {0}", countAllConflicts).Dump();
	foreach (var c in mapConflictCounters)
		String.Format("{0}: {1}", c.Key, c.Value).Dump();
}

public void AddInitialHistoryItem(string statuskey, string revision, object createDate, object checkDate, bool isHistoryData)
{	
	if (!isHistoryData)
	{	
		if (releasedStates.Contains(statuskey) || obsoleteStates.Contains(statuskey) || quickFixStates.Contains(statuskey))
		{
			if (!currentHasWIPState)
			{
				AddAndCheckDate(createDate, initialRevision, wipState, "CREATE_DATE");
				currentHasWIPState = true;				
			}
			if ((obsoleteStates.Contains(statuskey) || quickFixStates.Contains(statuskey)) && !currentHasReleasedState)
			{
				if (checkDate != null)
				{
					AddAndCheckDate(checkDate, initialRevision, releasedStates.First(), "CHECKED_1_DATE");
					currentHasReleasedState = true;
				}
			}
		}
		else
			currentHasWIPState = true;
	}
	else
	{
		AddAndCheckDate(createDate, initialRevision, wipState, "CREATE_DATE");
		currentHasWIPState = true;
		
		if (initialRevision != revision && checkDate != null)
		{
			AddAndCheckDate(checkDate, initialRevision, releasedStates.First(), "CHECKED_1_DATE");
			currentHasReleasedState = true;
		}		
	}
}

public void AddAndCheckDate(object dateObject, string newRevision, string newState, string newDateField)
{
	DateTime newDate;
	bool setToNow = false;
	try
	{
		newDate = Convert.ToDateTime(dateObject);
		if (newDate == DateTime.MinValue)
		{
			if (warnings) String.Format("WARNING: {0} - {1}: State: {2} Field '{3}' is empty. Set date to predecessor date", currentIdent, newRevision, newState, newDateField).Dump();
			newDate = currentDate;
		}			
	}
	catch
	{
		String.Format("{0} - {1}: State: {2} Field '{3}' doesn't contain valid date. Set date to predecessor date", currentIdent, newRevision, newState, newDateField).Dump();
		newDate = currentDate;
	}
	
	if (debug) String.Format("Add {0} - {1}: State: {2},  Date: {3} ({4})", currentIdent, newRevision, newState, newDate, newDateField).Dump();
	if (newDate < currentDate)
	{
		countAllConflicts++;
		var conflictDesc = newDateField;
		if (mapConflictCounters.ContainsKey(conflictDesc))
			mapConflictCounters[conflictDesc]++;
		else
			mapConflictCounters.Add(conflictDesc, 1);
	}
	
	currentDate = newDate;
	currentRevision = newRevision;
	currentState = newState;
	currentDateField = newDateField;
}