<Query Kind="SQL">
  <Connection>
    <ID>d01849d0-bc0d-4d5c-bd00-370fe31ee070</ID>
    <Persist>true</Persist>
    <Server>PSP-SERVER-DE</Server>
    <SqlSecurity>true</SqlSecurity>
    <UserName>sa</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA7cU9w3PGkkOaS/X3PaJIzgAAAAACAAAAAAAQZgAAAAEAACAAAADTJdCtSuHKTQZs368CHDYT0o+BmvvnXJxXf4OW+4rtrQAAAAAOgAAAAAIAACAAAABapkK+RNdNBYnpmo33vPKh5aYC343df08XkKib6zw9WhAAAACyziWENDwtQ/zp8useUhw2QAAAALjFGnK4ZYfIP0sfv4vmGt/AFsrBWg26kuT3YKmufp7uO5ER21pB1+7WtazkXwelxJQbhVxZ8dNEWeRQ0IPix8g=</Password>
    <Database>professional_pro_cO</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

-- Allgemeine Prüfungen zur schnellen Übersicht
declare @company varchar(64)
declare @product varchar(10)
declare @version varchar(10)

select @company=COMPANY, @product=PRODUCT, @version=VERSION from AIMKEY
PRINT 'Analyse für PSP-Datenbank der Firma ' + @company
PRINT 'Produkt: ' + @product
PRINT 'Version:' + @version
GO

-- Allgemeines
-- =========================
PRINT ''
PRINT 'Allgemeines'
GO

-- Prüfung auf neue Tabellen
-- =========================
PRINT ''
PRINT 'Prüfung auf neue Tabellen'
if exists(select name as Tabelle from sys.tables 
	where name not in ('ADDRESS','AIM_IMPORT_DML','AIM_TABLE_COLUMNS','AIM_XREF_IMPORT_DML','AIMKEY','CMP_LOG','CMP_USER','CONFIGURATION2','CONTACT','DML_LOG','DML_TEMP','DOCUMENT','ELEMENT','ENTITY_TYPE','HISTORY_DOCUMENT','HISTORY_PART','JOBSERVER','KEYGEN_TEMP','NAV_STRUCT_CACHE','PART','PROJECT','RELATIONSHIP_TYPE','STL_TMP','SWP','USER_GROUP','XREF_ELEMENT','XREF_USER','LOCATION','REPL_DOCUMENT','REPLICATORQ','CLASS','CLASSCRC','CRCLIST','HISTORY_CLASS','XREFCRCLIST','INVDOCANALYZER13')
		and name not like 'AIM_EXP_%' and name not like 'AIM_HEXP_%' and name not like 'AIM_XEXP_%')
BEGIN
	PRINT 'Die Datenbank enthält gegenüber dem Standard folgende zusätzliche Tabellen:'
	select name as Tabelle from sys.tables 
		where name not in ('ADDRESS','AIM_IMPORT_DML','AIM_TABLE_COLUMNS','AIM_XREF_IMPORT_DML','AIMKEY','CMP_LOG','CMP_USER','CONFIGURATION2','CONTACT','DML_LOG','DML_TEMP','DOCUMENT','ELEMENT','ENTITY_TYPE','HISTORY_DOCUMENT','HISTORY_PART','JOBSERVER','KEYGEN_TEMP','NAV_STRUCT_CACHE','PART','PROJECT','RELATIONSHIP_TYPE','STL_TMP','SWP','USER_GROUP','XREF_ELEMENT','XREF_USER','LOCATION','REPL_DOCUMENT','REPLICATORQ','CLASS','CLASSCRC','CRCLIST','HISTORY_CLASS','XREFCRCLIST','INVDOCANALYZER13')
			and name not like 'AIM_EXP_%' and name not like 'AIM_HEXP_%' and name not like 'AIM_XEXP_%'
		order by name
	PRINT 'Hinweis: Die zusätzlichen Tabellen sind hinsichtlich der Relevanz für die Datenübernahme genauer zu untersuchen und deren Bedeutung ist mit dem Kunden zu klären!'
END
else
	PRINT 'Die Datenbank enthält keine zusätzlichen Tabellen gegenüber dem Standard!'
GO

-- Prüfung auf neue Elementtypen
-- ======================
PRINT ''
PRINT 'Prüfung auf neue Elementtypen'
IF exists(select TYPE from ENTITY_TYPE where TYPE not in ('AIM','AIM.ADR','AIM.CLASS','AIM.CLASSCRC','AIM.CONTACT','AIM.CONTACT.ORG','AIM.CONTACT.PERSON','AIM.DOC','AIM.DOC.ENG','AIM.DOC.ENG.GEN','AIM.DOC.ENG.GEN.CAB','AIM.DOC.ENG.GEN.FRM','AIM.DOC.ENG.GEN.FUN','AIM.DOC.ENG.GEN.PIP','AIM.DOC.ENG.STD','AIM.DOC.OFF','AIM.DOC.SECONDARY','AIM.FAVORIT','AIM.FOLDER','AIM.FOLDER.DETAIL','AIM.FOLDER.IMPORT','AIM.FOLDER.ROOT','AIM.JOBSERVER.EXEC','AIM.JOBSERVER.TYPE','AIM.PART','AIM.PRO','AIM.SECTION','AIM.SECTION.ROOT','AIM.LOCATION'))
BEGIN
	PRINT 'Die Datenbank enthält gegenüber dem Standard folgende zusätzlichen Elementtypen:'
	select TYPE, DESCRIPTION from ENTITY_TYPE where TYPE not in ('AIM','AIM.ADR','AIM.CLASS','AIM.CLASSCRC','AIM.CONTACT','AIM.CONTACT.ORG','AIM.CONTACT.PERSON','AIM.DOC','AIM.DOC.ENG','AIM.DOC.ENG.GEN','AIM.DOC.ENG.GEN.CAB','AIM.DOC.ENG.GEN.FRM','AIM.DOC.ENG.GEN.FUN','AIM.DOC.ENG.GEN.PIP','AIM.DOC.ENG.STD','AIM.DOC.OFF','AIM.DOC.SECONDARY','AIM.FAVORIT','AIM.FOLDER','AIM.FOLDER.DETAIL','AIM.FOLDER.IMPORT','AIM.FOLDER.ROOT','AIM.JOBSERVER.EXEC','AIM.JOBSERVER.TYPE','AIM.PART','AIM.PRO','AIM.SECTION','AIM.SECTION.ROOT','AIM.LOCATION')
		order by TYPE
	PRINT 'Hinweis: Die zusätzlichen Elementtypen sind hinsichtlich der Relevanz für die Datenübernahme genauer zu untersuchen und deren Bedeutung ist mit dem Kunden zu klären!'
