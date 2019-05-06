<Query Kind="SQL">
  <Connection>
    <ID>d01849d0-bc0d-4d5c-bd00-370fe31ee070</ID>
    <Persist>true</Persist>
    <Server>PSP-SERVER-DE</Server>
    <SqlSecurity>true</SqlSecurity>
    <UserName>sa</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA7cU9w3PGkkOaS/X3PaJIzgAAAAACAAAAAAAQZgAAAAEAACAAAADTJdCtSuHKTQZs368CHDYT0o+BmvvnXJxXf4OW+4rtrQAAAAAOgAAAAAIAACAAAABapkK+RNdNBYnpmo33vPKh5aYC343df08XkKib6zw9WhAAAACyziWENDwtQ/zp8useUhw2QAAAALjFGnK4ZYfIP0sfv4vmGt/AFsrBWg26kuT3YKmufp7uO5ER21pB1+7WtazkXwelxJQbhVxZ8dNEWeRQ0IPix8g=</Password>
    <Database>PSP_PRO</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

-- General checks for a quick overview
declare @company varchar(64)
declare @product varchar(10)
declare @version varchar(10)

select @company=COMPANY, @product=PRODUCT, @version=VERSION from AIMKEY
PRINT 'Analysis for PSP database of company ' + @company
PRINT 'Product: ' + @product
PRINT 'Version:' + @version
GO

-- General
-- =========================
PRINT ''
PRINT 'General'
GO

-- Check for additional tables
-- ===========================
PRINT ''
PRINT 'Check for additional tables'
if exists(select name as Tabelle from sys.tables 
	where name not in ('ADDRESS','AIM_IMPORT_DML','AIM_TABLE_COLUMNS','AIM_XREF_IMPORT_DML','AIMKEY','CMP_LOG','CMP_USER','CONFIGURATION2','CONTACT','DML_LOG','DML_TEMP','DOCUMENT','ELEMENT','ENTITY_TYPE','HISTORY_DOCUMENT','HISTORY_PART','JOBSERVER','KEYGEN_TEMP','NAV_STRUCT_CACHE','PART','PROJECT','RELATIONSHIP_TYPE','STL_TMP','SWP','USER_GROUP','XREF_ELEMENT','XREF_USER','LOCATION','REPL_DOCUMENT','REPLICATORQ','CLASS','CLASSCRC','CRCLIST','HISTORY_CLASS','XREFCRCLIST','INVDOCANALYZER13')
		and name not like 'AIM_EXP_%' and name not like 'AIM_HEXP_%' and name not like 'AIM_XEXP_%')
BEGIN
	PRINT 'The database contains the following additional tables compared to the standard one:'
	select name as Tabelle from sys.tables 
		where name not in ('ADDRESS','AIM_IMPORT_DML','AIM_TABLE_COLUMNS','AIM_XREF_IMPORT_DML','AIMKEY','CMP_LOG','CMP_USER','CONFIGURATION2','CONTACT','DML_LOG','DML_TEMP','DOCUMENT','ELEMENT','ENTITY_TYPE','HISTORY_DOCUMENT','HISTORY_PART','JOBSERVER','KEYGEN_TEMP','NAV_STRUCT_CACHE','PART','PROJECT','RELATIONSHIP_TYPE','STL_TMP','SWP','USER_GROUP','XREF_ELEMENT','XREF_USER','LOCATION','REPL_DOCUMENT','REPLICATORQ','CLASS','CLASSCRC','CRCLIST','HISTORY_CLASS','XREFCRCLIST','INVDOCANALYZER13')
			and name not like 'AIM_EXP_%' and name not like 'AIM_HEXP_%' and name not like 'AIM_XEXP_%'
		order by name
	PRINT 'Note: The additional tables must be examined more closely with regard to their relevance for the data transfer and their meaning must be clarified with the customer!'
END
else
	PRINT 'The database does not contain any additional tables compared to the standard!'
GO

-- Check for new element types
-- ======================
PRINT ''
PRINT 'Check for new element types'
IF exists(select TYPE from ENTITY_TYPE where TYPE not in ('AIM','AIM.ADR','AIM.CLASS','AIM.CLASSCRC','AIM.CONTACT','AIM.CONTACT.ORG','AIM.CONTACT.PERSON','AIM.DOC','AIM.DOC.ENG','AIM.DOC.ENG.GEN','AIM.DOC.ENG.GEN.CAB','AIM.DOC.ENG.GEN.FRM','AIM.DOC.ENG.GEN.FUN','AIM.DOC.ENG.GEN.PIP','AIM.DOC.ENG.STD','AIM.DOC.OFF','AIM.DOC.SECONDARY','AIM.FAVORIT','AIM.FOLDER','AIM.FOLDER.DETAIL','AIM.FOLDER.IMPORT','AIM.FOLDER.ROOT','AIM.JOBSERVER.EXEC','AIM.JOBSERVER.TYPE','AIM.PART','AIM.PRO','AIM.SECTION','AIM.SECTION.ROOT','AIM.LOCATION'))
BEGIN
	PRINT 'The database contains the following additional element types compared to the standard one:'
	select TYPE, DESCRIPTION from ENTITY_TYPE where TYPE not in ('AIM','AIM.ADR','AIM.CLASS','AIM.CLASSCRC','AIM.CONTACT','AIM.CONTACT.ORG','AIM.CONTACT.PERSON','AIM.DOC','AIM.DOC.ENG','AIM.DOC.ENG.GEN','AIM.DOC.ENG.GEN.CAB','AIM.DOC.ENG.GEN.FRM','AIM.DOC.ENG.GEN.FUN','AIM.DOC.ENG.GEN.PIP','AIM.DOC.ENG.STD','AIM.DOC.OFF','AIM.DOC.SECONDARY','AIM.FAVORIT','AIM.FOLDER','AIM.FOLDER.DETAIL','AIM.FOLDER.IMPORT','AIM.FOLDER.ROOT','AIM.JOBSERVER.EXEC','AIM.JOBSERVER.TYPE','AIM.PART','AIM.PRO','AIM.SECTION','AIM.SECTION.ROOT','AIM.LOCATION')
		order by TYPE
	PRINT 'Note: The additional element types must be examined more closely with regard to their relevance for the data transfer and their meaning must be clarified with the customer!'
END
ELSE
	PRINT 'The database does not contain any additional element types compared to the standard!'
GO

-- Check for new relationship types
-- ======================
PRINT ''
PRINT 'Check for new relationship types'
IF exists(select RELATIONSHIP_ID from RELATIONSHIP_TYPE where RELATIONSHIP_ID not in ('AIM.XREF.CLASS','AIM.XREF.CLASS.CLASSCRC','AIM.XREF.CONTACT','AIM.XREF.CONTACT.ADR','AIM.XREF.CONTACT.DOC','AIM.XREF.CONTACT.ORG','AIM.XREF.CONTACT.PERSON','AIM.XREF.CONTACT.PRO','AIM.XREF.DOC.ENG','AIM.XREF.DOC.PART','AIM.XREF.DOC.PRO.ENG','AIM.XREF.DOC.PRO.OFF','AIM.XREF.DOC.SECONDARY','AIM.XREF.FAVORIT','AIM.XREF.FOLDER','AIM.XREF.PART','AIM.XREF.PART.CLASS','AIM.XREF.PART.PRO','AIM.XREF.PRO.PRO','AIM.XREF.ROOT.SECTION','AIM.XREF.SECTION','AIM.XREF.SECTION.FAVORIT','AIM.XREF.SECTION.FOLDER','AIM.XREF.DOC.LOCATION'))
BEGIN
	PRINT 'The database contains the following additional relationship types compared to the standard one:'
	select RELATIONSHIP_ID, DESCRIPTION, PARENT_ENTITY, CHILD_ENTITY from RELATIONSHIP_TYPE where RELATIONSHIP_ID not in ('AIM.XREF.CLASS','AIM.XREF.CLASS.CLASSCRC','AIM.XREF.CONTACT','AIM.XREF.CONTACT.ADR','AIM.XREF.CONTACT.DOC','AIM.XREF.CONTACT.ORG','AIM.XREF.CONTACT.PERSON','AIM.XREF.CONTACT.PRO','AIM.XREF.DOC.ENG','AIM.XREF.DOC.PART','AIM.XREF.DOC.PRO.ENG','AIM.XREF.DOC.PRO.OFF','AIM.XREF.DOC.SECONDARY','AIM.XREF.FAVORIT','AIM.XREF.FOLDER','AIM.XREF.PART','AIM.XREF.PART.CLASS','AIM.XREF.PART.PRO','AIM.XREF.PRO.PRO','AIM.XREF.ROOT.SECTION','AIM.XREF.SECTION','AIM.XREF.SECTION.FAVORIT','AIM.XREF.SECTION.FOLDER','AIM.XREF.DOC.LOCATION')
		order by RELATIONSHIP_ID
	PRINT 'Note: The additional relationship types must be examined more closely with regard to their relevance for the data transfer and their meaning must be clarified with the customer!'
