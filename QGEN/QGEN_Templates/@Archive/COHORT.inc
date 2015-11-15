$START_COHORT

# Select all the patients
$COHORT; CSALL; Full practice population
SUBSET <name> TEMP
FROM PATIENTS
WHERE ACTIVE IN ("R","T","S","D","L","P")
$ECOHORT

# Select all the newly registered patients
$COHORT; CSNEW; New patients
SUBSET <name> TEMP
FROM PATIENTS
WHERE ACTIVE IN ("R","T","S","D","L","P")
AND REGISTERED_DATE IN ("<delta_period_start>"-"<period_end>")
$ECOHORT

# Select patient who refused to share records
$COHORT; CSOUT; Patients who refused to share records
SUBSET <name> TEMP
FROM JOURNALS (LATEST FOR PATIENT)
WHERE ACTIVE IN ("R","T","S","D","L","P")
AND CODE IN ("XaMdS","XaKII","XaQVo")
AND CHOSEN.CODE IN ("XaKII","XaQVo")
$ECOHORT

# Select ALL consented patients
$COHORT; CALL; All opted in patients
$MAP; ALL
FOR CSALL NOT CSOUT
SUBSET <name> TEMP
FROM PATIENTS
$ECOHORT

# Select NEWLY REGISTERED consented patients
$COHORT; CNEW; New opted in patients
$MAP; NEW
FOR CSNEW NOT CSOUT
SUBSET <name> TEMP
FROM PATIENTS
$ECOHORT

$END_COHORT