END
ELSE
	PRINT 'Die Datenbank enthält keine zusätzlichen Elementtypen gegenüber dem Standard!'
GO

-- Prüfung auf neue Verknüpfungstypen
-- ======================
PRINT ''
PRINT 'Prüfung auf neue Verknüpfungstypen'
IF exists(select RELATIONSHIP_ID from RELATIONSHIP_TYPE where RELATIONSHIP_ID not in ('AIM.XREF.CLASS','AIM.XREF.CLASS.CLASSCRC','AIM.XREF.CONTACT','AIM.XREF.CONTACT.ADR','AIM.XREF.CONTACT.DOC','AIM.XREF.CONTACT.ORG','AIM.XREF.CONTACT.PERSON','AIM.XREF.CONTACT.PRO','AIM.XREF.DOC.ENG','AIM.XREF.DOC.PART','AIM.XREF.DOC.PRO.ENG','AIM.XREF.DOC.PRO.OFF','AIM.XREF.DOC.SECONDARY','AIM.XREF.FAVORIT','AIM.XREF.FOLDER','AIM.XREF.PART','AIM.XREF.PART.CLASS','AIM.XREF.PART.PRO','AIM.XREF.PRO.PRO','AIM.XREF.ROOT.SECTION','AIM.XREF.SECTION','AIM.XREF.SECTION.FAVORIT','AIM.XREF.SECTION.FOLDER','AIM.XREF.DOC.LOCATION'))
BEGIN
	PRINT 'Die Datenbank enthält gegenüber dem Standard folgende zusätzlichen Verknüpfungstypen:'
	select RELATIONSHIP_ID, DESCRIPTION, PARENT_ENTITY, CHILD_ENTITY from RELATIONSHIP_TYPE where RELATIONSHIP_ID not in ('AIM.XREF.CLASS','AIM.XREF.CLASS.CLASSCRC','AIM.XREF.CONTACT','AIM.XREF.CONTACT.ADR','AIM.XREF.CONTACT.DOC','AIM.XREF.CONTACT.ORG','AIM.XREF.CONTACT.PERSON','AIM.XREF.CONTACT.PRO','AIM.XREF.DOC.ENG','AIM.XREF.DOC.PART','AIM.XREF.DOC.PRO.ENG','AIM.XREF.DOC.PRO.OFF','AIM.XREF.DOC.SECONDARY','AIM.XREF.FAVORIT','AIM.XREF.FOLDER','AIM.XREF.PART','AIM.XREF.PART.CLASS','AIM.XREF.PART.PRO','AIM.XREF.PRO.PRO','AIM.XREF.ROOT.SECTION','AIM.XREF.SECTION','AIM.XREF.SECTION.FAVORIT','AIM.XREF.SECTION.FOLDER','AIM.XREF.DOC.LOCATION')
		order by RELATIONSHIP_ID
	PRINT 'Hinweis: Die zusätzlichen Verknüpfungstypen sind hinsichtlich der Relevanz für die Datenübernahme genauer zu untersuchen und deren Bedeutung ist mit dem Kunden zu klären!'
END
ELSE
	PRINT 'Die Datenbank enthält keine zusätzlichen Verknüpfungstypen gegenüber dem Standard!'
GO

-- Prüfung auf Elemente im Papierkorb
-- ======================
PRINT ''
PRINT 'Prüfung auf Elemente im Papierkorb'
IF exists(select * from ELEMENT where DELETE_INITIATOR is not null)
BEGIN
	PRINT 'Es sind folgende Elemente im Papierkorb:'
	select ENTITY_TYPE, DELETE_INITIATOR, count(*) as Anzahl from ELEMENT where DELETE_INITIATOR is not null group by ENTITY_TYPE, DELETE_INITIATOR order by ENTITY_TYPE, DELETE_INITIATOR
	PRINT 'Hinweis: Der Papierkorb sollte vor der Datenübernahme nach Vault geleert werden!'
END
ELSE
	PRINT 'Es sind aktuell keine Elemente im Papierkorb!'
GO

-- Prüfung auf Replikation
-- =========================
PRINT ''
PRINT 'Prüfung auf Replikation'
if exists (select name from sys.tables where name = 'LOCATION')
BEGIN
	PRINT 'Die Datenbank enthält eine Replikation mit folgenden Standorten:'
	SELECT IDENT, PUBLISHER, DB_SERVERNAME, DB_DATABASE from LOCATION
	PRINT 'Hinweis: Die Relevanz der Replikation ist für die Datenübernahme zu klären!'
	PRINT 'Es müssen auf jeden Fall alle nach Vault zu übernehmenden Dateien im Original an dem Standort liegen, an dem die Datenübernahme durchgeführt wird!'
	PRINT ''
	PRINT 'Nachfolgend eine Übersicht der Originalstandorte'
	SELECT FILE_ORIG_AT, FILE_ORIG_TO, count(*) as Anzahl from VIEW_ALL_DOCUMENT group by FILE_ORIG_AT, FILE_ORIG_TO order by FILE_ORIG_AT, FILE_ORIG_TO
END
else
	PRINT 'Die Datenbank enthält keine Replikation!'
GO

-- Prüfung auf Sachmerkmale
-- =========================
PRINT ''
PRINT 'Prüfung auf Sachmerkmale'
if exists (select name from sys.tables where name = 'CLASS')
BEGIN
	PRINT 'Die Datenbank enthält eine installierte Sachmerkmalleiste mit folgenden Einträgen:'
	select ENTITY_TYPE, count(*) as Anzahl from ELEMENT where ENTITY_TYPE in ('AIM.CLASS','AIM.CLASSCRC') group by ENTITY_TYPE order by ENTITY_TYPE
	PRINT 'und folgenden Verknüpfungen:'
	select RELATIONSHIP_ID, count(*) as Anzahl from XREF_ELEMENT where RELATIONSHIP_ID in ('AIM.XREF.CLASS','AIM.XREF.CLASS.CLASSCRC','AIM.XREF.PART.CLASS') group by RELATIONSHIP_ID order by RELATIONSHIP_ID
	PRINT 'Hinweis: Die Relevanz der Replikation ist für die Datenübernahme zu klären!'