END
ELSE
	PRINT 'The database does not contain any additional relationship types compared to the standard!'
GO

-- Check for items in trash
-- ======================
PRINT ''
PRINT 'Check for items in trash'
IF exists(select * from ELEMENT where DELETE_INITIATOR is not null)
BEGIN
	PRINT 'The following items are in the trash:'
	select ENTITY_TYPE, DELETE_INITIATOR, count(*) as Count from ELEMENT where DELETE_INITIATOR is not null group by ENTITY_TYPE, DELETE_INITIATOR order by ENTITY_TYPE, DELETE_INITIATOR
	PRINT 'Note: The trash should be emptied before data transfer to Vault!'
END
ELSE
	PRINT 'There are currently no items in the trash!'
GO

-- Check for replication
-- =========================
PRINT ''
PRINT 'Check for replication'
if exists (select name from sys.tables where name = 'LOCATION')
BEGIN
	PRINT 'The database contains a replication with the following locations:'
	SELECT IDENT, PUBLISHER, DB_SERVERNAME, DB_DATABASE from LOCATION
	PRINT 'Note: The relevance of replication for data transfer must be clarified!'
	PRINT 'In any case, all original files to be transferred to Vault must be located at the location where the data transfer is performed!'
	PRINT ''
	PRINT 'The following is an overview of the original locations'
	SELECT FILE_ORIG_AT, FILE_ORIG_TO, count(*) as Count from VIEW_ALL_DOCUMENT group by FILE_ORIG_AT, FILE_ORIG_TO order by FILE_ORIG_AT, FILE_ORIG_TO
END
else
	PRINT 'The database does not contain replication!'
GO

-- Check for characteristics
-- =========================
PRINT ''
PRINT 'Check for characteristics'
if exists (select name from sys.tables where name = 'CLASS')
BEGIN
	PRINT 'The database contains an installed characteristics list with the following entries:'
	select ENTITY_TYPE, count(*) as Count from ELEMENT where ENTITY_TYPE in ('AIM.CLASS','AIM.CLASSCRC') group by ENTITY_TYPE order by ENTITY_TYPE
	PRINT 'und folgenden Verknüpfungen:'
	select RELATIONSHIP_ID, count(*) as Count from XREF_ELEMENT where RELATIONSHIP_ID in ('AIM.XREF.CLASS','AIM.XREF.CLASS.CLASSCRC','AIM.XREF.PART.CLASS') group by RELATIONSHIP_ID order by RELATIONSHIP_ID
	PRINT 'Hinweis: Die Relevanz der Replikation ist für die Datenübernahme zu klären!'
END
else
BEGIN
	PRINT 'The database does not contain any characteristics!'
END
GO


-- Detailed Analysis General
-- ======================
PRINT ''
PRINT ''
PRINT 'Detailed Analysis General'
GO

-- Overview Element Types
-- ======================
PRINT ''
PRINT 'Overview Element Types'
-- Verwendete Elementtypen
PRINT 'The following element types were used:'
select ENTITY_TYPE, count(*) as Count, '' as 'Transfer yes/no' from ELEMENT where ENTITY_TYPE not in ('AIM.FAVORIT','AIM.FOLDER','AIM.FOLDER.DETAIL','AIM.FOLDER.IMPORT','AIM.FOLDER.ROOT','AIM.SECTION','AIM.SECTION.ROOT') group by ENTITY_TYPE order by ENTITY_TYPE
PRINT 'Note: Here you can define which elements should be transferred to Vault!'
GO

-- Overview relationships
-- =======================
PRINT ''
PRINT 'Overview relationships'
-- Used relationships
PRINT 'The following relationships were used:'
select RELATIONSHIP_ID, count(*) as Count, '' as 'Transfer yes/no' from XREF_ELEMENT where RELATIONSHIP_ID not in ('AIM.XREF.FAVORIT','AIM.XREF.FOLDER','AIM.XREF.ROOT.SECTION','AIM.XREF.SECTION','AIM.XREF.SECTION.FAVORIT','AIM.XREF.SECTION.FOLDER') group by RELATIONSHIP_ID order by RELATIONSHIP_ID
PRINT 'Note: Here you can define which elements should be transferred to Vault!'
GO

-- Overview of Users
-- ==============
PRINT ''
PRINT 'Overview of Users'
PRINT 'It is recommended to manually create users and groups in Vault before data migration. The Vault users already created will then be used for the data migration.
The Data Export Utility should be configured in such a way that no "unknown" users are created during the import of the data into Vault.
Users created once in Vault and used for projects, articles or files can no longer be deleted.
This means that the users in the corresponding PSP user fields that do not exist as Vault users should be corrected.'
PRINT ''
PRINT 'Attention: If users are mapped in a title block a correction may not be possible!'

-- List of PSP Users
PRINT ''
PRINT 'The following users are defined in PSP:'
select USERID, NAME, '' as 'Username in Vault' from CMP_USER order by USERID
PRINT 'Note: Not all users listed above may actually be used in PSP!
Therefore, it is usually sufficient to map the user names listed in the following tables.'
-- Overview CREATE_USER
PRINT ''
PRINT 'Used Users in ELEMENT.CREATE_USER (applies to all element types)'
select CREATE_USER, count(*) as Count, '' as 'Username in Vault' from ELEMENT group by CREATE_USER order by CREATE_USER
-- Overview CHANGE_USER
PRINT ''
PRINT 'Used Users in ELEMENT.CHANGE_USER (applies to all element types)'
select CHANGE_USER, count(*) as Count, '' as 'Username in Vault' from ELEMENT group by CHANGE_USER order by CHANGE_USER
-- Overview USERNAME
PRINT ''
PRINT 'Used Users in ELEMENT.USERNAME (applies to all element types)'
select USERNAME, count(*) as Count, '' as 'Username in Vault' from ELEMENT group by USERNAME order by USERNAME
-- Overview CHECKED_1_BY
PRINT ''
PRINT 'Used Users in ELEMENT.CHECKED_1_BY (applies to all element types)'
select CHECKED_1_BY, count(*) as Count, '' as 'Username in Vault' from ELEMENT group by CHECKED_1_BY order by CHECKED_1_BY
-- Overview CHANGE_USER in HISTORY_DOCUMENT
PRINT ''
PRINT 'Used Users in HISTORY_DOCUMENT.CHANGE_USER (only valid for documents)'
select CHANGE_USER, count(*) as Count, '' as 'Username in Vault' from HISTORY_DOCUMENT group by CHANGE_USER order by CHANGE_USER
-- Overview CHECK_USER in HISTORY_DOCUMENT
PRINT ''
PRINT 'Used Users in HISTORY_DOCUMENT.CHECK_USER (only valid for documents)'
select CHECK_USER, count(*) as Count, '' as 'Username in Vault' from HISTORY_DOCUMENT group by CHECK_USER order by CHECK_USER
-- Overview CHANGE_USER in HISTORY_PART
IF exists(select * from HISTORY_PART)
BEGIN
	PRINT ''
	PRINT 'Used Users in HISTORY_PART.CHANGE_USER (only valid for documents)'
	select CHANGE_USER, count(*) as Count, '' as 'Username in Vault' from HISTORY_PART group by CHANGE_USER order by CHANGE_USER
END
-- Übersicht CHECK_USER in HISTORY_PART
IF exists(select * from HISTORY_PART)
BEGIN
	PRINT ''
	PRINT 'Used Users in HISTORY_PART.CHECK_USER (only valid for documents)'
	select CHECK_USER, count(*) as Count, '' as 'Username in Vault' from HISTORY_PART group by CHECK_USER order by CHECK_USER
END

-- Documents
-- ======================
PRINT ''
PRINT ''
PRINT 'Documents'
GO

