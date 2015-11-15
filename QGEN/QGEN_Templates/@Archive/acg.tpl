# Top level export file for ACG export

# Create the population subgroups
# Create the SUBSETS for use in all the queries
# keywords
#	<name> is replaced with the query name.
#	<delta_period_start> is replaced with the start date of the delta period selected.
#	<active_period_start> is replaced with the start date of the active period selected (e.g. all current drugs/diagnoses).
#	<ltc_period_start> is replaced with the start date of the LTC period selected.
#	<period_end> is replaced with the end date of the period selected.

$START_COHORT

# Select all the patients
$COHORT, Cs_ALL, Internal subset select full practice population
SUBSET <name> TEMP
FROM PATIENTS
WHERE ACTIVE IN ("R","T","S","D","L","P")
$ECOHORT

# Select all the newly registered patients
$COHORT, Cs_NEW, Internal subset select new practice population
SUBSET <name> TEMP
FROM PATIENTS
WHERE ACTIVE IN ("R","T","S","D","L","P")
AND REGISTERED_DATE IN ("<delta_period_start>"-"<period_end>")
$ECOHORT

# Select patient who refused to share records
$COHORT, Cs_OPTOUT, Internal subset select those who refused to share records
SUBSET <name> TEMP
FROM JOURNALS (LATEST FOR PATIENT)
WHERE ACTIVE IN ("R","T","S","D","L","P")
AND CODE IN ("XaMdS","XaKII","XaQVo")
AND CHOSEN.CODE IN ("XaKII","XaQVo")
$ECOHORT

# Select ALL consented patients
$COHORT, C_ALLPAT, ACTIVE subset select all opted in patients
$MAP, ALL
FOR Cs_ALL NOT Cs_OPTOUT
SUBSET <name> TEMP
FROM PATIENTS
$ECOHORT

# Select NEWLY REGISTERED consented patients
$COHORT, C_NEWPAT, ACTIVE subset select new opted in patients
$MAP, NEW
FOR Cs_NEW NOT Cs_OPTOUT
SUBSET <name> TEMP
FROM PATIENTS
$ECOHORT

$END_COHORT

$INCLUDE, diagnoses.inc

$GENERATE_ENCOUNTERS