END
else
BEGIN
	PRINT 'Die Datenbank enthält keine Sachmerkmale!'
END
GO


-- Detailanalyse Allgemein
-- ======================
PRINT ''
PRINT ''
PRINT 'Detailanalyse Allgemein'
GO

-- Übersicht Elementtypen
-- ======================
PRINT ''
PRINT 'Übersicht Elementtypen'
-- Verwendete Elementtypen
PRINT 'Folgende Elementtypen wurden verwendet:'
select ENTITY_TYPE, count(*) as Anzahl, '' as 'Übernahme ja/nein' from ELEMENT where ENTITY_TYPE not in ('AIM.FAVORIT','AIM.FOLDER','AIM.FOLDER.DETAIL','AIM.FOLDER.IMPORT','AIM.FOLDER.ROOT','AIM.SECTION','AIM.SECTION.ROOT') group by ENTITY_TYPE order by ENTITY_TYPE
PRINT 'Hinweis: Hier kann definiert werden, welche Elemente nach Vault übernommen werden sollen!'
GO

-- Übersicht Verknüpfungen
-- =======================
PRINT ''
PRINT 'Übersicht Verknüpfungen'
-- Verwendete Verknüpfungstypen
PRINT 'Folgende Verknüpfungen wurden verwendet:'
select RELATIONSHIP_ID, count(*) as Anzahl, '' as 'Übernahme ja/nein' from XREF_ELEMENT where RELATIONSHIP_ID not in ('AIM.XREF.FAVORIT','AIM.XREF.FOLDER','AIM.XREF.ROOT.SECTION','AIM.XREF.SECTION','AIM.XREF.SECTION.FAVORIT','AIM.XREF.SECTION.FOLDER') group by RELATIONSHIP_ID order by RELATIONSHIP_ID
PRINT 'Hinweis: Hier kann definiert werden, welche Elemente nach Vault übernommen werden sollen!'
GO

-- Übersicht User
-- ==============
PRINT ''
PRINT 'Übersicht User'
PRINT 'Es wird empfohlen die User und Gruppen vor der Datenmigration manuell in Vault anzulegen. Die bereits angelegten Vault User werden dann bei der Datenmigration verwendet.
Das Data Export Utility sollte so konfiguriert werden, dass während dem Import der Daten in Vault keine „unbekannten“ User angelegt werden.
User die in Vault einmal angelegt werden und für Projekte, Artikel oder Dateien verwendet werden, können nicht mehr gelöscht werden.
Das heißt die Benutzer in den entsprechenden PSP-User-Feldern, die nicht gleichlautend als Vault User existieren, sollten korrigiert werden.'
PRINT ''
PRINT 'Achtung: Falls Benutzer in einem Schriftfeld gemappt sind ist ein Korrektur unter Umständen nicht möglich!'

-- Liste der PSP User
PRINT ''
PRINT 'Folgende User sind in PSP definiert:'
select USERID, NAME, '' as 'Username in Vault' from CMP_USER order by USERID
PRINT 'Hinweis: Es werden ggf. nicht alle oben gelisteten Benutzer tatsächlich in PSP verwendet!
Deshalb genügt es meistens, die in den nachfolgenden Tabellen aufgelisteten Benutzernamen zu mappen.'
-- Übersicht CREATE_USER
PRINT ''
PRINT 'Verwendete Benutzer in ELEMENT.CREATE_USER (gilt für alle Elementtypen)'
select CREATE_USER, count(*) as Anzahl, '' as 'Username in Vault' from ELEMENT group by CREATE_USER order by CREATE_USER
-- Übersicht CHANGE_USER
PRINT ''
PRINT 'Verwendete Benutzer in ELEMENT.CHANGE_USER (gilt für alle Elementtypen)'
select CHANGE_USER, count(*) as Anzahl, '' as 'Username in Vault' from ELEMENT group by CHANGE_USER order by CHANGE_USER
-- Übersicht USERNAME
PRINT ''
PRINT 'Verwendete Benutzer in ELEMENT.USERNAME (gilt für alle Elementtypen)'
select USERNAME, count(*) as Anzahl, '' as 'Username in Vault' from ELEMENT group by USERNAME order by USERNAME
-- Übersicht CHECKED_1_BY
PRINT ''
PRINT 'Verwendete Benutzer in ELEMENT.CHECKED_1_BY (gilt für alle Elementtypen)'
select CHECKED_1_BY, count(*) as Anzahl, '' as 'Username in Vault' from ELEMENT group by CHECKED_1_BY order by CHECKED_1_BY
-- Übersicht CHANGE_USER in HISTORY_DOCUMENT
PRINT ''
PRINT 'Verwendete Benutzer in HISTORY_DOCUMENT.CHANGE_USER (gilt nur für Dokumente)'
select CHANGE_USER, count(*) as Anzahl, '' as 'Username in Vault' from HISTORY_DOCUMENT group by CHANGE_USER order by CHANGE_USER
-- Übersicht CHECK_USER in HISTORY_DOCUMENT
PRINT ''
PRINT 'Verwendete Benutzer in HISTORY_DOCUMENT.CHECK_USER (gilt nur für Dokumente)'
select CHECK_USER, count(*) as Anzahl, '' as 'Username in Vault' from HISTORY_DOCUMENT group by CHECK_USER order by CHECK_USER
-- Übersicht CHANGE_USER in HISTORY_PART
IF exists(select * from HISTORY_PART)
BEGIN
	PRINT ''
	PRINT 'Verwendete Benutzer in HISTORY_PART.CHANGE_USER (gilt nur für Dokumente)'
	select CHANGE_USER, count(*) as Anzahl, '' as 'Username in Vault' from HISTORY_PART group by CHANGE_USER order by CHANGE_USER
END
-- Übersicht CHECK_USER in HISTORY_PART
IF exists(select * from HISTORY_PART)
BEGIN
	PRINT ''
	PRINT 'Verwendete Benutzer in HISTORY_PART.CHECK_USER (gilt nur für Dokumente)'
	select CHECK_USER, count(*) as Anzahl, '' as 'Username in Vault' from HISTORY_PART group by CHECK_USER order by CHECK_USER