-- Gesamtanzahl
PRINT ''
PRINT 'Total number of document data records:'
select count(*) as Count from VIEW_ALL_DOCUMENT
PRINT 'of which with different document numbers:'
select count(distinct IDENT) as Count from VIEW_ALL_DOCUMENT
GO

-- Considerations about the file structure in Vault
PRINT ''
PRINT 'File structure in Vault'
PRINT 'The documents can be transferred by default either project-specifically or in the PSP storage structure by year, month or Vault.'
-- Document types with project links
PRINT 'Documents with project links'
PRINT 'The following is an overview of the documents that are linked to at least one project:'
PRINT 'Grouped by element type:'
select ENTITY_TYPE,COUNT(*) as Count from VIEW_ALL_DOCUMENT
	where AIMKEY in (select PARENT_AIMKEY from XREF_ELEMENT where RELATIONSHIP_ID like 'AIM.XREF.DOC.PRO%')
	group by ENTITY_TYPE order by ENTITY_TYPE
PRINT 'Grouped by element type and file type:'
select ENTITY_TYPE,FILE_TYPE,COUNT(*) as Count from VIEW_ALL_DOCUMENT
	where AIMKEY in (select PARENT_AIMKEY from XREF_ELEMENT where RELATIONSHIP_ID like 'AIM.XREF.DOC.PRO%')
	group by ENTITY_TYPE,FILE_TYPE order by ENTITY_TYPE,FILE_TYPE
PRINT 'Note: When using the project-specific storage structure, these documents are later stored in a project folder in the Vault!'
PRINT 'If a document is linked more than once to several projects, the file is stored in the first project folder found and linked to the other project folders.'
GO
-- Documents without project links
PRINT ''
PRINT 'Documents without project links'
PRINT 'The following is an overview of the documents that are not linked to a project:'
PRINT 'Grouped by element type:'
select ENTITY_TYPE,COUNT(*) as Count from VIEW_ALL_DOCUMENT
	where AIMKEY not in (select PARENT_AIMKEY from XREF_ELEMENT where RELATIONSHIP_ID like 'AIM.XREF.DOC.PRO%')
	group by ENTITY_TYPE order by ENTITY_TYPE
PRINT 'Grouped by element type and file type:'
select ENTITY_TYPE,FILE_TYPE,COUNT(*) as Count from VIEW_ALL_DOCUMENT
	where AIMKEY not in (select PARENT_AIMKEY from XREF_ELEMENT where RELATIONSHIP_ID like 'AIM.XREF.DOC.PRO%')
	group by ENTITY_TYPE,FILE_TYPE order by ENTITY_TYPE,FILE_TYPE
PRINT 'Note: These documents will also appear later in the Vault in the PSP storage structure by year, month if you use the project-specific storage structure!'
GO

-- Documents by Element Type:
PRINT ''
PRINT 'Documents grouped by Element Type'
select ENTITY_TYPE, count(*) as Count from VIEW_ALL_DOCUMENT group by ENTITY_TYPE order by ENTITY_TYPE
GO

-- Documents by File Type
PRINT ''
PRINT 'Documents grouped by File Type'
select ENTITY_TYPE, FILE_TYPE, count(*) as Count from VIEW_ALL_DOCUMENT group by ENTITY_TYPE, FILE_TYPE order by ENTITY_TYPE, FILE_TYPE

