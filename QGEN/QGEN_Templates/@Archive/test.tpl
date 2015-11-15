# Top level export file for ACG export

# Create the population subgroups
# Create the SUBSETS for use in all the queries
# keywords
#	<name> is replaced with the query name.
#	<delta_period_start> is replaced with the start date of the delta period selected.
#	<active_period_start> is replaced with the start date of the active period selected
#	<ltc_period_start> is replaced with the start date of the LTC period selected.
#	<period_end> is replaced with the end date of the period selected.

# The query names must be extended in the file generation so in here they MUST be 5 or less characters

# Notes from test
# The query/subset names seem to get mangled if they include lower case so make them UPPER

$START_COHORT

# Select all the patients
$COHORT, CSALL, Full practice population
SUBSET <name> TEMP
FROM PATIENTS
WHERE ACTIVE IN ("R","T","S","D","L","P")
$ECOHORT

# Select all the newly registered patients
$COHORT, CSNEW, New patients
SUBSET <name> TEMP
FROM PATIENTS
WHERE ACTIVE IN ("R","T","S","D","L","P")
AND REGISTERED_DATE IN ("<delta_period_start>"-"<period_end>")
$ECOHORT

# Select patient who refused to share records
$COHORT, CSOUT, Patients who refused to share records
SUBSET <name> TEMP
FROM JOURNALS (LATEST FOR PATIENT)
WHERE ACTIVE IN ("R","T","S","D","L","P")
AND CODE IN ("XaMdS","XaKII","XaQVo")
AND CHOSEN.CODE IN ("XaKII","XaQVo")
$ECOHORT

# Select ALL consented patients
$COHORT, CALL, All opted in patients
$MAP, ALL
FOR CSALL NOT CSOUT
SUBSET <name> TEMP
FROM PATIENTS
$ECOHORT

# Select NEWLY REGISTERED consented patients
$COHORT, CNEW, New opted in patients
$MAP, NEW
FOR CSNEW NOT CSOUT
SUBSET <name> TEMP
FROM PATIENTS
$ECOHORT

$END_COHORT

$START_JOURNAL, CTV3, CURRENT, DIAGNOSES
$CODES, D01, "XE177%"
$END_JOURNAL

$GENERATE_ENCOUNTERS