END

-- Dokumente
-- ======================
PRINT ''
PRINT ''
PRINT 'Dokumente'
GO

-- Gesamtanzahl
PRINT ''
PRINT 'Gesamtanzahl der Dokumentdatensätze:'
select count(*) as Anzahl from VIEW_ALL_DOCUMENT
PRINT 'davon mit unterschiedlichen Dokumentnummern:'
select count(distinct IDENT) as Anzahl from VIEW_ALL_DOCUMENT
GO

-- Überlegungen zur Ablagestruktur in Vault
PRINT ''
PRINT 'Ablagestruktur in Vault'
PRINT 'Die Dokumente können standardmäßig entweder projektspezifisch oder in der PSP-Ablagestruktur nach Jahr, Monat nach Vault übertragen werden.'
-- Dokumenttypen mit Projekt verknüpft
PRINT 'Dokumente mit Projektverknüpfung'
PRINT 'Als Entscheidungsgrundlage, nachfolgend eine Übersicht der Dokumente, welche mit mindestens einem Projekt verknüpft sind:'
PRINT 'Gruppiert nach Elementtyp:'
select ENTITY_TYPE,COUNT(*) as Anzahl from VIEW_ALL_DOCUMENT
	where AIMKEY in (select PARENT_AIMKEY from XREF_ELEMENT where RELATIONSHIP_ID like 'AIM.XREF.DOC.PRO%')
	group by ENTITY_TYPE order by ENTITY_TYPE
PRINT 'Gruppiert nach Elementtyp und Dateityp:'
select ENTITY_TYPE,FILE_TYPE,COUNT(*) as Anzahl from VIEW_ALL_DOCUMENT
	where AIMKEY in (select PARENT_AIMKEY from XREF_ELEMENT where RELATIONSHIP_ID like 'AIM.XREF.DOC.PRO%')
	group by ENTITY_TYPE,FILE_TYPE order by ENTITY_TYPE,FILE_TYPE
PRINT 'Hinweis: Diese Dokumente werden bei Verwendung der projektspezifischen Ablagestruktur später im Vault in einem Projektordner abgelegt!'
PRINT 'Bei Mehrfachverknüpfung eines Dokuments mit mehreren Projekten wird die Datei im ersten gefundenen Projektordner abgelegt und in die anderen Projektordner verlinkt.'
GO
-- Dokumenttypen nicht mit Projekt verknüpft
PRINT ''
PRINT 'Dokumente ohne Projektverknüpfung'
PRINT 'Nachfolgend eine Übersicht der Dokumente, welche nicht mit einem Projekt verknüpft sind:'
PRINT 'Gruppiert nach Elementtyp:'
select ENTITY_TYPE,COUNT(*) as Anzahl from VIEW_ALL_DOCUMENT
	where AIMKEY not in (select PARENT_AIMKEY from XREF_ELEMENT where RELATIONSHIP_ID like 'AIM.XREF.DOC.PRO%')
	group by ENTITY_TYPE order by ENTITY_TYPE
PRINT 'Gruppiert nach Elementtyp und Dateityp:'
select ENTITY_TYPE,FILE_TYPE,COUNT(*) as Anzahl from VIEW_ALL_DOCUMENT
	where AIMKEY not in (select PARENT_AIMKEY from XREF_ELEMENT where RELATIONSHIP_ID like 'AIM.XREF.DOC.PRO%')
	group by ENTITY_TYPE,FILE_TYPE order by ENTITY_TYPE,FILE_TYPE
PRINT 'Hinweis: Diese Dokumente werden auch bei Verwendung der projektspezifischen Ablagestruktur später im Vault in der PSP-Ablagestruktur nach Jahr, Monat auftauchen!'
GO

-- Dokumente nach Elementtypen:
PRINT ''
PRINT 'Dokumente gruppiert nach Elementtypen'
select ENTITY_TYPE, count(*) as Anzahl from VIEW_ALL_DOCUMENT group by ENTITY_TYPE order by ENTITY_TYPE
GO

-- Dokumente nach Dateitypen
PRINT ''
PRINT 'Übersicht der verwendeten Dateitypen'
select ENTITY_TYPE, FILE_TYPE, count(*) as Anzahl from VIEW_ALL_DOCUMENT group by ENTITY_TYPE, FILE_TYPE order by ENTITY_TYPE, FILE_TYPE