-- Inventor Files (PSP Shell: #(DTYDocTypes:Ext0=IPT), #(DTYDocTypes:Ext0=IAM), ...)
PRINT ''
PRINT 'Number of Inventor files:'
select count(*) as Count from VIEW_ALL_DOCUMENT where FILE_TYPE in ('IPT', 'IPART', 'IPARTSTD', 'IPARTUSER', 'STDIPT', 'IPSKEL', 'IAM', 'IASSEMBLY', 'IASSEMBLYMEMBER', 'IPN', 'INVDWG', 'IDW') 
--  Inventor files without references
PRINT 'thereof Inventor files without references:'
select FILE_TYPE, count(*) as Count from VIEW_ALL_DOCUMENT where FILE_TYPE in ('IPT', 'IPART', 'IPARTSTD', 'IPARTUSER', 'STDIPT', 'IPSKEL', 'IAM', 'IASSEMBLY', 'IASSEMBLYMEMBER', 'IPN', 'INVDWG', 'IDW') 
	and AIMKEY not in (select PARENT_AIMKEY from XREF_ELEMENT where RELATIONSHIP_ID like 'AIM.XREF.DOC.ENG%') group by FILE_TYPE order by Count DESC
-- Inventor files without uses
PRINT 'thereof Inventor files without uses:'
select FILE_TYPE, count(*) as Count from VIEW_ALL_DOCUMENT where FILE_TYPE in ('IPT', 'IPART', 'IPARTSTD', 'IPARTUSER', 'STDIPT', 'IPSKEL', 'IAM', 'IASSEMBLY', 'IASSEMBLYMEMBER', 'IPN', 'INVDWG', 'IDW') 
	and AIMKEY not in (select CHILD_AIMKEY from XREF_ELEMENT where RELATIONSHIP_ID like 'AIM.XREF.DOC.ENG%') group by FILE_TYPE order by Count DESC
-- Top10 Inventor files with the most direct references
PRINT 'Top10 Inventor files with the most direct references:'
select TOP(10) IDENT, REVISION, count(*) as Count from VIEW_XREF_PARENT_DOCUMENT where X_RELATIONSHIP_ID in ('AIM.XREF.DOC.ENG') group by AIMKEY, IDENT, REVISION order by Count DESC
-- Top10 Inventor files with the most direct uses
PRINT 'Top10 Inventor files with the most direct uses:'
select TOP(10) IDENT, REVISION, count(*) as Count from VIEW_XREF_CHILD_DOCUMENT where X_RELATIONSHIP_ID in ('AIM.XREF.DOC.ENG') group by AIMKEY, IDENT, REVISION order by Count DESC

-- AutoCAD files (PSP Shell: #(DTYDocTypes:Ext0=DWG) without INVDWG))
PRINT ''
PRINT 'Number of AutoCAD files:'
select count(*) as Count from VIEW_ALL_DOCUMENT where FILE_TYPE in ('A', 'DLA', 'RI', 'AX', 'DL', 'PX')
-- AutoCAD Dateien mit Referenzen
PRINT 'thereof AutoCAD files without references:'
select count(*) as Count from VIEW_ALL_DOCUMENT where FILE_TYPE in ('A', 'DLA', 'RI', 'AX', 'DL', 'PX') and AIMKEY in (select PARENT_AIMKEY from XREF_ELEMENT where RELATIONSHIP_ID like 'AIM.XREF.DOC.ENG%')
--select IDENT, count(*) as Count from VIEW_ALL_DOCUMENT where FILE_TYPE in ('A', 'DLA', 'RI', 'AX', 'DL', 'PX')
--	and AIMKEY in (select PARENT_AIMKEY from XREF_ELEMENT where RELATIONSHIP_ID like 'AIM.XREF.DOC.ENG%') group by IDENT

-- Other engineering documents
PRINT ''
PRINT 'Number of other engineering documents:'
select count(*) as Count from VIEW_ALL_DOCUMENT where ENTITY_TYPE like 'AIM.DOC.ENG%' and FILE_TYPE not in
('IPT', 'IPART', 'IPARTSTD', 'IPARTUSER', 'STDIPT', 'IPSKEL', 'IAM', 'IASSEMBLY', 'IASSEMBLYMEMBER', 'IPN', 'INVDWG', 'IDW', 'A', 'DLA', 'RI', 'AX', 'DL', 'PX') 
GO

-- Status states used for documents
PRINT ''
PRINT 'Status states used for documents:'
select ENTITY_TYPE, STATUSKEY,COUNT(*) as Count, '' as 'State definition in Vault' from VIEW_ALL_DOCUMENT group by ENTITY_TYPE, STATUSKEY order by ENTITY_TYPE, STATUSKEY
IF exists(select STATUSKEY from VIEW_ALL_DOCUMENT where STATUSKEY not in ('00001','00002','00003','00004','00005','00006','00203','00301','00303','EXP01','EXP04'))
BEGIN
	PRINT 'Attention: Additional status definitions have been used compared to the standard. These must be examined more closely and assigned to the new statuses in Vault!'
	PRINT 'Standard status definitions for documents are: 00001, 00002, 00003, 00004, 00005, 00006 (for AIM.DOC.ENG) - 00203 (for AIM.DOC.ENG.STD) - 00301, 00303 (for AIM.DOC.OFF) and EXP01 und EXP04'
END
-- Document Categories by Element, Document Category, and Status
PRINT ''
PRINT 'Statuszustände gruppiert nach Element- und Dokumenttyp:'
select ENTITY_TYPE,FILE_TYPE,STATUSKEY,COUNT(*) as Count from VIEW_ALL_DOCUMENT group by ENTITY_TYPE,FILE_TYPE,STATUSKEY order by ENTITY_TYPE,FILE_TYPE,STATUSKEY
GO

-- Used revisions for documents
PRINT ''
PRINT 'Used revisions for documents:'
select ENTITY_TYPE, REVISION, count(*) as Count from VIEW_ALL_DOCUMENT group by ENTITY_TYPE, REVISION order by ENTITY_TYPE, REVISION
IF exists(select REVISION from VIEW_ALL_DOCUMENT where REVISION is null)
BEGIN
	PRINT 'Note: Document data records with "blank revision" exist. In Vault, however, there is no "blank revision", which is why it should be converted to a revision like "-" via the migration configuration.'
	PRINT 'This can be handled in the XML for the export at attribute VaultInitialRevision.'
END
GO

-- Documents with different FILE_TYPE
PRINT ''
PRINT 'Documents with different FILE_TYPE'
select count(*) as Count from VIEW_ALL_DOCUMENT where IDENT in (select IDENT from VIEW_ALL_DOCUMENT group by IDENT having COUNT(DISTINCT FILE_TYPE) > 1)
--select top 10 IDENT, COUNT(distinct FILE_TYPE) as Count from VIEW_ALL_DOCUMENT
--group by IDENT having COUNT(DISTINCT FILE_TYPE) > 1 order by IDENT
GO

-- Documents with FILE_LINKNAME
PRINT ''
PRINT 'Documents with FILE_LINKNAME'
select ENTITY_TYPE, count(*) as Count from VIEW_ALL_DOCUMENT where FILE_LINKNAME is not null group by ENTITY_TYPE
PRINT ''
PRINT 'Hull paths in FILE_LINKNAME not beginning with $(DATPATH) or #(INI:Inventor:StdPartPath_'
select substring(FILE_LINKNAME,0,20), count(*) as Count from VIEW_ALL_DOCUMENT 
	where FILE_LINKNAME is not null and FILE_LINKNAME not like '$(DATPATH)%' and FILE_LINKNAME not like '$DATPATH%' and FILE_LINKNAME not like '#(INI:Inventor:StdPartPath_%'
	group by substring(FILE_LINKNAME,0,20) order by substring(FILE_LINKNAME,0,20)
--select IDENT, FILE_LINKNAME from VIEW_ALL_DOCUMENT
--	where FILE_LINKNAME is not null and FILE_LINKNAME not like '$(DATPATH)%' and FILE_LINKNAME not like '$DATPATH%' and FILE_LINKNAME not like '#(INI:Inventor:StdPartPath_%'
--	order by IDENT
GO

-- Document Links
PRINT ''
PRINT 'Overview of Document Links:'
select RELATIONSHIP_ID, count(*) as Count from XREF_ELEMENT where RELATIONSHIP_ID in ('AIM.XREF.CONTACT.DOC','AIM.XREF.DOC.ENG','AIM.XREF.DOC.PART','AIM.XREF.DOC.PRO.ENG','AIM.XREF.DOC.PRO.OFF','AIM.XREF.DOC.SECONDARY') group by RELATIONSHIP_ID order by RELATIONSHIP_ID
GO

--	-- Linking documents to items
--	-- ==========================
--	PRINT ''
--	-- Dokumenttypen mit Artikeln verknüpft
--	PRINT 'Dokumenttypen mit Artikeln verknüpft'
--	select ENTITY_TYPE,FILE_TYPE, COUNT(*) as Anzahl from VIEW_ALL_DOCUMENT
--	where AIMKEY in (select PARENT_AIMKEY from XREF_ELEMENT where RELATIONSHIP_ID like 'AIM.XREF.DOC.PART%')
--	group by ENTITY_TYPE,FILE_TYPE order by ENTITY_TYPE,FILE_TYPE
--	-- Dokumenttypen nicht mit Artikeln verknüpft
--	PRINT 'Dokumenttypen nicht mit Artikeln verknüpft'
--	select ENTITY_TYPE,FILE_TYPE, COUNT(*) as Anzahl from VIEW_ALL_DOCUMENT
--	where AIMKEY not in (select PARENT_AIMKEY from XREF_ELEMENT where RELATIONSHIP_ID like 'AIM.XREF.DOC.PART%')
--	group by ENTITY_TYPE,FILE_TYPE order by ENTITY_TYPE,FILE_TYPE
--	GO

-- Evaluation of columns
IF exists(select * from VIEW_ALL_DOCUMENT)
BEGIN
	PRINT ''
	PRINT 'Using the Document Properties (in VIEW_ALL_DOCUMENT):'
	DECLARE @table NVARCHAR(128),
	  @table_id INT,
	  @column NVARCHAR(128),
	  @system_type_id int,
	  @zahl INT, 
	  @length INT,
	  @query NVARCHAR(128), 
	  @Param nvarchar(128)
	IF OBJECT_ID('tempdb..#cOTemp_Columns', 'U') IS NOT NULL DROP TABLE #cOTemp_Columns
	CREATE TABLE #cOTemp_Columns(cO_TABLE varchar(256),cO_COLUMN varchar(256),cO_ANZAHL varchar(50),cO_MAXLENGTH varchar(50))
	DECLARE mycursor CURSOR FOR   
	SELECT name, object_id FROM sys.views where name in ('VIEW_ALL_DOCUMENT')
	OPEN mycursor
	FETCH NEXT FROM mycursor INTO @table,@table_id
	WHILE @@FETCH_STATUS = 0
	BEGIN
	  DECLARE mycolumncursor CURSOR FOR   
	  SELECT name, system_type_id FROM sys.columns where object_id = @table_id order by name
	  OPEN mycolumncursor
	  FETCH NEXT FROM mycolumncursor INTO @column, @system_type_id
	  WHILE @@FETCH_STATUS = 0
	  BEGIN
	    SET @Param = N'@zahlOUT int OUTPUT';
	    SET @query = N'SELECT @zahlOUT = COUNT(*) from ' + @table + ' where ' + @column + ' is not null'
	    EXECUTE sp_executesql @query, @Param, @zahlOUT=@zahl OUTPUT;
	    if(@system_type_id not in (35,34))
		BEGIN
		  SET @Param = N'@zahlOUT int OUTPUT';
	      SET @query = N'SELECT @zahlOUT = MAX(LEN(' + @column + ')) from ' + @table + ' where ' + @column + ' is not null'
	      EXECUTE sp_executesql @query, @Param, @zahlOUT=@length OUTPUT;
	    END
		--PRINT @table + ' - ' + @column
		INSERT INTO #cOTemp_Columns VALUES(@table, @column, cast(@zahl as nvarchar(50)), cast(@length as nvarchar(50)))
	 
	     FETCH NEXT FROM mycolumncursor INTO @column, @system_type_id
	  END
	  CLOSE mycolumncursor
	  DEALLOCATE mycolumncursor
	  FETCH NEXT FROM mycursor INTO @table,@table_id
	END
	CLOSE mycursor
	DEALLOCATE mycursor
	SELECT cO_COLUMN as 'COLUMN', cO_ANZAHL as 'Count', cO_MAXLENGTH as 'MAXLENGTH', '' as 'Property in Vault' FROM #cOTemp_Columns order by cO_COLUMN
END
GO


-- Projects
-- ======================
PRINT ''
PRINT ''
PRINT 'Projects'
GO

IF exists(select * from VIEW_ALL_PROJECT)
BEGIN
	-- Gesamtanzahl
	PRINT ''
	PRINT 'Total number of project records:'
	select count(*) as Count from VIEW_ALL_PROJECT
	-- Verwendete Statuszustände für Projekte
	PRINT ''
	PRINT 'Status states used for projects'
	select STATUSKEY,COUNT(*) as Count, '' as 'Status definition in Vault' from VIEW_ALL_PROJECT group by STATUSKEY order by STATUSKEY
	IF exists(select STATUSKEY from VIEW_ALL_PROJECT where STATUSKEY not in ('00401','00402','00403'))
	BEGIN
		PRINT 'Attention: The status definitions used are additional to those used in the standard. These must be examined more closely and assigned to the new status states in Vault!'
		PRINT 'Standard Status definitionen for projects are: 00401, 00402 und 00403'
	END
	-- project links
	PRINT ''
	PRINT 'Overview of project links:'
	select RELATIONSHIP_ID, count(*) as Count from XREF_ELEMENT where RELATIONSHIP_ID in ('AIM.XREF.CONTACT.PRO','AIM.XREF.DOC.PRO.ENG','AIM.XREF.DOC.PRO.OFF','AIM.XREF.PART.PRO','AIM.XREF.PRO.PRO') group by RELATIONSHIP_ID order by RELATIONSHIP_ID
END
ELSE
	PRINT 'The database does not contain any projects!'
GO

-- Evaluation of columns
IF exists(select * from VIEW_ALL_PROJECT)
BEGIN
	PRINT ''
	PRINT 'Using the Project Properties (in VIEW_ALL_PROJECTS):'
	DECLARE @table NVARCHAR(128),
	  @table_id INT,
	  @column NVARCHAR(128),
	  @system_type_id int,
	  @zahl INT, 
	  @length INT,
	  @query NVARCHAR(128), 
	  @Param nvarchar(128)
	IF OBJECT_ID('tempdb..#cOTemp_Columns', 'U') IS NOT NULL DROP TABLE #cOTemp_Columns
	CREATE TABLE #cOTemp_Columns(cO_TABLE varchar(256),cO_COLUMN varchar(256),cO_ANZAHL varchar(50),cO_MAXLENGTH varchar(50))
	DECLARE mycursor CURSOR FOR   
	SELECT name, object_id FROM sys.views where name in ('VIEW_ALL_PROJECT')
	OPEN mycursor
	FETCH NEXT FROM mycursor INTO @table,@table_id
	WHILE @@FETCH_STATUS = 0
	BEGIN
	  DECLARE mycolumncursor CURSOR FOR   
	  SELECT name, system_type_id FROM sys.columns where object_id = @table_id order by name
	  OPEN mycolumncursor
	  FETCH NEXT FROM mycolumncursor INTO @column, @system_type_id
	  WHILE @@FETCH_STATUS = 0
	  BEGIN
	    SET @Param = N'@zahlOUT int OUTPUT';
	    SET @query = N'SELECT @zahlOUT = COUNT(*) from ' + @table + ' where ' + @column + ' is not null'
	    EXECUTE sp_executesql @query, @Param, @zahlOUT=@zahl OUTPUT;
	    if(@system_type_id not in (35,34))
		BEGIN
		  SET @Param = N'@zahlOUT int OUTPUT';
	      SET @query = N'SELECT @zahlOUT = MAX(LEN(' + @column + ')) from ' + @table + ' where ' + @column + ' is not null'
	      EXECUTE sp_executesql @query, @Param, @zahlOUT=@length OUTPUT;
	    END
		--PRINT @table + ' - ' + @column
		INSERT INTO #cOTemp_Columns VALUES(@table, @column, cast(@zahl as nvarchar(50)), cast(@length as nvarchar(50)))
	 
	     FETCH NEXT FROM mycolumncursor INTO @column, @system_type_id
	  END
	  CLOSE mycolumncursor
	  DEALLOCATE mycolumncursor
	  FETCH NEXT FROM mycursor INTO @table,@table_id
	END
	CLOSE mycursor
	DEALLOCATE mycursor
	SELECT cO_COLUMN as 'COLUMN', cO_ANZAHL as 'Count', cO_MAXLENGTH as 'MAXLENGTH', '' as 'Property in Vault' FROM #cOTemp_Columns order by cO_COLUMN
END
GO


-- Items
-- ======================
PRINT ''
PRINT ''
PRINT 'Items'
GO

IF exists(select * from VIEW_ALL_PART)
BEGIN
	-- Total number of article data records
	PRINT ''
	PRINT 'Total number of article data records:'
	select count(*) as Count from VIEW_ALL_PART
	PRINT 'of which with different article numbers:'
	select count(distinct IDENT) as Count from VIEW_ALL_PART
	if exists (select name from sys.tables where name = 'CLASS')
	BEGIN
		PRINT 'of which unclassified articles::'
		select count(*) as Count from VIEW_ALL_PART where AIMKEY not in (select ELEMENT_AIMKEY from VIEW_CRCLIST)
	END
	-- item categories
	PRINT ''
	PRINT 'used item categories:'
	select CATEGORY, count(*) as Count from VIEW_ALL_PART group by CATEGORY order by CATEGORY
	-- Status states used for items
	PRINT ''
	PRINT 'Status states used for items:'
	select STATUSKEY,COUNT(*) as Count, '' as 'Status definition in Vault' from VIEW_ALL_PART group by STATUSKEY order by STATUSKEY
	IF exists(select STATUSKEY from VIEW_ALL_PART where STATUSKEY not in ('00100','00103','00104','00105'))
	BEGIN
		PRINT 'Attention: The status definitions used are additional to those used in the standard. These must be examined more closely and assigned to the new status states in Vault!'
		PRINT 'Standard Status definitionen for items are: 00100, 00103, 00104 und 00105'
	END
	-- Revisions used for items
	PRINT ''
	PRINT 'Revisions used for items:'
	select REVISION, count(*) as Count from VIEW_ALL_PART group by REVISION order by REVISION
	IF exists(select REVISION from VIEW_ALL_PART where REVISION is null)
	BEGIN
		PRINT 'Note: There are article data records with "blank revision". In Vault, however, there is no "empty revision", which is why it should be converted to a revision like "-" via the migration configuration.'
	END
	-- item links
	PRINT ''
	PRINT 'Overview of item links:'
	select RELATIONSHIP_ID, count(*) as Count from XREF_ELEMENT where RELATIONSHIP_ID in ('AIM.XREF.DOC.PART','AIM.XREF.PART','AIM.XREF.PART.CLASS','AIM.XREF.PART.PRO') group by RELATIONSHIP_ID order by RELATIONSHIP_ID


	---- Linking items to documents
	---- =======================
	--PRINT ''
	---- Artikelkategorien mit Dokumenten verknüpft
	--PRINT 'Artikelkategorien mit Dokumenten verknüpft'
	--select CATEGORY,COUNT(*) as Anzahl from VIEW_ALL_PART
	--where AIMKEY in (select CHILD_AIMKEY from XREF_ELEMENT where RELATIONSHIP_ID like 'AIM.XREF.DOC.PART%')
	--group by CATEGORY order by CATEGORY
	---- Artikelkategorien nicht mit Dokumenten verknüpft
	--PRINT 'Artikelkategorien nicht mit Dokumenten verknüpft'
	--select CATEGORY,COUNT(*) as Anzahl from VIEW_ALL_PART
	--where AIMKEY not in (select CHILD_AIMKEY from XREF_ELEMENT where RELATIONSHIP_ID like 'AIM.XREF.DOC.PART%')
	--group by CATEGORY order by CATEGORY

END
ELSE
	PRINT 'The database does not contain any items!'
GO

-- Evaluation of columns
IF exists(select * from VIEW_ALL_PART)
BEGIN
	PRINT ''
	PRINT 'Using the Item Properties (in VIEW_ALL_PART):'
	DECLARE @table NVARCHAR(128),
	  @table_id INT,
	  @column NVARCHAR(128),
	  @system_type_id int,
	  @zahl INT, 
	  @length INT,
	  @query NVARCHAR(128), 
	  @Param nvarchar(128)
	IF OBJECT_ID('tempdb..#cOTemp_Columns', 'U') IS NOT NULL DROP TABLE #cOTemp_Columns
	CREATE TABLE #cOTemp_Columns(cO_TABLE varchar(256),cO_COLUMN varchar(256),cO_ANZAHL varchar(50),cO_MAXLENGTH varchar(50))
	DECLARE mycursor CURSOR FOR   
	SELECT name, object_id FROM sys.views where name in ('VIEW_ALL_PART')
	OPEN mycursor
	FETCH NEXT FROM mycursor INTO @table,@table_id
	WHILE @@FETCH_STATUS = 0
	BEGIN
	  DECLARE mycolumncursor CURSOR FOR   
	  SELECT name, system_type_id FROM sys.columns where object_id = @table_id order by name
	  OPEN mycolumncursor
	  FETCH NEXT FROM mycolumncursor INTO @column, @system_type_id
	  WHILE @@FETCH_STATUS = 0
	  BEGIN
	    SET @Param = N'@zahlOUT int OUTPUT';
	    SET @query = N'SELECT @zahlOUT = COUNT(*) from ' + @table + ' where ' + @column + ' is not null'
	    EXECUTE sp_executesql @query, @Param, @zahlOUT=@zahl OUTPUT;
	    if(@system_type_id not in (35,34))
		BEGIN
		  SET @Param = N'@zahlOUT int OUTPUT';
	      SET @query = N'SELECT @zahlOUT = MAX(LEN(' + @column + ')) from ' + @table + ' where ' + @column + ' is not null'
	      EXECUTE sp_executesql @query, @Param, @zahlOUT=@length OUTPUT;
	    END
		--PRINT @table + ' - ' + @column
		INSERT INTO #cOTemp_Columns VALUES(@table, @column, cast(@zahl as nvarchar(50)), cast(@length as nvarchar(50)))
	 
	     FETCH NEXT FROM mycolumncursor INTO @column, @system_type_id
	  END
	  CLOSE mycolumncursor
	  DEALLOCATE mycolumncursor
	  FETCH NEXT FROM mycursor INTO @table,@table_id
	END
	CLOSE mycursor
	DEALLOCATE mycursor
	SELECT cO_COLUMN as 'COLUMN', cO_ANZAHL as 'Count', cO_MAXLENGTH as 'MAXLENGTH', '' as 'Property in Vault' FROM #cOTemp_Columns order by cO_COLUMN
END
GO


-- Classes
-- ======================
if exists (select name from sys.tables where name = 'CLASS')
BEGIN
	PRINT ''
	PRINT ''
	PRINT 'Classes'
	-- total number
	PRINT ''
	PRINT 'total number Total number of class data records:'
	select count(*) as Count from VIEW_ALL_CLASS
	PRINT 'of which with different class numbers:'
	select count(distinct IDENT) as Count from VIEW_ALL_CLASS
	-- Using the Classes
	PRINT ''
	PRINT 'Using the Classes:'
	select IDENT, SHORT_DESC, count(*) as Count  from VIEW_XREF_CHILD_CLASS where X_RELATIONSHIP_ID = 'AIM.XREF.PART.CLASS' group by IDENT, SHORT_DESC order by IDENT
	-- Unused classes (not assigned to an item)
	PRINT ''
	PRINT 'Unused classes (not assigned to an item):'
	select IDENT, SHORT_DESC from VIEW_ALL_CLASS where AIMKEY not in (select CLASS_AIMKEY from VIEW_CRCLIST) and IDENT not in ('not classified')
	-- Verwendete Statuszustände für Klassen
	PRINT ''
	PRINT 'Status States Used for Classes:'
	select STATUSKEY,COUNT(*) as Count, '' as 'Status definition in Vault' from VIEW_ALL_CLASS group by STATUSKEY order by STATUSKEY
	IF exists(select STATUSKEY from VIEW_ALL_CLASS where STATUSKEY not in ('00001'))
	BEGIN
		PRINT 'Attention: Additional status definitions have been used compared to the standard. These must be examined more closely and assigned to the new statuses in Vault!'
		PRINT 'Standard status definitions for classes: 00001'
	END
	-- Used revisions for classes
	PRINT ''
	PRINT 'Used revisions for classes:'
	select REVISION, count(*) as Count from VIEW_ALL_CLASS group by REVISION order by REVISION
	IF exists(select REVISION from VIEW_ALL_CLASS where REVISION is null)
	BEGIN
		PRINT 'Note: Class data records with "Blank Revision" exist. In Vault, however, there is no "empty revision", which is why it should be converted to a revision like "-" via the migration configuration.'
	END
END
GO

-- Evaluation of columns
if exists (select name from sys.tables where name = 'CLASS')
BEGIN
	PRINT ''
	PRINT 'Using the Properties Classes (in VIEW_ALL_CLASS):'
	DECLARE @table NVARCHAR(128),
	  @table_id INT,
	  @column NVARCHAR(128),
	  @system_type_id int,
	  @zahl INT, 
	  @length INT,
	  @query NVARCHAR(128), 
	  @Param nvarchar(128)
	IF OBJECT_ID('tempdb..#cOTemp_Columns', 'U') IS NOT NULL DROP TABLE #cOTemp_Columns
	CREATE TABLE #cOTemp_Columns(cO_TABLE varchar(256),cO_COLUMN varchar(256),cO_ANZAHL varchar(50),cO_MAXLENGTH varchar(50))
	DECLARE mycursor CURSOR FOR   
	SELECT name, object_id FROM sys.views where name in ('VIEW_ALL_CLASS')
	OPEN mycursor
	FETCH NEXT FROM mycursor INTO @table,@table_id
	WHILE @@FETCH_STATUS = 0
	BEGIN
	  DECLARE mycolumncursor CURSOR FOR   
	  SELECT name, system_type_id FROM sys.columns where object_id = @table_id order by name
	  OPEN mycolumncursor
	  FETCH NEXT FROM mycolumncursor INTO @column, @system_type_id
	  WHILE @@FETCH_STATUS = 0
	  BEGIN
	    SET @Param = N'@zahlOUT int OUTPUT';
	    SET @query = N'SELECT @zahlOUT = COUNT(*) from ' + @table + ' where ' + @column + ' is not null'
	    EXECUTE sp_executesql @query, @Param, @zahlOUT=@zahl OUTPUT;
	    if(@system_type_id not in (35,34))
		BEGIN
		  SET @Param = N'@zahlOUT int OUTPUT';
	      SET @query = N'SELECT @zahlOUT = MAX(LEN(' + @column + ')) from ' + @table + ' where ' + @column + ' is not null'
	      EXECUTE sp_executesql @query, @Param, @zahlOUT=@length OUTPUT;
	    END
		--PRINT @table + ' - ' + @column
		INSERT INTO #cOTemp_Columns VALUES(@table, @column, cast(@zahl as nvarchar(50)), cast(@length as nvarchar(50)))
	 
	     FETCH NEXT FROM mycolumncursor INTO @column, @system_type_id
	  END
	  CLOSE mycolumncursor
	  DEALLOCATE mycolumncursor
	  FETCH NEXT FROM mycursor INTO @table,@table_id
	END
	CLOSE mycursor
	DEALLOCATE mycursor
	SELECT cO_COLUMN as 'COLUMN', cO_ANZAHL as 'Count', cO_MAXLENGTH as 'MAXLENGTH', '' as 'Property in Vault' FROM #cOTemp_Columns order by cO_COLUMN
END
GO


-- Sachmerkmale
-- ======================
if exists (select name from sys.tables where name = 'CLASSCRC')
BEGIN
	PRINT ''
	PRINT ''
	PRINT 'Sachmerkmale'
	-- Gesamtanzahl
	PRINT ''
	PRINT 'Gesamtanzahl der Sachmerkmale:'
	select count(*) as Count from VIEW_ALL_CLASSCRC
	-- Verwendung der Sachmerkmale
	PRINT ''
	PRINT 'Verwendung der Sachmerkmale in Klassen:'
	select IDENT, SHORT_DESC, count(*) as Count  from VIEW_XREF_CHILD_CLASSCRC where X_RELATIONSHIP_ID = 'AIM.XREF.CLASS.CLASSCRC' group by IDENT, SHORT_DESC order by IDENT
	-- Nicht verwendete Sachmerkmale (enthält keine Werte bei Artikelzuordnung)
	PRINT ''
	PRINT 'Nicht verwendete Sachmerkmale (enthält keine Werte bei Artikelzuordnung):'
	select IDENT, SHORT_DESC from VIEW_ALL_CLASSCRC where AIMKEY in (select CLASSCRC_AIMKEY from VIEW_CRCLIST where VALUE is null and CLASSCRC_AIMKEY not in (select CLASSCRC_AIMKEY from VIEW_CRCLIST where VALUE is not null group by CLASSCRC_AIMKEY) group by CLASSCRC_AIMKEY)
	-- Nicht verwendete Sachmerkmale (keinem Artikel zugeordnet)
	PRINT ''
	PRINT 'Nicht verwendete Sachmerkmale (keinem Artikel zugeordnet):'
	select IDENT, SHORT_DESC from VIEW_ALL_CLASSCRC where AIMKEY not in (select CLASSCRC_AIMKEY from VIEW_CRCLIST)
END
GO

-- Auswertung der Spalten
if exists (select name from sys.tables where name = 'CLASSCRC')
BEGIN
	PRINT ''
	PRINT 'Verwendung der Sachmerkmale Properties (in VIEW_ALL_CLASSCRC):'
	DECLARE @table NVARCHAR(128),
	  @table_id INT,
	  @column NVARCHAR(128),
	  @system_type_id int,
	  @zahl INT, 
	  @length INT,
	  @query NVARCHAR(128), 
	  @Param nvarchar(128)
	IF OBJECT_ID('tempdb..#cOTemp_Columns', 'U') IS NOT NULL DROP TABLE #cOTemp_Columns
	CREATE TABLE #cOTemp_Columns(cO_TABLE varchar(256),cO_COLUMN varchar(256),cO_ANZAHL varchar(50),cO_MAXLENGTH varchar(50))
	DECLARE mycursor CURSOR FOR   
	SELECT name, object_id FROM sys.views where name in ('VIEW_ALL_CLASSCRC')
	OPEN mycursor
	FETCH NEXT FROM mycursor INTO @table,@table_id
	WHILE @@FETCH_STATUS = 0
	BEGIN
	  DECLARE mycolumncursor CURSOR FOR   
	  SELECT name, system_type_id FROM sys.columns where object_id = @table_id order by name
	  OPEN mycolumncursor
	  FETCH NEXT FROM mycolumncursor INTO @column, @system_type_id
	  WHILE @@FETCH_STATUS = 0
	  BEGIN
	    SET @Param = N'@zahlOUT int OUTPUT';
	    SET @query = N'SELECT @zahlOUT = COUNT(*) from ' + @table + ' where ' + @column + ' is not null'
	    EXECUTE sp_executesql @query, @Param, @zahlOUT=@zahl OUTPUT;
	    if(@system_type_id not in (35,34))
		BEGIN
		  SET @Param = N'@zahlOUT int OUTPUT';
	      SET @query = N'SELECT @zahlOUT = MAX(LEN(' + @column + ')) from ' + @table + ' where ' + @column + ' is not null'
	      EXECUTE sp_executesql @query, @Param, @zahlOUT=@length OUTPUT;
	    END
		--PRINT @table + ' - ' + @column
		INSERT INTO #cOTemp_Columns VALUES(@table, @column, cast(@zahl as nvarchar(50)), cast(@length as nvarchar(50)))
	 
	     FETCH NEXT FROM mycolumncursor INTO @column, @system_type_id
	  END
	  CLOSE mycolumncursor
	  DEALLOCATE mycolumncursor
	  FETCH NEXT FROM mycursor INTO @table,@table_id
	END
	CLOSE mycursor
	DEALLOCATE mycursor
	SELECT cO_COLUMN as 'COLUMN', cO_ANZAHL as 'Count', cO_MAXLENGTH as 'MAXLENGTH', '' as 'Property in Vault' FROM #cOTemp_Columns order by cO_COLUMN
END
GO


-- Organisations
-- ======================
PRINT ''
PRINT ''
PRINT 'Organisations'
GO

IF exists(select * from VIEW_CONTACT_ORGANISATION)
BEGIN
	-- Total number
	PRINT ''
	PRINT 'Total number of organisation data records:'
	select count(*) as Count from VIEW_CONTACT_ORGANISATION
	-- Verwendete Statuszustände für Organisationen
	PRINT ''
	PRINT 'Used status states for organizations'
	select STATUSKEY,COUNT(*) as Count, '' as 'Status definition in Vault' from VIEW_CONTACT_ORGANISATION group by STATUSKEY order by STATUSKEY
	IF exists(select STATUSKEY from VIEW_CONTACT_ORGANISATION where STATUSKEY not in ('00001'))
	BEGIN
		PRINT 'Attention: Additional status definitions have been used compared to the standard. These must be examined more closely and assigned to the new statuses in Vault!'
		PRINT 'Standard Status definition for organizations: 00001'
	END
	-- Relationships for Organizations
	PRINT ''
	PRINT 'Overview of  Relationships for Organizations:'
	select RELATIONSHIP_ID, count(*) as Count from XREF_ELEMENT
		where (PARENT_AIMKEY in (select AIMKEY from VIEW_CONTACT_ORGANISATION) and RELATIONSHIP_ID in ('AIM.XREF.CONTACT.ADR','AIM.XREF.CONTACT.DOC','AIM.XREF.CONTACT.ORG','AIM.XREF.CONTACT.PRO'))
		or (CHILD_AIMKEY in (select AIMKEY from VIEW_CONTACT_ORGANISATION) and RELATIONSHIP_ID in ('AIM.XREF.CONTACT','AIM.XREF.CONTACT.ORG'))
		group by RELATIONSHIP_ID order by RELATIONSHIP_ID
END
ELSE
	PRINT 'The database does not contain any organizations!'
GO

-- Evaluation of columns
IF exists(select * from VIEW_CONTACT_ORGANISATION)
BEGIN
	PRINT ''
	PRINT 'Using the Organization Properties  (in VIEW_CONTACT_ORGANISATION):'
	DECLARE @table NVARCHAR(128),
	  @table_id INT,
	  @column NVARCHAR(128),
	  @system_type_id int,
	  @zahl INT, 
	  @length INT,
	  @query NVARCHAR(128), 
	  @Param nvarchar(128)
	IF OBJECT_ID('tempdb..#cOTemp_Columns', 'U') IS NOT NULL DROP TABLE #cOTemp_Columns
	CREATE TABLE #cOTemp_Columns(cO_TABLE varchar(256),cO_COLUMN varchar(256),cO_ANZAHL varchar(50),cO_MAXLENGTH varchar(50))
	DECLARE mycursor CURSOR FOR   
	SELECT name, object_id FROM sys.views where name in ('VIEW_CONTACT_ORGANISATION')
	OPEN mycursor
	FETCH NEXT FROM mycursor INTO @table,@table_id
	WHILE @@FETCH_STATUS = 0
	BEGIN
	  DECLARE mycolumncursor CURSOR FOR   
	  SELECT name, system_type_id FROM sys.columns where object_id = @table_id order by name
	  OPEN mycolumncursor
	  FETCH NEXT FROM mycolumncursor INTO @column, @system_type_id
	  WHILE @@FETCH_STATUS = 0
	  BEGIN
	    SET @Param = N'@zahlOUT int OUTPUT';
	    SET @query = N'SELECT @zahlOUT = COUNT(*) from ' + @table + ' where ' + @column + ' is not null'
	    EXECUTE sp_executesql @query, @Param, @zahlOUT=@zahl OUTPUT;
	    if(@system_type_id not in (35,34))
		BEGIN
		  SET @Param = N'@zahlOUT int OUTPUT';
	      SET @query = N'SELECT @zahlOUT = MAX(LEN(' + @column + ')) from ' + @table + ' where ' + @column + ' is not null'
	      EXECUTE sp_executesql @query, @Param, @zahlOUT=@length OUTPUT;
	    END
		--PRINT @table + ' - ' + @column
		INSERT INTO #cOTemp_Columns VALUES(@table, @column, cast(@zahl as nvarchar(50)), cast(@length as nvarchar(50)))
	 
	     FETCH NEXT FROM mycolumncursor INTO @column, @system_type_id
	  END
	  CLOSE mycolumncursor
	  DEALLOCATE mycolumncursor
	  FETCH NEXT FROM mycursor INTO @table,@table_id
	END
	CLOSE mycursor
	DEALLOCATE mycursor
	SELECT cO_COLUMN as 'COLUMN', cO_ANZAHL as 'Count', cO_MAXLENGTH as 'MAXLENGTH', '' as 'Property in Vault' FROM #cOTemp_Columns order by cO_COLUMN
END
GO


-- Persons
-- ======================
PRINT ''
PRINT ''
PRINT 'Persons'
GO

IF exists(select * from VIEW_CONTACT_PERSON)
BEGIN
	-- Total number of data records for persons
	PRINT ''
	PRINT 'Total number of data records for persons:'
	select count(*) as Count from VIEW_CONTACT_PERSON
	-- Status states used
	PRINT ''
	PRINT 'Status states used for persons'
	select STATUSKEY,COUNT(*) as Count, '' as 'Status definition in Vault' from VIEW_CONTACT_PERSON group by STATUSKEY order by STATUSKEY
	IF exists(select STATUSKEY from VIEW_CONTACT_PERSON where STATUSKEY not in ('00001'))
	BEGIN
		PRINT 'Attention: Additional status definitions have been used compared to the standard. These must be examined more closely and assigned to the new statuses in Vault!'
		PRINT 'Standard Status definition for persons: 00001'
	END
	-- person links
	PRINT ''
	PRINT 'Overview of person links:'
	select RELATIONSHIP_ID, count(*) as Count from XREF_ELEMENT
		where (PARENT_AIMKEY in (select AIMKEY from VIEW_CONTACT_PERSON) and RELATIONSHIP_ID in ('AIM.XREF.CONTACT','AIM.XREF.CONTACT.ADR','AIM.XREF.CONTACT.DOC','AIM.XREF.CONTACT.PERSON','AIM.XREF.CONTACT.PRO'))
		or (CHILD_AIMKEY in (select AIMKEY from VIEW_CONTACT_PERSON) and RELATIONSHIP_ID in ('AIM.XREF.CONTACT.PERSON'))
		group by RELATIONSHIP_ID order by RELATIONSHIP_ID
END
ELSE
	PRINT 'The database does not contain any persons!'
GO

-- Evaluation of columns
IF exists(select * from VIEW_CONTACT_PERSON)
BEGIN
	PRINT ''
	PRINT 'Use of the Person Properties (in VIEW_CONTACT_PERSON):'
	DECLARE @table NVARCHAR(128),
	  @table_id INT,
	  @column NVARCHAR(128),
	  @system_type_id int,
	  @zahl INT, 
	  @length INT,
	  @query NVARCHAR(128), 
	  @Param nvarchar(128)
	IF OBJECT_ID('tempdb..#cOTemp_Columns', 'U') IS NOT NULL DROP TABLE #cOTemp_Columns
	CREATE TABLE #cOTemp_Columns(cO_TABLE varchar(256),cO_COLUMN varchar(256),cO_ANZAHL varchar(50),cO_MAXLENGTH varchar(50))
	DECLARE mycursor CURSOR FOR   
	SELECT name, object_id FROM sys.views where name in ('VIEW_CONTACT_PERSON')
	OPEN mycursor
	FETCH NEXT FROM mycursor INTO @table,@table_id
	WHILE @@FETCH_STATUS = 0
	BEGIN
	  DECLARE mycolumncursor CURSOR FOR   
	  SELECT name, system_type_id FROM sys.columns where object_id = @table_id order by name
	  OPEN mycolumncursor
	  FETCH NEXT FROM mycolumncursor INTO @column, @system_type_id
	  WHILE @@FETCH_STATUS = 0
	  BEGIN
	    SET @Param = N'@zahlOUT int OUTPUT';
	    SET @query = N'SELECT @zahlOUT = COUNT(*) from ' + @table + ' where ' + @column + ' is not null'
	    EXECUTE sp_executesql @query, @Param, @zahlOUT=@zahl OUTPUT;
	    if(@system_type_id not in (35,34))
		BEGIN
		  SET @Param = N'@zahlOUT int OUTPUT';
	      SET @query = N'SELECT @zahlOUT = MAX(LEN(' + @column + ')) from ' + @table + ' where ' + @column + ' is not null'
	      EXECUTE sp_executesql @query, @Param, @zahlOUT=@length OUTPUT;
	    END
		--PRINT @table + ' - ' + @column
		INSERT INTO #cOTemp_Columns VALUES(@table, @column, cast(@zahl as nvarchar(50)), cast(@length as nvarchar(50)))
	 
	     FETCH NEXT FROM mycolumncursor INTO @column, @system_type_id
	  END
	  CLOSE mycolumncursor
	  DEALLOCATE mycolumncursor
	  FETCH NEXT FROM mycursor INTO @table,@table_id
	END
	CLOSE mycursor
	DEALLOCATE mycursor
	SELECT cO_COLUMN as 'COLUMN', cO_ANZAHL as 'Count', cO_MAXLENGTH as 'MAXLENGTH', '' as 'Property in Vault' FROM #cOTemp_Columns order by cO_COLUMN
END
GO


-- Adressen
-- ======================
PRINT ''
PRINT ''
PRINT 'Adressen'
GO

IF exists(select * from VIEW_ALL_ADDRESS)
BEGIN
	-- Total number
	PRINT ''
	PRINT 'Total number of address records:'
	select count(*) as Count from VIEW_ALL_ADDRESS
	-- Status states used for addresses
	PRINT ''
	PRINT 'Status states used for addresses'
	select STATUSKEY,COUNT(*) as Count, '' as 'Status definition in Vault' from VIEW_ALL_ADDRESS group by STATUSKEY order by STATUSKEY
	IF exists(select STATUSKEY from VIEW_ALL_ADDRESS where STATUSKEY not in ('00001'))
	BEGIN
		PRINT 'Attention: Additional status definitions have been used compared to the standard. These must be examined more closely and assigned to the new statuses in Vault!'
		PRINT 'Standard Status definition for addresses: 00001'
	END
	-- Overview of address links
	PRINT ''
	PRINT 'Overview of address links:'
	select RELATIONSHIP_ID, count(*) as Count from XREF_ELEMENT where RELATIONSHIP_ID in ('AIM.XREF.CONTACT.ADR') group by RELATIONSHIP_ID order by RELATIONSHIP_ID
END
ELSE
	PRINT 'The database does not contain any addresses!'
GO

-- Evaluation of columns
IF exists(select * from VIEW_ALL_ADDRESS)
BEGIN
	PRINT ''
	PRINT 'Using the Address Properties (in VIEW_ALL_ADDRESS):'
	DECLARE @table NVARCHAR(128),
	  @table_id INT,
	  @column NVARCHAR(128),
	  @system_type_id int,
	  @zahl INT, 
	  @length INT,
	  @query NVARCHAR(128), 
	  @Param nvarchar(128)
	IF OBJECT_ID('tempdb..#cOTemp_Columns', 'U') IS NOT NULL DROP TABLE #cOTemp_Columns
	CREATE TABLE #cOTemp_Columns(cO_TABLE varchar(256),cO_COLUMN varchar(256),cO_ANZAHL varchar(50),cO_MAXLENGTH varchar(50))
	DECLARE mycursor CURSOR FOR   
	SELECT name, object_id FROM sys.views where name in ('VIEW_ALL_ADDRESS')
	OPEN mycursor
	FETCH NEXT FROM mycursor INTO @table,@table_id
	WHILE @@FETCH_STATUS = 0
	BEGIN
	  DECLARE mycolumncursor CURSOR FOR   
	  SELECT name, system_type_id FROM sys.columns where object_id = @table_id order by name
	  OPEN mycolumncursor
	  FETCH NEXT FROM mycolumncursor INTO @column, @system_type_id
	  WHILE @@FETCH_STATUS = 0
	  BEGIN
	    SET @Param = N'@zahlOUT int OUTPUT';
	    SET @query = N'SELECT @zahlOUT = COUNT(*) from ' + @table + ' where ' + @column + ' is not null'
	    EXECUTE sp_executesql @query, @Param, @zahlOUT=@zahl OUTPUT;
	    if(@system_type_id not in (35,34))
		BEGIN
		  SET @Param = N'@zahlOUT int OUTPUT';
	      SET @query = N'SELECT @zahlOUT = MAX(LEN(' + @column + ')) from ' + @table + ' where ' + @column + ' is not null'
	      EXECUTE sp_executesql @query, @Param, @zahlOUT=@length OUTPUT;
	    END
		--PRINT @table + ' - ' + @column
		INSERT INTO #cOTemp_Columns VALUES(@table, @column, cast(@zahl as nvarchar(50)), cast(@length as nvarchar(50)))
	 
	     FETCH NEXT FROM mycolumncursor INTO @column, @system_type_id
	  END
	  CLOSE mycolumncursor
	  DEALLOCATE mycolumncursor
	  FETCH NEXT FROM mycursor INTO @table,@table_id
	END
	CLOSE mycursor
	DEALLOCATE mycursor
	SELECT cO_COLUMN as 'COLUMN', cO_ANZAHL as 'Count', cO_MAXLENGTH as 'MAXLENGTH', '' as 'Property in Vault' FROM #cOTemp_Columns order by cO_COLUMN
END
GO