-- Inventor Dateien (PSP Shell: #(DTYDocTypes:Ext0=IPT), #(DTYDocTypes:Ext0=IAM), ...)
PRINT ''
PRINT 'Anzahl der Inventor Dateien:'
select count(*) as Anzahl from VIEW_ALL_DOCUMENT where FILE_TYPE in ('IPT', 'IPART', 'IPARTSTD', 'IPARTUSER', 'STDIPT', 'IPSKEL', 'IAM', 'IASSEMBLY', 'IASSEMBLYMEMBER', 'IPN', 'INVDWG', 'IDW') 
-- Inventor Dateien ohne Referenzen
PRINT 'davon Inventor Dateien ohne Referenzen:'
select FILE_TYPE, count(*) as Anzahl from VIEW_ALL_DOCUMENT where FILE_TYPE in ('IPT', 'IPART', 'IPARTSTD', 'IPARTUSER', 'STDIPT', 'IPSKEL', 'IAM', 'IASSEMBLY', 'IASSEMBLYMEMBER', 'IPN', 'INVDWG', 'IDW') 
	and AIMKEY not in (select PARENT_AIMKEY from XREF_ELEMENT where RELATIONSHIP_ID like 'AIM.XREF.DOC.ENG%') group by FILE_TYPE order by Anzahl DESC
-- Inventor Dateien ohne Verwendung
PRINT 'davon Inventor Dateien ohne Verwendung:'
select FILE_TYPE, count(*) as Anzahl from VIEW_ALL_DOCUMENT where FILE_TYPE in ('IPT', 'IPART', 'IPARTSTD', 'IPARTUSER', 'STDIPT', 'IPSKEL', 'IAM', 'IASSEMBLY', 'IASSEMBLYMEMBER', 'IPN', 'INVDWG', 'IDW') 
	and AIMKEY not in (select CHILD_AIMKEY from XREF_ELEMENT where RELATIONSHIP_ID like 'AIM.XREF.DOC.ENG%') group by FILE_TYPE order by Anzahl DESC
-- Top10 Inventor Dateien mit den meisten direkten Referenzen
PRINT 'Top10 Inventor Dateien mit den meisten direkten Referenzen:'
select TOP(10) IDENT, REVISION, count(*) as Anzahl from VIEW_XREF_PARENT_DOCUMENT where X_RELATIONSHIP_ID in ('AIM.XREF.DOC.ENG') group by AIMKEY, IDENT, REVISION order by Anzahl DESC
-- Top10 Inventor Dateien mit den meisten direkten Verwendungen
PRINT 'Top10 Inventor Dateien mit den meisten direkten Verwendungen:'
select TOP(10) IDENT, REVISION, count(*) as Anzahl from VIEW_XREF_CHILD_DOCUMENT where X_RELATIONSHIP_ID in ('AIM.XREF.DOC.ENG') group by AIMKEY, IDENT, REVISION order by Anzahl DESC

-- AutoCAD Dateien (PSP Shell: #(DTYDocTypes:Ext0=DWG) ohne INVDWG))
PRINT ''
PRINT 'Anzahl der AutoCAD Dateien:'
select count(*) as Anzahl from VIEW_ALL_DOCUMENT where FILE_TYPE in ('A', 'DLA', 'RI', 'AX', 'DL', 'PX')
-- AutoCAD Dateien mit Referenzen
PRINT 'davon AutoCAD Dateien mit Referenzen:'
select count(*) as Anzahl from VIEW_ALL_DOCUMENT where FILE_TYPE in ('A', 'DLA', 'RI', 'AX', 'DL', 'PX') and AIMKEY in (select PARENT_AIMKEY from XREF_ELEMENT where RELATIONSHIP_ID like 'AIM.XREF.DOC.ENG%')
--select IDENT, count(*) as Anzahl from VIEW_ALL_DOCUMENT where FILE_TYPE in ('A', 'DLA', 'RI', 'AX', 'DL', 'PX')
--	and AIMKEY in (select PARENT_AIMKEY from XREF_ELEMENT where RELATIONSHIP_ID like 'AIM.XREF.DOC.ENG%') group by IDENT

-- Sonstige Konstruktions-Dokumente
PRINT ''
PRINT 'Anzahl sonstiger Konstruktions-Dokumente:'
select count(*) as Anzahl from VIEW_ALL_DOCUMENT where ENTITY_TYPE like 'AIM.DOC.ENG%' and FILE_TYPE not in
('IPT', 'IPART', 'IPARTSTD', 'IPARTUSER', 'STDIPT', 'IPSKEL', 'IAM', 'IASSEMBLY', 'IASSEMBLYMEMBER', 'IPN', 'INVDWG', 'IDW', 'A', 'DLA', 'RI', 'AX', 'DL', 'PX') 
GO

-- Verwendete Statuszustände für Dokumente
PRINT ''
PRINT 'Verwendete Statuszustände für Dokumente:'
select ENTITY_TYPE, STATUSKEY,COUNT(*) as Anzahl, '' as 'Statusdefinition in Vault' from VIEW_ALL_DOCUMENT group by ENTITY_TYPE, STATUSKEY order by ENTITY_TYPE, STATUSKEY
IF exists(select STATUSKEY from VIEW_ALL_DOCUMENT where STATUSKEY not in ('00001','00002','00003','00004','00005','00006','00203','00301','00303','EXP01','EXP04'))
BEGIN
	PRINT 'Achtung: Es wurden gegenüber dem Standard zusätzliche Statusdefinitionen verwendet. Diese müssen genauer untersucht und den neuen Statuszuständen in Vault zugeordnet werden!'
	PRINT 'Standard Statusdefinitionen für Dokumente sind: 00001, 00002, 00003, 00004, 00005, 00006 (für AIM.DOC.ENG) - 00203 (für AIM.DOC.ENG.STD) - 00301, 00303 (für AIM.DOC.OFF) sowie EXP01 und EXP04'
END
-- Dokumenttypen nach Element-, Dokumenttyp und Status
PRINT ''
PRINT 'Statuszustände gruppiert nach Element- und Dokumenttyp:'
select ENTITY_TYPE,FILE_TYPE,STATUSKEY,COUNT(*) as Anzahl from VIEW_ALL_DOCUMENT group by ENTITY_TYPE,FILE_TYPE,STATUSKEY order by ENTITY_TYPE,FILE_TYPE,STATUSKEY
GO

-- Verwendete Revisionen für Dokumente
PRINT ''
PRINT 'Verwendete Revisionen für Dokumente:'
select ENTITY_TYPE, REVISION, count(*) as Anzahl from VIEW_ALL_DOCUMENT group by ENTITY_TYPE, REVISION order by ENTITY_TYPE, REVISION
IF exists(select REVISION from VIEW_ALL_DOCUMENT where REVISION is null)
BEGIN
	PRINT 'Hinweis: Es existieren Dokumentdatensätze mit "Leer-Revision". In Vault gibt es allerdings keine "Leer-Revision", weshalb diese über die Migrationskonfiguration z.B. in Revision "-" umgewandelt werden sollte.
	Das kann z.B. über eine SQL Stored Procedure oder einer Zusatzprogramierung umgesetzt werden.'
END
GO

-- Dokumentnummern mit unterschiedlichen Dateitypen
PRINT ''
PRINT 'Dokumente mit unterschiedlichem FILE_TYPE'
select count(*) as Anzahl from VIEW_ALL_DOCUMENT where IDENT in (select IDENT from VIEW_ALL_DOCUMENT group by IDENT having COUNT(DISTINCT FILE_TYPE) > 1)
--select top 10 IDENT, COUNT(distinct FILE_TYPE) as Anzahl from VIEW_ALL_DOCUMENT
--group by IDENT having COUNT(DISTINCT FILE_TYPE) > 1 order by IDENT
GO

-- Dokumente mit FILE_LINKNAME
PRINT ''
PRINT 'Dokumente mit FILE_LINKNAME'
select ENTITY_TYPE, count(*) as Anzahl from VIEW_ALL_DOCUMENT where FILE_LINKNAME is not null group by ENTITY_TYPE
PRINT ''
PRINT 'Rumpfpfade in FILE_LINKNAME nicht beginnend mit $(DATPATH) oder #(INI:Inventor:StdPartPath_'
select substring(FILE_LINKNAME,0,20), count(*) as Anzahl from VIEW_ALL_DOCUMENT 
	where FILE_LINKNAME is not null and FILE_LINKNAME not like '$(DATPATH)%' and FILE_LINKNAME not like '$DATPATH%' and FILE_LINKNAME not like '#(INI:Inventor:StdPartPath_%'
	group by substring(FILE_LINKNAME,0,20) order by substring(FILE_LINKNAME,0,20)
--select IDENT, FILE_LINKNAME from VIEW_ALL_DOCUMENT
--	where FILE_LINKNAME is not null and FILE_LINKNAME not like '$(DATPATH)%' and FILE_LINKNAME not like '$DATPATH%' and FILE_LINKNAME not like '#(INI:Inventor:StdPartPath_%'
--	order by IDENT
GO

-- Dokumentverknüpfungen
PRINT ''
PRINT 'Übersicht der Dokumentverknüpfungen:'
select RELATIONSHIP_ID, count(*) as Anzahl from XREF_ELEMENT where RELATIONSHIP_ID in ('AIM.XREF.CONTACT.DOC','AIM.XREF.DOC.ENG','AIM.XREF.DOC.PART','AIM.XREF.DOC.PRO.ENG','AIM.XREF.DOC.PRO.OFF','AIM.XREF.DOC.SECONDARY') group by RELATIONSHIP_ID order by RELATIONSHIP_ID
GO

--	-- Verknüpfung der Dokumente zu Artikeln
--	-- =======================
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

-- Auswertung der Spalten
IF exists(select * from VIEW_ALL_DOCUMENT)
BEGIN
	PRINT ''
	PRINT 'Verwendung der Dokument Properties (in VIEW_ALL_DOCUMENT):'
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
	SELECT cO_COLUMN as 'COLUMN', cO_ANZAHL as 'ANZAHL', cO_MAXLENGTH as 'MAXLENGTH', '' as 'Property in Vault' FROM #cOTemp_Columns order by cO_COLUMN
END
GO


-- Projekte
-- ======================
PRINT ''
PRINT ''
PRINT 'Projekte'
GO

IF exists(select * from VIEW_ALL_PROJECT)
BEGIN
	-- Gesamtanzahl
	PRINT ''
	PRINT 'Gesamtanzahl der Projektdatensätze:'
	select count(*) as Anzahl from VIEW_ALL_PROJECT
	-- Verwendete Statuszustände für Projekte
	PRINT ''
	PRINT 'Verwendete Statuszustände für Projekte'
	select STATUSKEY,COUNT(*) as Anzahl, '' as 'Statusdefinition in Vault' from VIEW_ALL_PROJECT group by STATUSKEY order by STATUSKEY
	IF exists(select STATUSKEY from VIEW_ALL_PROJECT where STATUSKEY not in ('00401','00402','00403'))
	BEGIN
		PRINT 'Achtung: Es wurden gegenüber dem Standard zusätzliche Statusdefinitionen verwendet. Diese müssen genauer untersucht und den neuen Statuszuständen in Vault zugeordnet werden!'
		PRINT 'Standard Statusdefinitionen für Projekte sind: 00401, 00402 und 00403'
	END
	-- Projektverknüpfungen
	PRINT ''
	PRINT 'Übersicht der Projektverknüpfungen:'
	select RELATIONSHIP_ID, count(*) as Anzahl from XREF_ELEMENT where RELATIONSHIP_ID in ('AIM.XREF.CONTACT.PRO','AIM.XREF.DOC.PRO.ENG','AIM.XREF.DOC.PRO.OFF','AIM.XREF.PART.PRO','AIM.XREF.PRO.PRO') group by RELATIONSHIP_ID order by RELATIONSHIP_ID
END
ELSE
	PRINT 'Die Datenbank enthält keine Projekte!'
GO

-- Auswertung der Spalten
IF exists(select * from VIEW_ALL_PROJECT)
BEGIN
	PRINT ''
	PRINT 'Verwendung der Projekt Properties (in VIEW_ALL_PROJECTS):'
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
	SELECT cO_COLUMN as 'COLUMN', cO_ANZAHL as 'ANZAHL', cO_MAXLENGTH as 'MAXLENGTH', '' as 'Property in Vault' FROM #cOTemp_Columns order by cO_COLUMN
END
GO


-- Artikel
-- ======================
PRINT ''
PRINT ''
PRINT 'Artikel'
GO

IF exists(select * from VIEW_ALL_PART)
BEGIN
	-- Gesamtanzahl
	PRINT ''
	PRINT 'Gesamtanzahl der Artikeldatensätze:'
	select count(*) as Anzahl from VIEW_ALL_PART
	PRINT 'davon mit unterschiedlichen Artikelnummern:'
	select count(distinct IDENT) as Anzahl from VIEW_ALL_PART
	if exists (select name from sys.tables where name = 'CLASS')
	BEGIN
		PRINT 'davon nicht klassifizierte Artikel:'
		select count(*) as Anzahl from VIEW_ALL_PART where AIMKEY not in (select ELEMENT_AIMKEY from VIEW_CRCLIST)
	END
	-- Artikelkategorien
	PRINT ''
	PRINT 'Verwendete Artikelkategorien:'
	select CATEGORY, count(*) as Anzahl from VIEW_ALL_PART group by CATEGORY order by CATEGORY
	-- Verwendete Statuszustände für Artikel
	PRINT ''
	PRINT 'Verwendete Statuszustände für Artikel:'
	select STATUSKEY,COUNT(*) as Anzahl, '' as 'Statusdefinition in Vault' from VIEW_ALL_PART group by STATUSKEY order by STATUSKEY
	IF exists(select STATUSKEY from VIEW_ALL_PART where STATUSKEY not in ('00100','00103','00104','00105'))
	BEGIN
		PRINT 'Achtung: Es wurden gegenüber dem Standard zusätzliche Statusdefinitionen verwendet. Diese müssen genauer untersucht und den neuen Statuszuständen in Vault zugeordnet werden!'
		PRINT 'Standard Statusdefinitionen für Artikel sind: 00100, 00103, 00104 und 00105'
	END
	-- Verwendete Revisionen für Artikel
	PRINT ''
	PRINT 'Verwendete Revisionen für Artikel:'
	select REVISION, count(*) as Anzahl from VIEW_ALL_PART group by REVISION order by REVISION
	IF exists(select REVISION from VIEW_ALL_PART where REVISION is null)
	BEGIN
		PRINT 'Hinweis: Es existieren Artikeldatensätze mit "Leer-Revision". In Vault gibt es allerdings keine "Leer-Revision", weshalb diese über die Migrationskonfiguration z.B. in Revision "-" umgewandelt werden sollte.
		Das kann z.B. über eine SQL Stored Procedure oder einer Zusatzprogramierung umgesetzt werden.'
	END
	-- Artikelverknüpfungen
	PRINT ''
	PRINT 'Übersicht der Artikelverknüpfungen:'
	select RELATIONSHIP_ID, count(*) as Anzahl from XREF_ELEMENT where RELATIONSHIP_ID in ('AIM.XREF.DOC.PART','AIM.XREF.PART','AIM.XREF.PART.CLASS','AIM.XREF.PART.PRO') group by RELATIONSHIP_ID order by RELATIONSHIP_ID


	---- Verknüpfung der Artikel zu Dokumenten
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
	PRINT 'Die Datenbank enthält keine Artikel!'
GO

-- Auswertung der Spalten
IF exists(select * from VIEW_ALL_PART)
BEGIN
	PRINT ''
	PRINT 'Verwendung der Artikel Properties (in VIEW_ALL_PART):'
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
	SELECT cO_COLUMN as 'COLUMN', cO_ANZAHL as 'ANZAHL', cO_MAXLENGTH as 'MAXLENGTH', '' as 'Property in Vault' FROM #cOTemp_Columns order by cO_COLUMN
END
GO


-- Klassen
-- ======================
if exists (select name from sys.tables where name = 'CLASS')
BEGIN
	PRINT ''
	PRINT ''
	PRINT 'Klassen'
	-- Gesamtanzahl
	PRINT ''
	PRINT 'Gesamtanzahl der Klassendatensätze:'
	select count(*) as Anzahl from VIEW_ALL_CLASS
	PRINT 'davon mit unterschiedlichen Klassennummern:'
	select count(distinct IDENT) as Anzahl from VIEW_ALL_CLASS
	-- Verwendung der Klassen
	PRINT ''
	PRINT 'Verwendung der Klassen:'
	select IDENT, SHORT_DESC, count(*) as Anzahl  from VIEW_XREF_CHILD_CLASS where X_RELATIONSHIP_ID = 'AIM.XREF.PART.CLASS' group by IDENT, SHORT_DESC order by IDENT
	-- Nicht verwendete Klassen (keinem Artikel zugeordnet)
	PRINT ''
	PRINT 'Nicht verwendete Klassen (keinem Artikel zugeordnet):'
	select IDENT, SHORT_DESC from VIEW_ALL_CLASS where AIMKEY not in (select CLASS_AIMKEY from VIEW_CRCLIST) and IDENT not in ('not classified')
	-- Verwendete Statuszustände für Klassen
	PRINT ''
	PRINT 'Verwendete Statuszustände für Klassen:'
	select STATUSKEY,COUNT(*) as Anzahl, '' as 'Statusdefinition in Vault' from VIEW_ALL_CLASS group by STATUSKEY order by STATUSKEY
	IF exists(select STATUSKEY from VIEW_ALL_CLASS where STATUSKEY not in ('00001'))
	BEGIN
		PRINT 'Achtung: Es wurden gegenüber dem Standard zusätzliche Statusdefinitionen verwendet. Diese müssen genauer untersucht und den neuen Statuszuständen in Vault zugeordnet werden!'
		PRINT 'Standard Statusdefinitionen für Klassen sind: 00001'
	END
	-- Verwendete Revisionen für Klassen
	PRINT ''
	PRINT 'Verwendete Revisionen für Klassen:'
	select REVISION, count(*) as Anzahl from VIEW_ALL_CLASS group by REVISION order by REVISION
	IF exists(select REVISION from VIEW_ALL_CLASS where REVISION is null)
	BEGIN
		PRINT 'Hinweis: Es existieren Klassendatensätze mit "Leer-Revision". In Vault gibt es allerdings keine "Leer-Revision", weshalb diese über die Migrationskonfiguration z.B. in Revision "-" umgewandelt werden sollte.
		Das kann z.B. über eine SQL Stored Procedure oder einer Zusatzprogramierung umgesetzt werden.'
	END
END
GO

-- Auswertung der Spalten
if exists (select name from sys.tables where name = 'CLASS')
BEGIN
	PRINT ''
	PRINT 'Verwendung der Klassen Properties (in VIEW_ALL_CLASS):'
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
	SELECT cO_COLUMN as 'COLUMN', cO_ANZAHL as 'ANZAHL', cO_MAXLENGTH as 'MAXLENGTH', '' as 'Property in Vault' FROM #cOTemp_Columns order by cO_COLUMN
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
	select count(*) as Anzahl from VIEW_ALL_CLASSCRC
	-- Verwendung der Sachmerkmale
	PRINT ''
	PRINT 'Verwendung der Sachmerkmale in Klassen:'
	select IDENT, SHORT_DESC, count(*) as Anzahl  from VIEW_XREF_CHILD_CLASSCRC where X_RELATIONSHIP_ID = 'AIM.XREF.CLASS.CLASSCRC' group by IDENT, SHORT_DESC order by IDENT
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
	SELECT cO_COLUMN as 'COLUMN', cO_ANZAHL as 'ANZAHL', cO_MAXLENGTH as 'MAXLENGTH', '' as 'Property in Vault' FROM #cOTemp_Columns order by cO_COLUMN
END
GO


-- Organisationen
-- ======================
PRINT ''
PRINT ''
PRINT 'Organisationen'
GO

IF exists(select * from VIEW_CONTACT_ORGANISATION)
BEGIN
	-- Gesamtanzahl
	PRINT ''
	PRINT 'Gesamtanzahl der Organisationdatensätze:'
	select count(*) as Anzahl from VIEW_CONTACT_ORGANISATION
	-- Verwendete Statuszustände für Organisationen
	PRINT ''
	PRINT 'Verwendete Statuszustände für Organisationen'
	select STATUSKEY,COUNT(*) as Anzahl, '' as 'Statusdefinition in Vault' from VIEW_CONTACT_ORGANISATION group by STATUSKEY order by STATUSKEY
	IF exists(select STATUSKEY from VIEW_CONTACT_ORGANISATION where STATUSKEY not in ('00001'))
	BEGIN
		PRINT 'Achtung: Es wurden gegenüber dem Standard zusätzliche Statusdefinitionen verwendet. Diese müssen genauer untersucht und den neuen Statuszuständen in Vault zugeordnet werden!'
		PRINT 'Standard Statusdefinitionen für Projekte sind: 00001'
	END
	-- Organisationverknüpfungen
	PRINT ''
	PRINT 'Übersicht der Organisationverknüpfungen:'
	select RELATIONSHIP_ID, count(*) as Anzahl from XREF_ELEMENT
		where (PARENT_AIMKEY in (select AIMKEY from VIEW_CONTACT_ORGANISATION) and RELATIONSHIP_ID in ('AIM.XREF.CONTACT.ADR','AIM.XREF.CONTACT.DOC','AIM.XREF.CONTACT.ORG','AIM.XREF.CONTACT.PRO'))
		or (CHILD_AIMKEY in (select AIMKEY from VIEW_CONTACT_ORGANISATION) and RELATIONSHIP_ID in ('AIM.XREF.CONTACT','AIM.XREF.CONTACT.ORG'))
		group by RELATIONSHIP_ID order by RELATIONSHIP_ID
END
ELSE
	PRINT 'Die Datenbank enthält keine Organisationen!'
GO

-- Auswertung der Spalten
IF exists(select * from VIEW_CONTACT_ORGANISATION)
BEGIN
	PRINT ''
	PRINT 'Verwendung der Organisation Properties (in VIEW_CONTACT_ORGANISATION):'
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
	SELECT cO_COLUMN as 'COLUMN', cO_ANZAHL as 'ANZAHL', cO_MAXLENGTH as 'MAXLENGTH', '' as 'Property in Vault' FROM #cOTemp_Columns order by cO_COLUMN
END
GO


-- Personen
-- ======================
PRINT ''
PRINT ''
PRINT 'Personen'
GO

IF exists(select * from VIEW_CONTACT_PERSON)
BEGIN
	-- Gesamtanzahl
	PRINT ''
	PRINT 'Gesamtanzahl der Personendatensätze:'
	select count(*) as Anzahl from VIEW_CONTACT_PERSON
	-- Verwendete Statuszustände für Personen
	PRINT ''
	PRINT 'Verwendete Statuszustände für Personen'
	select STATUSKEY,COUNT(*) as Anzahl, '' as 'Statusdefinition in Vault' from VIEW_CONTACT_PERSON group by STATUSKEY order by STATUSKEY
	IF exists(select STATUSKEY from VIEW_CONTACT_PERSON where STATUSKEY not in ('00001'))
	BEGIN
		PRINT 'Achtung: Es wurden gegenüber dem Standard zusätzliche Statusdefinitionen verwendet. Diese müssen genauer untersucht und den neuen Statuszuständen in Vault zugeordnet werden!'
		PRINT 'Standard Statusdefinitionen für Projekte sind: 00001'
	END
	-- Projektverknüpfungen
	PRINT ''
	PRINT 'Übersicht der Personenverknüpfungen:'
	select RELATIONSHIP_ID, count(*) as Anzahl from XREF_ELEMENT
		where (PARENT_AIMKEY in (select AIMKEY from VIEW_CONTACT_PERSON) and RELATIONSHIP_ID in ('AIM.XREF.CONTACT','AIM.XREF.CONTACT.ADR','AIM.XREF.CONTACT.DOC','AIM.XREF.CONTACT.PERSON','AIM.XREF.CONTACT.PRO'))
		or (CHILD_AIMKEY in (select AIMKEY from VIEW_CONTACT_PERSON) and RELATIONSHIP_ID in ('AIM.XREF.CONTACT.PERSON'))
		group by RELATIONSHIP_ID order by RELATIONSHIP_ID
END
ELSE
	PRINT 'Die Datenbank enthält keine Personen!'
GO

-- Auswertung der Spalten
IF exists(select * from VIEW_CONTACT_PERSON)
BEGIN
	PRINT ''
	PRINT 'Verwendung der Personen Properties (in VIEW_CONTACT_PERSON):'
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
	SELECT cO_COLUMN as 'COLUMN', cO_ANZAHL as 'ANZAHL', cO_MAXLENGTH as 'MAXLENGTH', '' as 'Property in Vault' FROM #cOTemp_Columns order by cO_COLUMN
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
	-- Gesamtanzahl
	PRINT ''
	PRINT 'Gesamtanzahl der Adressdatensätze:'
	select count(*) as Anzahl from VIEW_ALL_ADDRESS
	-- Verwendete Statuszustände für Adressen
	PRINT ''
	PRINT 'Verwendete Statuszustände für Adressen'
	select STATUSKEY,COUNT(*) as Anzahl, '' as 'Statusdefinition in Vault' from VIEW_ALL_ADDRESS group by STATUSKEY order by STATUSKEY
	IF exists(select STATUSKEY from VIEW_ALL_ADDRESS where STATUSKEY not in ('00001'))
	BEGIN
		PRINT 'Achtung: Es wurden gegenüber dem Standard zusätzliche Statusdefinitionen verwendet. Diese müssen genauer untersucht und den neuen Statuszuständen in Vault zugeordnet werden!'
		PRINT 'Standard Statusdefinitionen für Projekte sind: 00001'
	END
	-- Adressverknüpfungen
	PRINT ''
	PRINT 'Übersicht der Adressverknüpfungen:'
	select RELATIONSHIP_ID, count(*) as Anzahl from XREF_ELEMENT where RELATIONSHIP_ID in ('AIM.XREF.CONTACT.ADR') group by RELATIONSHIP_ID order by RELATIONSHIP_ID
END
ELSE
	PRINT 'Die Datenbank enthält keine Adressen!'
GO

-- Auswertung der Spalten
IF exists(select * from VIEW_ALL_ADDRESS)
BEGIN
	PRINT ''
	PRINT 'Verwendung der Adress Properties (in VIEW_ALL_ADDRESS):'
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
	SELECT cO_COLUMN as 'COLUMN', cO_ANZAHL as 'ANZAHL', cO_MAXLENGTH as 'MAXLENGTH', '' as 'Property in Vault' FROM #cOTemp_Columns order by cO_COLUMN
END
GO